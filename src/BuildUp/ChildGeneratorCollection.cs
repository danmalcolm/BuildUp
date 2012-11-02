using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildUp
{
	/// <summary>
	/// A collection of child generators that provide values used by a parent generator to generate objects
	/// </summary>
	public class ChildGeneratorCollection
	{
		public static readonly ChildGeneratorCollection Empty = new ChildGeneratorCollection();

		private readonly IGenerator[] childGenerators;

		public int Count{ get { return childGenerators.Length; }}

		internal ChildGeneratorCollection(params IGenerator[] childGenerators)
		{
			this.childGenerators = childGenerators;
		}

		/// <summary>
		/// Creates a copy of this ChildGeneratorCollection replacing an item at the specified position
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="index"></param>
		/// <param name="generator"></param>
		/// <returns></returns>
		public ChildGeneratorCollection ReplaceAt<T>(int index, IGenerator<T> generator)
		{
			if (index > childGenerators.Length)
			{
				throw new IndexOutOfRangeException("A child generator does not exist at the specified index and cannot be replaced");
			}
			return Clone(x =>
			{
				x[index] = generator;
				return x;
			});
		}

		/// <summary>
		/// Creates a copy of this ChildGeneratorCollection with an additional generator at the end of the collection
		/// </summary>
		/// <param name="sequence"></param>
		/// <returns></returns>
		public ChildGeneratorCollection Add(IGenerator sequence)
		{
			return Clone(x => x.Concat(new [] { sequence }));
		}

		private ChildGeneratorCollection Clone(Func<IGenerator[], IEnumerable<IGenerator>> changeValues)
		{
			var newGenerators = changeValues(childGenerators.ToArray()).ToArray();
			return new ChildGeneratorCollection(newGenerators);
		}

		/// <summary>
		/// Iterates through the child generators, yielding a sequence of arrays (tuples), each containing
		/// a value from each sequence.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<object[]> Tuplize()
		{
			var enumerators = childGenerators.Select(x => x.Build().GetEnumerator()).ToArray();
			while (enumerators.All(x => x.MoveNext()))
			{
				yield return enumerators.Select(x => x.Current).ToArray();
			}
		}
	}
}