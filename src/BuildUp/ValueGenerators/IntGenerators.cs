namespace BuildUp.ValueGenerators
{
	public static class IntGenerators
	{
		public static IGenerator<int> Incrementing(int start, int increment=1)
		{
			return Generators.Create(c => start + c.Index * increment); 
		}

		public static IGenerator<int> Constant(int value)
		{
			return Generators.Create(c => value);
		}
	}
}