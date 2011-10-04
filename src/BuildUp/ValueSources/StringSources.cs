namespace BuildUp.ValueSources
{
	public static class StringSources
	{
		/// <summary>
		/// Creates a sequence of strings using the zero-based indexed of the position of the sequence
		/// to the {0} placeholder in a format string
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		public static Source<string> Indexed(string format)
		{
			return Source.Create(c => string.Format(format, c.Index));
		}

		/// <summary>
		/// Creates a sequence of strings using the one-based indexed of the position of the sequence
		/// to the {0} placeholder in a format string. Generates more human friendly sequences, e.g. customer-1, customer-2
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		public static Source<string> Numbered(string format)
		{
			return Source.Create(c => string.Format(format, c.Index + 1));
		}

		/// <summary>
		/// Creates a sequence of the same value
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Source<string> Constant(string value)
		{
			return Source.Create(c => value);
		}

		
	}
}