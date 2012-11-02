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
	public abstract class BuilderBase<T,TBuilder> : IGenerator<T> 
		where TBuilder : BuilderBase<T,TBuilder>, new()
	{
		private IGenerator<T> generator;

	    protected abstract IGenerator<T> GetDefaultGenerator();

        protected void UseCustomGenerator(IGenerator<T> customGenerator)
        {
            this.generator = customGenerator;
        }

		private IGenerator<T> Generator
	    {
	        get { return generator ?? (generator = GetDefaultGenerator()); }
	    }

		/// <summary>
		/// Creates a new instance of the current builder class using a different child generator
		/// </summary>
		protected TBuilder ReplaceChildAtIndex<TChild>(int index, IGenerator<TChild> childGenerator)
		{
			var complexGenerator = Generator as IComplexGenerator<T>;
			if(complexGenerator == null)
			{
				throw new InvalidOperationException("Cannot modify child generators when this builder is not based on a IComplexGenerator.");
			}
			var newGenerator = complexGenerator.ChangeChildren(generators => generators.ReplaceAt(index, childGenerator));
			return Clone(newGenerator);
		}
		
        /// <summary>
        /// Creates a new instance of the builder using a different generator. This is intended to allow implementors
        /// to use the chainable methods that return a new builder, e.g. orderBuilder.WithCustomer(customers).StartingOn(dates)
        /// </summary>
        /// <param name="newGenerator"></param>
        /// <returns></returns>
		protected TBuilder Clone(IGenerator<T> newGenerator)
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

	}
}