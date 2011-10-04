using System;
using System.Linq;
using BuildUp.Tests.BuilderExamples.Builders;
using BuildUp.Tests.ComplexModelExamples.Simple.Sources;
using BuildUp.ValueSources;
using NUnit.Framework;
using BuildUp.Tests.Common;
using BuildUp;

namespace BuildUp.Tests.BuilderExamples
{
	[TestFixture]
	public class Examples
	{
		[Test]
		public void default_ctor_arg_sources_used_by_builder()
		{
			var hotels = new HotelBuilder().Take(3);
			var expected = new[]
			{
				new {Code = "hotel-1", Name = "Hotel 1"},
				new {Code = "hotel-2", Name = "Hotel 2"},
				new {Code = "hotel-3", Name = "Hotel 3"},
			};
			hotels.Select(x => new { x.Code, x.Name}).ShouldMatchSequence(expected);
		}

		[Test]
		public void lots_of_bookings()
		{
			var hotels = new HotelBuilder().RepeatEach(5);
			var dates = DateTimeSources.IncrementingDays(DateTime.Now.Date).RepeatEach(25); // 5 bookings at each hotel per day
			var bookings = new BookingBuilder().AtHotel(hotels).StartingOn(dates).Take(50).ToArray();
			
		}

		
	}
}