using BuildUp.Builders;
using BuildUp.ValueGenerators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class HotelBuilder : BuilderBase<Hotel,HotelBuilder>
	{
		protected override IGenerator<Hotel> GetDefaultGenerator()
		{
			return Generator.Create
			(
				(context, code, name) => new Hotel(code, name),
				StringGenerators.Numbered("hotel-{1}"),
				StringGenerators.Numbered("Hotel {1}")
			);
		}
	}
}