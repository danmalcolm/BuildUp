using System;
using System.Collections.Generic;
using System.Linq;
using BuildUp.ValueGenerators;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests
{
	[TestFixture]
	public class BuilderSpecs
	{
		private void AssertObjectsMatch<TObject,TCompare>(IGenerator<TObject> generator, Func<TObject,TCompare> map, TCompare[] expected)
		{
			var actual = generator.Take(expected.Length);
			var mapped = actual.Select(map);
			mapped.ShouldMatchSequence(expected);
		}

		[Test]
		public void building_with_default_generators()
		{
			var builder = new MutablePersonBuilder();
			var expected = new[] { new { Name = "Man 1", Age = 38 }, new { Name = "Man 2", Age = 38 }, new { Name = "Man 3", Age = 38 } };
			AssertObjectsMatch(builder, x => new { x.Name, x.Age }, expected);
		}

        [Test]
        public void building_after_changing_a_child_generator_in_place()
        {
            var builder = new MutablePersonBuilder().WithName(StringGenerators.Numbered("Super Man {1}"));
            var expected = new[] { new { Name = "Super Man 1", Age = 38 }, new { Name = "Super Man 2", Age = 38 }, new { Name = "Super Man 3", Age = 38 } };
			AssertObjectsMatch(builder, x => new { x.Name, x.Age }, expected);
        }

        [Test]
        public void building_after_changing_multiple_child_generators_in_place()
        {
            var builder = new MutablePersonBuilder()
				.WithName(StringGenerators.Numbered("Super Man {1}"))
				.WithAge(IntGenerators.Incrementing(30, 2));
            var expected = new[] { new { Name = "Super Man 1", Age = 30 }, new { Name = "Super Man 2", Age = 32 }, new { Name = "Super Man 3", Age = 34 } };
			AssertObjectsMatch(builder, x => new { x.Name, x.Age }, expected);
        }

		[Test]
		public void copy_should_use_new_generator()
		{
			var builder = new ImmutablePersonBuilder().WithName(StringGenerators.Numbered("Super Man {1}"));
			var expected = new[] { new { Name = "Super Man 1", Age = 38 }, new { Name = "Super Man 2", Age = 38 }, new { Name = "Super Man 3", Age = 38 } };
			AssertObjectsMatch(builder, x => new { x.Name, x.Age }, expected);
		}

		[Test]
		public void copy_should_use_multiple_new_generators()
		{
			var builder = new ImmutablePersonBuilder().WithName(StringGenerators.Numbered("Super Man {1}")).WithAge(IntGenerators.Incrementing(30, 2));
			var expected = new[] { new { Name = "Super Man 1", Age = 30 }, new { Name = "Super Man 2", Age = 32 }, new { Name = "Super Man 3", Age = 34 } };
			AssertObjectsMatch(builder, x => new { x.Name, x.Age }, expected);
		}

		[Test]
		public void creating_modified_copies_should_not_modify_originals()
		{
			var first = new ImmutablePersonBuilder();
			var second = first.WithName(StringGenerators.Numbered("Super Man {1}"));
			var third = second.WithAge(IntGenerators.Incrementing(38, 1));

			var expected = new[] { new { Name = "Man 1", Age = 38 }, new { Name = "Man 2", Age = 38 }, new { Name = "Man 3", Age = 38 } };
			AssertObjectsMatch(first, x => new { x.Name, x.Age }, expected);

			expected = new[] { new { Name = "Super Man 1", Age = 38 }, new { Name = "Super Man 2", Age = 38 }, new { Name = "Super Man 3", Age = 38 } };
			AssertObjectsMatch(second, x => new { x.Name, x.Age }, expected);

			expected = new[] { new { Name = "Super Man 1", Age = 38 }, new { Name = "Super Man 2", Age = 39 }, new { Name = "Super Man 3", Age = 40 } };
			AssertObjectsMatch(third, x => new { x.Name, x.Age }, expected);
		}


		public class Person
		{
			public Person(string name, int age)
			{
				Name = name;
				Age = age;
			}

			public string Name { get; private set; }

			public int Age { get; private set; }
		}

		public class MutablePersonBuilder : BuilderBase<Person,MutablePersonBuilder>
		{
			private IGenerator<string> names = StringGenerators.Numbered("Man {1}");
			private IGenerator<int> ages = Generator.Constant(38);

			protected override IGenerator<Person> GetGenerator()
			{
				return from name in names
					   from age in ages
					   select new Person(name, age);
			}

			public MutablePersonBuilder WithName(IGenerator<string> names)
			{
				return Change(me => me.names = names);
			}

			public MutablePersonBuilder WithAge(IGenerator<int> ages)
			{
				return Change(me => me.ages = ages);
			}
		}

		public class ImmutablePersonBuilder : BuilderBase<Person, ImmutablePersonBuilder>
		{
			private IGenerator<string> names = StringGenerators.Numbered("Man {1}");
			private IGenerator<int> ages = Generator.Constant(38);

			protected override IGenerator<Person> GetGenerator()
			{
				return from name in names
					   from age in ages
					   select new Person(name, age);
			}

			public ImmutablePersonBuilder WithName(IGenerator<string> names)
			{
				return Copy(me => me.names = names);
			}

			public ImmutablePersonBuilder WithAge(IGenerator<int> ages)
			{
				return Copy(me => me.ages = ages);
			}
		} 
	}
}