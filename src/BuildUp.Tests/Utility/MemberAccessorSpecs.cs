using System;
using System.Linq.Expressions;
using System.Reflection;
using BuildUp.Utility.Reflection;
using FluentAssertions;
using Xunit;

namespace BuildUp.Tests.Utility
{
	
	public class MemberAccessorSpecs
	{
		public class A
		{
			public string SettableProperty { get; set; }
		}


		[Fact]
		public void accessor_for_settable_property()
		{
			var accessor = MemberAccessor.For((A a) => a.SettableProperty);
			var instance = new A();
			accessor.SetValue(instance, "Something");

			instance.SettableProperty.Should().Be("Something");
		}

	}
}