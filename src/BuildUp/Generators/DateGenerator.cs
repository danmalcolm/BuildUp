using System;
using System.Linq;

namespace BuildUp.Generators
{
    /// <summary>
    /// Provides access to some useful DateTime value generators
    /// </summary>
	public static class DateGenerator
	{
        // TODO - better name?

		/// <summary>
		/// Generates a sequence of dates beginning with a start date and incrementing by the specified
		/// amount for each subsequent value
		/// </summary>
		/// <param name="start"></param>
		/// <param name="increment"></param>
		/// <returns></returns>
		public static IGenerator<DateTime> Incrementing(DateTime start, TimeSpan increment)
		{
			return Generator.Create(index => start + new TimeSpan(increment.Ticks * index));
		}

        /// <summary>
        /// Generates a sequence of dates beginning with a start date and incrementing by the specified
        /// amount for each subsequent value. The start date is specified as a string that will be converted
        /// to a date using DateTime.Parse. The ISO 8601 format (yyyy-MM-ddTHH:mm:ssZ) is recommended,
        /// e.g. 2012-12-31T04:03:00)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        public static IGenerator<DateTime> Incrementing(string start, TimeSpan increment)
        {
            var startDate = DateTime.Parse(start);
            return Incrementing(startDate, increment);
        }

        /// <summary>
        /// Generates a sequence of dates specified as string values. The values are converted
        /// using DateTime.Parse. The ISO 8601 format (yyyy-MM-ddTHH:mm:ssZ) is recommended,
        /// e.g. 2012-12-31T04:03:00)
        /// </summary>
        /// <param name="values"> </param>
        /// <returns></returns>
        public static IGenerator<DateTime> Values(params string[] values)
        {
            var dates = values.Select(DateTime.Parse);
            return Generator.Values(dates);
        }

	}
}