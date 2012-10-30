using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BuildUp
{
	/// <summary>
    /// Contains factory methods for creating sources
    /// </summary>
	public class Source
	{
		public static Source<T> Create<T>(Func<CreateContext, T> create)
		{
			return new Source<T>(ChildSourceCollection.Empty, create);
		}

		private static Source<T> Create<T>(Func<CreateContext, T> create, ChildSourceCollection childSources)
		{
			return new Source<T>(childSources, create);
		}

		#region Create functions

		/// <summary>
		/// Creates a source based on an existing sequence
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sequence"></param>
		/// <returns></returns>
		public static Source<T> Create<T>(IEnumerable<T> sequence)
		{
			return Create((context) => sequence.ElementAt(context.Index));
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
		/// <param name="source1">The child source used to provide the first parameter to the create function</param>
		/// <returns></returns>
		public static Source<T> Create<T, T1>(Func<CreateContext, T1, T> create, ISource<T1> source1)
		{
			var sourceMap = new ChildSourceCollection(source1);
			return Create(context =>
			{
				var value1 = (T1)context.ChildSourceValues[0];
				return create(context, value1);
			}, sourceMap);
		}

		/// <summary>
		/// Creates a source using the supplied function and specified child sources. The child sources will
		/// also be stored in the Sources property of the CompositeSource instance so that they are available
		/// for use when creating a new version of the CompositeSource. Variations of this method exist for 
		/// creation functions requiring from 1 to 16 parameters. 
		/// TODO: T4 template to generate these methods
		/// </summary>
		/// <typeparam name="T">The type of the object that the source will create</typeparam>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <param name="create">The function used to create an object using values from the child sources</param>
		/// <param name="source1">The child source used to provide the first parameter to the create function</param>
		/// <param name="source2">The child source used to provide the second parameter to the create function</param>
		/// <returns></returns>
		public static Source<T> Create<T, T1, T2>(Func<CreateContext, T1, T2, T> create, ISource<T1> source1, ISource<T2> source2)
		{
			var sourceMap = new ChildSourceCollection(source1, source2);
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
			var sourceMap = new ChildSourceCollection(source1, source2, source3);
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

}