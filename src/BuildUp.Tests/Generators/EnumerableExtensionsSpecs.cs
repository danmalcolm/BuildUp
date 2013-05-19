using System.Linq;
using BuildUp.Generators;
using BuildUp.Tests.Common;
using NUnit.Framework;

namespace BuildUp.Tests.Generators
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
		    var generator = from name in StringGenerator.Numbered("Little Man {1}")
		                    from age in Generator.Constant(38)
		                    select new Person(name, age);
            generator = generator.Loop(2);
			generator.Take(4).Distinct().Count().ShouldEqual(2);
		}

        [Test]
        public void repeat_each()
        {
            var generator = Generator.Values(1, 2, 3).RepeatEach(3);
            generator.Take(9).ShouldMatchSequence(1, 1, 1, 2, 2, 2, 3, 3, 3);
        }

        [Test]
        public void repeat_each_with_generator_of_complex_objects_should_repeat_same_instances()
        {
            var generator = from name in StringGenerator.Numbered("Little Man {1}")
                            from age in Generator.Constant(38)
                            select new Person(name, age);
            generator = generator.RepeatEach(3);
            generator.Take(6).Distinct().Count().ShouldEqual(2);
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