using BuildUp.Builders;
using BuildUp.ValueGenerators;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class CustomerBuilder : BuilderBase<Customer,CustomerBuilder>
	{
		protected override IGenerator<Customer> GetDefaultGenerator()
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