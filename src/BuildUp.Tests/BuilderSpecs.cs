using System.Collections.Generic;
using System.Linq;
using BuildUp.Builders;
using BuildUp.ValueGenerators;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests
{
	[TestFixture]
	public class BuilderSpecs
	{

		[Test]
		public void building_with_default_generators()
		{
			var generator = new LittleManBuilder();
			var expected = new[]
			{new {Name = "Little Man 1", Age = 38}, new {Name = "Little Man 2", Age = 38}, new {Name = "Little Man 3", Age = 38}};
			generator.Take(3).Select(x => new {x.Name, x.Age}).ShouldMatchSequence(expected);
		}

        [Test]
        public void building_after_changing_a_child_generator()
        {
            var generator = new LittleManBuilder().WithName(StringGenerators.Numbered("Super Little Man {1}"));
            var expected = new[] { new { Name = "Super Little Man 1", Age = 38 }, new { Name = "Super Little Man 2", Age = 38 }, new { Name = "Super Little Man 3", Age = 38 } };
            generator.Take(3).Select(x => new { x.Name, x.Age }).ShouldMatchSequence(expected);
        }

        [Test]
        public void building_after_changing_both_child_generators()
        {
            var generator = new LittleManBuilder().WithName(StringGenerators.Numbered("Super Little Man {1}")).WithAge(IntGenerators.Incrementing(30, 2));
            var expected = new[] { new { Name = "Super Little Man 1", Age = 30 }, new { Name = "Super Little Man 2", Age = 32 }, new { Name = "Super Little Man 3", Age = 34 } };
            generator.Take(3).Select(x => new { x.Name, x.Age }).ShouldMatchSequence(expected);
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

		public class LittleManBuilder : BuilderBase<LittleMan,LittleManBuilder>
		{
            protected override IGenerator<LittleMan> GetDefaultGenerator()
            {
                return Generator.Create
                (
                    (context, name, age) => new LittleMan(name, age),
                    StringGenerators.Numbered("Little Man {1}"),
                    Generator.Constant(38)
                );
            }

            // The problem with these With* modifying methods is they rely on the index of the
            // generators used by the create function. If the position of two constructor
            // parameters of the same type were changed, then refactoring tools would
            // not change this logic. Possibly need to use expressions and some funky
            // syntax to make this more refactoring friendly, e.g. 

			public LittleManBuilder WithName(IGenerator<string> name)
			{
				return ReplaceChildAtIndex(0, name);
			}

			public LittleManBuilder WithAge(IGenerator<int> age)
            {
				return ReplaceChildAtIndex(1, age);
            }
            
		} 
	}
}