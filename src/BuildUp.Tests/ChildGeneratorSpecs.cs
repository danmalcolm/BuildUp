using System.Linq;
using BuildUp.Tests.Common;
using BuildUp.ValueGenerators;
using NUnit.Framework;

namespace BuildUp.Tests
{
	[TestFixture]
	public class ChildGeneratorSpecs
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

		[Test]
		public void objects_constructed_from_2_simple_generators()
		{
			var generator1 = Generators.Create
				(
					(context, name, age) => new Person(name, age),
					StringGenerators.Numbered("Little Man {1}"),
					IntGenerators.Constant(31)
				);
			generator1.Take(3).Select(x => x.Name).ShouldMatchSequence("Little Man 1", "Little Man 2", "Little Man 3");
			generator1.Take(3).Select(x => x.Age).ShouldMatchSequence(31, 31, 31);
		}

		[Test]
		public void objects_constructed_from_simple_generator_and_composite_generator()
		{
			var cars = Generators.Create((context, name, age) => new Car(name, age),
			                                       StringGenerators.Numbered("Car {1}"),
			                                       IntGenerators.Constant(2));
			var drivers = Generators.Create
				(
					(context, name, age, car) => new Driver(name, age, car),
					StringGenerators.Numbered("Leopard {1}"),
					IntGenerators.Constant(31),
					cars
				);
			drivers.Take(3).Select(x => x.Name).ShouldMatchSequence("Leopard 1", "Leopard 2", "Leopard 3");
			drivers.Take(3).Select(x => x.Age).ShouldMatchSequence(31, 31, 31);
			drivers.Take(3).Select(x => x.Car.Name).ShouldMatchSequence("Car 1", "Car 2", "Car 3");
		}

		[Test]
		public void clone_replacing_child_generator_by_position()
		{
			var generator1 = Generators.Create
				(
					(context, name, age) => new Person(name, age),
					StringGenerators.Numbered("Little Man {1}"),
					IntGenerators.Constant(38)
				);
			var generator2 = generator1.ModifyChildGenerators(generators => generators.ReplaceGeneratorAt(1, IntGenerators.Incrementing(44)));
			
			generator1.Take(3).Select(x => x.Age).ShouldMatchSequence(38, 38, 38);
			generator2.Take(3).Select(x => x.Age).ShouldMatchSequence(44, 45, 46);
		}
	}
		 
	
}