namespace BuildUp
{
	public static class CompositeSourceExtensions
	{
		public static ICompositeSource<T> ChangeSource<T, TSource>(this ICompositeSource<T> compositeSource, string name, ISource<TSource> source)
		{	
			var newSources = compositeSource.Sources.Add(name, source);
			return new CompositeSource<T>(compositeSource.Create, newSources);
		}	
	}
}