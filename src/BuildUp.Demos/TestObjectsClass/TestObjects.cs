using BuildUp.Generators;

namespace BuildUp.Demos.TestObjectsClass
{
    public class TestObjects
    {
        static TestObjects()
        {
            Users = from userName in StringGenerator.Numbered("user-{1}")
                    from displayName in StringGenerator.Numbered("User {1}")
                    select new User
                    {
                        UserName = userName,
                        DisplayName = displayName
                    };
        }

        public static readonly IGenerator<User> Users;
    }
}