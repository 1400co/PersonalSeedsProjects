using ExampleNancy.Raven;
using ExampleNancy.UsersModule.Views;
using Nancy;

namespace ExampleNancy.modules
{
    public class UserModule : NancyModule
    {
        //private IDocStore _docStore;

        //public UserModule(IDocStore docStore)
        //{
        //    _docStore = docStore;

        //    Get("/usuarios/{Id}", parameters =>
        //    {
        //        var wallet = new UsersView()
        //        {
        //            FirstName = "Oscar",
        //            LastName = "Rueda",
        //            UserId = "oscar.rueda@gmail.com"
        //        };

        //        return wallet;
        //    });
        //}
    }
}
