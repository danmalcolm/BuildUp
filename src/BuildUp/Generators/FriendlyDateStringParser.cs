using System;
using System.Globalization;

namespace BuildUp.Generators
{
    /// <summary>
    /// 
    /// </summary>
    public static class FriendlyDateStringParser
    {
        private static readonly string[] Formats =
            {
                // ISO8601 formats of varying precision (local and offset)
                "yyyyMMddTHHmmssfffzzz",
                "yyyyMMddTHHmmssfffZ",
                "yyyyMMddTHHmmssfff",
                
                "yyyyMMddTHHmmsszzz",
                "yyyyMMddTHHmmssZ",
                "yyyyMMddTHHmmss",
                
                "yyyyMMddTHHmmzzz",
                "yyyyMMddTHHmmZ",
                "yyyyMMddTHHmm",
                
                "yyyyMMddzzz",
                "yyyyMMddZ",
                "yyyyMMdd",
                
                // extended format (using separators)
                "yyyy-MM-ddTHH:mm:ss.fffZ",
                "yyyy-MM-ddTHH:mm:ss.fffzzz",
                "yyyy-MM-ddTHH:mm:ss.fff",

                "yyyy-MM-ddTHH:mm:ssZ",
                "yyyy-MM-ddTHH:mm:sszzz",
                "yyyy-MM-ddTHH:mm:ss",
                
                "yyyy-MM-ddTHH:mmzzz",
                "yyyy-MM-ddTHH:mmZ",
                "yyyy-MM-ddTHH:mm",
                
                "yyyy-MM-ddzzz",
                "yyyy-MM-ddZ",
                "yyyy-MM-dd",
            };

        private const string InvalidFormatMessageTemplate =
            @"The value {0} is not a supported ""friendly"" DateTime format. Support formats are:

{1}
";

        /// <summary>
        /// Creates a DateTime from a range of date formats
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime Parse(string value)
        {
            if(value.EndsWith("Z"))
                return ParseAsUtc(value);

            DateTime dateTime;
            if(!DateTime.TryParseExact(value, Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                string message = string.Format(InvalidFormatMessageTemplate, value,
                                               string.Join(Environment.NewLine, Formats));
                throw new ArgumentException(message, value);                
            }
            return dateTime;
        }

        private static DateTime ParseAsUtc(string value)
        {
            // Omit the timezone designator so that parsing results in a DateTime with DateTimeKind.Unspecified
            string valueWithoutZ = value.Substring(0, value.Length - 1);
            DateTime dateTime;
            if (!DateTime.TryParseExact(valueWithoutZ, Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                string message = string.Format(InvalidFormatMessageTemplate, value,
                                               string.Join(Environment.NewLine, Formats));
                throw new ArgumentException(message, "value");
            }
            // Convert to UTC date
            var utcDateTime = new DateTime(dateTime.Ticks, DateTimeKind.Utc);
            return utcDateTime;
        }
    }
}