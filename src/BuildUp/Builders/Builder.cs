using System;
using System.Collections;
using System.Collections.Generic;

namespace BuildUp.Builders
{
	/// <summary>
	/// Base class for typical *Builder classes (CustomerBuilder, OrderBuilder) that allow test code to vary values used
	/// to create the objects via chainable methods, e.g. new OrderBuilder().WithCustomer( ...). In some projects, builder 
	/// classes will be the most suitable place to locate methods that are used to vary parameters used to construct the objects.
	/// This base class provides some convenience methods to make these chainable methods simple to write.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TBuilder">The concrete type of the builder class. This self-referencing generic type parameter
	/// allows us to define behaviour in this base class that returns an instance of the concrete builder type.</typeparam>
	public abstract class Builder<T,TBuilder> : IGenerator<T> 
		where TBuilder : Builder<T,TBuilder>, new()
	{
	    private Generator<T> generator;

	    protected abstract Generator<T> GetDefaultGenerator();

        protected void UseCustomGenerator(Generator<T> customGenerator)
        {
            this.generator = customGenerator;
        }

		private Generator<T> Generator
	    {
	        get { return generator ?? (generator = GetDefaultGenerator()); }
	    }

		/// <summary>
		/// Creates a new instance of the current builder class using a different child generator
		/// </summary>
		protected TBuilder ChangeChildGenerator<TChild>(int index, IGenerator<TChild> childGenerator)
		{
			var newGenerator = Generator.ModifyChildGenerators(generators => generators.ReplaceGeneratorAt(index, childGenerator));
			return Clone(newGenerator);
		}
		
        /// <summary>
        /// Creates a new instance of the builder using a different generator
        /// </summary>
        /// <param name="newGenerator"></param>
        /// <returns></returns>
		protected TBuilder Clone(Generator<T> newGenerator)
		{
		    var builder = new TBuilder();
            builder.UseCustomGenerator(newGenerator);
		    return builder;
		}

		public IEnumerable<T> Build()
		{
			return Generator.Build();
		}

		IEnumerable IGenerator.Build()
		{
			return Build();
		}

		public IGenerator<TResult> Select<TResult>(Func<T, TResult> selector)
		{
			return Generator.Select(selector);
		}

		public IGenerator<T> Select(Action<T> action)
		{
			return Generator.Select(action);
		}

		public IGenerator<TResult> SelectMany<TCollection, TResult>(Func<IGenerator<T>, IGenerator<TCollection>> sequenceSelector, Func<T, TCollection, TResult> resultSelector)
		{
			return Generator.SelectMany(sequenceSelector, resultSelector);
		}

		public IGenerator<T> SelectMany<TCollection>(Func<IGenerator<T>, IGenerator<TCollection>> childSequenceSelector, Action<T, TCollection> modify)
		{
			return Generator.SelectMany(childSequenceSelector, modify);
		}

		public IGenerator<T> ModifySequence(Func<IEnumerable<object>, IEnumerable<object>> modify)
		{
			return Generator.ModifySequence(modify);
		}
	}
}