namespace BuildUp
{
	public static class IntSources
	{
		public static Source<int> Incrementing
		{
			get { return Source.Create(c => c.Index); }
		}

		public static Source<int> Constant(int value)
		{
			return Source.Create(c => value);
		}
	}
}