using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildUp
{
	/// <summary>
    /// Contains factory methods for creating generators
    /// </summary>
	public class Generator
	{
		public static IGenerator<T> Constant<T>(T instance)
		{
			return FromSequence(InfiniteSequence(instance));
		} 

		private static IEnumerable<T> InfiniteSequence<T>(T instance)
		{
			while(true)
			{
				yield return instance;
			}
		}

		public static IGenerator<T> Create<T>(Func<int, T> create)
		{
			return new SimpleGenerator<T>(create);
		}

		private static ComplexGenerator<T> Create<T>(Func<CreateContext, T> create, ChildGeneratorCollection childGenerators)
		{
			return new ComplexGenerator<T>(childGenerators, create);
		}

		/// <summary>
		/// Creates a generator based on an existing sequence
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sequence"></param>
		/// <returns></returns>
		public static IGenerator<T> FromSequence<T>(IEnumerable<T> sequence)
		{
			return new SequenceGenerator<T>(() => sequence);
		}

		/// <summary>
		/// Creates a generator based on a function that returns an existing sequence
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sequence"></param>
		/// <returns></returns>
		public static IGenerator<T> FromSequence<T>(Func<IEnumerable<T>> sequence)
		{
			return new SequenceGenerator<T>(sequence);
		}

		/// <summary>
		/// Creates a generator using the supplied function and child generator that provides an additional value used by the 
		/// create function. Variations of this method exist for creation functions requiring from 1 to 16 parameters. 
		/// TODO: T4 template to generate these methods
		/// </summary>
		/// <typeparam name="T">The type of the object that the generator will create</typeparam>
		/// <typeparam name="T1"></typeparam>
		/// <param name="create">The function used to create an object using values from the child generators</param>
		/// <param name="generator1">The child generator used to provide the first additional parameter supplied to the create function</param>
		/// <returns></returns>
		public static ComplexGenerator<T> Create<T, T1>(Func<CreateContext, T1, T> create, IGenerator<T1> generator1)
		{
			var generatorMap = new ChildGeneratorCollection(generator1);
			return Create(context =>
			{
				var value1 = (T1)context.ChildValues[0];
				return create(context, value1);
			}, generatorMap);
		}

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
		public static ComplexGenerator<T> Create<T, T1, T2>(Func<CreateContext, T1, T2, T> create, IGenerator<T1> generator1, IGenerator<T2> generator2)
		{
			var generators = new ChildGeneratorCollection(generator1, generator2);
			return Create(context =>
			{
				var value1 = (T1)context.ChildValues[0];
				var value2 = (T2)context.ChildValues[1];
				return create(context, value1, value2);
			}, generators);
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
		public static ComplexGenerator<T> Create<T, T1, T2, T3>(Func<CreateContext, T1, T2, T3, T> create,
															   IGenerator<T1> generator1,
															   IGenerator<T2> generator2, IGenerator<T3> generator3)
		{
			var generatorMap = new ChildGeneratorCollection(generator1, generator2, generator3);
			return Create(context =>
			{
				var value1 = (T1)context.ChildValues[0];
				var value2 = (T2)context.ChildValues[1];
				var value3 = (T3)context.ChildValues[2];
				return create(context, value1, value2, value3);
			}, generatorMap);
		}
	}
}