using System;
using System.Collections.Generic;

namespace BuildUp.Tests.BuilderExamples
{
	public class Customer
	{
		private readonly List<CustomerHistoryItem> history = new List<CustomerHistoryItem>();

		public Customer(string code, Name name)
		{
			Code = code;
			Name = name;
		}

		public string Code { get; private set; }

		public Name Name { get; private set; }

		public IList<CustomerHistoryItem> History
		{
			get { return history.AsReadOnly(); }
		}

		public void RecordHistory(DateTime date, string details)
		{
			history.Add(new CustomerHistoryItem(date, details));
		}

		public override string ToString()
		{
			return string.Format("Code: {0}, Name: {1}", Code, Name);
		}
	}
}