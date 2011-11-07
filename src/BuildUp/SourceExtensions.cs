using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BuildUp.Utility.Reflection;

namespace BuildUp
{
	public static class SourceExtensions
	{
		/// <summary>
		/// Sets the member specified by the expression to a given value
		/// </summary>
		/// <typeparam name="TObject"></typeparam>
		/// <typeparam name="TMember"></typeparam>
		/// <param name="source"></param>
		/// <param name="expression"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static ISource<TObject> Set<TObject,TMember>(this ISource<TObject> source, Expression<Func<TObject,TMember>> expression, TMember value)
		{
			var accessor = MemberAccessor.For(expression);
			Action<TObject> set = x => accessor.SetValue(x, value);
			return source.Select(set);
		}

		/// <summary>
		/// Sets the member specified by the expression using values from a sequence
		/// </summary>
		/// <typeparam name="TObject"></typeparam>
		/// <typeparam name="TMember"></typeparam>
		/// <param name="source"></param>
		/// <param name="expression"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public static ISource<TObject> Set<TObject, TMember>(this ISource<TObject> source, Expression<Func<TObject, TMember>> expression, ISource<TMember> values)
		{
			var accessor = MemberAccessor.For(expression);
			return source.SelectMany(s => values, (@object, value) => accessor.SetValue(@object, value));
		}
	}
}