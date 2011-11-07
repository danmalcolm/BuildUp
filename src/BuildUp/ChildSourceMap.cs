using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BuildUp.Utility;

namespace BuildUp
{
	/// <summary>
	/// Contains the collection of child sources that provide values used by a source to create objects
	/// </summary>
	public class ChildSourceMap
	{
		private readonly ISource[] sources;
		public int Count{ get { return sources.Length; }}

		internal ChildSourceMap(params ISource[] sources)
		{
			this.sources = sources;
		}

		/// <summary>
		/// Creates a copy of this ChildSourceMap replacing an item at the specified position
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="index"></param>
		/// <param name="source"></param>
		/// <returns></returns>
		public ChildSourceMap ReplaceAt<T>(int index, ISource<T> source)
		{
			if (index > sources.Length)
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
		/// Creates a copy of this ChildSourceMap with an additional source at the end of the collection
		/// </summary>
		/// <param name="sequence"></param>
		/// <returns></returns>
		public ChildSourceMap Add(ISource sequence)
		{
			return Clone(x => x.Concat(new [] { sequence }));
		}

		private ChildSourceMap Clone(Func<ISource[], IEnumerable<ISource>> changeValues)
		{
			var newSources = changeValues(sources.ToArray()).ToArray();
			return new ChildSourceMap(newSources);
		}

		public IEnumerable<object[]> Tuplize()
		{
			var enumerators = sources.Select(x => x.Build().GetEnumerator()).ToArray();
			while (enumerators.All(x => x.MoveNext()))
			{
				yield return enumerators.Select(x => x.Current).ToArray();
			}
		}
	}
}