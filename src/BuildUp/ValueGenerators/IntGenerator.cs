using System;

namespace BuildUp.ValueGenerators
{
	public static class IntGenerator
	{
		public static IGenerator<int> Incrementing(int start, int increment=1)
		{
			return Generator.Create(index => start + index * increment); 
		}

        // Generates a random sequence of values within a given range
        public static IGenerator<int> Scatter(int min, int max, int seed)
        {
            throw new NotImplementedException("TODO");
        } 
	}
}