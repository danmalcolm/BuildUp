using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BuildUp.Utility.Reflection
{

	public class MemberAccessor
	{
		public static MemberAccessor<TObject, TMember> For<TObject, TMember>(Expression<Func<TObject, TMember>> expression)
		{
			var memberExpression = expression.Body as MemberExpression;
			if (memberExpression == null)
				throw new InvalidOperationException("The expression does not represent access to a field or property");
			return new MemberAccessor<TObject, TMember>((PropertyInfo)memberExpression.Member);
		}
	}
	// TODO: Improve efficiency by: caching accessors, using compiled expression trees to get / set
	// Priority right now is best API for IGenerators
	public class MemberAccessor<TObject, TMember>
	{
		private readonly PropertyInfo property;

		public MemberAccessor(PropertyInfo property)
		{
			this.property = property;
		}
		
		public void SetValue(TObject target, TMember value)
		{
			property.SetValue(target, value, null);
		}
	}
}