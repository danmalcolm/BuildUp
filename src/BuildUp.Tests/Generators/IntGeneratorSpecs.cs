using System;
using System.Linq;
using BuildUp.Generators;
using BuildUp.Tests.Common;
using FluentAssertions;
using Xunit;

namespace BuildUp.Tests.Generators.IntGeneratorSpecs
{
	
	public class StepSpecs
	{
		[Fact]
		public void should_create_ascending_sequence()
		{
		    var generator = IntGenerator.Step(1, 3);
            generator.Take(3).Should().Equal(1, 4, 7);
		}

        [Fact]
        public void should_create_descending_sequence()
        {
            var generator = IntGenerator.Step(7, -3);
            generator.Take(3).Should().Equal(7, 4, 1);
        }
	}

    
    public class RandomSpecs
    {
        [Fact]
        public void should_generate_random_sequence_within_range()
        {
            var generator = IntGenerator.Random(1, 1000, 1);
            var values = generator.Take(1000).ToList();

            values.Should().OnlyContain(x => x >= 1 && x <= 1000);
        }

        [Fact]
        public void should_generate_reasonable_distribution_of_values()
        {
            var generator = IntGenerator.Random(1, 1000, 1);
            generator.Take(1000).Distinct().Should().HaveCount(c => c > 600);
        }
    }

    public class RandomStepSpecs
    {
        [Fact]
        public void each_step_should_be_within_range()
        {
            int minStep = 10;
            int maxStep = 30;
            var generator = IntGenerator.RandomStep(1, minStep, maxStep);
            var values = generator.Take(1000).ToList();
            var steps = values.Skip(1).Zip(values, (value, prev) => value - prev);
            steps.Should().OnlyContain(difference => difference >= minStep && difference <= maxStep);
        }
    }
}