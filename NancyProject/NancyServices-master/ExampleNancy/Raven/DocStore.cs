using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Session;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace ExampleNancy.Raven
{
    public class DocStore : IDocStore
    {

        private  IDocumentStore _documentStore;

        public DocStore(string uri, string databaseName)
        {
            using (_documentStore = new DocumentStore
            {
                Urls = new[] // URL to the Server,
                {
                    // or list of URLs 
                    uri // to all Cluster Servers (Nodes)
                },
                Database = databaseName, // Default database that DocumentStore will interact with
                Conventions = { } // DocumentStore customizations
            })
            {
                _documentStore.Initialize(); // Each DocumentStore needs to be initialized before use.
                // This process establishes the connection with the Server
                // and downloads various configurations
                // e.g. cluster topology or client configuration
                EnsureDatabaseExists(_documentStore, databaseName);
            }
        }

        public IDocumentSession GetSession()
        {
            return _documentStore.OpenSession();
        }

        private void EnsureDatabaseExists(IDocumentStore store, string database = null, bool createDatabaseIfNotExists = true)
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
