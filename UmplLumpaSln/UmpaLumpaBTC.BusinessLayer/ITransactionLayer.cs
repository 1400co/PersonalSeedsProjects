using System;
using UmpaLumpaBTC.Common;

namespace UmpaLumpaBTC.BusinessLayer
{
    public interface ITransactionLayer
    {
        Boolean IsOpenTransaction();
        Transaction GetTransaction();
    }
}