using System;
using System.Linq.Expressions;

namespace BuildUp
{
	public static class CompositeSourceExtensions
	{
		public static ICompositeSource<T> ChangeSource<T, TSource>(this ICompositeSource<T> compositeSource, int index, ISource<TSource> source)
		{	
			var newSources = compositeSource.Sources.Change(index, source);
			return new CompositeSource<T>(compositeSource.Create, newSources);
		}

        /// <summary>
        /// Experimental syntax for specifying changing one or more parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="compositeSource"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public static ICompositeSource<T> ChangeSource<T>(this ICompositeSource<T> compositeSource, Expression<Func<T>> create)
        {
            return null;
        }	
	}
}