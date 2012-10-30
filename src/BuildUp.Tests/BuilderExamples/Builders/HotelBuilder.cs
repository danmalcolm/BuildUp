using BuildUp.Builders;
using BuildUp.ValueGenerators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class HotelBuilder : Builder<Hotel,HotelBuilder>
	{
		protected override Generator<Hotel> GetDefaultGenerator()
		{
			return Generators.Create
			(
				(context, code, name) => new Hotel(code, name),
				StringGenerators.Numbered("hotel-{1}"),
				StringGenerators.Numbered("Hotel {1}")
			);
		}
	}
}