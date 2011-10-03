namespace BuildUp.Tests.ComplexModelExamples.Simple.Sources
{
	public static class CustomerSourceExtensions
	{
		 public static ICompositeSource<Customer> WithCode(this ICompositeSource<Customer> customers, ISource<string> source)
		 {
		 	return null;
		 }

         public static ICompositeSource<Customer> WithCode2(this ICompositeSource<Customer> customers, ISource<string> source)
         {
             return customers.ChangeSource(() => new Customer(Use.Existing<string>(), Use.Source(source)));
         }
	}
}