using BuildUp.Builders;
using BuildUp.ValueGenerators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class HotelBuilder : Builder<Hotel,HotelBuilder>
	{
		protected override ComplexGenerator<Hotel> GetDefaultGenerator()
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