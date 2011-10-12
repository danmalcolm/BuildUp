using System.Linq;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests.SourceExtensionsSpecs
{
	[TestFixture]
	public class setting_member_of_source
	{
		ISource<LittleMan> source1 = Source.Create(context => new LittleMan("Man " + (context.Index + 1), 20));
			
		[Test]
		public void settable_property_with_value()
		{
			var source = source1.Set(x => x.FavouriteColour, "Pink");
			source.Take(3).Select(x => x.FavouriteColour).ShouldMatchSequence("Pink", "Pink", "Pink");
		}

		[Test]
		public void settable_property_with_sequence()
		{
			var sequence = new [] { "Pink", "Blue" };
			var source = source1.Set(x => x.FavouriteColour, sequence);
			source.Take(4).Select(x => x.FavouriteColour).ShouldMatchSequence("Pink", "Blue", "Pink", "Blue");
		}

		[Test]
		public void action_with_sources()
		{
			var names = Source.Create(x => "Name " + x.Index);
			var source = source1.Select((man, name) => man.ChangeName(name), names);
		}

	}
}