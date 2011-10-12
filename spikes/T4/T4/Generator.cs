using System.Linq;
using System.Text;

namespace T4
{
	public class Generator
	{
		public static string GenerateConcatMethods()
        {
			var methods = new StringBuilder();
			for(int i = 1; i <= 10; i++)
			{
				var values = Enumerable.Range(0, i).ToArray();
				var args = string.Join(", ", values.Select(index => string.Format("string value{0}", index + 1)));
				var concatArgs = string.Join(", ", values.Select(index => string.Format("value{0}", index + 1)));
				methods.AppendFormat(@"
				public static string GetValue({0})
				{{
					return string.Concat({1});
				}}", args, concatArgs);
			}
			return methods.ToString();
        } 
	}
}