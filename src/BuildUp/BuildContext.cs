namespace BuildUp
{
	public class BuildContext
	{
		public BuildContext(int index)
		{
			Index = index;
		}

		public int Index { get; private set; }

		public BuildContext Next()
		{
			return new BuildContext(Index + 1);
		} 
	}
}