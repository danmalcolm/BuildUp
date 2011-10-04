using System;
using System.Collections.Generic;

namespace BuildUp
{
    /// <summary>
    /// Creates sequences of objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public interface ISource<T> : IEnumerable<T>
	{
		T Create(BuildContext context);
	}
}