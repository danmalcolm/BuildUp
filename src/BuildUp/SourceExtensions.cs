using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BuildUp
{
	public static class SourceExtensions
	{
		public static ISource<TObject> Set<TObject,TMember>(this ISource<TObject> source, Expression<Func<TObject,TMember>> action, TMember value)
		{
			Action<TObject> set = null; // TODO ReflectionHelper and MemberAccessor. Build cached expressions etc
			return source.Select(set);
		}

		public static ISource<TObject> Set<TObject, TMember>(this ISource<TObject> source, Expression<Func<TObject, TMember>> action, IEnumerable<TMember> source1)
		{
			

			return null;
		}

		/// <summary>
		/// Creates a CompositeSource using the supplied function and specified source. The sources will
		/// also be stored in the Sources property of the CompositeSource instance so that they are available
		/// for use when creating a new version of the CompositeSource. Variations of this method exist for 
		/// creation functions requiring from 1 (TODO) to 16 (TODO) parameters. 
		/// </summary>
		/// <typeparam name="T">The type of the object that the source will create</typeparam>
		/// <typeparam name="T1"></typeparam>
		/// <param name="create">The function used to create an object using values from the specified sources</param>
		/// <param name="source1">The child source used to provide the value used to create the object</param>
		/// <returns></returns>
		public static ISource<T> Select<T, T1>(this ISource<T> source, Action<T, T1> create, IEnumerable<T1> source1)
		{
			var sourceMap = new ChildSourceMap(source1);
			return null;
			/*
			return Something((context, values) =>
			{
				T o = default(T);
				var value1 = (T1)values[0];
				create(context, o, value1);
			}, sourceMap);
			 */
		}
	}
}