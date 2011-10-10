namespace BuildUp.Tests
{
	public class LittleMan
	{
		public LittleMan(string name, int age)
		{
			Name = name;
			Age = age;
		}

		public string Name { get; private set; }

		public int Age { get; private set; }

		public void ChangeName(string name)
		{
			Name = name;
		}
	}
}