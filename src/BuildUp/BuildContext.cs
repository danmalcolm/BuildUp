namespace BuildUp
{
	/// <summary>
	/// Contains context made available when created objects. Currently indicates only the position of the
	/// object being created in the sequence, which is useful for generating unique / grouped values.
	/// </summary>
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