using System;
using System.Collections.Generic;

namespace BuildUp
{
	/// <summary>
	/// Contains a collection of ISource instances keyed by name
	/// </summary>
	public class SourceMap
	{
		private readonly Dictionary<string,object> sources;

		public SourceMap()
			: this(new Dictionary<string, object>())
		{
			
		}

		private SourceMap(Dictionary<string, object> sources)
		{
			this.sources = sources;
		}

		/// <summary>
		/// Creates a copy of the map adding or replacing the specified item
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <param name="source"></param>
		/// <returns></returns>
		public SourceMap Add<T>(string name, ISource<T> source)
		{
			return Clone(x => x.Add(name, source));
		}

		private SourceMap Clone(Action<Dictionary<string, object>> mutate)
		{
			var newSources = new Dictionary<string, object>(this.sources);
			mutate(newSources);
			return new SourceMap(newSources);
		}

		public T Create<T>(string name, BuildContext context)
		{
			var source = sources[name];

			if (!typeof (ISource<T>).IsAssignableFrom(source.GetType()))
			{
				throw new ArgumentException(string.Format("Source {0} is of the expected type {1}. Actual type {2}", name, typeof(T), source.GetType()));
			}
			return ((ISource<T>)source).Create(context);
		}
	}


}