using System;
using System.Linq.Expressions;

namespace BuildUp
{
	public class CompositeSource
	{
		public static CompositeSource<T> Create<T>(Func<BuildContext, CtorArgSourceMap, T> create,
			CtorArgSourceMap sources)
		{
			return new CompositeSource<T>(create, sources);
		}

		// 	Using an explicit creation function is resilient to refactoring / changes to constructor signatures
		//	and is better than a reflection oriented solution, such as arrays of values and ConstructorInfos. It will
		//	survive changes to parameter order, although the order in which the sources are supplied won't be switched,
		// which could cause confusion, so we might want an alternative like ..

		#region Create from function

		/// <summary>
		/// Creates a CompositeSource using the supplied function and specified sources. The sources will
		/// also be stored in the Sources property of the CompositeSource instance so that they are available
		/// for use when creating a new version of the CompositeSource. A variation of this method exists for 
		/// methods requiring zero (TODO) to 16 (TODO) parameters. 
		/// </summary>
		/// <typeparam name="T">The type of the object that the source will create</typeparam>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <param name="create">The function used to create an object using values from the specified sources</param>
		/// <param name="source1">The source used to provide the first value used to create the object</param>
		/// <param name="source2">The source used to provide the second value used to create the object</param>
		/// <returns></returns>
		public static CompositeSource<T> Create<T, T1, T2>(Func<BuildContext, T1, T2, T> create, ISource<T1> source1,
			ISource<T2> source2)
		{
			var sourceMap = new CtorArgSourceMap(source1, source2);
			return Create((context, sources) =>
			{
				var value1 = sources.Create<T1>(0, context);
				var value2 = sources.Create<T2>(1, context);
				return create(context, value1, value2);
			}, sourceMap);
		}

		/// <summary>
		/// Experimenting with new syntax for specifying sources using expression - more refactoring friendly
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="create"></param>
		/// <returns></returns>
		public static CompositeSource<T> Create<T>(Expression<Func<T>> create)
		{
			return null;
		}

		#endregion

		#region Create from existing

		/// <summary>
		/// Creates a new instance of a composite source, with changes made to one of the sources used to provide
		/// values to the create function. This returns a new ICompositeSource instance based on the current instance,
		/// without making any changes to the current instance. This is mainly designed to support selective changes to 
		/// sources via typical chainable refining methods, like CatSource.WithParent(parentCatSource)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TSource"></typeparam>
		/// <param name="compositeSource"></param>
		/// <param name="index"></param>
		/// <param name="source"></param>
		/// <returns></returns>
		public static ICompositeSource<T> BasedOn<T, TSource>(ICompositeSource<T> compositeSource, int index, ISource<TSource> source)
		{
			var newSources = compositeSource.Sources.Change(index, source);
			return new CompositeSource<T>(compositeSource.CreateFunc, newSources);
		}

		#endregion

	}

	

	/// <summary>
	/// An implementation of ISource that creates objects using a number of child ISources.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class CompositeSource<T> : Source<T>, ICompositeSource<T>
	{
		private readonly Func<BuildContext, CtorArgSourceMap, T> createFunc;
		private readonly CtorArgSourceMap sources;

		public CompositeSource(Func<BuildContext, CtorArgSourceMap, T> createFunc, CtorArgSourceMap sources)
			: base(c => createFunc(c, sources)) // partially applying the source param gives us function signature used by ISource
		{
			// These properties are needed to allow new instances to be derived from the current instance
			// We need access to these members to be able to derive new ICompositeSources from this one,
			// perhaps to swap out one of the sources.
			this.createFunc = createFunc;
			this.sources = sources;
		}

		#region ICompositeSource<T> Members

		CtorArgSourceMap ICompositeSource<T>.Sources
		{
			get { return sources; }
		}

		Func<BuildContext, CtorArgSourceMap, T> ICompositeSource<T>.CreateFunc
		{
			get { return createFunc; }
		}

		#endregion
	}
}