using BuildUp.Tests.ComplexModelExamples.Simple;
using BuildUp.ValueSources;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class HotelBuilder : Builder<Hotel,HotelBuilder>
	{
		protected override ICompositeSource<Hotel> GetDefaultSource()
		{
			return CompositeSource.Create
			(
				(context, code, name) => new Hotel(code, name),
				StringSources.Numbered("hotel-{0}"),
				StringSources.Numbered("Hotel {0}")
			);
		}
	}
}