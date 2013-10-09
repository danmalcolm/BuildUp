using System;
using System.Linq;
using BuildUp.Generators;
using BuildUp.Tests.BuilderExamples.Builders;
using FluentAssertions;
using Xunit;
using BuildUp.Tests.Common;

namespace BuildUp.Tests.BuilderExamples
{
	
	public class Examples
	{
		[Fact]
		public void default_ctor_arg_generators_used_by_builder()
		{
			var hotels = new HotelBuilder().Take(3);
			var expected = new[]
			{
				new {Code = "hotel-1", Name = "Hotel 1"},
				new {Code = "hotel-2", Name = "Hotel 2"},
				new {Code = "hotel-3", Name = "Hotel 3"},
			};
			hotels.Select(x => new { x.Code, x.Name}).Should().Equal(expected);
		}

		[Fact]
		public void lots_of_bookings()
		{
			var hotels = new HotelBuilder().Loop(5);
			var dates = DateGenerator.Step(DateTime.Now.Date, TimeSpan.FromDays(1)).Loop(25); // 5 bookings at each hotel per day
			var bookings = new BookingBuilder().AtHotel(hotels).StartingOn(dates).Take(50).ToArray();
			
		}

		[Fact]
		public void modifying_instances_after_creation()
		{
			var notes = StringGenerator.Numbered("Stuff {1}");
			var customers = new CustomerBuilder().Modify(c => c.RecordHistory(DateTime.Now, "some notes")).Take(5);
		}

		
	}
}