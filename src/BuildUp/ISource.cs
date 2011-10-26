using System;
using System.Collections.Generic;

namespace BuildUp
{
	/// <summary>
	/// Creates a sequence of objects suitable for unit testing, test data generation etc
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface ISource<TObject> : IEnumerable<TObject>
	{
		/// <summary>
		/// Creates a new source that will create objects by invoking a function against each item generated
		/// by the current source
		/// </summary>
		/// <param name="selector"></param>
		/// <returns></returns>
		ISource<TResult> Select<TResult>(Func<TObject,TResult> selector);

		/// <summary>
		/// Creates a new source that will mutate objects built from the current source using the specified action
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		ISource<TObject> Select(Action<TObject> action);
				
		/// <summary>
		/// Creates a new source that will combine the sequence created by the current source with items from an additional sequence and 
		/// invokes the specified function to create a new object.
		/// </summary>
		/// <typeparam name="TCollection"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="sourceSelector"></param>
		/// <param name="resultSelector"></param>
		/// <returns></returns>
		ISource<TResult> SelectMany<TCollection, TResult>(Func<ISource<TObject>, IEnumerable<TCollection>> sourceSelector, Func<TObject, TCollection, TResult> resultSelector);

		ISource<TObject> SelectMany<TCollection>(Func<ISource<TObject>, IEnumerable<TCollection>> sourceSelector, Action<TObject, TCollection> modify);

	}
}