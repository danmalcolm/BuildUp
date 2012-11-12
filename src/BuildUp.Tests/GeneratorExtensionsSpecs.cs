using System.Linq;
using BuildUp.ValueGenerators;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests
{
	[TestFixture]
	public class GeneratorExtensionsSpecs
	{
			
		[Test]
		public void settable_property_with_value()
		{
			var generator = Generator.Create(index => new Person("Man " + (index + 1), 20));
			var generator1 = generator.Set(x => x.FavouriteColour, "Pink");
			generator1.Take(3).Select(x => x.FavouriteColour).ShouldMatchSequence("Pink", "Pink", "Pink");
		}

		[Test]
		public void settable_property_with_sequence()
		{
			var generator = Generator.Create(index => new Person("Man " + (index + 1), 20));
			var colours = new[] { "Pink", "Blue", "Green", "Yellow" };
			var colourGenerator = Generator.FromSequence(colours);
			var generator1 = generator.Set(x => x.FavouriteColour, colourGenerator);
			
			generator1.Take(4).Select(x => x.FavouriteColour).ShouldMatchSequence(colours);
		}

		[Test]
		public void combining_two_generators()
		{
			var generator1 = Generator.FromSequence(new[] {"a", "b", "c", "d"});
			var generator2 = Generator.FromSequence(new[] {"1", "2", "3", "4"});
			var combined = generator1.Combine(generator2, (x, y) => x + y);
			combined.Take(4).ShouldMatchSequence("a1", "b2", "c3", "d4");
		}


		[Test]
		public void combining_two_generators_length_should_be_length_of_shortest()
		{
			var generator1 = Generator.FromSequence(new[] { "a", "b", "c", "d" });
			var generator2 = Generator.FromSequence(new[] { "1", "2" });
			var combined = generator1.Combine(generator2, (x, y) => x + y);
			combined.Take(10).ShouldMatchSequence("a1", "b2");
		}

		[Test]
		public void combining_2_generators_with_select_many_method_call()
		{
			var names = StringGenerators.Numbered("Man {1}");
			var ages = IntGenerators.Incrementing(30);

			var generator = names.SelectMany(x => ages, (name, age) => new Person(name, age));
			generator.Take(3).Select(x => new { x.Name, x.Age }).ShouldMatchSequence(new { Name = "Man 1", Age = 30 }, new { Name = "Man 2", Age = 31 }, new { Name = "Man 3", Age = 32 });

		}

		[Test]
		public void combining_2_generators_with_select_many_query_syntax()
		{
			var generator = from name in StringGenerators.Numbered("Man {1}")
							from age in IntGenerators.Incrementing(30)
							select new Person(name, age);
			generator.Take(3).Select(x => new { x.Name, x.Age }).ShouldMatchSequence(new { Name = "Man 1", Age = 30 }, new { Name = "Man 2", Age = 31 }, new { Name = "Man 3", Age = 32 });

		}

		[Test]
		public void combining_3_generators_with_select_many_query_syntax()
		{
			var generator = from name in StringGenerators.Numbered("Man {1}")
							from age in IntGenerators.Incrementing(30)
							from colour in StringGenerators.Numbered("Colour {1}")
							select new Person(name, age) { FavouriteColour = colour }; // cheers, compiler!
			var expectedValues = new[]
			{
				new {Name = "Man 1", Age = 30, FavouriteColour = "Colour 1"},
				new {Name = "Man 2", Age = 31, FavouriteColour = "Colour 2"},
				new {Name = "Man 3", Age = 32, FavouriteColour = "Colour 3"}
			};
			generator.Take(3).Select(x => new { x.Name, x.Age, x.FavouriteColour }).ShouldMatchSequence(expectedValues);
		}

		[Test]
		public void mapping_using_function()
		{
			var generator1 = Generator.Create(index => index);
			var generator2 = generator1.Select(value => value * 10);

			generator1.Take(3).ShouldMatchSequence(0, 1, 2);
			generator2.Take(3).ShouldMatchSequence(0, 10, 20);
		}

		[Test]
		public void modifying_using_action()
		{
			var generator1 = Generator.Create(index => new Person("Man " + (index + 1), 20));
			var generator2 = generator1.Select(man => man.ChangeName("Frank"));

			generator1.Take(3).Select(x => new { x.Name, x.Age })
				.ShouldMatchSequence(new { Name = "Man 1", Age = 20 }, new { Name = "Man 2", Age = 20 }, new { Name = "Man 3", Age = 20 });
			generator2.Take(3).Select(x => new { x.Name, x.Age })
				.ShouldMatchSequence(new { Name = "Frank", Age = 20 }, new { Name = "Frank", Age = 20 }, new { Name = "Frank", Age = 20 });
		}

		private class Person
		{
			public Person(string name, int age)
			{
				Name = name;
				Age = age;
			}

			public string Name { get; private set; }

			public int Age { get; private set; }

			public string FavouriteColour { get; set; }

			public void ChangeName(string name)
			{
				Name = name;
			}
		}
	}
}