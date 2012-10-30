using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildUp
{
	/// <summary>
	/// A collection of child sources that provide values used by a parent source to generate objects
	/// </summary>
	public class ChildSourceCollection
	{
		public static readonly ChildSourceCollection Empty = new ChildSourceCollection();

		private readonly ISource[] childSources;

		public int Count{ get { return childSources.Length; }}

		internal ChildSourceCollection(params ISource[] childSources)
		{
			this.childSources = childSources;
		}

		/// <summary>
		/// Creates a copy of this ChildSourceCollection replacing an item at the specified position
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="index"></param>
		/// <param name="source"></param>
		/// <returns></returns>
		public ChildSourceCollection ReplaceSourceAt<T>(int index, ISource<T> source)
		{
			if (index > childSources.Length)
			{
				throw new IndexOutOfRangeException("A child source does not exist at the specified index and cannot be replaced");
			}
			return Clone(x =>
			{
				x[index] = source;
				return x;
			});
		}

		/// <summary>
		/// Creates a copy of this ChildSourceCollection with an additional source at the end of the collection
		/// </summary>
		/// <param name="sequence"></param>
		/// <returns></returns>
		public ChildSourceCollection Add(ISource sequence)
		{
			return Clone(x => x.Concat(new [] { sequence }));
		}

		private ChildSourceCollection Clone(Func<ISource[], IEnumerable<ISource>> changeValues)
		{
			var newSources = changeValues(childSources.ToArray()).ToArray();
			return new ChildSourceCollection(newSources);
		}

		/// <summary>
		/// Iterates through the child sources, yielding a sequence of arrays (tuples), each containing
		/// a value from each sequence.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<object[]> Tuplize()
		{
			var enumerators = childSources.Select(x => x.Build().GetEnumerator()).ToArray();
			while (enumerators.All(x => x.MoveNext()))
			{
				yield return enumerators.Select(x => x.Current).ToArray();
			}
		}
	}
}