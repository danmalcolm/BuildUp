using System.Linq;
using BuildUp.Tests.Common;
using NUnit.Framework;

namespace BuildUp.Tests
{
	[TestFixture]
	public class SourceExtensionsSpecs
	{

		[Test]
		public void simple_select()
		{
			var source1 = new Source<int>(context => context.Index);
			var source2 = source1.Select(x => x*2);

			source2.Take(5).ShouldMatchSequence(0, 2, 4, 6, 8);
		}

		[Test]
		public void simple_freeze()
		{
			var source1 = new Source<int>(context => context.Index);
			var source2 = source1.Freeze();

			source1.Take(5).ShouldMatchSequence(0, 1, 2, 3, 4);
			source2.Take(5).ShouldMatchSequence(0, 0, 0, 0, 0);
		}

		[Test]
		public void repeat_each()
		{
			var source1 = new Source<int>(context => context.Index);
			var source2 = source1.RepeatEach(3);

			source1.Take(3).ShouldMatchSequence(0, 1, 2);
			source2.Take(9).ShouldMatchSequence(0, 0, 0, 1, 1, 1, 2, 2, 2);
		}

		
	}
}