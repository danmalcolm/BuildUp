namespace BuildUp
{
	/// <summary>
	/// Contains context in which objects are being created.
	/// </summary>
	public class CreateContext
	{
		public CreateContext(int index)
		{
			Index = index;
		}

        /// <summary>
        /// The position of the object being created in the sequence
        /// </summary>
		public int Index { get; private set; }

		public CreateContext Next()
		{
			return new CreateContext(Index + 1);
		} 
	}
}