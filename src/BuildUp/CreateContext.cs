using System.Collections.Generic;

namespace BuildUp
{
	/// <summary>
	/// Contains context in which objects are being created.
	/// </summary>
	public class CreateContext
	{

		public static IEnumerable<CreateContext> Sequence
		{
			get
			{
				var context = new CreateContext(0);
				while(true)
				{
					yield return context;
					context = context.Next();
				}
			}
		}

		public CreateContext(int index)
		{
			Index = index;
		}

        /// <summary>
        /// The position of the object being created in the sequence
        /// </summary>
		public int Index { get; private set; }

		public CreateContext Next()
		{
			return new CreateContext(Index + 1);
		} 
	}


}