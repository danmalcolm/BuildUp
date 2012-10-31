using System;
using System.Collections;
using System.Collections.Generic;

namespace BuildUp
{
	/// <summary>
	/// Provides access by enumerating an existing sequence
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public class SequenceGenerator<TObject> : IGenerator<TObject>
	{
		private readonly Func<IEnumerable<TObject>> getSequence;

		public SequenceGenerator(Func<IEnumerable<TObject>> getSequence)
		{
			this.getSequence = getSequence;
		}

		IEnumerable IGenerator.Build()
		{
			return Build();
		}

		public IEnumerable<TObject> Build()
		{
			var sequence = getSequence();
// ReSharper disable LoopCanBeConvertedToQuery - don't want to expose the original sequence, which could be a mutable
			foreach(var item in sequence)
// ReSharper restore LoopCanBeConvertedToQuery
			{
				yield return item;
			}
		}
	}
}