using System;
using System.Linq.Expressions;
using System.Reflection;
using BuildUp.Utility.Reflection;
using NUnit.Framework;

namespace BuildUp.Tests.Utility
{
	[TestFixture]
	public class MemberAccessorSpecs
	{
		public class A
		{
			public string SettableProperty { get; set; }
		}


		[Test]
		public void accessor_for_settable_property()
		{
			var accessor = MemberAccessor.For((A a) => a.SettableProperty);
			var instance = new A();
			accessor.SetValue(instance, "Something");
			Assert.AreEqual("Something", instance.SettableProperty);
		}

	}
}