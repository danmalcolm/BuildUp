using System;

namespace BuildUp.ValueGenerators
{
	public static class DateTimeGenerators
	{
		public static IGenerator<DateTime> IncrementingDays(DateTime start)
		{
			return Generator.Create(index => start.AddDays(index));
		}
	}
}