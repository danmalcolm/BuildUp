//using System.Linq;

using System.Linq;
using BuildUp.ValueSources;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests
{
	[TestFixture]
	public class SourceSpecs
	{

		[Test]
		public void simple_source()
		{
			var source = Source.Create(context => context.Index);
			source.Take(5).ShouldMatchSequence(0,1,2,3,4);
		}

		[Test]
		public void different_iterations_should_return_separate_sequences()
		{
			var source = Source.Create(context => context.Index);
			var first = source.Take(5);
			var second = source.Take(5);
			first.ShouldMatchSequence(second);
		}

		[Test]
		public void combining_with_function()
		{
			var source1 = Source.Create(context => context.Index);
			var source2 = source1.Select(value => value*10);
			
			source1.Take(3).ShouldMatchSequence(0, 1, 2);
			source2.Take(3).ShouldMatchSequence(0, 10, 20);
		}

		[Test]
		public void combining_with_action()
		{
			var source1 = Source.Create(context => new LittleMan("Man " + (context.Index + 1), 20));
			var source2 = source1.Select(man => man.ChangeName("Frank"));

			source1.Take(3).Select(x => new { x.Name, x.Age }).ShouldMatchSequence(new { Name = "Man 1", Age = 20 }, new { Name = "Man 2", Age = 20 }, new { Name = "Man 3", Age = 20 });
			source2.Take(3).Select(x => new { x.Name, x.Age }).ShouldMatchSequence(new { Name = "Frank", Age = 20 }, new { Name = "Frank", Age = 20 }, new { Name = "Frank", Age = 20 });
		}

		[Test]
		public void combining_2_sources_with_select_many_method_call()
		{
			var names = StringSources.Numbered("Man {0}");
			var ages = IntSources.Incrementing(30);

			var source = names.SelectMany(x => ages, (name, age) => new LittleMan(name, age));
			source.Take(3).Select(x => new { x.Name, x.Age }).ShouldMatchSequence(new { Name = "Man 1", Age = 30 }, new { Name = "Man 2", Age = 31 }, new { Name = "Man 3", Age = 32 });

		}

		[Test]
		public void combining_2_sources_with_select_many_query_syntax()
		{
			var source = from name in StringSources.Numbered("Man {0}")
			             from age in IntSources.Incrementing(30)
			             select new LittleMan(name, age);
			source.Take(3).Select(x => new { x.Name, x.Age }).ShouldMatchSequence(new { Name = "Man 1", Age = 30 }, new { Name = "Man 2", Age = 31 }, new { Name = "Man 3", Age = 32 });

		}

		[Test]
		public void combining_3_sources_with_select_many_query_syntax()
		{
			var source = from name in StringSources.Numbered("Man {0}")
						 from age in IntSources.Incrementing(30)
						 from colour in StringSources.Numbered("Colour {0}")
						 select new LittleMan(name, age) { FavouriteColour = colour }; // cheers, compiler!
			var expectedValues = new[]
			{
				new {Name = "Man 1", Age = 30, FavouriteColour = "Colour 1"},
				new {Name = "Man 2", Age = 31, FavouriteColour = "Colour 2"},
				new {Name = "Man 3", Age = 32, FavouriteColour = "Colour 3"}
			};
			source.Take(3).Select(x => new { x.Name, x.Age, x.FavouriteColour }).ShouldMatchSequence(expectedValues);

		}

	}
}