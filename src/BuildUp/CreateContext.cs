using System.Collections.Generic;
using System.Linq;

namespace BuildUp
{
	/// <summary>
	/// Contains values made available to the function used by a source to generate objects
	/// </summary>
	public class CreateContext
	{
		public CreateContext(int index) : this(index, null)
		{
			
		}

		public CreateContext(int index, IEnumerable<object> values)
		{
			Index = index;
			ChildSourceValues = values.ToArray();
		}

        /// <summary>
        /// The position of the object being created in the sequence
        /// </summary>
		public int Index { get; private set; }

		/// <summary>
		/// The values from child sources that provide values used to create the object
		/// </summary>
		public object[] ChildSourceValues { get; private set; }
	}

}