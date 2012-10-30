using BuildUp.Builders;
using BuildUp.ValueGenerators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class CustomerBuilder : Builder<Customer,CustomerBuilder>
	{
		protected override Generator<Customer> GetDefaultGenerator()
		{
			return Generators.Create
			(
				(context, code, name) => new Customer(code, name),
				StringGenerators.Numbered("Customer-{1}"),
				Names.Default
			);
		}
	}
}