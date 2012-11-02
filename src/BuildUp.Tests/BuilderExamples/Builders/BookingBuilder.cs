using System;
using System.Collections.Generic;
using BuildUp.Builders;
using BuildUp.ValueGenerators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class BookingBuilder : BuilderBase<Booking, BookingBuilder>
	{
		protected override IGenerator<Booking> GetDefaultGenerator()
		{
			return Generator.Create
			(
				(context, hotel, customer, startDate) => new Booking(hotel, customer, startDate),
				new HotelBuilder(),
				new CustomerBuilder(),
				DateTimeGenerators.IncrementingDays(DateTime.Now.Date)
			);
		}

		public BookingBuilder AtHotel(IGenerator<Hotel> hotels)
		{
			return ReplaceChildAtIndex(0, hotels);
		}

		public BookingBuilder WithCustomer(IGenerator<Customer> customers)
		{
			return ReplaceChildAtIndex(1, customers);
		}

		public BookingBuilder StartingOn(IGenerator<DateTime> startDates)
		{
			return ReplaceChildAtIndex(2, startDates);
		}
	}
}