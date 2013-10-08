using System;
using System.Collections.Generic;
using BuildUp.Builders;
using BuildUp.Generators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class BookingBuilder : BuilderBase<Booking, BookingBuilder>
	{
		private IGenerator<Hotel> hotels = new HotelBuilder().Freeze();
		private IGenerator<Customer> customers = new CustomerBuilder();
		private IGenerator<DateTime> startDates = DateGenerator.Step(new DateTime(2012, 1, 1), TimeSpan.FromDays(1));

		protected override IGenerator<Booking> GetGenerator()
		{
			return from hotel in hotels
			       from customer in customers
			       from date in startDates
			       select new Booking(hotel, customer, date);
		}

		public BookingBuilder AtHotel(IGenerator<Hotel> hotels)
		{
			return Copy(me => me.hotels = hotels);
		}

		public BookingBuilder WithCustomer(IGenerator<Customer> customers)
		{
			return Copy(me => me.customers = customers);
		}

		public BookingBuilder StartingOn(IGenerator<DateTime> startDates)
		{
			return Copy(me => me.startDates = startDates);
		}
	}
}