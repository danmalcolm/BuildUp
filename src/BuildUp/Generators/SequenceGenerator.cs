using System;
using System.Collections;
using System.Collections.Generic;

namespace BuildUp.Generators
{
	/// <summary>
	/// Generator implementation that uses a sequence of values provided by a function
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public class SequenceGenerator<TObject> : IGenerator<TObject>
	{
		private readonly Func<IEnumerable<TObject>> getSequence;

		public SequenceGenerator(Func<IEnumerable<TObject>> getSequence)
		{
			this.getSequence = getSequence;
		}

		IEnumerable IGenerator.Create()
		{
			return Create();
		}

		public IEnumerable<TObject> Create()
		{
			var sequence = getSequence();
// ReSharper disable LoopCanBeConvertedToQuery - exposing original sequence could allow it to be mutated
			foreach(var item in sequence)
// ReSharper restore LoopCanBeConvertedToQuery
			{
				yield return item;
			}
		}
	}
}