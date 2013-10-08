using System;

namespace BuildUp.Generators
{
    /// <summary>
    /// Provides access to some useful int value generators
    /// </summary>
    public static class IntGenerator
    {
        /// <summary>
        /// Generates an incrementing sequence of values
        /// </summary>
        /// <param name="start"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        public static IGenerator<int> Incrementing(int start, int increment = 1)
        {
            return Generator.Create(index => start + index * increment);
        }

        /// <summary>
        /// Generates a random sequence of values within a given range. The sequence is 
        /// deterministic, i.e., using the same seed will result in the same sequence.
        /// </summary>
        /// <param name="min">The inclusive lower bound of the range</param>
        /// <param name="max">The inclusive upper bound of the range</param>
        /// <param name="seed">Value used to seed the generated numbers</param>
        /// <returns></returns>
        public static IGenerator<int> Random(int min, int max, int seed)
        {
            return Generator.Create((random, index) => random.Next(min, max + 1), () => new Random(seed));
        }
    }
}