using Nancy.Hosting.Self;
using Raven.Client.Documents;
using System;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace ExampleNancy
{
    class Program
    {


        private static readonly string Db = "SubscribersPart0";

        static void Main(string[] args)
        {

            ConfigureDatabase();

            HostConfiguration hostConfiguration = new HostConfiguration();
            hostConfiguration.UrlReservations.CreateAutomatically = true;

            using (var host = new NancyHost(new Uri("http://localhost:1070")))
            {
                
                host.Start();
                Console.WriteLine("running");
                Console.ReadKey();
            }
        }

        static void ConfigureDatabase()
        {
            using (IDocumentStore store = new DocumentStore
            {
                Urls = new[]                        // URL to the Server,
                {                                   // or list of URLs 
                    "http://127.0.0.1:8091"  // to all Cluster Servers (Nodes)
                },
                Database = "SubscribersPart0",             // Default database that DocumentStore will interact with
                Conventions = { }                   // DocumentStore customizations
            })
            {
                store.Initialize();                 // Each DocumentStore needs to be initialized before use.
                                                    // This process establishes the connection with the Server
                                                    // and downloads various configurations
                                                    // e.g. cluster topology or client configuration
                EnsureDatabaseExists(store, Db);
            }

        }


        static void EnsureDatabaseExists(IDocumentStore store, string database = null, bool createDatabaseIfNotExists = true)
        {
            database = database ?? store.Database;

            if (string.IsNullOrWhiteSpace(database))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(database));

            try
            {
                store.Maintenance.ForDatabase(database).Send(new GetStatisticsOperation());
            }
            catch (DatabaseDoesNotExistException)
            {
                if (createDatabaseIfNotExists == false)
                    throw;

                try
                {
                    store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(database)));
                }
                catch (ConcurrencyException)
                {
                    // The database was already created before calling CreateDatabaseOperation
                }

            }
        }

    }
}
