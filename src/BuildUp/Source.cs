using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BuildUp
{
	#region Creation methods

	/// <summary>
    /// Contains factory methods for creation of ISource&lt;TObject&gt;
    /// </summary>
	public class Source
	{
		public static Source<T> Create<T>(Func<CreateContext, T> create)
		{
			return new Source<T>(ContextSource.ForSimpleSource(), create);
		}

		private static Source<T> Create<T>(Func<CreateContext, T> create,
													ChildSourceMap sources)
		{
			return new Source<T>(ContextSource.WithChildSources(sources), create);
		}

		#region Create functions

		public static Source<T> Create<T>(IList<T> sequence)
		{
			return Source.Create((context) => sequence[context.Index]);
		}

		public static Source<T> Create<T>(T[] sequence)
		{
			return Source.Create((context) => sequence[context.Index]);
		}


		/// <summary>
		/// Creates a source using the supplied function and specified source. The sources will
		/// also be stored in the Sources property of the CompositeSource instance so that they are available
		/// for use when creating a new version of the CompositeSource. Variations of this method exist for 
		/// creation functions requiring from 1 (TODO) to 16 (TODO) parameters. 
		/// </summary>
		/// <typeparam name="T">The type of the object that the source will create</typeparam>
		/// <typeparam name="T1"></typeparam>
		/// <param name="create">The function used to create an object using values from the specified sources</param>
		/// <param name="source1">The child source used to provide the value used to create the object</param>
		/// <returns></returns>
		public static Source<T> Create<T, T1>(Func<CreateContext, T1, T> create, ISource<T1> source1)
		{
			var sourceMap = new ChildSourceMap(source1);
			return Create(context =>
			{
				var value1 = (T1)context.ChildSourceValues[0];
				return create(context, value1);
			}, sourceMap);
		}

		/// <summary>
		/// Creates a CompositeSource using the supplied function and specified sources. The sources will
		/// also be stored in the Sources property of the CompositeSource instance so that they are available
		/// for use when creating a new version of the CompositeSource. Variations of this method exist for 
		/// creation functions requiring from 1 (TODO) to 16 (TODO) parameters. 
		/// </summary>
		/// <typeparam name="T">The type of the object that the source will create</typeparam>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <param name="create">The function used to create an object using values from the specified sources</param>
		/// <param name="source1">The child source used to provide the first value used to create the object</param>
		/// <param name="source2">The child source used to provide the second value used to create the object</param>
		/// <returns></returns>
		public static Source<T> Create<T, T1, T2>(Func<CreateContext, T1, T2, T> create, ISource<T1> source1, ISource<T2> source2)
		{
			var sourceMap = new ChildSourceMap(source1, source2);
			return Create(context =>
			{
				var value1 = (T1)context.ChildSourceValues[0];
				var value2 = (T2)context.ChildSourceValues[1];
				return create(context, value1, value2);
			}, sourceMap);
		}

		public static Source<T> Create<T, T1, T2, T3>(Func<CreateContext, T1, T2, T3, T> create,
															   ISource<T1> source1,
															   ISource<T2> source2, ISource<T3> source3)
		{
			var sourceMap = new ChildSourceMap(source1, source2, source3);
			return Create(context =>
			{
				var value1 = (T1)context.ChildSourceValues[0];
				var value2 = (T2)context.ChildSourceValues[1];
				var value3 = (T3)context.ChildSourceValues[2];
				return create(context, value1, value2, value3);
			}, sourceMap);
		}

		#endregion
	}

	#endregion



	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public class Source<TObject> : ISource<TObject>
	{
		private readonly ContextSource contextSource;
		private readonly Func<CreateContext, TObject> create;
		private readonly Func<IEnumerable<object>, IEnumerable<object>> modifySequence;

		public Source(ContextSource contextSource, Func<CreateContext, TObject> create, Func<IEnumerable<object>, IEnumerable<object>> modifySequence = null)
		{
			this.contextSource = contextSource;
			this.create = create;
			this.modifySequence = modifySequence ?? (x => x);
		}
		
		public IEnumerable<TObject> Build()
		{
			return modifySequence(contextSource.Select(x => create(x)).Cast<object>()).Cast<TObject>();
		}

		IEnumerable ISource.Build()
		{
			return Build();
		}

		#region Select, SelectMany

		public ISource<TResult> Select<TResult>(Func<TObject, TResult> selector)
		{
			return new Source<TResult>(contextSource, @base => selector(this.create(@base)), modifySequence);
		}

		public ISource<TObject> Select(Action<TObject> action)
		{
			return Select(instance =>
			{
				action(instance);
				return instance;
			});
		}

		public ISource<TResult> SelectMany<TChild, TResult>(Func<ISource<TObject>, ISource<TChild>> sourceSelector, Func<TObject, TChild, TResult> resultSelector)
		{
			var newSequence = sourceSelector(this);
			// add sequence to current child sources
			var newSources = contextSource.ChildSources.Add(newSequence);
			var newContextSource = ContextSource.WithChildSources(newSources);
			int newSourceIndex = newSources.Count - 1;

			// Create new source with new child sources plus a function that first invokes this source's create function,
			// then invokes the new create function on the result, together with the value from the child sequence
			return new Source<TResult>(newContextSource, x => resultSelector(create(x), (TChild)x.ChildSourceValues[newSourceIndex]), modifySequence);
		}

		public ISource<TObject> SelectMany<TCollection>(Func<ISource<TObject>, ISource<TCollection>> childSequenceSelector, Action<TObject, TCollection> modify)
		{
			return SelectMany(childSequenceSelector, (a, b) =>
			{
				modify(a, b);
				return a;
			});
		}

		public ISource<TObject> ModifySequence(Func<IEnumerable<object>, IEnumerable<object>> modify)
		{
			Func<IEnumerable<object>, IEnumerable<object>> newSequenceModifier = x => modify(modifySequence(x));
			return new Source<TObject>(contextSource, create, newSequenceModifier);
		}

		#endregion

		/// <summary>
		/// Creates a copy of this source using this Sources
		/// </summary>
		/// <param name="copy"></param>
		/// <returns></returns>
		public Source<TObject> Clone(Func<ContextSource,Func<CreateContext,TObject>,Source<TObject>> copy)
		{
			return copy(contextSource, create);
		}

		/// <summary>
		/// Creates a copy of this source using a different collection of child sources
		/// </summary>
		/// <param name="modify"></param>
		/// <returns></returns>
		public Source<TObject> ModifyChildSources(Func<ChildSourceMap, ChildSourceMap> modify)
		{
			var newChildSources = modify(contextSource.ChildSources);
			return new Source<TObject>(ContextSource.WithChildSources(newChildSources), create, modifySequence);
		}

	}
}