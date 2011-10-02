using System.Linq;
using BuildUp.Tests.Common;
using NUnit.Framework;

namespace BuildUp.Tests
{
	[TestFixture]
	public class SourceCombinatorExtensionsSpecs
	{

		[Test]
		public void simple_select()
		{
			var source1 = new Source<int>(context => context.Index);
			var source2 = source1.Select(x => x*2);

			source2.Take(5).ShouldMatchSequence(0, 2, 4, 6, 8);
		}
	}
}