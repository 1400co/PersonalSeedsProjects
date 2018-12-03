using System;
using System.Collections.Generic;
using UmpaLumpaBTC.Common.Coinmarketcap;

namespace UmpaLumpaBTC.BusinessLayer
{
    public interface IMarketProvider
    {
        bool SellCoins(string currency, double price, double ammount);
        bool BuyCoins(string currency, double price, double ammount);
        IEnumerable<BtcPrice> GetCoinsBalance();
    }
}