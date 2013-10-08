using System;
using System.Collections;
using BuildUp.Generators;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests.Generators.DateTimeTrimmerSpecs
{
    public class TrimSpecs
    {
        
        [Test, TestCaseSource("TrimTestCases")]
        public void should_trim_to_specified_level(DateTimePrecision precision, string expected)
        {
            DateTime original = FriendlyDateStringParser.Parse("2010-08-20T12:34:56.345");
            DateTime expectedDate = FriendlyDateStringParser.Parse(expected);
            DateTime result = DateTrimmer.ToPrecision(original, precision);
            result.ShouldEqual(expectedDate);
        }

        static IEnumerable TrimTestCases()
        {
            yield return new TestCaseData(DateTimePrecision.Millisecond, "2010-08-20T12:34:56.345");
            yield return new TestCaseData(DateTimePrecision.Second, "2010-08-20T12:34:56.000");
            yield return new TestCaseData(DateTimePrecision.Minute, "2010-08-20T12:34:00.000");
            yield return new TestCaseData(DateTimePrecision.Hour, "2010-08-20T12:00:00.000");
            yield return new TestCaseData(DateTimePrecision.Day, "2010-08-20T00:00:00.000");
            yield return new TestCaseData(DateTimePrecision.Month, "2010-08-01T00:00:00.000");
            yield return new TestCaseData(DateTimePrecision.Year, "2010-01-01T00:00:00.000");
        }

        [Test]
        public void should_preserve_original_DateTimeKind()
        {
            DateTime local = new DateTime(2010, 12, 31, 1, 2, 3, 345, DateTimeKind.Local);
            DateTrimmer.ToPrecision(local, DateTimePrecision.Minute).Kind.ShouldEqual(DateTimeKind.Local);

            DateTime utc = new DateTime(2010, 12, 31, 1, 2, 3, 345, DateTimeKind.Utc);
            DateTrimmer.ToPrecision(utc, DateTimePrecision.Minute).Kind.ShouldEqual(DateTimeKind.Utc);
        }
    }
}