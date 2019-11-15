using System;

namespace ExampleNancy.UsersModule.Views
{
    public class UsersView
    {
        public UsersView()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
