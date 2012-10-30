using System.Linq;
using BuildUp.Tests.Common;
using BuildUp.ValueSources;
using NUnit.Framework;

namespace BuildUp.Tests
{
	[TestFixture]
	public class EnumerableExtensionsSpecs
	{
		[Test]
		public void simple_freeze()
		{
			var source1 = Source.Create(context => context.Index);
			var source2 = source1.Freeze();

			source1.Take(5).ShouldMatchSequence(0, 1, 2, 3, 4);
			source2.Take(5).ShouldMatchSequence(0, 0, 0, 0, 0);
		}

		[Test]
		public void loop()
		{
			var source1 = Source.Create(context => context.Index);
			var source2 = source1.Loop(3);

			source1.Take(9).ShouldMatchSequence(0, 1, 2, 3, 4, 5, 6, 7, 8);
			source2.Take(9).ShouldMatchSequence(0, 1, 2, 0, 1, 2, 0, 1, 2);
		}

		[Test]
		public void looping_source_of_complex_objects_should_repeat_same_instances()
		{
			var source = Source.Create
				(
					(context, name, age) => new LittleMan(name, age),
					StringSources.Numbered("Little Man {1}"),
					IntSources.Constant(38)
				).Loop(2);
			source.Take(4).Distinct().Count().ShouldEqual(2);

		}

		
	}
}