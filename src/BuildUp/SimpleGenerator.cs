using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BuildUp
{
	/// <summary>
	/// Uses a function to generate a sequence of objects
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public class SimpleGenerator<TObject> : IGenerator<TObject>
	{
		private readonly Func<int, TObject> create;

		public SimpleGenerator(Func<int, TObject> create)
		{
			this.create = create;
		}
		
		public IEnumerable<TObject> Build()
		{
			int index = 0;
			while(true)
			{
				yield return this.create(index++);
			}
		}

		IEnumerable IGenerator.Build()
		{
			return Build();
		}
	}
}