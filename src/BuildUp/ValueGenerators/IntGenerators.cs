namespace BuildUp.ValueGenerators
{
	public static class IntGenerators
	{
		public static IGenerator<int> Incrementing(int start, int increment=1)
		{
			return Generator.Create(index => start + index * increment); 
		}
	}
}