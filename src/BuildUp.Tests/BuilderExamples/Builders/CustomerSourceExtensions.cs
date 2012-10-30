using System;
using System.Collections.Generic;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	// example of setting up for given scenarios. This may be generic, or more likely more specified to a given set
	// of tests and would be located within the relevant scope
	public static class CustomerSetupExtensions
	{
		public static IGenerator<Customer> WithHistory(this IGenerator<Customer> customers, DateTime date, string notes)
		{
			return customers.Select(customer => customer.RecordHistory(date, notes));
		}
	}
}