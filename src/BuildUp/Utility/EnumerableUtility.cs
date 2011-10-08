using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BuildUp.Utility
{
	/// <summary>
	/// Generates a sequence of sets of values combining values from multiple enumerables
	/// </summary>
	internal static class EnumerableUtility
	{
		// I guess we could have methods like this to support different combinations
		// but let's keep it simple for now - boxing / unboxing probably not a problem
		private static IEnumerable<Tuple<T1,T2>> Tuplize<T1, T2>(IEnumerable<T1> first, IEnumerable<T2> second)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Generates a sequence of sets combining values from multiple enumerables
		/// </summary>
		/// <param name="sequences"></param>
		/// <returns></returns>
		public static IEnumerable<object[]> Tuplize(IEnumerable<IEnumerable> sequences)
		{
			var enumerators = sequences.Select(x => x.GetEnumerator()).ToArray();
			while (enumerators.All(x => x.MoveNext()))
			{
				yield return enumerators.Select(x => x.Current).ToArray();
			}
			// that's it!
		}
	}
}