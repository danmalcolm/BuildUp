using System;
using System.Linq;
using BuildUp.Utility;

namespace BuildUp.Generators
{
    /// <summary>
    /// Provides access to some useful DateTime value generators
    /// </summary>
	public static class DateGenerator
	{
		/// <summary>
		/// Generates a sequence of dates beginning with a start date and incrementing by the specified
		/// amount for each subsequent value
		/// </summary>
		/// <param name="start"></param>
		/// <param name="increment"></param>
		/// <returns></returns>
		public static IGenerator<DateTime> Step(DateTime start, TimeSpan increment)
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
        public static IGenerator<DateTime> Step(string start, TimeSpan increment)
        {
            var startDate = DateTime.Parse(start);
            return Step(startDate, increment);
        }

        /// <summary>
        /// Generates a sequence of dates beginning with a start date and incrementing by a random value
        /// within the specified range
        /// </summary>
        /// <param name="start"></param>
        /// <param name="minStep"> </param>
        /// <param name="maxStep"> </param>
        /// <returns></returns>
        public static IGenerator<DateTime> RandomStep(DateTime start, int seed, TimeSpan minStep, TimeSpan maxStep)
        {
            return Generator.Create((context, index) =>
            {
                var next = context.Last.Add(TimeSpan.FromTicks(context.Random.NextLong(minStep.Ticks, maxStep.Ticks)));
                context.Last = next;
                return next;
            }, () => new RandomStepContext { Random = new Random(seed), Last = start });
        }

        private class RandomStepContext
        {
            public Random Random { get; set; }
            public DateTime Last { get; set; }
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

        /// <summary>
        /// Generates a random sequence of DateTime values within a given range. The sequence is 
        /// deterministic, i.e., using the same seed will result in the same sequence.
        /// </summary>
        /// <param name="min">The inclusive lower bound of the range</param>
        /// <param name="max">The inclusive upper bound of the range</param>
        /// <param name="seed">Value used to seed the generated Dates</param>
        /// <returns></returns>
        public static IGenerator<DateTime> Random(DateTime min, DateTime max, int seed, DateTimePrecision precision = DateTimePrecision.Millisecond)
        {
            if(max < min)
                throw new ArgumentException("Should be greater than the min date", "max");
            var timeSpan = max - min;
return Generator.Create((random, index) =>
            {
                long ticks = random.NextLong(0, timeSpan.Ticks);
                DateTime dateTime = min.AddTicks(ticks);
                return DateTrimmer.ToPrecision(dateTime, precision);
            }, () => new Random(seed));
        }

	}
}