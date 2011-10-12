using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BuildUp.Utility;

namespace BuildUp
{
	/// <summary>
	/// Contains the collection of child sources used by CompositeSource, indexed by the position in which
	/// they were declared in the create function - see Source.Create
	/// </summary>
	internal class ChildSourceMap
	{
		private readonly IEnumerable[] sources;

		public ChildSourceMap()
			: this(new Dictionary<string, object>())
		{
			
		}

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
			if(index > sources.Length)
			{
				throw new IndexOutOfRangeException("A child source does not exist at the specified index and cannot be replaced");
			}
			return CreateCopyWithChanges(x => x[index] = source);
		}

		private ChildSourceMap CreateCopyWithChanges(Action<object[]> changeValues)
		{
			var newSources = this.sources.ToArray();
			changeValues(newSources);
			return new ChildSourceMap(newSources);
		}

		public IEnumerable<object[]> Tuplize()
		{
			return EnumerableUtility.Tuplize(this.sources);
		}
	}
}