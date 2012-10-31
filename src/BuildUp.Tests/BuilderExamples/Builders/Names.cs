using System.Collections.Generic;
using BuildUp.ValueGenerators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class Names
	{
		public static IGenerator<Name> Default
		{
			get
			{
				return Generator.Create((context, firstName, lastName) => new Name("Mr", firstName, lastName),
				                              StringGenerators.Constant("John"),
				                              StringGenerators.Numbered("Last Name {1}"));
			}
		}
	}
}