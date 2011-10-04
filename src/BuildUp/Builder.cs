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
	/// Mainly intended as a "home" for methods that are used to vary constructor parameters and 
	/// provides support for chaining together modifying methods
	/// </summary>
	/// <typeparam name="T"></typeparam>
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
        /// Creates a new instance of TBuilder with a new source
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

		T ISource<T>.Create(BuildContext context)
		{
			return Source.Create(context);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return Source.GetEnumerator();
		}

		public CtorArgSourceMap Sources
		{
			get { return Source.Sources; }
		}

		public Func<BuildContext, CtorArgSourceMap, T> CreateFunc
		{
			get { return Source.CreateFunc; }
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}