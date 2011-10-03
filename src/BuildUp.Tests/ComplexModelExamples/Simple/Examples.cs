using System.Linq;
using BuildUp.Tests.ComplexModelExamples.Simple.Sources;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests.ComplexModelExamples.Simple
{
	[TestFixture]
	public class Examples
	{
		[Test]
		public void default_ctor_arg_sources_used_by_composite_source()
		{
			var customers = TestObjects.Customers.Take(3);
			var expected = new[]
			{
				new {Code = "customer-1", Name = "Customer Number 1"},
				new {Code = "customer-2", Name = "Customer Number 2"},
				new {Code = "customer-3", Name = "Customer Number 3"},
			};
			customers.Select(x => new { x.Code, x.Name}).ShouldMatchSequence(expected);
		}

		[Test]
		public void changing_ctor_arg_sources_used_by_composite_source()
		{
			var source = TestObjects.Customers.WithCode(StringSources.FormatWithItemNumber("super-customer-{0}"));
			var customers = source.Take(3);
			var expected = new[]
			{
				new {Code = "super-customer-1", Name = "Customer Number 1"},
				new {Code = "super-customer-2", Name = "Customer Number 2"},
				new {Code = "super-customer-3", Name = "Customer Number 3"},
			};
			customers.Select(x => new { x.Code, x.Name }).ShouldMatchSequence(expected);
		}
	}
}