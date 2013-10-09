using System.Linq;
using BuildUp.Generators;
using BuildUp.Tests.Common;
using FluentAssertions;
using Xunit;

namespace BuildUp.Tests.Generators
{
	
	public class GeneratorExtensionsSpecs
	{
			
		[Fact]
		public void settable_property_with_value()
		{
			var generator = Generator.Create(index => new Person("Man " + (index + 1), 20));
			var generator1 = generator.Set(x => x.FavouriteColour, "Pink");
			generator1.Take(3).Select(x => x.FavouriteColour).Should().Equal(new [] { "Pink", "Pink", "Pink" });
		}

		[Fact]
		public void settable_property_with_sequence()
		{
			var generator = Generator.Create(index => new Person("Man " + (index + 1), 20));
			var colours = new[] { "Pink", "Blue", "Green", "Yellow" };
			var colourGenerator = Generator.Values(colours);
			var generator1 = generator.Set(x => x.FavouriteColour, colourGenerator);
			
			generator1.Take(4).Select(x => x.FavouriteColour).Should().Equal(colours);
		}

		[Fact]
		public void combining_two_generators()
		{
			var generator1 = Generator.Values(new[] {"a", "b", "c", "d"});
			var generator2 = Generator.Values(new[] {"1", "2", "3", "4"});
			var combined = generator1.Combine(generator2, (x, y) => x + y);
			combined.Take(4).Should().Equal(new[] { "a1", "b2", "c3", "d4"});
		}


		[Fact]
		public void combining_two_generators_length_should_be_length_of_shortest()
		{
			var generator1 = Generator.Values(new[] { "a", "b", "c", "d" });
			var generator2 = Generator.Values(new[] { "1", "2" });
			var combined = generator1.Combine(generator2, (x, y) => x + y);
			combined.Take(10).Should().Equal(new[] { "a1", "b2" });
		}

		[Fact]
		public void combining_2_generators_with_select_many_method_call()
		{
			var names = StringGenerator.Numbered("Man {1}");
			var ages = IntGenerator.Step(30);

			var generator = names.SelectMany(x => ages, (name, age) => new Person(name, age));
			generator.Take(3).Select(x => new { x.Name, x.Age }).Should().Equal(new { Name = "Man 1", Age = 30 }, new { Name = "Man 2", Age = 31 }, new { Name = "Man 3", Age = 32 });

		}

		[Fact]
		public void combining_2_generators_with_select_many_query_syntax()
		{
			var generator = from name in StringGenerator.Numbered("Man {1}")
							from age in IntGenerator.Step(30)
							select new Person(name, age);
			generator.Take(3).Select(x => new { x.Name, x.Age }).Should().Equal(new { Name = "Man 1", Age = 30 }, new { Name = "Man 2", Age = 31 }, new { Name = "Man 3", Age = 32 });

		}

		[Fact]
		public void combining_3_generators_with_select_many_query_syntax()
		{
			var generator = from name in StringGenerator.Numbered("Man {1}")
							from age in IntGenerator.Step(30)
							from colour in StringGenerator.Numbered("Colour {1}")
							select new Person(name, age) { FavouriteColour = colour }; // cheers, compiler!
			var expectedValues = new[]
			{
				new {Name = "Man 1", Age = 30, FavouriteColour = "Colour 1"},
				new {Name = "Man 2", Age = 31, FavouriteColour = "Colour 2"},
				new {Name = "Man 3", Age = 32, FavouriteColour = "Colour 3"}
			};
			generator.Take(3).Select(x => new { x.Name, x.Age, x.FavouriteColour }).Should().Equal(expectedValues);
		}

		[Fact]
		public void mapping_using_function()
		{
			var generator1 = Generator.Create(index => index);
			var generator2 = generator1.Select(value => value * 10);

			generator1.Take(3).Should().Equal(0, 1, 2);
			generator2.Take(3).Should().Equal(0, 10, 20);
		}

		[Fact]
		public void modifying_using_action()
		{
			var generator1 = Generator.Create(index => new Person("Man " + (index + 1), 20));
			var generator2 = generator1.Modify(man => man.ChangeName("Frank"));

			generator1.Take(3).Select(x => new { x.Name, x.Age })
				.Should().Equal(new { Name = "Man 1", Age = 20 }, new { Name = "Man 2", Age = 20 }, new { Name = "Man 3", Age = 20 });
			generator2.Take(3).Select(x => new { x.Name, x.Age })
				.Should().Equal(new { Name = "Frank", Age = 20 }, new { Name = "Frank", Age = 20 }, new { Name = "Frank", Age = 20 });
		}

        [Fact]
        public void modifying_using_action_and_values_from_another_generator()
        {
            var generator1 = Generator.Create(index => new Person("Man " + (index + 1), 20));
            var generator2 = StringGenerator.Numbered("Frank Man {1}");
            var generator3 = generator1.Modify(generator2, (man, name) => man.ChangeName(name));

            generator1.Take(3).Select(x => new { x.Name, x.Age })
                .Should().Equal(new { Name = "Man 1", Age = 20 }, new { Name = "Man 2", Age = 20 }, new { Name = "Man 3", Age = 20 });
            generator3.Take(3).Select(x => new { x.Name, x.Age })
                .Should().Equal(new { Name = "Frank Man 1", Age = 20 }, new { Name = "Frank Man 2", Age = 20 }, new { Name = "Frank Man 3", Age = 20 });
        }


        [Fact]
        public void when_building_sequences_of_values_should_create_collections_of_length_specified()
        {
            var generator1 = IntGenerator.Step(1);
            var generator2 = generator1.SequencesOf(2);
            var collections = generator2.Take(3).ToList();
            collections[0].Should().Equal(1, 2);
            collections[1].Should().Equal(3, 4);
            collections[2].Should().Equal(5, 6);
        }


        [Fact]
        public void when_building_sequences_of_values_should_create_collections_of_lengths_specified_by_generator()
        {
            var generator1 = IntGenerator.Step(1);
            var generator2 = generator1.SequencesOf(IntGenerator.Step(1));
            var collections = generator2.Take(3).ToList();
            collections[0].Should().Equal(1);
            collections[1].Should().Equal(2, 3);
            collections[2].Should().Equal(4, 5, 6);
        }

        [Fact]
        public void when_building_sequences_of_values_should_stop_populating_collections_when_at_end_of_source_sequence()
        {
            var generator1 = Generator.Values(1, 2, 3, 4, 5, 6, 7);
            var generator2 = generator1.SequencesOf(IntGenerator.Step(1));
            var collections = generator2.Take(5).ToList();
            collections[0].Should().Equal(1);
            collections[1].Should().Equal(2, 3);
            collections[2].Should().Equal(4, 5, 6); 
            collections[3].Should().Equal(7);
            collections[4].Should().BeEmpty();
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