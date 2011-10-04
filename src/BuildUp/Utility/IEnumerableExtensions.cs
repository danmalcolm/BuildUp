using System;
using System.Collections.Generic;

namespace BuildUp.Utility
{
	internal static class IEnumerableExtensions
	{
		 public static void Each<T>(this IEnumerable<T> sequence, Action<T> action)
		 {
			foreach (var item in sequence)
			{
				action(item);
			}
		 }

		 public static void EachWithIndex<T>(this IEnumerable<T> sequence, Action<T,int> action)
		 {
		 	int index = 0;
			 foreach (var item in sequence)
			 {
				 action(item, index);
			 	index++;
			 }
		 }
	}
}