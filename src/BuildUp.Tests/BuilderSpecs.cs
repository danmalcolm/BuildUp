using System.Linq;
using BuildUp.ValueSources;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests
{
	[TestFixture]
	public class BuilderSpecs
	{

		[Test]
		public void building_with_default_sources()
		{
			var source = new LittleManBuilder();
			var expected = new[]
			{new {Name = "Little Man 1", Age = 38}, new {Name = "Little Man 2", Age = 38}, new {Name = "Little Man 3", Age = 38}};
			source.Take(3).Select(x => new {x.Name, x.Age}).ShouldMatchSequence(expected);
		}

        [Test]
        public void building_after_changing_a_source()
        {
            var source = new LittleManBuilder().WithName(StringSources.Numbered("Super Little Man {0}"));
            var expected = new[] { new { Name = "Super Little Man 1", Age = 38 }, new { Name = "Super Little Man 2", Age = 38 }, new { Name = "Super Little Man 3", Age = 38 } };
            source.Take(3).Select(x => new { x.Name, x.Age }).ShouldMatchSequence(expected);
        }

        [Test]
        public void building_after_changing_both_sources()
        {
            var source = new LittleManBuilder().WithName(StringSources.Numbered("Super Little Man {0}")).WithAge(IntSources.Incrementing(30, 2));
            var expected = new[] { new { Name = "Super Little Man 1", Age = 30 }, new { Name = "Super Little Man 2", Age = 32 }, new { Name = "Super Little Man 3", Age = 34 } };
            source.Take(3).Select(x => new { x.Name, x.Age }).ShouldMatchSequence(expected);
        }

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

		public class LittleManBuilder : Builder<LittleMan,LittleManBuilder>
		{
            protected override ICompositeSource<LittleMan> GetDefaultSource()
            {
                return CompositeSource.Create
                (
                    (context, name, age) => new LittleMan(name, age),
                    StringSources.Numbered("Little Man {0}"),
                    IntSources.Constant(38)
                );
            }

            // The problem with these With* modifying methods is they rely on the index of the
            // sources used by the create function. If the position of two constructor
            // parameters of the same type were changed, then refactoring tools would
            // not change this logic. Possibly need to use expressions and some funky
            // syntax to make this more refactoring friendly, e.g. 

			public LittleManBuilder WithName(ISource<string> name)
			{
				return ChangeChildSource(0, name);
			}

            public LittleManBuilder WithAge(ISource<int> age)
            {
				return ChangeChildSource(1, age);
            }
            
		} 
	}
}