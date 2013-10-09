using System;
using System.Collections;
using System.Collections.Generic;
using BuildUp.Generators;
using BuildUp.Tests.Common;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace BuildUp.Tests.Generators.FriendlyDateStringParserSpecs
{
    public class ParsingSpecs
    {

        [Fact]
        public void utc_time_zone_designator_should_give_utc_date()
        {
            DateTime result = FriendlyDateStringParser.Parse("2013-12-31T16:09:02Z");
            result.Should().Be(new DateTime(2013, 12, 31, 16, 9, 2, 0));
            result.Kind.Should().Be(DateTimeKind.Utc);
        }

        [Fact]
        public void no_time_zone_designator_should_give_unspecified_kind()
        {
            DateTime result = FriendlyDateStringParser.Parse("2013-12-31T16:09:02");
            result.Should().Be(new DateTime(2013, 12, 31, 16, 9, 2, 0));
            result.Kind.Should().Be(DateTimeKind.Unspecified);
        }

        [Theory, PropertyData("ParseCases")]
        public void should_support_range_of_date_formats_and_precisions(string value, DateTime expected, DateTimeKind expectedKind )
        {
            DateTime result = FriendlyDateStringParser.Parse(value);
            result.Should().Be(expected);
            result.Kind.Should().Be(expectedKind);
        }

        public static IEnumerable<object[]> ParseCases
        {
            get
            {
                yield return
                    new object[]
                    {"20131231T160902345", new DateTime(2013, 12, 31, 16, 9, 2, 345), DateTimeKind.Unspecified};
                yield return
                    new object[] {"20131231T160902345Z", new DateTime(2013, 12, 31, 16, 9, 2, 345), DateTimeKind.Utc};
                yield return
                    new object[]
                    {"20131231T160902345-07:00", new DateTime(2013, 12, 31, 23, 9, 2, 345), DateTimeKind.Local};

                yield return
                    new object[] {"20131231T160902", new DateTime(2013, 12, 31, 16, 9, 2), DateTimeKind.Unspecified};
                yield return new object[] {"20131231T160902Z", new DateTime(2013, 12, 31, 16, 9, 2), DateTimeKind.Utc};
                yield return
                    new object[] {"20131231T160902-07:00", new DateTime(2013, 12, 31, 23, 9, 2), DateTimeKind.Local};

                yield return
                    new object[] {"20131231T1609", new DateTime(2013, 12, 31, 16, 9, 0), DateTimeKind.Unspecified};
                yield return new object[] {"20131231T1609Z", new DateTime(2013, 12, 31, 16, 9, 0), DateTimeKind.Utc};
                yield return
                    new object[] {"20131231T1609-07:00", new DateTime(2013, 12, 31, 23, 9, 0), DateTimeKind.Local};

                yield return new object[] {"20131231", new DateTime(2013, 12, 31, 0, 0, 0), DateTimeKind.Unspecified};
                yield return new object[] {"20131231Z", new DateTime(2013, 12, 31, 0, 0, 0), DateTimeKind.Utc};
                yield return new object[] {"20131231-07:00", new DateTime(2013, 12, 31, 7, 0, 0), DateTimeKind.Local};


                yield return
                    new object[]
                    {"2013-12-31T16:09:02.345", new DateTime(2013, 12, 31, 16, 9, 2, 345), DateTimeKind.Unspecified};
                yield return
                    new object[]
                    {"2013-12-31T16:09:02.345Z", new DateTime(2013, 12, 31, 16, 9, 2, 345), DateTimeKind.Utc};
                yield return
                    new object[]
                    {"2013-12-31T16:09:02.345-07:00", new DateTime(2013, 12, 31, 23, 9, 2, 345), DateTimeKind.Local};

                yield return
                    new object[] {"2013-12-31T16:09:02", new DateTime(2013, 12, 31, 16, 9, 2), DateTimeKind.Unspecified}
                    ;
                yield return
                    new object[] {"2013-12-31T16:09:02Z", new DateTime(2013, 12, 31, 16, 9, 2), DateTimeKind.Utc};
                yield return
                    new object[] {"2013-12-31T16:09:02-07:00", new DateTime(2013, 12, 31, 23, 9, 2), DateTimeKind.Local}
                    ;

                yield return
                    new object[] {"2013-12-31T16:09", new DateTime(2013, 12, 31, 16, 9, 0), DateTimeKind.Unspecified};
                yield return new object[] {"2013-12-31T16:09Z", new DateTime(2013, 12, 31, 16, 9, 0), DateTimeKind.Utc};
                yield return
                    new object[] {"2013-12-31T16:09-07:00", new DateTime(2013, 12, 31, 23, 9, 0), DateTimeKind.Local};

                yield return new object[] {"2013-12-31", new DateTime(2013, 12, 31, 0, 0, 0), DateTimeKind.Unspecified};
                yield return new object[] {"2013-12-31Z", new DateTime(2013, 12, 31, 0, 0, 0), DateTimeKind.Utc};
                yield return new object[] {"2013-12-31-07:00", new DateTime(2013, 12, 31, 7, 0, 0), DateTimeKind.Local};
            }
        }
    }
}