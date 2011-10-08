using System;
using System.Collections.Generic;
using BuildUp.ValueSources;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class BookingBuilder : Builder<Booking, BookingBuilder>
	{
		protected override CompositeSource<Booking> GetDefaultSource()
		{
			return CompositeSource.Create
			(
				(context, hotel, customer, startDate) => new Booking(hotel, customer, startDate),
				new HotelBuilder(),
				new CustomerBuilder(),
				DateTimeSources.IncrementingDays(DateTime.Now.Date)
			);
		}

		public BookingBuilder AtHotel(IEnumerable<Hotel> hotels)
		{
			return ChangeChildSource(0, hotels);
		}

		public BookingBuilder WithCustomer(IEnumerable<Customer> customers)
		{
			return ChangeChildSource(1, customers);
		}

		public BookingBuilder StartingOn(IEnumerable<DateTime> startDates)
		{
			return ChangeChildSource(2, startDates);
		}
	}
}