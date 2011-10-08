using BuildUp.ValueSources;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class CustomerBuilder : Builder<Customer,CustomerBuilder>
	{
		protected override CompositeSource<Customer> GetDefaultSource()
		{
			return CompositeSource.Create
			(
				(context, code, name) => new Customer(code, name),
				StringSources.Numbered("Customer-{0}"),
				Names.Default
			);
		}
	}
}