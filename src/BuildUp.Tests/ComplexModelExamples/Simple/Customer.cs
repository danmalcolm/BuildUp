using System;
using System.Collections.Generic;

namespace BuildUp.Tests.ComplexModelExamples.Simple
{
	public class Customer
	{
		private readonly List<CustomerHistoryItem> history = new List<CustomerHistoryItem>();

		public Customer(string code, string name)
		{
			Code = code;
			Name = name;
		}

		public string Code { get; private set; }

		public string Name { get; private set; }

		public IList<CustomerHistoryItem> History
		{
			get { return history.AsReadOnly(); }
		}

		public void RecordHistory(DateTime date, string details)
		{
			history.Add(new CustomerHistoryItem(date, details));
		}
	}
}