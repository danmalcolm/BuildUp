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
        public static IGenerator<int> Step(int start, int increment = 1)
        {
            return Generator.Create(index => start + index * increment);
        }

        /// <summary>
        /// Generates an incrementing sequence of values
        /// </summary>
        /// <param name="start"></param>
        /// <param name="minStep"></param>
        /// <param name="maxStep"> </param>
        /// <param name="seed"> </param>
        /// <returns></returns>
        public static IGenerator<int> RandomStep(int start, int minStep, int maxStep, int? seed = null)
        {
            return Generator.Create((context, index) =>
            {
                int result = context.Last + context.Random.Next(minStep, maxStep);
                context.Last = result;
                return result;
            }, 
            () => new RandomStepContext { Random = RandomFactory.Create(seed), Last = start } );
        }

        private class RandomStepContext
        {
            public Random Random { get; set; }
            public int Last { get; set; }
        }

        /// <summary>
        /// Generates a random sequence of values within a given range. The sequence is 
        /// deterministic, i.e., using the same seed will result in the same sequence.
        /// </summary>
        /// <param name="min">The inclusive lower bound of the range</param>
        /// <param name="max">The inclusive upper bound of the range</param>
        /// <param name="seed">Value used to seed the generated numbers</param>
        /// <returns></returns>
        public static IGenerator<int> Random(int min, int max, int? seed = null)
        {
            return Generator.Create((random, index) => random.Next(min, max + 1), () => RandomFactory.Create(seed));
        }
    }
}