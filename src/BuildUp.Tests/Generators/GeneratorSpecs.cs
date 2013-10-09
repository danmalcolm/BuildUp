using System.Linq;
using BuildUp.Generators;
using BuildUp.Tests.Common;
using FluentAssertions;
using Xunit;

namespace BuildUp.Tests.Generators
{
	
	public class GeneratorSpecs
	{

		[Fact]
		public void simple_generator()
		{
			var generator = Generator.Create(index => index);
			generator.Take(5).Should().Equal(0,1,2,3,4);
		}

		[Fact]
		public void different_iterations_should_return_identical_sequences()
		{
			var generator = Generator.Create(index => index);
			var first = generator.Take(5);
			var second = generator.Take(5);
			first.Should().Equal(second);
		}

        [Fact]
        public void simple_generator_with_context()
        {
            var generator = Generator.Create((index, context) => index + context, () => 3);
            generator.Take(5).Should().Equal(3, 4, 5, 6, 7);
        }


	}
}