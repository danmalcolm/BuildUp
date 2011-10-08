using System;
using System.Collections;
using System.Collections.Generic;

namespace BuildUp
{
    /// <summary>
    /// Contains methods for creation of ISource instances
    /// </summary>
	public class Source
	{
		public static Source<T> Create<T>(Func<CreateContext, T> create)
		{
			return new Source<T>(create);
		}
	}

	/// <summary>
	/// Generates a sequence of objects suitable for unit testing, test data generation etc
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Source<T> : IEnumerable<T>
	{
		private readonly Func<CreateContext, T> create;

		public Source(Func<CreateContext, T> create)
		{
            this.create = create;
		}

		public IEnumerator<T> GetEnumerator()
		{
            // TODO - throw with an informative "are you sure?" exception if we exceed 1m or similar or just let people run with the scissors?
			var context = new CreateContext(0);
			while (true)
			{
                yield return create(context);
				context = context.Next();
			}
			// ReSharper disable FunctionNeverReturns
			// Intentionally returns infinite sequence, user should use Take, First etc
		}

		// ReSharper restore FunctionNeverReturns

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}