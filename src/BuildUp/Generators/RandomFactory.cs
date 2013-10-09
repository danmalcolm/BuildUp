using System;

namespace BuildUp.Generators
{
    internal static class RandomFactory
    {
        /// <summary>
        /// Creates a Random, with or without a seed
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static Random Create(int? seed)
        {
            return seed != null ? new Random(seed.Value) : new Random();
        }
    }
}