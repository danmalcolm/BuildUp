using BuildUp.Builders;
using BuildUp.Generators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class HotelBuilder : BuilderBase<Hotel,HotelBuilder>
	{
		private readonly IGenerator<string> codes = StringGenerator.Numbered("hotel-{1}");  
		private readonly IGenerator<string> names = StringGenerator.Numbered("Hotel {1}");

		protected override IGenerator<Hotel> GetGenerator()
		{
			return from code in codes
			       from name in names
			       select new Hotel(code, name);
		}
	}
}