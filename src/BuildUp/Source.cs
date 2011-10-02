using System;
using System.Collections;
using System.Collections.Generic;

namespace BuildUp
{
	public class Source
	{
		public static Source<T> Create<T>(Func<BuildContext, T> build)
		{
			return new Source<T>(build);
		}

		public static ISource<T> Composite<T, T1, T2>(ISource<T1> source1, ISource<T2> source2, Func<T1, T2, T> build)
		{
			return new Source<T>(context =>
			{
				T1 value1 = source1.Create(context);
				T2 value2 = source2.Create(context);
				return build(value1, value2);
			});
		}
	}

	public class Source<T> : ISource<T>
	{
		private readonly Func<BuildContext, T> build;

		public Source(Func<BuildContext, T> build)
		{
			this.build = build;
		}

		#region ISource<T> Members

		public T Create(BuildContext context)
		{
			return build(context);
		}

		public IEnumerator<T> GetEnumerator()
		{
			var context = new BuildContext(0);
			while (true)
			{
				yield return build(context);
				context = context.Next();
			}
			// ReSharper disable FunctionNeverReturns
			// Intentionally returns infinite sequence
		}

		// ReSharper restore FunctionNeverReturns

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}