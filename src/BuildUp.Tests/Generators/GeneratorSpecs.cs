using System.Linq;
using BuildUp.Generators;
using BuildUp.Tests.Common;
using NUnit.Framework;

namespace BuildUp.Tests.Generators
{
	[TestFixture]
	public class GeneratorSpecs
	{

		[Test]
		public void simple_generator()
		{
			var generator = Generator.Create(index => index);
			generator.Take(5).ShouldMatchSequence(0,1,2,3,4);
		}

		[Test]
		public void different_iterations_should_return_identical_sequences()
		{
			var generator = Generator.Create(index => index);
			var first = generator.Take(5);
			var second = generator.Take(5);
			first.ShouldMatchSequence(second);
		}

        [Test]
        public void simple_generator_with_context()
        {
            var generator = Generator.Create((index, context) => index + context, () => 3);
            generator.Take(5).ShouldMatchSequence(3, 4, 5, 6, 7);
        }


	}
}