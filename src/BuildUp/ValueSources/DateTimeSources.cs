using System;
using System.Collections.Generic;

namespace BuildUp.ValueSources
{
	public static class DateTimeSources
	{
		public static IEnumerable<DateTime> Constant(DateTime value)
		{
			return Source.Create(c => value);
		}

		public static IEnumerable<DateTime> IncrementingDays(DateTime start)
		{
			return Source.Create(c => start.AddDays(c.Index));
		}
	}
}