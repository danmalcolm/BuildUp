namespace BuildUp.Tests.ComplexModelExamples.Simple
{
	public class Hotel
	{
		public Hotel(string code, string name)
		{
			Code = code;
			Name = name;
		}

		public string Code { get; private set; }

		public string Name { get; private set; }
	}
}