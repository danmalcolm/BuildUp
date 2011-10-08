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
			var frozen = new Lazy<T>(sequence.FirstOrDefault);
			if(sequence.Any())
			{
				while(true)
				{
					yield return sequence.First();
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

		/// <summary>
		/// Modifies each element in a sequence, using a value from another sequence
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IEnumerable<T> Modify<T,T1>(this IEnumerable<T> source, IEnumerable<T1> first, Action<T, T1> action)
		{
			return source.Zip(first, (item, other) =>
			{
				action(item, other);
				return item;
			});
		}
	}
}