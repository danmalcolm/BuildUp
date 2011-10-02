using System.Linq;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests
{
	[TestFixture]
	public class SourceSpecs
	{

		[Test]
		public void simple_source()
		{
			var source = new Source<int>(context => context.Index);
			source.Take(5).ShouldMatchSequence(0,1,2,3,4);
		}

		[Test]
		public void different_iterations_should_return_separate_sequences()
		{
			var source = new Source<int>(context => context.Index);
			var first = source.Take(5);
			var second = source.Take(5);
			first.ShouldMatchSequence(second);
		}
		 
	}
}