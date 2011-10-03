using System;
using System.Collections.Generic;

namespace BuildUp
{
	/// <summary>
	/// Contains a collection of ISource instances used to instantiate an object, keyed by index of their position in constructor
	/// </summary>
	public class CtorArgSourceMap
	{
		private readonly Dictionary<string,object> sources;

		public CtorArgSourceMap()
			: this(new Dictionary<string, object>())
		{
			
		}

		private CtorArgSourceMap(Dictionary<string, object> sources)
		{
			this.sources = sources;
		}

		/// <summary>
		/// Creates a copy of the map adding or replacing an item at the specified position
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="index"></param>
		/// <param name="source"></param>
		/// <returns></returns>
		public CtorArgSourceMap Change<T>(int index, ISource<T> source)
		{
			return Clone(x => x[index.ToString()] = source);
		}

		private CtorArgSourceMap Clone(Action<Dictionary<string, object>> mutate)
		{
			var newSources = new Dictionary<string, object>(this.sources);
			mutate(newSources);
			return new CtorArgSourceMap(newSources);
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