﻿using System.Linq;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests
{
	[TestFixture]
	public class GeneratorExtensionsSpecs
	{
		readonly IGenerator<Person> generator = Generator.Create(index => new Person("Man " + (index + 1), 20));
			
		[Test]
		public void settable_property_with_value()
		{
			var generator1 = generator.Set(x => x.FavouriteColour, "Pink");
			generator1.Take(3).Select(x => x.FavouriteColour).ShouldMatchSequence("Pink", "Pink", "Pink");
		}

		[Test]
		public void settable_property_with_sequence()
		{
			var colours = new [] { "Pink", "Blue", "Green", "Yellow" };
			var colourGenerator = Generator.FromSequence(colours);
			var generator1 = generator.Set(x => x.FavouriteColour, colourGenerator);
			
			generator1.Take(4).Select(x => x.FavouriteColour).ShouldMatchSequence(colours);
		}

		
	}
}