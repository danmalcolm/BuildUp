using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BuildUp
{
	#region Creation methods

	/// <summary>
    /// Contains methods for creation of ISource instances
    /// </summary>
	public class Source
	{
		public static ISource<T> Create<T>(Func<CreateContext, T> create)
		{
			return new Source<CreateContext,T>(CreateContext.Sequence, create);
		}
	}

	#endregion

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	/// <typeparam name="TBase"></typeparam>
	public class Source<TBase,TObject> : ISource<TObject>
	{
		private readonly IEnumerable<TBase> baseSequence;
		private readonly Func<TBase, TObject> create;

		public Source(IEnumerable<TBase> baseSequence, Func<TBase,TObject> create)
		{
			this.baseSequence = baseSequence;
			this.create = create;
		}
		
		public IEnumerator<TObject> GetEnumerator()
		{
			return baseSequence.Select(@base => create(@base)).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#region Select, SelectMany

		public ISource<TResult> Select<TResult>(Func<TObject, TResult> selector)
		{
			return new Source<TBase,TResult>(baseSequence, @base => selector(this.create(@base)));
		}

		public ISource<TObject> Select(Action<TObject> action)
		{
			return Select(instance =>
			{
				action(instance);
				return instance;
			});
		}

		public ISource<TResult> SelectMany<TCollection, TResult>(Func<ISource<TObject>, IEnumerable<TCollection>> sourceSelector, Func<TObject, TCollection, TResult> resultSelector)
		{
			var sequence = sourceSelector(this);
			// join base sequence with new sequence
			var combined = baseSequence.Zip(sequence, Tuple.Create);
			// Create new source with combined sequence as base sequence plus a function that invokes the resultSelector on result
			// of this source's create function
			return new Source<Tuple<TBase,TCollection>,TResult> (combined, tuple => resultSelector(create(tuple.Item1), tuple.Item2));
		}

		public ISource<TObject> SelectMany<TCollection>(Func<ISource<TObject>, IEnumerable<TCollection>> sourceSelector, Action<TObject, TCollection> modify)
		{
			return SelectMany(sourceSelector, (a, b) =>
			{
				modify(a, b);
				return a;
			});
		}

		#endregion
	}
}