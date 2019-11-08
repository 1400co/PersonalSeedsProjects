using Nancy.Hosting.Self;
using Raven.Client.Documents;
using System;
using ExampleNancy.Raven;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace ExampleNancy
{
    class Program
    {
        private static readonly string Db = "SubscribersPart0";
        static IUnityContainer _container;

        static void Main(string[] args)
        {
            ConfigureContainer();
        
            ConfigureNancy();

        }

        private static void ConfigureContainer()
        {
            _container = new UnityContainer();

            _container.RegisterInstance(new DocStore("http://127.0.0.1:8091",Db));

           
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
