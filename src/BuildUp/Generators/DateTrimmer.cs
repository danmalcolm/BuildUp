using System;
using System.Collections.Generic;

namespace BuildUp.Generators
{
    /// <summary>
    /// Trims DateTime values to a given precision
    /// </summary>
    public class DateTrimmer
    {
        private static readonly Dictionary<DateTimePrecision, Func<DateTime, DateTime>> TrimFuncs;

        static DateTrimmer()
        {
            TrimFuncs = new Dictionary<DateTimePrecision, Func<DateTime, DateTime>>
            {
                { DateTimePrecision.Year, 
                    date => new DateTime(date.Year, 1, 1, 0, 0, 0, 0, date.Kind)},
                { DateTimePrecision.Month, 
                    date => new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0, date.Kind)},
                { DateTimePrecision.Day, 
                    date => new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Kind)},
                { DateTimePrecision.Hour, 
                    date => new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0, date.Kind)},
                { DateTimePrecision.Minute, 
                    date => new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, 0, date.Kind) },
                { DateTimePrecision.Second, 
                    date => new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0, date.Kind) },
                { DateTimePrecision.Millisecond, 
                    date => new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond, date.Kind) },
            };
        }

        /// <summary>
        /// Trims a date, zeroing any values below the specified precision
        /// </summary>
        /// <param name="value"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static DateTime ToPrecision(DateTime value, DateTimePrecision precision)
        {
            return TrimFuncs[precision](value);
        }
   }
}
