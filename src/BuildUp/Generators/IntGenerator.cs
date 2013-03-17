using System;

namespace BuildUp.Generators
{
    /// <summary>
    /// Creates generators of int
    /// </summary>
	public static class IntGenerator
	{
		public static IGenerator<int> Incrementing(int start, int increment=1)
		{
			return Generator.Create(index => start + index * increment); 
		}

        /// <summary>
        /// Generates a random sequence of values within a given range
        /// </summary>
        /// <param name="min">The inclusive lower bound of the range</param>
        /// <param name="max">The inclusive upper bound of the range</param>
        /// <param name="seed">Value used to seed numbers generated</param>
        /// <returns></returns>
        public static IGenerator<int> Random(int min, int max, int seed)
        {
            return Generator.Create((random,index) => random.Next(min, max + 1), () => new Random(seed));
        } 
	}
}