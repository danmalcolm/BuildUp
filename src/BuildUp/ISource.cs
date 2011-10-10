using System;
using System.Collections.Generic;

namespace BuildUp
{
	/// <summary>
	/// Contains a sequence of objects
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface ISource<TObject> : IEnumerable<TObject>
	{
		/// <summary>
		/// Creates a new ISource instance by combining with a function that selects a different result
		/// </summary>
		/// <param name="select"></param>
		/// <returns></returns>
		ISource<TResult> Combine<TResult>(Func<TObject,TResult> select);

		/// <summary>
		/// Creates a new ISource instance by combining its creation behaviour with an action that performs some additional
		/// modification to the object. Useful for sources that generate mutable objects
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		ISource<TObject> Combine(Action<TObject> action);
	}
}