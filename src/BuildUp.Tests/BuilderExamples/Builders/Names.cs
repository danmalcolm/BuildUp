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
				return from firstName in Generator.Constant("John")
				       from lastName in StringGenerator.Numbered("Last Name {1}")
				       select new Name("Mr", firstName, lastName);
			}
		}
	}
}