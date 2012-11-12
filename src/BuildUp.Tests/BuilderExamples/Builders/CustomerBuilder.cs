using BuildUp.ValueGenerators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class CustomerBuilder : BuilderBase<Customer,CustomerBuilder>
	{
		private IGenerator<string> codes = StringGenerators.Numbered("Customer-{1}");
		private IGenerator<Name> names = Names.Default;

		protected override IGenerator<Customer> GetGenerator()
		{
			return from code in codes
			       from name in names
			       select new Customer(code, name);
		}

		// alternative creation of complex generator
		protected IGenerator<Customer> GetGenerator2()
		{
			return Generator.Create((code, name) => new Customer(code, name), codes, names);
		}

	}
}