using System;
using System.Linq;
using BuildUp.Generators;
using BuildUp.Tests.Common;
using NUnit.Framework;

namespace BuildUp.Tests.Generators.DateGeneratorSpecs
{
    [TestFixture]
    public class ValuesSpecs
    {
        [Test]
        public void should_create_dates_from_strings()
        {
            var generator = DateGenerator.Values("2012-01-05T11:00", "2012-01-05T12:00");
            generator.Take(2).ShouldMatchSequence(new DateTime(2012, 1, 5, 11, 0, 0), new DateTime(2012, 1, 5, 12, 0, 0));
        }
    }

    [TestFixture]
    public class StepSpecs
    {
        [Test]
        public void should_generate_ascending_sequence()
        {
            var generator = DateGenerator.Step(new DateTime(2000, 1, 1, 0, 0, 0), TimeSpan.FromHours(1));
            generator.Take(3).ShouldMatchSequence(new DateTime(2000, 1, 1, 0, 0, 0), new DateTime(2000, 1, 1, 1, 0, 0), new DateTime(2000, 1, 1, 2, 0, 0));
        }

        [Test]
        public void should_generate_descending_sequence()
        {
            var generator = DateGenerator.Step(new DateTime(2000, 1, 1, 12, 0, 0), TimeSpan.FromHours(-1));
            generator.Take(3).ShouldMatchSequence(new DateTime(2000, 1, 1, 12, 0, 0), new DateTime(2000, 1, 1, 11, 0, 0), new DateTime(2000, 1, 1, 10, 0, 0));
        }

        [Test]
        public void should_generate_sequence_based_on_friendly_start_date_string()
        {
            var generator = DateGenerator.Step("2000-01-01T00:00", TimeSpan.FromHours(1));
            generator.Take(3).ShouldMatchSequence(new DateTime(2000, 1, 1, 0, 0, 0), new DateTime(2000, 1, 1, 1, 0, 0), new DateTime(2000, 1, 1, 2, 0, 0));
        }
    }

    [TestFixture]
    public class RandomStepSpecs
    {
        [Test]
        public void each_step_should_be_within_range()
        {
            var generator = DateGenerator.RandomStep(new DateTime(2000, 1, 1, 0, 0, 0), TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(30));
            var values = generator.Take(100).ToList();
        }
    }

    public class RandomDateSpecs
    {
        [Test]
        public void should_generate_dates_within_range()
        {
            DateTime min = new DateTime(2012, 1, 1);
            DateTime max = new DateTime(2013, 12, 31);
            var generator = DateGenerator.Random(min, max, 1);
            var values = generator.Take(1000).ToList();

            values.Any(x => x < min).ShouldBeFalse();
            values.Any(x => x > max).ShouldBeFalse();
        }

        [Test]
        public void should_generate_reasonable_distribution_of_dates()
        {
            DateTime min = new DateTime(2012, 1, 1);
            DateTime max = new DateTime(2013, 12, 31);
            var generator = DateGenerator.Random(min, max, 1);
            generator.Take(1000).Distinct().Count().ShouldBeGreaterThan(600);
        }

        [Test]
        public void should_trim_dates_to_specified_precision()
        {
            DateTime min = new DateTime(2012, 1, 1);
            DateTime max = new DateTime(2013, 12, 31);
            var precision = DateTimePrecision.Minute;
            var generator = DateGenerator.Random(min, max, 1, precision);
            generator.Take(100).Any(x => x != DateTrimmer.ToPrecision(x, precision)).ShouldBeFalse();
        }
    }
}