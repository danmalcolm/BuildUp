namespace BuildUp.Tests.ComplexModelExamples.Simple.Sources
{
	public static class TestObjects 
	{
		public static ICompositeSource<Customer> Customers
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

        public static ICompositeSource<Customer> Customers2
        {
            get
            {
                var codes = StringSources.FormatWithItemNumber("customer-{0}");
				var names = StringSources.FormatWithItemNumber("Customer Number {0}");
                return CompositeSource.Create(() => new Customer(Use.Source(codes), Use.Source(names)));
            }
        }
	}
}