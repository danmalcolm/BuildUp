using System;
using System.Collections;
using System.Collections.Generic;

namespace BuildUp
{
	#region Creation methods

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

	#endregion
	
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public class Source<TObject> : ISource<TObject>
	{
		private readonly Func<CreateContext, TObject> create;

		public Source(Func<CreateContext, TObject> create)
		{
			this.create = create;
		}

		public ISource<TResult> Select<TResult>(Func<TObject, TResult> newCreate)
		{
			return new Source<TResult>(context => newCreate(this.create(context)));
		}

		public ISource<TObject> Select(Action<TObject> action)
		{
			return Select(instance =>
			{
				action(instance);
				return instance;
			});
		}

		public IEnumerator<TObject> GetEnumerator()
		{
            // TODO - throw with an informative "are you sure?" exception if we exceed 1m?
			var context = new CreateContext(0);
			while (true)
			{
                yield return this.create(context);
				context = context.Next();
			}
			// ReSharper disable FunctionNeverReturns
			// Intentionally returns infinite sequence, applications are responsible for limiting results using Take, First etc
		}

		// ReSharper restore FunctionNeverReturns

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}