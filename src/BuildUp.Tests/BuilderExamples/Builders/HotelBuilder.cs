using BuildUp.ValueGenerators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class HotelBuilder : BuilderBase<Hotel,HotelBuilder>
	{
		private readonly IGenerator<string> codes = StringGenerators.Numbered("hotel-{1}");  
		private readonly IGenerator<string> names = StringGenerators.Numbered("Hotel {1}");

		protected override IGenerator<Hotel> GetGenerator()
		{
			return from code in codes
			       from name in names
			       select new Hotel(code, name);
		}
	}
}