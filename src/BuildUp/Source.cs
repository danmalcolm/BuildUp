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
		public static Source<T> Create<T>(Func<BuildContext, T> create)
		{
			return new Source<T>(create);
		}

        
	}

	public class Source<T> : ISource<T>
	{
		public Source(Func<BuildContext, T> create)
		{
            this.CreateFunc = create;
		}

		#region ISource<T> Members

	    public Func<BuildContext, T> CreateFunc { get; private set; }

		public IEnumerator<T> GetEnumerator()
		{
            // TODO - throw with an informative "are you sure?" exception if we exceed 1m or similar or just let people run with the scissors?
			var context = new BuildContext(0);
			while (true)
			{
                yield return CreateFunc(context);
				context = context.Next();
			}
			// ReSharper disable FunctionNeverReturns
			// Intentionally returns infinite sequence, user should use Take, Single etc
		}

		// ReSharper restore FunctionNeverReturns

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}