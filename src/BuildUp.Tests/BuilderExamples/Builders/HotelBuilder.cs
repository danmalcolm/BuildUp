using BuildUp.Builders;
using BuildUp.ValueSources;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class HotelBuilder : Builder<Hotel,HotelBuilder>
	{
		protected override Source<Hotel> GetDefaultSource()
		{
			return Source.Create
			(
				(context, code, name) => new Hotel(code, name),
				StringSources.Numbered("hotel-{1}"),
				StringSources.Numbered("Hotel {1}")
			);
		}
	}
}