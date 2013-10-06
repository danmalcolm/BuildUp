using System.Collections;
using System.Collections.Generic;

namespace BuildUp.Generators
{
	/// <summary>
	/// Generates a sequence of values 
	/// </summary>
	public interface IGenerator
	{
        /// <summary>
        /// Generates the sequence of objects
        /// </summary>
        /// <returns></returns>
		IEnumerable Create();
	}

	/// <summary>
	/// Generates a sequence of values 
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface IGenerator<out TObject> : IGenerator
	{
		/// <summary>
        /// Generates the sequence of objects
		/// </summary>
		/// <returns></returns>
		new IEnumerable<TObject> Create();
	}
}