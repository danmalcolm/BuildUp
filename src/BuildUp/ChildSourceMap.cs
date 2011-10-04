using System;
using System.Collections.Generic;
using BuildUp.Utility;

namespace BuildUp
{
	/// <summary>
	/// Contains the collection of child sources used by a composite source, which are stored / retrieved either
	/// by name or position in the create function
	/// </summary>
	public class ChildSourceMap
	{
		private readonly Dictionary<string,object> sources;

		public ChildSourceMap()
			: this(new Dictionary<string, object>())
		{
			
		}

		internal ChildSourceMap(params object[] argSources)
		{
		    this.sources = new Dictionary<string, object>();
			argSources.EachWithIndex((source, index) => this.sources[index.ToString()] = source);
		}

		private ChildSourceMap(Dictionary<string, object> sources)
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
		public ChildSourceMap Replace<T>(int index, ISource<T> source)
		{
			if(!sources.ContainsKey(index.ToString()))
			{
				throw new IndexOutOfRangeException("A child source does not exist at the specified index");
			}
			return Clone(x => x[index.ToString()] = source);
		}

		private ChildSourceMap Clone(Action<Dictionary<string, object>> mutate)
		{
			var newSources = new Dictionary<string, object>(this.sources);
			mutate(newSources);
			return new ChildSourceMap(newSources);
		}

		public T Create<T>(int index, BuildContext context)
		{
			var source = sources[index.ToString()];

			if (!typeof (ISource<T>).IsAssignableFrom(source.GetType()))
			{
				throw new ArgumentException(string.Format("Source at index {0} is of the expected type {1}. Actual type {2}", index, typeof(T), source.GetType()));
			}
			return ((ISource<T>)source).Create(context);
		}
	}


}