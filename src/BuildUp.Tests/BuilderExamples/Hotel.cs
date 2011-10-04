namespace BuildUp.Tests.BuilderExamples
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

		public override string ToString()
		{
			return string.Format("Code: {0}, Name: {1}", Code, Name);
		}
	}
}