using System;
using System.Linq;

namespace BuildUp
{
	public static class SourceExtensions
	{
		public static ISource<R> Select<T,R>(this ISource<T> source, Func<T,R> create)
		{
			return Source.Create(context => create(source.CreateFunc(context)));
		}

		/// <summary>
		/// Always returns the first object created by the source
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static ISource<T> Freeze<T>(this ISource<T> source)
		{
			var frozen = new Lazy<T>(source.FirstOrDefault);
			return Source.Create(context => frozen.Value);
		}

		/// <summary>
		/// Repeats each element returned by the source a specified number of times
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static ISource<T> RepeatEach<T>(this ISource<T> source, int times)
		{
			return Source.Create(context =>
			{
				var index = context.Index / times;
				//TODO - this is wrong right now, because it's creating maybe we need IEnumerables instead of ISources?... Is this suitable for child sources
				// used by composite sources?
				return source.CreateFunc(new BuildContext(index));
			});
		}
	}
}