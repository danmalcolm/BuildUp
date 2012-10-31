using System.Linq;
using BuildUp.ValueGenerators;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests
{
	[TestFixture]
	public class GeneratorSpecs
	{

		[Test]
		public void simple_generator()
		{
			var generator = Generator.Create(index => index);
			generator.Take(5).ShouldMatchSequence(0,1,2,3,4);
		}

		[Test]
		public void different_iterations_should_return_identical_sequences()
		{
			var generator = Generator.Create(index => index);
			var first = generator.Take(5);
			var second = generator.Take(5);
			first.ShouldMatchSequence(second);
		}

		[Test]
		public void combining_with_function()
		{
			var generator1 = Generator.Create(index => index);
			var generator2 = generator1.Select(value => value*10);
			
			generator1.Take(3).ShouldMatchSequence(0, 1, 2);
			generator2.Take(3).ShouldMatchSequence(0, 10, 20);
		}

		[Test]
		public void combining_with_action()
		{
			var generator1 = Generator.Create(index => new Person("Man " + (index + 1), 20));
			var generator2 = generator1.Select(man => man.ChangeName("Frank"));

			generator1.Take(3).Select(x => new { x.Name, x.Age }).ShouldMatchSequence(new { Name = "Man 1", Age = 20 }, new { Name = "Man 2", Age = 20 }, new { Name = "Man 3", Age = 20 });
			generator2.Take(3).Select(x => new { x.Name, x.Age }).ShouldMatchSequence(new { Name = "Frank", Age = 20 }, new { Name = "Frank", Age = 20 }, new { Name = "Frank", Age = 20 });
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
		public void modifying_sequence_should_be_applied_to_generated_objects()
		{
			var generator = StringGenerators.Numbered("{1}").ModifySequence(x => x.Skip(2));
			generator.Take(3).ShouldMatchSequence("3", "4", "5");
		}

	}
}