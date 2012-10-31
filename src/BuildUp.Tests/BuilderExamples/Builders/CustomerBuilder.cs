using BuildUp.Builders;
using BuildUp.ValueGenerators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class CustomerBuilder : Builder<Customer,CustomerBuilder>
	{
		protected override ComplexGenerator<Customer> GetDefaultGenerator()
		{
			return Generator.Create
			(
				(context, code, name) => new Customer(code, name),
				StringGenerators.Numbered("Customer-{1}"),
				Names.Default
			);
		}
	}
}