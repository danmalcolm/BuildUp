﻿using System.Collections.Generic;
using System.Linq;

namespace BuildUp
{
	/// <summary>
	/// Contains values made available to an ISource's object creation function
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
		/// The values from base sources that are used to create objects
		/// </summary>
		public object[] ChildSourceValues { get; private set; }
	}

}