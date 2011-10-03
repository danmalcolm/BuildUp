namespace BuildUp.Tests.ComplexModelExamples.Simple.Sources
{
	public static class CustomerSource 
	{
		public static ICompositeSource<Customer> Default
		{
			get
			{
				return CompositeSource.Create
                (
                    (context, code, name) => new Customer(code, name),
                    StringSources.FormatWithItemNumber("customer-{0}"),
                    StringSources.FormatWithItemNumber("Customer Number {0}")
                );
			}
		}
	}
}