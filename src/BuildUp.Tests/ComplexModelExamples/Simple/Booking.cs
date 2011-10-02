using System;

namespace BuildUp.Tests.ComplexModelExamples.Simple
{
	public class Booking
	{
		public Booking(Hotel hotel, DateTime startDate)
		{
			Hotel = hotel;
			StartDate = startDate;
		}

		public Hotel Hotel { get; private set; }

		public DateTime StartDate { get; private set; }
	}
}