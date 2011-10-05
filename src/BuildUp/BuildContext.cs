namespace BuildUp
{
	/// <summary>
	/// Contains context in which objects are being created.
	/// </summary>
	public class BuildContext
	{
		public BuildContext(int index)
		{
			Index = index;
		}

        /// <summary>
        /// The position of the object being created in the sequence, which is useful for generating unique / grouped values.
        /// </summary>
		public int Index { get; private set; }

		public BuildContext Next()
		{
			return new BuildContext(Index + 1);
		} 
	}
}