using System;
using System.Linq;
using BuildUp.Generators;
using BuildUp.Tests.Common;
using NUnit.Framework;

namespace BuildUp.Tests.Generators.IntGeneratorSpecs
{
	[TestFixture]
	public class IncrementingSpecs
	{
		[Test]
		public void should_create_ascending_sequence()
		{
		    var generator = IntGenerator.Incrementing(1, 3);
            generator.Take(3).ShouldMatchSequence(1, 4, 7);
		}
	}

    [TestFixture]
    public class RandomSpecs
    {
        [Test]
        public void should_generate_random_sequence_within_range()
        {
            var generator = IntGenerator.Random(1, 1000, 1);
            var values = generator.Take(1000).ToList();

            values.Any(x => x < 1).ShouldBeFalse();
            values.Any(x => x > 1000).ShouldBeFalse();
        }

        [Test]
        public void should_generate_reasonable_distribution_of_numbers()
        {
            var generator = IntGenerator.Random(1, 1000, 1);
            generator.Take(1000).Distinct().Count().ShouldBeGreaterThan(600);
        }

      
    }
}