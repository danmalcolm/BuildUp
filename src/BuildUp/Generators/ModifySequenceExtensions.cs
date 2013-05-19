using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildUp.Generators
{
    /// <summary>
    /// Extension methods for applying change to sequence of objects generated
    /// </summary>
	public static class ModifySequenceExtensions
	{
		/// <summary>
		/// Creates a new generator that changes the sequence of objects generated by the current generator
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="generator"></param>
		/// <param name="modify"></param>
		/// <returns></returns>
		public static IGenerator<T> ModifySequence<T>(this IGenerator<T> generator, Func<IEnumerable<T>,IEnumerable<T>> modify)
		{
			return Generator.Sequence(() => modify(generator.Build()));
		} 

		/// <summary>
		/// Creates a new generator that generates an infinite sequence, repeating the first object generated by the current generator
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="generator"></param>
		/// <returns></returns>
		public static IGenerator<T> Freeze<T>(this IGenerator<T> generator)
		{
			return generator.Loop(1);
		}

        /// <summary>
        /// Stores and repeats each element from the current sequence the specified 
        /// number of times
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="generator"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public static IGenerator<T> RepeatEach<T>(this IGenerator<T> generator, int times)
        {
            if(times < 1)
                throw new ArgumentOutOfRangeException("times", "Should be greater than zero");
            return generator.ModifySequence(sequence => RepeatEach(sequence, times));
        }

        private static IEnumerable<T> RepeatEach<T>(IEnumerable<T> sequence, int times)
        {
            foreach (var item in sequence)
            {
                for (int i = 0; i < times; i++)
                {
                    yield return item;
                }
            }
        } 



		/// <summary>
		/// Stores the specified number of elements from the start of the current sequence and creates
		/// an infinite sequence that loops repeatedly over the stored items. 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="generator"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public static IGenerator<T> Loop<T>(this IGenerator<T> generator, int count)
		{
			return Generator.Sequence(() => Loop(generator.Build(), count));
		}

		private static IEnumerable<T> Loop<T>(IEnumerable<T> sequence, int count)
		{
			var values = sequence.Take(count).ToArray();
			if (values.Length > 0)
			{
				int index = 0;
				while (true)
				{
					yield return values[index];
					index = index < values.Length - 1 ? index + 1 : 0;
				}
			}
		}
	}
}