using System.Linq;
using BuildUp.Tests.Common;
using BuildUp.ValueGenerators;
using NUnit.Framework;

namespace BuildUp.Tests
{
	[TestFixture]
	public class EnumerableExtensionsSpecs
	{
		[Test]
		public void simple_freeze()
		{
			var generator1 = Generator.Create(index => index);
			var generator2 = generator1.Freeze();

			generator1.Take(5).ShouldMatchSequence(0, 1, 2, 3, 4);
			generator2.Take(5).ShouldMatchSequence(0, 0, 0, 0, 0);
		}

		[Test]
		public void loop()
		{
			var generator1 = Generator.Create(index => index);
			var generator2 = generator1.Loop(3);

			generator1.Take(9).ShouldMatchSequence(0, 1, 2, 3, 4, 5, 6, 7, 8);
			generator2.Take(9).ShouldMatchSequence(0, 1, 2, 0, 1, 2, 0, 1, 2);
		}

		[Test]
		public void looping_generator_of_complex_objects_should_repeat_same_instances()
		{
			var generator = Generator.Create
				(
					(name, age) => new Person(name, age),
					StringGenerators.Numbered("Little Man {1}"),
					Generator.Constant(38)
				).Loop(2);
			generator.Take(4).Distinct().Count().ShouldEqual(2);

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