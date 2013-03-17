using BuildUp.Builders;
using BuildUp.Generators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class CustomerBuilder : BuilderBase<Customer,CustomerBuilder>
	{
		private IGenerator<string> codes = StringGenerator.Numbered("Customer-{1}");
		private IGenerator<Name> names = Names.Default;

		protected override IGenerator<Customer> GetGenerator()
		{
			return from code in codes
			       from name in names
			       select new Customer(code, name);
		}
	}
}