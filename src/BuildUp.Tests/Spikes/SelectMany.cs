using System;
using System.Collections.Generic;
using System.Linq;
using BuildUp.Tests.Common;
using BuildUp.ValueGenerators;
using NUnit.Framework;

namespace BuildUp.Tests.Spikes
{
	[TestFixture]
	public class SelectMany
	{
		public class A
		{
			public A(string name)
			{
				Name = name;
			}

			public string Name { get; private set; }


			public TResult SelectMany<TIntermediate, TResult>(Func<A,TIntermediate> project,
			                                                  Func<A, TIntermediate, TResult> select)
			{
				TIntermediate second = project(this);
				TResult result = select(this, second);
				return result;
			}
		}

		public class B
		{
			public B(int index)
			{
				Index = index;
			}

			public int Index { get; private set; }
		}

		public class TestResult
		{
			public readonly int Index;
			public readonly string Name;

			public TestResult(string name, int index)
			{
				Name = name;
				Index = index;
			}
		}

		[Test]
		public void CustomSelectManyUsingChain()
		{
			var a1 = new A("Dave");
			var a2 = new B(0);

			var result = a1.SelectMany(a => a2, (b, c) => new TestResult(a1.Name, a2.Index));
		}

		[Test]
		public void CustomSelectManyUsingQuery()
		{
			var a1 = new A("Dave");
			var b2 = new B(0);


			var result = from a in a1
			             from b in b2
			             select new TestResult(a.Name, b.Index);
		}

		[Test]
		public void CustomSelectManyWithIEnumerableUsingQueryComprehensionSyntax()
		{
//			var a1 = new TestClass1();
//			var a2 = Enumerable.Range(0, 10);
//			var result = from a in a1
//						 from b in a2
//						 select new TestClass4 { Name = a.Name, Index = b };
		}

		[Test]
		public void GeneratorSelectMany()
		{
			var names = StringGenerators.Numbered("Item {1}");
			var indexes = IntGenerators.Incrementing(0);

			var result = names.SelectMany(x => indexes, (name, index) => new TestResult(name, index));

			result.ShouldBeOfType<IGenerator<TestResult>>();
		}

		[Test]
		public void GeneratorSelectManyAsQuery()
		{
			var names = StringGenerators.Numbered("Item {1}");
			var indexes = IntGenerators.Incrementing(0);

			IGenerator<TestResult> generator = from name in names
			             from index in indexes
			             select new TestResult(name, index);
			
			var results = generator.Take(3);
		}
	}
}