using BuildUp.Tests.ComplexModelExamples.Simple;
using BuildUp.ValueSources;

namespace BuildUp.Tests.BuilderExamples.Builders
{
	public class CustomerBuilder : Builder<Customer,CustomerBuilder>
	{
		protected override ICompositeSource<Customer> GetDefaultSource()
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