using System.Linq;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests
{
	[TestFixture]
	public class BuilderSpecs
	{

		[Test]
		public void simple_builder()
		{
			var source = new LittleManBuilder();
			var expected = new[]
			{new {Name = "Little Man 1", Age = 38}, new {Name = "Little Man 2", Age = 38}, new {Name = "Little Man 3", Age = 38}};
			source.Take(3).Select(x => new {x.Name, x.Age}).ShouldMatchSequence(expected);
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

		public class LittleManBuilder : Builder<LittleMan,LittleManBuilder>
		{
			public LittleManBuilder()
				: base(InitSource())
			{}

			private static ICompositeSource<LittleMan> InitSource()
			{
				return CompositeSource.Create
				(
					(context, name, age) => new LittleMan(name, age),
					StringSources.FormatWithItemNumber("Little Man {0}"),
					IntSources.Constant(38)
				);
			}

			public LittleManBuilder WithName(ISource<string> names)
			{
				var newSource = CompositeSource.BasedOn(Source, 0, names);
				return Copy(newSource);
			}

			
		} 
	}
}