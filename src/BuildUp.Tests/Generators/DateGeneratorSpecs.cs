using System;
using System.Linq;
using BuildUp.Generators;
using BuildUp.Tests.Common;
using NUnit.Framework;

namespace BuildUp.Tests.Generators
{
	[TestFixture]
	public class DateGeneratorSpecs
	{

		[Test]
		public void sequence_should_parse_values()
		{
		    var generator = DateGenerator.Values("2012-01-05T11:00", "2012-01-05T12:00");
            generator.Take(2).ShouldMatchSequence(new DateTime(2012, 1, 5, 11, 0, 0), new DateTime(2012, 1, 5, 12, 0, 0));
		}

	}
}