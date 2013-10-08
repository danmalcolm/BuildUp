using System;
using System.Collections;
using BuildUp.Generators;
using BuildUp.Tests.Common;
using NUnit.Framework;

namespace BuildUp.Tests.Generators.FriendlyDateStringParserSpecs
{
    public class ParsingSpecs
    {

        [Test]
        public void utc_time_zone_designator_should_give_utc_date()
        {
            DateTime result = FriendlyDateStringParser.Parse("2013-12-31T16:09:02Z");
            result.ShouldEqual(new DateTime(2013, 12, 31, 16, 9, 2, 0));
            result.Kind.ShouldEqual(DateTimeKind.Utc);
        }

        [Test]
        public void no_time_zone_designator_should_give_unspecified_kind()
        {
            DateTime result = FriendlyDateStringParser.Parse("2013-12-31T16:09:02");
            result.ShouldEqual(new DateTime(2013, 12, 31, 16, 9, 2, 0));
            result.Kind.ShouldEqual(DateTimeKind.Unspecified);
        }

        [Test, TestCaseSource("ParseCases")]
        public void should_support_range_of_date_formats_and_precisions(string value, DateTime expected, DateTimeKind expectedKind )
        {
            DateTime result = FriendlyDateStringParser.Parse(value);
            result.ShouldEqual(expected);
            result.Kind.ShouldEqual(expectedKind);
        }

        public static IEnumerable ParseCases()  
        {
            yield return new TestCaseData("20131231T160902345", new DateTime(2013, 12, 31, 16, 9, 2, 345), DateTimeKind.Unspecified);
            yield return new TestCaseData("20131231T160902345Z", new DateTime(2013, 12, 31, 16, 9, 2, 345), DateTimeKind.Utc);
            yield return new TestCaseData("20131231T160902345-07:00", new DateTime(2013, 12, 31, 23, 9, 2, 345), DateTimeKind.Local);

            yield return new TestCaseData("20131231T160902", new DateTime(2013, 12, 31, 16, 9, 2), DateTimeKind.Unspecified);
            yield return new TestCaseData("20131231T160902Z", new DateTime(2013, 12, 31, 16, 9, 2), DateTimeKind.Utc);
            yield return new TestCaseData("20131231T160902-07:00", new DateTime(2013, 12, 31, 23, 9, 2), DateTimeKind.Local);

            yield return new TestCaseData("20131231T1609", new DateTime(2013, 12, 31, 16, 9, 0), DateTimeKind.Unspecified);
            yield return new TestCaseData("20131231T1609Z", new DateTime(2013, 12, 31, 16, 9, 0), DateTimeKind.Utc);
            yield return new TestCaseData("20131231T1609-07:00", new DateTime(2013, 12, 31, 23, 9, 0), DateTimeKind.Local);

            yield return new TestCaseData("20131231", new DateTime(2013, 12, 31, 0, 0, 0), DateTimeKind.Unspecified);
            yield return new TestCaseData("20131231Z", new DateTime(2013, 12, 31, 0, 0, 0), DateTimeKind.Utc);
            yield return new TestCaseData("20131231-07:00", new DateTime(2013, 12, 31, 7, 0, 0), DateTimeKind.Local);


            yield return new TestCaseData("2013-12-31T16:09:02.345", new DateTime(2013, 12, 31, 16, 9, 2, 345), DateTimeKind.Unspecified);
            yield return new TestCaseData("2013-12-31T16:09:02.345Z", new DateTime(2013, 12, 31, 16, 9, 2, 345), DateTimeKind.Utc);
            yield return new TestCaseData("2013-12-31T16:09:02.345-07:00", new DateTime(2013, 12, 31, 23, 9, 2, 345), DateTimeKind.Local);

            yield return new TestCaseData("2013-12-31T16:09:02", new DateTime(2013, 12, 31, 16, 9, 2), DateTimeKind.Unspecified);
            yield return new TestCaseData("2013-12-31T16:09:02Z", new DateTime(2013, 12, 31, 16, 9, 2), DateTimeKind.Utc);
            yield return new TestCaseData("2013-12-31T16:09:02-07:00", new DateTime(2013, 12, 31, 23, 9, 2), DateTimeKind.Local);

            yield return new TestCaseData("2013-12-31T16:09", new DateTime(2013, 12, 31, 16, 9, 0), DateTimeKind.Unspecified);
            yield return new TestCaseData("2013-12-31T16:09Z", new DateTime(2013, 12, 31, 16, 9, 0), DateTimeKind.Utc);
            yield return new TestCaseData("2013-12-31T16:09-07:00", new DateTime(2013, 12, 31, 23, 9, 0), DateTimeKind.Local);

            yield return new TestCaseData("2013-12-31", new DateTime(2013, 12, 31, 0, 0, 0), DateTimeKind.Unspecified);
            yield return new TestCaseData("2013-12-31Z", new DateTime(2013, 12, 31, 0, 0, 0), DateTimeKind.Utc);
            yield return new TestCaseData("2013-12-31-07:00", new DateTime(2013, 12, 31, 7, 0, 0), DateTimeKind.Local);
        }

    }
}