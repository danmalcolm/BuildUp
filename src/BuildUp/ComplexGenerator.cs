using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BuildUp
{
	/// <summary>
	/// Generates a sequence of objects based on values provided by one or more child generators
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public class ComplexGenerator<TObject> : IGenerator<TObject>
	{
		private readonly ChildGeneratorCollection childGenerators;
		private readonly Func<CreateContext, TObject> create;

		public ComplexGenerator(ChildGeneratorCollection childGenerators, Func<CreateContext, TObject> create)
		{
			this.childGenerators = childGenerators;
			this.create = create;
		}
		
		public IEnumerable<TObject> Build()
		{
			// Generate the sequence
			var contexts = childGenerators.Tuplize().Select((tuple, index) => new CreateContext(index, tuple));
			var sequence = contexts.Select(x => create(x));
			return sequence;
		}

		IEnumerable IGenerator.Build()
		{
			return Build();
		}

		/// <summary>
		/// Creates a copy of this generator using a different collection of child generators
		/// </summary>
		/// <param name="modify"></param>
		/// <returns></returns>
		public ComplexGenerator<TObject> ModifyChildGenerators(Func<ChildGeneratorCollection, ChildGeneratorCollection> modify)
		{
			var newChildGenerators = modify(childGenerators);
			return new ComplexGenerator<TObject>(newChildGenerators, create);
		}

	}
}