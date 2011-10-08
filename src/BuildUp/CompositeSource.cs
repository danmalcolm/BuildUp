using System;
using System.Collections;
using System.Collections.Generic;

namespace BuildUp
{
	public class CompositeSource
	{
		private static CompositeSource<T> Create<T>(Func<CreateContext, object[], T> create,
		                                            ChildSourceMap sources)
		{
			return new CompositeSource<T>(create, sources);
		}

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
		public static CompositeSource<T> Create<T, T1, T2>(Func<CreateContext, T1, T2, T> create, IEnumerable<T1> source1,
		                                                   IEnumerable<T2> source2)
		{
			var sourceMap = new ChildSourceMap(source1, source2);
			return Create((context, values) =>
			{
				var value1 = (T1) values[0];
				var value2 = (T2) values[1];
				return create(context, value1, value2);
			}, sourceMap);
		}

		public static CompositeSource<T> Create<T, T1, T2, T3>(Func<CreateContext, T1, T2, T3, T> create,
		                                                       IEnumerable<T1> source1,
		                                                       IEnumerable<T2> source2, IEnumerable<T3> source3)
		{
			var sourceMap = new ChildSourceMap(source1, source2, source3);
			return Create((context, values) =>
			{
				var value1 = (T1) values[0];
				var value2 = (T2) values[1];
				var value3 = (T3) values[2];
				return create(context, value1, value2, value3);
			}, sourceMap);
		}

		#endregion
	}


	/// <summary>
	/// Creates a sequence of objects using values from one or more child sequences
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class CompositeSource<T> : IEnumerable<T>
	{
		private readonly ChildSourceMap childSources;
		private readonly Func<CreateContext, object[], T> create;

		internal CompositeSource(Func<CreateContext, object[], T> create, ChildSourceMap childSources)
		{
			this.create = create;
			this.childSources = childSources;
		}

		public IEnumerator<T> GetEnumerator()
		{
			var context = new CreateContext(0);
			foreach (var tuple in childSources.Tuplize())
			{
				yield return create(context, tuple);
				context = context.Next();
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public CompositeSource<T> ReplaceChildSource<C>(int index, IEnumerable<C> source)
		{
			ChildSourceMap newChildSources = childSources.Replace(index, source);
			return new CompositeSource<T>(create, newChildSources);
		}
	}
}