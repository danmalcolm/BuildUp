using System.Collections.Generic;

namespace BuildUp.ValueSources
{
	public static class StringSources
	{
		/// <summary>
		/// Creates a sequence of strings with numeric values used for the placeholders in a format string. The {0} 
		/// placeholder will be replaced by the zero-based index of the value's position in the sequences. The {1} 
		/// placeholder will be replaced by the one-based index. supplied format string the one-based indexed of the position of the sequence
		/// to the {0} placeholder in a format string. Generates more human friendly sequences, e.g. customer-1, customer-2
		/// <example>
		/// <para>
		/// StringSources.Numbered("customer-{0}"); // gives "customer-0", "customer-1", "customer-2" ...
		/// </para>
		/// </example>
		/// <example>
		/// <para>
		/// StringSources.Numbered("customer-{1}"); // gives "customer-1", "customer-2", "customer-3" ...
		/// </para>
		/// </example>
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		public static ISource<string> Numbered(string format)
		{
			return Source.Create(c => string.Format(format, c.Index, c.Index + 1));
		}

		/// <summary>
		/// Creates a sequence repeating the same constant value
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static ISource<string> Constant(string value)
		{
			return Source.Create(c => value);
		}

		
	}
}