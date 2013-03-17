using System;
using System.Collections.Generic;

namespace BuildUp.Generators
{
	/// <summary>
    /// Creates instances of IGenerator&lt;T&gt; using a range of creation methods
    /// </summary>
	public class Generator
	{
		#region Simple generator creation

		/// <summary>
		/// Creates a generator that generates an infinite sequence repeating a single object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static IGenerator<T> Constant<T>(T instance)
		{
			return Create(index => instance);
		}

        /// <summary>
        /// Creates a generator that generates each object in a sequence using a function
        /// </summary>
        /// <typeparam name="TObject">The type of object generated</typeparam>
        /// <param name="create">The function used to generate each object in the sequence. It is invoked with one parameter: the zero-based index of the object in the sequence.
        /// </param>
        /// <returns></returns>
        public static IGenerator<TObject> Create<TObject>(Func<int, TObject> create)
        {
            return new SimpleGenerator<TObject,EmptyContext>((c,i) => create(i), () => null);
        }

	    /// <summary>
	    /// Creates a generator that generates each object in a sequence using a function
	    /// </summary>
	    /// <typeparam name="TObject">The type of object generated</typeparam>
	    /// <typeparam name="TContext">An additional value made available to the create function </typeparam>
	    /// <param name="create">The function used to generate each object in the sequence. It is invoked with two parameters, the context value and the zero-based index of the object in the sequence.
	    /// </param>
	    /// <param name="createContext"></param>
	    /// <returns></returns>
	    public static IGenerator<TObject> Create<TObject,TContext>(Func<TContext,int,TObject> create, Func<TContext> createContext)
		{
			return new SimpleGenerator<TObject,TContext>(create, createContext);
		}

		#endregion

		#region SequenceGenerator creation

        /// <summary>
        /// Creates a generator that provides objects from an existing sequence
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static IGenerator<T> FromSequence<T>(params T[] sequence)
        {
            return FromSequence((IEnumerable<T>)sequence);
    	}

		/// <summary>
		/// Creates a generator that provides objects from an existing sequence
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sequence"></param>
		/// <returns></returns>
		public static IGenerator<T> FromSequence<T>(IEnumerable<T> sequence)
		{
			return new SequenceGenerator<T>(() => sequence);
		}

		/// <summary>
		/// Creates a generator that provides objects from an existing sequence
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="getSequence"></param>
		/// <returns></returns>
		public static IGenerator<T> FromSequence<T>(Func<IEnumerable<T>> getSequence)
		{
			return new SequenceGenerator<T>(getSequence);
		}

		#endregion

        
    }
}