using BuildUp.ValueSources;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class CustomerBuilder : Builder<Customer,CustomerBuilder>
	{
		protected override Source<Customer> GetDefaultSource()
		{
			return Source.Create
			(
				(context, code, name) => new Customer(code, name),
				StringSources.Numbered("Customer-{1}"),
				Names.Default
			);
		}
	}
}