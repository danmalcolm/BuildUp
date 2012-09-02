using System;
using System.Linq.Expressions;

namespace BuildUp
{
	/// <summary>
	/// Contains methods for creation of ICompositeSources
	/// </summary>
	public class CompositeSource
	{
		public static CompositeSource<T> Create<T>(Func<BuildContext, ChildSourceMap, T> create,
			ChildSourceMap sources)
		{
			return new CompositeSource<T>(create, sources);
		}

		// 	Using an explicit creation function is resilient to refactoring / changes to constructor signatures
		//	and is better than a reflection oriented solution, such as arrays of values and ConstructorInfos. It will
		//	survive changes to parameter order, although the order in which the sources are supplied won't be switched,
		// which could cause confusion, so we might want an alternative like ..

		#region Create functions

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
		/// <param name="source1">The child source used to provide the first value used to create the object</param>
		/// <param name="source2">The child source used to provide the second value used to create the object</param>
		/// <returns></returns>
		public static CompositeSource<T> Create<T, T1, T2>(Func<BuildContext, T1, T2, T> create, ISource<T1> source1,
			ISource<T2> source2)
		{
			var sourceMap = new ChildSourceMap(source1, source2);
			// TODO - use Expression tree parsing to get the names of the parameters, which are then used to 
			// access items within the ChildSourceMap
			return Create((context, sources) =>
			{
				var value1 = sources.Create<T1>(0, context);
				var value2 = sources.Create<T2>(1, context);
				return create(context, value1, value2);
			}, sourceMap);
		}

		public static CompositeSource<T> Create<T, T1, T2, T3>(Func<BuildContext, T1, T2, T3, T> create, ISource<T1> source1,
			ISource<T2> source2, ISource<T3> source3)
		{
			var sourceMap = new ChildSourceMap(source1, source2, source3);
			return Create((context, sources) =>
			{
				var value1 = sources.Create<T1>(0, context);
				var value2 = sources.Create<T2>(1, context);
				var value3 = sources.Create<T3>(2, context);
				return create(context, value1, value2, value3);
			}, sourceMap);
		}

		public static CompositeSource<T> Create<T, T1, T2, T3, T4>(Func<BuildContext, T1, T2, T3, T4, T> create, ISource<T1> source1,
			ISource<T2> source2, ISource<T3> source3, ISource<T4> source4)
		{
			var sourceMap = new ChildSourceMap(source1, source2, source3, source4);
			return Create((context, sources) =>
			{
				var value1 = sources.Create<T1>(0, context);
				var value2 = sources.Create<T2>(1, context);
				var value3 = sources.Create<T3>(2, context);
				var value4 = sources.Create<T4>(3, context);
				return create(context, value1, value2, value3, value4);
			}, sourceMap);
		}

		
		#endregion


	}

	

	/// <summary>
	/// <see cref="ICompositeSource&lt;T&gt;"/> implementation
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class CompositeSource<T> : Source<T>, ICompositeSource<T>
	{
		private readonly Func<BuildContext, ChildSourceMap, T> compCreateFunc;
		private readonly ChildSourceMap childSources;

		public CompositeSource(Func<BuildContext, ChildSourceMap, T> compCreateFunc, ChildSourceMap childSources)
			: base(c => compCreateFunc(c, childSources)) // partially applying the source param gives us function signature used by ISource
		{
			this.compCreateFunc = compCreateFunc;
			this.childSources = childSources;
		}

		#region ICompositeSource<T> Members

		ChildSourceMap ICompositeSource<T>.ChildSources
		{
			get { return childSources; }
		}

		Func<BuildContext, ChildSourceMap, T> ICompositeSource<T>.CompCreateFunc
		{
			get { return compCreateFunc; }
		}

		#endregion
	}
}