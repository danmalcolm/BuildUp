using System.Linq;
using BuildUp.Tests.Common;
using BuildUp.ValueSources;
using NUnit.Framework;

namespace BuildUp.Tests.CompositeSourceSpecs
{
	[TestFixture]
	public class CompositeSourceExtensionsSpecs
	{
		private class AdvancedLeopard
		{
			public AdvancedLeopard(string name, int age, Car car)
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
		public void objects_constructed_from_2_simple_sources()
		{
			var source1 = Source.Create
				(
					(context, name, age) => new LittleMan(name, age),
					StringSources.Numbered("Little Man {0}"),
					IntSources.Constant(31)
				);
			source1.Take(3).Select(x => x.Name).ShouldMatchSequence("Little Man 1", "Little Man 2", "Little Man 3");
			source1.Take(3).Select(x => x.Age).ShouldMatchSequence(31, 31, 31);
		}

		[Test]
		public void objects_constructed_from_simple_source_and_composite_source()
		{
			var cars = Source.Create((context, name, age) => new Car(name, age),
			                                       StringSources.Numbered("Car {0}"),
			                                       IntSources.Constant(2));
			var leopards = Source.Create
				(
					(context, name, age, car) => new AdvancedLeopard(name, age, car),
					StringSources.Numbered("Leopard {0}"),
					IntSources.Constant(31),
					cars
				);
			leopards.Take(3).Select(x => x.Name).ShouldMatchSequence("Leopard 1", "Leopard 2", "Leopard 3");
			leopards.Take(3).Select(x => x.Age).ShouldMatchSequence(31, 31, 31);
			leopards.Take(3).Select(x => x.Car.Name).ShouldMatchSequence("Car 1", "Car 2", "Car 3");
		}

		[Test]
		public void clone_replacing_child_source_by_position()
		{
			var source1 = Source.Create
				(
					(context, name, age) => new LittleMan(name, age),
					StringSources.Numbered("Little Man {0}"),
					IntSources.Constant(38)
				);
			var source2 = source1.ModifyChildSources(childSources => childSources.Replace(1, IntSources.Incrementing(44)));
			
			source1.Take(3).Select(x => x.Age).ShouldMatchSequence(38, 38, 38);
			source2.Take(3).Select(x => x.Age).ShouldMatchSequence(44, 45, 46);
		}
	}
		 
	
}