using System.Collections.Generic;
using System.Linq;

namespace BuildUp.Generators
{
	/// <summary>
	/// Contains convenience extension methods for accessing the values created by a generator
	/// </summary>
	public static class SequenceAccessExtensions
	{
		/// <summary>
		/// Shortcut method that returns the specified number of objects created by the generator, 
		/// equivalent to generator.Create().Take(...)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="generator"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public static IEnumerable<T> Take<T>(this IGenerator<T> generator, int count)
		{
			return generator.Create().Take(count);
		}

		/// <summary>
		/// Shortcut method that returns the first object created by the generator, equivalent to generator.Create().First()
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="generator"></param>
		/// <returns></returns>
		public static T First<T>(this IGenerator<T> generator)
		{
			return generator.Create().First();
		}
	}
}