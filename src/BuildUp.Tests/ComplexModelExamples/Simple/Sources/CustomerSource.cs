namespace BuildUp.Tests.ComplexModelExamples.Simple
{
	public static class CustomerSource 
	{
		private static readonly SourceMap Defaults = new SourceMap()
					.Add("code", StringSources.FormatWithItemNumber("customer-{0}"))
					.Add("name", StringSources.FormatWithItemNumber("Customer Number {0}"));
			

		public static ICompositeSource<Customer> Default
		{
			get
			{
				return CompositeSource.Create((context, sources) => new Customer(sources.Create<string>("code", context), sources.Create<string>("name", context)), Defaults);
			}
		}
	}
}