using Raven.Client.Documents.Session;

namespace ExampleNancy.Raven
{
    public interface IDocStore
    {
        IDocumentSession GetSession();
    }
}