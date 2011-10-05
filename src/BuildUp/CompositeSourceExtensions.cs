using System;
using System.Linq.Expressions;

namespace BuildUp
{
	public static class CompositeSourceExtensions
	{
		/// <summary>
		/// Creates a new instance of a composite source that uses a different child source
		/// </summary>
		public static ICompositeSource<T> ReplaceChildSource<T,C>(this ICompositeSource<T> source, int index, ISource<C> child)
        {
			var newChildSources = source.ChildSources.Replace(index, child);
			return new CompositeSource<T>(source.CompCreateFunc, newChildSources);
        }
	}
}