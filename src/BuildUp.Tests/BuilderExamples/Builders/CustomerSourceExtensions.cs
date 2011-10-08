using System;
using System.Collections.Generic;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	// example of setting up for given scenarios. This may be generic, or more likely more specified to a given set
	// of tests and would be located within the relevant scope
	public static class CustomerSetupExtensions
	{
		public static IEnumerable<Customer> WithHistory(this IEnumerable<Customer> customers, IEnumerable<DateTime> dates, IEnumerable<string> notes)
		{
			return customers.Modify(notes, (c, s) => c.RecordHistory(DateTime.Now, s));
		}
	}
}