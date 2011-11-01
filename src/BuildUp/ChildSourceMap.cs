using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BuildUp.Utility;

namespace BuildUp
{
	/// <summary>
	/// Contains the collection of child sources used by Sources using a base sequence that  have multiple sources
	/// </summary>
	public class ChildSourceMap
	{
		private readonly IEnumerable[] sources;
		public int Count{ get { return sources.Length; }}

		internal ChildSourceMap(params IEnumerable[] sources)
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
		public ChildSourceMap Replace<T>(int index, IEnumerable<T> source)
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
		/// <param name="newSource"></param>
		/// <returns></returns>
		public ChildSourceMap Add(IEnumerable newSource)
		{
			return Clone(x => x.Concat(new [] { newSource }));
		}

		private ChildSourceMap Clone(Func<IEnumerable[], IEnumerable<IEnumerable>> changeValues)
		{
			var newSources = changeValues(sources.ToArray()).ToArray();
			return new ChildSourceMap(newSources);
		}

		public IEnumerable<object[]> Tuplize()
		{
			return EnumerableUtility.Tuplize(sources);
		}
	}
}