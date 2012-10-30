using System.Collections.Generic;
using System.Linq;

namespace BuildUp
{
	/// <summary>
	/// Contains values made available to a generator's create function. A separate instance is created for
	/// each object within the sequence being generated.
	/// </summary>
	public class CreateContext
	{
		public CreateContext(int index) : this(index, null)
		{
			
		}

		public CreateContext(int index, IEnumerable<object> childGeneratorValues)
		{
			Index = index;
			ChildValues = childGeneratorValues.ToArray();
		}

        /// <summary>
        /// The position of the object being created in the sequence
        /// </summary>
		public int Index { get; private set; }

		/// <summary>
		/// The values from child generators made available to the create function to generate an object
		/// </summary>
		public object[] ChildValues { get; private set; }
	}

}