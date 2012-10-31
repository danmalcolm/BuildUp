namespace BuildUp.ValueGenerators
{
	public static class StringGenerators
	{
		/// <summary>
		/// Creates a sequence of strings with numeric values used for the placeholders in a format string. The {0} 
		/// placeholder will be replaced by the zero-based index of the value's position in the sequence. The {1} 
		/// placeholder will be replaced by the one-based index. 
		/// <example>
		/// <para>
		/// StringGenerators.Numbered("customer-{0}"); // gives "customer-0", "customer-1", "customer-2" ...
		/// </para>
		/// </example>
		/// <example>
		/// <para>
		/// StringGenerators.Numbered("customer-{1}"); // gives "customer-1", "customer-2", "customer-3" ...
		/// </para>
		/// </example>
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		public static IGenerator<string> Numbered(string format)
		{
			return Generator.Create(index => string.Format(format, index, index + 1));
		}

		/// <summary>
		/// Creates a sequence repeating the same constant value
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static IGenerator<string> Constant(string value)
		{
			return Generator.Create(c => value);
		}

		
	}
}