using ExampleNancy.Raven;
using Nancy.Hosting.Self;
using System;
using StructureMap;


namespace ExampleNancy
{
    class Program
    {
        private static readonly string Db = "SubscribersPart0";
        static IContainer _container;

        static void Main(string[] args)
        {
            ConfigureContainer();
            ConfigureNancy();
        }

        private static void ConfigureContainer()
        {
            _container = new Container();

            _container.Configure(cfg =>
            {
               
            });
        }

        static void ConfigureNancy()
        {
            HostConfiguration hostConfiguration = new HostConfiguration();
            hostConfiguration.UrlReservations.CreateAutomatically = true;

            using (var host = new NancyHost(new Uri("http://localhost:1070")))
            {

                host.Start();
                Console.WriteLine("Nancy server Running.");
                Console.ReadKey();
            }
        }



    }
}
