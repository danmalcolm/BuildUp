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
                    StringSources.Numbered("customer-{0}"),
                    StringSources.Numbered("Customer Number {0}")
                );
			}
		}

        public static ICompositeSource<Customer> Customers2
        {
            get
            {
                var codes = StringSources.Numbered("customer-{0}");
				var names = StringSources.Numbered("Customer Number {0}");
                return CompositeSource.Create(() => new Customer(Use.Source(codes), Use.Source(names)));
            }
        }
	}
}