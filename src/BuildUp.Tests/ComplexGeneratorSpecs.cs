using System.Linq;
using BuildUp.Tests.Common;
using BuildUp.ValueGenerators;
using NUnit.Framework;

namespace BuildUp.Tests
{
	[TestFixture]
	public class ComplexGeneratorSpecs
	{
		private class Driver
		{
			public Driver(string name, int age, Car car)
			{
				Name = name;
				Age = age;
				Car = car;
			}

			public string Name { get; private set; }

			public int Age { get; private set; }

			public Car Car { get; private set; }
		}

		private class Car
		{
			public Car(string name, int age)
			{
				Name = name;
				Age = age;
			}

			public string Name { get; private set; }

			public int Age { get; private set; }
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
		}

		[Test]
		public void objects_constructed_from_2_simple_generators()
		{
			var generator1 = Generator.Create
				(
					(name, age) => new Person(name, age),
					StringGenerators.Numbered("Little Man {1}"),
					Generator.Constant(31)
				);
			generator1.Take(3).Select(x => x.Name).ShouldMatchSequence("Little Man 1", "Little Man 2", "Little Man 3");
			generator1.Take(3).Select(x => x.Age).ShouldMatchSequence(31, 31, 31);
		}

		[Test]
		public void objects_constructed_from_simple_generator_and_complex_generator()
		{
			var cars = Generator.Create((name, age) => new Car(name, age),
			                                       StringGenerators.Numbered("Car {1}"),
			                                       Generator.Constant(2));
			var drivers = Generator.Create
				(
					(name, age, car) => new Driver(name, age, car),
					StringGenerators.Numbered("Driver {1}"),
					Generator.Constant(31),
					cars
				);
			drivers.Take(3).Select(x => x.Name).ShouldMatchSequence("Driver 1", "Driver 2", "Driver 3");
			drivers.Take(3).Select(x => x.Age).ShouldMatchSequence(31, 31, 31);
			drivers.Take(3).Select(x => x.Car.Name).ShouldMatchSequence("Car 1", "Car 2", "Car 3");
		}
	}
}