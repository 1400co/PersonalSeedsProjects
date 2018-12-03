using UmpaLumpaBTC.Common;

namespace UmpaLumpaBTC.Persistence
{
    public interface ITransactionDAO
    {
        Transaction InsertTransaction(Transaction btcPrice);
        Transaction CloseTransaction(Transaction btcPrice);
        Transaction GetTransaction();
    }
}