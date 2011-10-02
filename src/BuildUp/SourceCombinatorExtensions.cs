using System;

namespace BuildUp
{
	public static class SourceCombinatorExtensions
	{
		public static ISource<R> Select<T,R>(this ISource<T> source, Func<T,R> modify)
		{
			return Source.Create(context => modify(source.Create(context)));
		}

	}
}