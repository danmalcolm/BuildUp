namespace BuildUp
{
	public static class CompositeSourceExtensions
	{
		public static ICompositeSource<T> ChangeSource<T, TSource>(this ICompositeSource<T> compositeSource, int index, ISource<TSource> source)
		{	
			var newSources = compositeSource.Sources.Change(index, source);
			return new CompositeSource<T>(compositeSource.Create, newSources);
		}	
	}
}