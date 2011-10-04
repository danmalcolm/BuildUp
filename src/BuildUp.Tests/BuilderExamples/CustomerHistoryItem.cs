using System;

namespace BuildUp.Tests.BuilderExamples
{
	public class CustomerHistoryItem
	{
		public CustomerHistoryItem(DateTime recordedDate, string details)
		{
			RecordedDate = recordedDate;
			Details = details;
		}

		public DateTime RecordedDate { get; private set; }

		public string Details { get; private set; }
	}
}