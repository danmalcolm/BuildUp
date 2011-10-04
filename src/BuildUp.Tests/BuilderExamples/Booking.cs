using System;

namespace BuildUp.Tests.BuilderExamples
{
	public class Booking
	{
		public Booking(Hotel hotel, Customer customer, DateTime startDate)
		{
			Hotel = hotel;
			Customer = customer;
			StartDate = startDate;
		}

		public Hotel Hotel { get; private set; }

		public Customer Customer { get; set; }

		public DateTime StartDate { get; private set; }

		public override string ToString()
		{
			return string.Format("Hotel: {0}, Customer: {1}, StartDate: {2}", Hotel, Customer, StartDate);
		}
	}
}