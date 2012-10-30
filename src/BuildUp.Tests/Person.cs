namespace BuildUp.Tests
{
	public class Person
	{
		public Person(string name, int age)
		{
			Name = name;
			Age = age;
		}

		public string Name { get; private set; }

		public int Age { get; private set; }

		public string FavouriteColour { get; set; }

		public void ChangeName(string name)
		{
			Name = name;
		}
	}
}