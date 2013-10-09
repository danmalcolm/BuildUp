using BuildUp.Generators;
using FluentAssertions;
using Xunit;

namespace BuildUp.Tests.Generators.SequenceAccessExtensions
{
    public class TakeSpecs
    {
        [Fact]
        public void should_return_first_n_elements_requested()
        {
            IntGenerator.Step(1).Take(3).Should().Equal(1, 2, 3);
        }
    }

    public class FirstSpecs
    {
        [Fact]
        public void should_return_first_element_created()
        {
            IntGenerator.Step(1).Take(3).Should().Equal(1, 2, 3);
        }
    }
}