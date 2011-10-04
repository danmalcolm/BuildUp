namespace BuildUp.Tests.BuilderExamples
{
	public class Name
	{
		public Name(string title, string firstName, string lastName)
		{
			Title = title;
			FirstName = firstName;
			LastName = lastName;
		}

		public string Title { get; private set; } 
		public string FirstName { get; private set; } 
		public string LastName { get; private set; }

		public override string ToString()
		{
			return string.Format("Title: {0}, FirstName: {1}, LastName: {2}", Title, FirstName, LastName);
		}
	}
}