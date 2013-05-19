using System;
using System.Linq;

namespace BuildUp.Generators
{
    /// <summary>
    /// Provides access to some useful DateTime value generators
    /// </summary>
	public static class DateGenerator
	{
		public static IGenerator<DateTime> Incrementing(DateTime start, TimeSpan increment)
		{
			return Generator.Create(index => start + new TimeSpan(increment.Ticks * index));
		}

        /// <summary>
        /// Generates a sequence of dates specified as string values. The values are converted
        /// using DateTime.Parse(, converting from values specified using DateTime.Parse
        /// (ISO 8601 format is recommended yyyy-MM-ddTHH:mm:ssZ, e.g. 2012-12-24T04:03:00)
        /// </summary>
        /// <param name="values"> </param>
        /// <returns></returns>
        public static IGenerator<DateTime> Values(params string[] values)
        {
            var dates = values.Select(DateTime.Parse);
            return Generator.Sequence(dates);
        }

	}
}