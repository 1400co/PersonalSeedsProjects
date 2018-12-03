using ServiceStack;
using System.Collections.Generic;
using UmpaLumpaBTC.Common.Coinmarketcap;
using UmpaLumpaBTC.Providers.Interface;

namespace UmpaLumpaBTC.Coinmarketcap.Client
{
    public class Coinmarketcap : IProviders
    {
        public Coinmarketcap()
        {
                
        }

        public IEnumerable<BtcPrice> GetCoinsBalance()
        {
            var client = new JsonServiceClient("https://api.coinmarketcap.com/v1/ticker/");
         
            return client.Get<IEnumerable<BtcPrice>>("");
        }

        public bool  SellCoins(string currency, double price, double ammount)
        {
            return true;
        }

        public bool  BuyCoins(string currency, double price, double ammount)
        {
            return true;
        }
    }
}
