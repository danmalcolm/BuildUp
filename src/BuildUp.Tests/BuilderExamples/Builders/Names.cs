using BuildUp.ValueSources;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class Names
	{
		public static ISource<Name> Default
		{
			get
			{
				return CompositeSource.Create((context, firstName, lastName) => new Name("Mr", firstName, lastName),
				                              StringSources.Constant("John"),
				                              StringSources.Numbered("Last Name {0}"));
			}
		}
	}
}