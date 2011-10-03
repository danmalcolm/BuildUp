using System;
using System.Linq.Expressions;

namespace BuildUp
{
	public static class CompositeSourceExtensions
	{
		/// <summary>
        /// Experimental syntax for specifying changes to one or more sources used by the creation function
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