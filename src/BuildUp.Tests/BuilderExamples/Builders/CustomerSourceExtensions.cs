using System;
using System.Collections.Generic;
using BuildUp.Generators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	// Example of defining set-up methods for custom scenarios. This may be generic, or more likely more specified to a given set
	// of tests and would be located within the relevant scope
	public static class CustomerSetupExtensions
	{
		public static IGenerator<Customer> WithHistoryItem(this IGenerator<Customer> customers, DateTime date, string notes)
		{
			return customers.Modify(customer => customer.RecordHistory(date, notes));
		}
	}
}