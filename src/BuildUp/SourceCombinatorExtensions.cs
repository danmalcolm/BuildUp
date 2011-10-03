using System;

namespace BuildUp
{
	public static class SourceCombinatorExtensions
	{
		public static ISource<R> Select<T,R>(this ISource<T> source, Func<T,R> create)
		{
			return Source.Create(context => create(source.Create(context)));
		}

	}
}