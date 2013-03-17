using System;

namespace BuildUp.Generators
{
    /// <summary>
    /// Creates generators for dates
    /// </summary>
	public static class DateGenerator
	{
		public static IGenerator<DateTime> Incrementing(DateTime start, TimeSpan increment)
		{
			return Generator.Create(index => start + new TimeSpan(increment.Ticks * index));
		}
	}
}