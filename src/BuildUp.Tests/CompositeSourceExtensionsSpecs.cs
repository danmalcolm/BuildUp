using System.Linq;
using BuildUp.Tests.Common;
using BuildUp.ValueSources;
using NUnit.Framework;

namespace BuildUp.Tests
{
	[TestFixture]
	public class CompositeSourceExtensionsSpecs
	{
		public class LittleMan
		{
			public LittleMan(string name, int age)
			{
				Name = name;
				Age = age;
			}

			public string Name { get; private set; }

			public int Age { get; private set; }
		}

		[Test]
		public void replacing_child_source_by_position()
		{
			var source1 = CompositeSource.Create
				(
					(context, name, age) => new LittleMan(name, age),
					StringSources.Numbered("Little Man {0}"),
					IntSources.Constant(38)
				);
			var source2 = source1.ReplaceChildSource(1, IntSources.Incrementing(44));

			source1.Take(3).Select(x => x.Age).ShouldMatchSequence(38, 38, 38);
			source2.Take(3).Select(x => x.Age).ShouldMatchSequence(44, 45, 46);
		}
	}
}