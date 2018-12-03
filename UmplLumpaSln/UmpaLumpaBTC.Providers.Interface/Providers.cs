using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmpaLumpaBTC.Common.Coinmarketcap;

namespace UmpaLumpaBTC.Providers.Interface
{
    public interface IProviders
    {
        IEnumerable<BtcPrice> GetCoinsBalance();
        bool SellCoins(string currency, double price, double ammount);
        bool BuyCoins(string currency, double price, double ammount);
    }
}
