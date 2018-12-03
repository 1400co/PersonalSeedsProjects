using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmpaLumpaBTC.Common;
using UmpaLumpaBTC.Persistence;

namespace UmpaLumpaBTC.BusinessLayer
{
    public class TransactionLayer : ITransactionLayer
    {
        protected ITransactionDAO _transactionDao;

        public TransactionLayer(ITransactionDAO transactionDao)
        {
            _transactionDao = transactionDao;
        }

        public Boolean IsOpenTransaction()
        {
            return _transactionDao.GetTransaction().SellPrice > 0;
        }

        public Transaction GetTransaction()
        {

            return _transactionDao.GetTransaction();
        }
    }
}
