using System;

namespace BuildUp.ValueSources
{
	public static class DateTimeSources
	{
		public static Source<DateTime> Constant(DateTime value)
		{
			return Source.Create(c => value);
		}

		public static Source<DateTime> IncrementingDays(DateTime start)
		{
			return Source.Create(c => start.AddDays(c.Index));
		}
	}
}