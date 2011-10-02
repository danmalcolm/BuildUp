using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BuildUp.Tests.Common
{
	public static class CollectionAssertionExtensions
	{
        /// <summary>
        /// Asserts that exactly one element in a sequence matches the supplied predicate.
        /// </summary>
        public static void ShouldContainSingle<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            var items = collection.Where(x => predicate(x));
            if(items.Count() == 0)
            {
                Assert.Fail("No matching element was found in the collection");
            }
            if(items.Count() > 1)
            {
                Assert.Fail("More than one matching element was found in the collection");
            }
        }

		public static void ShouldNotContain(this IList collection, object expected)
		{
			CollectionAssert.DoesNotContain(collection, expected);
		}

		public static void ShouldMatchSequence<T>(this IEnumerable<T> collection, IEnumerable<T> expected)
		{
			CollectionAssert.AreEqual(expected.ToList(), collection.ToList());
		}

        public static void ShouldMatchSequence<T>(this IEnumerable<T> collection, params T[] expected)
        {
            CollectionAssert.AreEqual(expected.ToList(), collection.ToList());
        }

		public static void ShouldContainSameElementsInAnyOrder<T>(this IEnumerable<T> collection, IEnumerable<T> expected)
		{
			CollectionAssert.AreEquivalent(expected.ToList(), collection.ToList());
		}

		public static void ShouldContainSameElementsInAnyOrder(this IEnumerable collection, IEnumerable expected)
		{
			var actualList = new ArrayList();
			actualList.AddRange(collection.Cast<object>().ToArray());
			var expectedList = new ArrayList();
			expectedList.AddRange(expected.Cast<object>().ToArray());
			CollectionAssert.AreEquivalent(expectedList, actualList);
		}

		public static void ShouldBeEmpty<T>(this IEnumerable<T> collection)
		{
			CollectionAssert.IsEmpty(collection);
		}
		
	}
}
