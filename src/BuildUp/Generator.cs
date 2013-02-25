using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildUp
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
		/// <typeparam name="T"></typeparam>
		/// <param name="create">The function used to generate each object in the sequence. It is invoked with a
		/// single parameter, which is the zero-based index of the object in the sequence.
		/// </param>
		/// <returns></returns>
		public static IGenerator<T> Create<T>(Func<int, T> create)
		{
			return new SimpleGenerator<T>(create);
		}

		#endregion

		#region SequenceGenerator creation

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