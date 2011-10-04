using System;
using System.Collections.Generic;

namespace BuildUp
{
    /// <summary>
    /// Represents a source of object instances
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public interface ISource<T> : IEnumerable<T>
	{
		T Create(BuildContext context);
	}
}