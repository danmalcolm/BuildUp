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
	/// Mainly intended as a "home" for methods that builders typically implement and 
	/// provide support for chaining together modifying methods
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Builder<T,TBuilder> : ICompositeSource<T> 
		where TBuilder : Builder<T,TBuilder>, new()
	{
		protected ICompositeSource<T> Source { get; set; }

		protected Builder(ICompositeSource<T> source)
		{
			this.Source = source;
		}
		

		protected TBuilder Copy(ICompositeSource<T> source)
		{
			return new TBuilder
			{
				Source = source
			};
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