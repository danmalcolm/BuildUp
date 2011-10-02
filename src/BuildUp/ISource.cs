using System;
using System.Collections.Generic;

namespace BuildUp
{
	public interface ISource<T> : IEnumerable<T>
	{
		T Create(BuildContext context);
	}
}