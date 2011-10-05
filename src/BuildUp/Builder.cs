using System;
using System.Collections;
using System.Collections.Generic;

namespace BuildUp
{


	// Bit sad to be departing from more functional model here, but realistically most test object builders need a home
	// for some shared knowledge about how objects are constructed. On balance, deriving a CustomerBuilder
	// from Builder and adding methods like WithCode, WithName is probably better than adding extension methods to ICompositeSource<Customer>. We've ended up wrapping our nice library of functions and combinators into a more user-friendly
	// OO outer shell

	/// <summary>
	/// Base class for typical *Builder classes (CustomerBuilder, OrderBuilder) that allow test code to vary values used
	/// to create the objects via chainable methods, e.g. new OrderBuilder().WithCustomer( ...). In some projects, builder 
	/// classes will be the most suitable place to locate methods that are used to vary parameters used to construct the objects.
	/// This base class provides some convenience methods to make these chainable methods simple to write.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TBuilder">The concrete type of the builder class. This self-referencing generic type parameter
	/// is required to support chainable methods, where an instance of the concrete builder type is returned.</typeparam>
	public abstract class Builder<T,TBuilder> : ICompositeSource<T> 
		where TBuilder : Builder<T,TBuilder>, new()
	{
	    private ICompositeSource<T> source;

	    protected abstract ICompositeSource<T> GetDefaultSource();

        protected void UseCustomSource(ICompositeSource<T> customSource)
        {
            this.source = customSource;
        }

	    protected ICompositeSource<T> Source
	    {
	        get { return source ?? (source = GetDefaultSource()); }
	    }

		/// <summary>
		/// Creates a new instance of the current builder class using a different child source
		/// </summary>
		protected TBuilder ChangeChildSource<TSource>(int index, ISource<TSource> childSource)
		{
			var newSource = Source.ReplaceChildSource(index, childSource);
			return ChangeSource(newSource);
		}
		
        /// <summary>
        /// Creates a new instance of the builder using a different source
        /// </summary>
        /// <param name="newSource"></param>
        /// <returns></returns>
		protected TBuilder ChangeSource(ICompositeSource<T> newSource)
		{
		    var builder = new TBuilder();
            builder.UseCustomSource(newSource);
		    return builder;
		}

		#region ICompositeSource<T> Members

		Func<BuildContext,T> ISource<T>.CreateFunc
		{
            get { return Source.CreateFunc; }
		}

		public IEnumerator<T> GetEnumerator()
		{
			return Source.GetEnumerator();
		}

		public ChildSourceMap ChildSources
		{
			get { return Source.ChildSources; }
		}

		public Func<BuildContext, ChildSourceMap, T> CompCreateFunc
		{
			get { return Source.CompCreateFunc; }
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}