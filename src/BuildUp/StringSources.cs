namespace BuildUp
{
	public static class StringSources
	{
		public static Source<string> FormatWithIndex(string format)
		{
			return Source.Create(c => string.Format(format, c.Index));
		}

		public static Source<string> Numbered(string format)
		{
			return Source.Create(c => string.Format(format, c.Index + 1));
		}

		public static Source<string> Constant(string value)
		{
			return Source.Create(c => value);
		}

		
	}
}