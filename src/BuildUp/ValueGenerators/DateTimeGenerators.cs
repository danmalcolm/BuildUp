using System;

namespace BuildUp.ValueGenerators
{
	public static class DateTimeGenerators
	{
		public static IGenerator<DateTime> Constant(DateTime value)
		{
			return Generators.Create(c => value);
		}

		public static IGenerator<DateTime> IncrementingDays(DateTime start)
		{
			return Generators.Create(c => start.AddDays(c.Index));
		}
	}
}