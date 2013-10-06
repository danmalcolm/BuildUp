using System;
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
    public class IncrementingSpecs
    {
        [Test]
        public void should_generate_ascending_sequence()
        {
            var generator = DateGenerator.Incrementing(new DateTime(2000, 1, 1, 0, 0, 0), TimeSpan.FromHours(1));
            generator.Take(3).ShouldMatchSequence(new DateTime(2000, 1, 1, 0, 0, 0), new DateTime(2000, 1, 1, 1, 0, 0), new DateTime(2000, 1, 1, 2, 0, 0));
        }

        [Test]
        public void should_generate_descending_sequence()
        {
            var generator = DateGenerator.Incrementing(new DateTime(2000, 1, 1, 12, 0, 0), TimeSpan.FromHours(-1));
            generator.Take(3).ShouldMatchSequence(new DateTime(2000, 1, 1, 12, 0, 0), new DateTime(2000, 1, 1, 11, 0, 0), new DateTime(2000, 1, 1, 10, 0, 0));
        }

        [Test]
        public void should_generate_ascending_sequence_based_on_easy_to_read_start_date()
        {
            var generator = DateGenerator.Incrementing("2000-01-01T00:00", TimeSpan.FromHours(1));
            generator.Take(3).ShouldMatchSequence(new DateTime(2000, 1, 1, 0, 0, 0), new DateTime(2000, 1, 1, 1, 0, 0), new DateTime(2000, 1, 1, 2, 0, 0));
        }
    }
}