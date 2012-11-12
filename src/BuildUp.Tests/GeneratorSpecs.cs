using System.Linq;
using BuildUp.ValueGenerators;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests
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
		public void modifying_sequence_should_be_applied_to_generated_objects()
		{
			var generator = StringGenerators.Numbered("{1}").ModifySequence(x => x.Skip(2));
			generator.Take(3).ShouldMatchSequence("3", "4", "5");
		}

	}
}