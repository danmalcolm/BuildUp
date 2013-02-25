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

		#region ComplexGenerator creation

		// Below are shortcut methods used to combine multiple generators

		/// <summary>
		/// Creates a generator using the supplied function and child generators that provide additional values used by the 
		/// create function. Variations of this method exist for creation functions requiring from 1 to 16 parameters. 
		/// TODO: T4 template to generate these methods
		/// </summary>
		/// <typeparam name="T">The type of the object that the generator will create</typeparam>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <param name="create">The function used to create an object using values from the child generators</param>
		/// <param name="generator1">The child generator used to provide the first additional parameter supplied to the create function</param>
		/// <param name="generator2">The child generator used to provide the second additional parameter supplied to the create function</param>
		/// <returns></returns>
		public static IGenerator<T> Create<T, T1, T2>(Func<T1, T2, T> create, IGenerator<T1> generator1, IGenerator<T2> generator2)
		{
			return generator1.Combine(generator2, create);
		}

		/// <summary>
		/// Creates a generator using the supplied function and child generators that provide values used by the 
		/// create function. Variations of this method exist for creation functions requiring from 1 to 16 parameters. 
		/// TODO: T4 template to generate these methods
		/// </summary>
		/// <typeparam name="T">The type of the object that the generator will create</typeparam>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <param name="create">The function used to create an object using values from the child generators</param>
		/// <param name="generator1">The child generator used to provide the first additional parameter supplied to the create function</param>
		/// <param name="generator2">The child generator used to provide the second additional parameter supplied to the create function</param>
		/// <param name="generator3">The child generator used to provide the third additional parameter supplied to the create function</param>
		/// <returns></returns>
		public static IGenerator<T> Create<T, T1, T2, T3>(Func<T1, T2, T3, T> create,
															   IGenerator<T1> generator1,
															   IGenerator<T2> generator2, IGenerator<T3> generator3)
		{
			return from value1 in generator1
			       from value2 in generator2
			       from value3 in generator3
			       select create(value1, value2, value3);
		}

		#endregion
	}
}