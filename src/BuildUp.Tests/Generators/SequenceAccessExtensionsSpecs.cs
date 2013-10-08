using BuildUp.Generators;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests.Generators.SequenceAccessExtensions
{
    public class TakeSpecs
    {
        [Test]
        public void should_return_first_n_elements_requested()
        {
            IntGenerator.Step(1).Take(3).ShouldMatchSequence(1, 2, 3);
        }
    }

    public class FirstSpecs
    {
        [Test]
        public void should_return_first_element_created()
        {
            IntGenerator.Step(1).Take(3).ShouldMatchSequence(1, 2, 3);
        }
    }
}