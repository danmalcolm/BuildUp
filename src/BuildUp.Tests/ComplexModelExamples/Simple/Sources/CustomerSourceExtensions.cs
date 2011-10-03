namespace BuildUp.Tests.ComplexModelExamples.Simple.Sources
{
	public static class CustomerSourceExtensions
	{
		 public static ICompositeSource<Customer> WithCode(this ICompositeSource<Customer> customers, ISource<string> source)
		 {
		 	return customers.ChangeSource(0, source);
		 }
	}
}