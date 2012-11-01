using System;
using System.Collections.Generic;
using BuildUp.Builders;
using BuildUp.ValueGenerators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class BookingBuilder : Builder<Booking, BookingBuilder>
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
			return ChangeChildGenerator(0, hotels);
		}

		public BookingBuilder WithCustomer(IGenerator<Customer> customers)
		{
			return ChangeChildGenerator(1, customers);
		}

		public BookingBuilder StartingOn(IGenerator<DateTime> startDates)
		{
			return ChangeChildGenerator(2, startDates);
		}
	}
}