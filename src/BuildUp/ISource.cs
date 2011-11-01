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
		/// Creates a new source that will create a new sequence of objects by invoking a function against each object created from
		/// the current source
		/// </summary>
		/// <param name="selector"></param>
		/// <returns></returns>
		ISource<TResult> Select<TResult>(Func<TObject,TResult> selector);

		/// <summary>
		/// Creates a new source that will perform a mutating operation on each object created by the current source
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		ISource<TObject> Select(Action<TObject> action);
				
		/// <summary>
		/// Creates a new source that will combine the sequence of objects created by the current source with those from an additional sequence,
		/// then invoke the specified function on each pair of objects.
		/// </summary>
		/// <typeparam name="TChild"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="childSequenceSelector"></param>
		/// <param name="resultSelector"></param>
		/// <returns></returns>
		ISource<TResult> SelectMany<TChild, TResult>(Func<ISource<TObject>, IEnumerable<TChild>> childSequenceSelector, Func<TObject, TChild, TResult> resultSelector);

		/// <summary>
		/// Creates a new source that will combine the sequence of objects created by the current source with those from an additional sequence and 
		/// invokes the specified function on each pair of objects.
		/// </summary>
		/// <typeparam name="TChild"></typeparam>
		/// <param name="childSequenceSelector"></param>
		/// <param name="modify"></param>
		/// <returns></returns>
		ISource<TObject> SelectMany<TChild>(Func<ISource<TObject>, IEnumerable<TChild>> childSequenceSelector, Action<TObject, TChild> modify);

	}
}