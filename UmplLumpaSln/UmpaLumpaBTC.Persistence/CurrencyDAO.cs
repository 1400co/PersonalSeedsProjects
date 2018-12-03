using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmpaLumpaBTC.Common.Coinmarketcap;

namespace UmpaLumpaBTC.Persistence
{

    public class CurrencyDAO : ICurrencyDAO
    {
        public CurrencyDAO()
        {

        }

        public BtcPrice InsertCurrency(BtcPrice btcPrice)
        {

            return new BtcPrice();
        }

        public BtcPrice GetCurrency(string currency)
        {

            return new BtcPrice();
        }
    }
}
