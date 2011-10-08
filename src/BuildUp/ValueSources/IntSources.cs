using System.Collections.Generic;

namespace BuildUp.ValueSources
{
	public static class IntSources
	{
		public static IEnumerable<int> Incrementing(int start, int increment=1)
		{
			return Source.Create(c => start + c.Index * increment); 
		}

		public static IEnumerable<int> Constant(int value)
		{
			return Source.Create(c => value);
		}
	}
}