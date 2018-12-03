using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UmpaLumpaBTC.Common;
using UmpaLumpaBTC.Common.Coinmarketcap;
using UmpaLumpaBTC.Persistence;
using UmpaLumpaBTC.Providers.Interface;

namespace UmpaLumpaBTC.BusinessLayer
{
    public class MarketProvider : IMarketProvider
    {
        protected IProviders _providers;
        protected ITransactionDAO _transactionDAO;
      
        public MarketProvider(IProviders providers, ITransactionDAO transactionDAO)
        {
            this._providers = providers;
            this._transactionDAO = transactionDAO;
        }

        public bool SellCoins(string currency, double price, double ammount)
        {
            try
            {
                _providers.SellCoins(currency, price, ammount);
                _transactionDAO.InsertTransaction(new Transaction() { });

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool BuyCoins(string currency, double  price, double totalBtcToBuy)
        {
            try
            {
                var ammount = totalBtcToBuy / price;
                _providers.BuyCoins(currency, totalBtcToBuy, ammount);
                _transactionDAO.CloseTransaction(new Transaction() { });

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public IEnumerable<BtcPrice> GetCoinsBalance()
        {
            return _providers.GetCoinsBalance();
        }

    }
}
