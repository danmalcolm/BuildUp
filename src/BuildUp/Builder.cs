using System;
using System.Collections;
using System.Collections.Generic;

namespace BuildUp
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
	public abstract class Builder<T,TBuilder> : ISource<T> 
		where TBuilder : Builder<T,TBuilder>, new()
	{
	    private CompositeSource<T> source;

	    protected abstract CompositeSource<T> GetDefaultSource();

        protected void UseCustomSource(CompositeSource<T> customSource)
        {
            this.source = customSource;
        }

	    protected CompositeSource<T> Source
	    {
	        get { return source ?? (source = GetDefaultSource()); }
	    }

		/// <summary>
		/// Creates a new instance of the current builder class using a different child source
		/// </summary>
		protected TBuilder ChangeChildSource<TChild>(int index, IEnumerable<TChild> childSource)
		{
			var newSource = Source.ReplaceChildSource(index, childSource);
			return CloneUsingNewSource(newSource);
		}
		
        /// <summary>
        /// Creates a new instance of the builder using a different source
        /// </summary>
        /// <param name="newSource"></param>
        /// <returns></returns>
		protected TBuilder CloneUsingNewSource(CompositeSource<T> newSource)
		{
		    var builder = new TBuilder();
            builder.UseCustomSource(newSource);
		    return builder;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return Source.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public ISource<TResult> Select<TResult>(Func<T, TResult> selector)
		{
			return Source.Select(selector);
		}

		public ISource<T> Select(Action<T> action)
		{
			return Source.Select(action);
		}

		public ISource<TResult> SelectMany<TCollection, TResult>(Func<ISource<T>, IEnumerable<TCollection>> sourceSelector, Func<T, TCollection, TResult> resultSelector)
		{
			return Source.SelectMany(sourceSelector, resultSelector);
		}

		public ISource<T> SelectMany<TCollection>(Func<ISource<T>, IEnumerable<TCollection>> sourceSelector, Action<T, TCollection> modify)
		{
			return Source.SelectMany(sourceSelector, modify);
		}
	}
}