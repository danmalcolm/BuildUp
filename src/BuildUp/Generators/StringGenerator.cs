namespace BuildUp.Generators
{
	public static class StringGenerator
	{
		/// <summary>
		/// Creates a sequence of strings based on a template, with numeric values used for the placeholders in
		/// the template. The {0} placeholder will be replaced by the zero-based index of the value's position 
		/// in the sequence. The {1} placeholder will be replaced by the 1-based index. 
		/// <example>
		/// <para>
		/// StringGenerator.Numbered("customer-{0}"); // gives "customer-0", "customer-1", "customer-2" ...
		/// </para>
		/// </example>
		/// <example>
		/// <para>
		/// StringGenerator.Numbered("customer-{1}"); // gives "customer-1", "customer-2", "customer-3" ...
		/// </para>
		/// </example>
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		public static IGenerator<string> Numbered(string format)
		{
			return Generator.Create(index => string.Format(format, index, index + 1));
		}
	}
}