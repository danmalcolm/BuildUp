using System;
using BuildUp.ValueSources;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class BookingBuilder : Builder<Booking, BookingBuilder>
	{
		protected override ICompositeSource<Booking> GetDefaultSource()
		{
			return CompositeSource.Create
			(
				(context, hotel, customer, startDate) => new Booking(hotel, customer, startDate),
				new HotelBuilder(),
				new CustomerBuilder(),
				DateTimeSources.IncrementingDays(DateTime.Now.Date)
			);
		}

		public BookingBuilder AtHotel(ISource<Hotel> hotels)
		{
			return ChangeChildSource(0, hotels);
		}

		public BookingBuilder WithCustomer(ISource<Customer> customers)
		{
			return ChangeChildSource(1, customers);
		}

		public BookingBuilder StartingOn(ISource<DateTime> startDates)
		{
			return ChangeChildSource(2, startDates);
		}
	}
}