using System.Linq;
using BuildUp.Tests.Common;
using BuildUp.ValueSources;
using NUnit.Framework;

namespace BuildUp.Tests
{
	[TestFixture]
	public class SourceExtensionsSpecs
	{
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

		[Test]
		public void repeat_each_with_composite_source_should_repeat_same_instance()
		{
			var source = CompositeSource.Create
				(
					(context, name, age) => new LittleMan(name, age),
					StringSources.Numbered("Little Man {0}"),
					IntSources.Constant(38)
				).RepeatEach(2);
			source.Take(4).Distinct().Count().ShouldEqual(2);

		}

		
	}
}