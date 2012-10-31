using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BuildUp
{
	/// <summary>
	/// IGenerator implementation
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public class Generator<TObject> : IGenerator<TObject>
	{
		private readonly ChildGeneratorCollection childGenerators;
		private readonly Func<CreateContext, TObject> create;
		private readonly Func<IEnumerable<object>, IEnumerable<object>> modifySequence;

		public Generator(ChildGeneratorCollection childGenerators, Func<CreateContext, TObject> create, Func<IEnumerable<object>, IEnumerable<object>> modifySequence = null)
		{
			this.childGenerators = childGenerators;
			this.create = create;
			this.modifySequence = modifySequence ?? (x => x);
		}
		
		public IEnumerable<TObject> Build()
		{
			// Generate the sequence
			var contexts = childGenerators.Tuplize().Select((tuple, index) => new CreateContext(index, tuple));
			var sequence = contexts.Select(x => create(x));
			// Modify the sequence
			return modifySequence(sequence.Cast<object>()).Cast<TObject>();
		}

		IEnumerable IGenerator.Build()
		{
			return Build();
		}

		#region Select, SelectMany

		public IGenerator<TResult> Select<TResult>(Func<TObject, TResult> selector)
		{
			return new Generator<TResult>(childGenerators, @base => selector(this.create(@base)), modifySequence);
		}

		public IGenerator<TObject> Select(Action<TObject> action)
		{
			return Select(instance =>
			{
				action(instance);
				return instance;
			});
		}

		public IGenerator<TResult> SelectMany<TChild, TResult>(Func<IGenerator<TObject>, IGenerator<TChild>> generatorSelector, Func<TObject, TChild, TResult> resultSelector)
		{
			var newGenerator = generatorSelector(this);
			// combine other generator with current child generators
			var newChildGenerators = childGenerators.Add(newGenerator);
			int newGeneratorIndex = newChildGenerators.Count - 1;
			// Create new generator with combined generators plus a function that first invokes this generator's create function,
			// then invokes the new create function on the result, together with the value from the other generator
			Func<CreateContext, TResult> newCreate = context => resultSelector(create(context), (TChild) context.ChildValues[newGeneratorIndex]);

			return new Generator<TResult>(newChildGenerators, newCreate, modifySequence);
		}

		public IGenerator<TObject> SelectMany<TCollection>(Func<IGenerator<TObject>, IGenerator<TCollection>> childSequenceSelector, Action<TObject, TCollection> modify)
		{
			return SelectMany(childSequenceSelector, (a, b) =>
			{
				modify(a, b);
				return a;
			});
		}

		public IGenerator<TObject> ModifySequence(Func<IEnumerable<object>, IEnumerable<object>> modify)
		{
			Func<IEnumerable<object>, IEnumerable<object>> newSequenceModifier = x => modify(modifySequence(x));
			return new Generator<TObject>(childGenerators, create, newSequenceModifier);
		}

		#endregion

		/// <summary>
		/// Creates a copy of this generator using a different collection of child generators
		/// </summary>
		/// <param name="modify"></param>
		/// <returns></returns>
		public Generator<TObject> ModifyChildGenerators(Func<ChildGeneratorCollection, ChildGeneratorCollection> modify)
		{
			var newChildGenerators = modify(childGenerators);
			return new Generator<TObject>(newChildGenerators, create, modifySequence);
		}

	}
}