using System.Linq;
using BuildUp.ValueSources;
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
			var colours = new [] { "Pink", "Blue", "Green", "Yellow" };
			var source = source1.Set(x => x.FavouriteColour, colours);
			
			source.Take(4).Select(x => x.FavouriteColour).ShouldMatchSequence(colours);
		}

		
	}
}