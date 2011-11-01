using System;
using System.Collections.Generic;
using System.Linq;
using BuildUp.Utility;

namespace BuildUp
{
	/// <summary>
	/// Utility methods useful in object builder scenarios
	/// </summary>
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Returns an infinite sequence repeating the first object in a sequence
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sequence"></param>
		/// <returns></returns>
		public static IEnumerable<T> Freeze<T>(this IEnumerable<T> sequence)
		{
			var enumerator = sequence.GetEnumerator();
			if(enumerator.MoveNext())
			{
				while(true)
				{
					yield return enumerator.Current;
				}
			}
		}

		/// <summary>
		/// Repeats each element in a sequence the specified number of times
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public static IEnumerable<T> RepeatEach<T>(this IEnumerable<T> source, int count)
		{
			return from item in source
			       from repeat in Enumerable.Range(0, count)
			       select item;
		}
	}
}