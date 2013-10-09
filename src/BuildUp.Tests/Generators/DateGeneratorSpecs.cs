using System;
using System.Linq;
using BuildUp.Generators;
using BuildUp.Tests.Common;
using FluentAssertions;
using Xunit;

namespace BuildUp.Tests.Generators.DateGeneratorSpecs
{
    
    public class ValuesSpecs
    {
        [Fact]
        public void should_create_dates_from_strings()
        {
            var generator = DateGenerator.Values("2012-01-05T11:00", "2012-01-05T12:00");
            generator.Take(2).Should().Equal(new DateTime(2012, 1, 5, 11, 0, 0), new DateTime(2012, 1, 5, 12, 0, 0));
        }
    }

    
    public class StepSpecs
    {
        [Fact]
        public void should_generate_ascending_sequence()
        {
            var generator = DateGenerator.Step(new DateTime(2000, 1, 1, 0, 0, 0), TimeSpan.FromHours(1));
            generator.Take(3).Should().Equal(new DateTime(2000, 1, 1, 0, 0, 0), new DateTime(2000, 1, 1, 1, 0, 0), new DateTime(2000, 1, 1, 2, 0, 0));
        }

        [Fact]
        public void should_generate_descending_sequence()
        {
            var generator = DateGenerator.Step(new DateTime(2000, 1, 1, 12, 0, 0), TimeSpan.FromHours(-1));
            generator.Take(3).Should().Equal(new DateTime(2000, 1, 1, 12, 0, 0), new DateTime(2000, 1, 1, 11, 0, 0), new DateTime(2000, 1, 1, 10, 0, 0));
        }

        [Fact]
        public void should_generate_sequence_based_on_friendly_start_date_string()
        {
            var generator = DateGenerator.Step("2000-01-01T00:00", TimeSpan.FromHours(1));
            generator.Take(3).Should().Equal(new DateTime(2000, 1, 1, 0, 0, 0), new DateTime(2000, 1, 1, 1, 0, 0), new DateTime(2000, 1, 1, 2, 0, 0));
        }
    }

    
    public class RandomStepSpecs
    {
        [Fact]
        public void each_step_should_be_within_range()
        {
            var minStep = TimeSpan.FromMinutes(10);
            var maxStep = TimeSpan.FromMinutes(30);
            var generator = DateGenerator.RandomStep(new DateTime(2000, 1, 1, 0, 0, 0), minStep, maxStep);
            var values = generator.Take(1000).ToList();
            var steps = values.Skip(1).Zip(values, (value, prev) => value - prev);
            steps.Should().OnlyContain(difference => difference >= minStep && difference <= maxStep);
        }

        [Fact]
        public void should_produce_same_result_with_date_string()
        {
            var generator1 = DateGenerator.RandomStep(new DateTime(2000, 1, 1, 0, 0, 0), TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(30));
            var generator2 = DateGenerator.RandomStep("2000-01-01", TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(30));
            generator1.Take(100).Should().Equal(generator2.Take(100));
        }
    }

    public class RandomDateSpecs
    {
        [Fact]
        public void should_generate_dates_within_range()
        {
            DateTime min = new DateTime(2012, 1, 1);
            DateTime max = new DateTime(2013, 12, 31);
            var generator = DateGenerator.Random(min, max, 1);
            var values = generator.Take(1000).ToList();

            values.Should().OnlyContain(x => x >= min && x <= max);
        }

        [Fact]
        public void should_generate_reasonable_distribution_of_dates()
        {
            DateTime min = new DateTime(2012, 1, 1);
            DateTime max = new DateTime(2013, 12, 31);
            var generator = DateGenerator.Random(min, max, 1);
            generator.Take(1000).Distinct().Should().HaveCount(x => x > 600);
        }

        [Fact]
        public void should_trim_dates_to_specified_precision()
        {
            DateTime min = new DateTime(2012, 1, 1);
            DateTime max = new DateTime(2013, 12, 31);
            var precision = DateTimePrecision.Minute;
            var generator = DateGenerator.Random(min, max, 1, precision);
            generator.Take(100).Should().NotContain(x => x != DateTrimmer.ToPrecision(x, precision));
        }
    }
}