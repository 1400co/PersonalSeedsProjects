using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmpaLumpaBTC.Common
{
   public class Transaction
    {
        public int Id { get; set; }
        /// <summary>
        /// Name of the currency to buy
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Bitcoins at wallet.
        /// </summary>
        public double BtcAtWallet { get; set; }
        /// <summary>
        /// Ammount of coins Bought
        /// </summary>
        public double BuyAmmount { get; set; }
        /// <summary>
        /// Price of single coin.
        /// </summary>
        public double BoughtPrice { get; set; }
        /// <summary>
        /// total price of coins.
        /// </summary>
        public double TotalBoughtPrice { get; set; }
        /// <summary>
        /// date of bought.
        /// </summary>
        public DateTime BuyTime { get; set; }
        /// <summary>
        /// Percent to determine the sell price.
        /// </summary>
        public double Percent { get; set; }
        /// <summary>
        /// base sell price calculated using Percent. 
        /// </summary>
        public double BaseSellPrice { get; set; }
        /// <summary>
        /// real sell price, if real price is above BaseSellPrice.
        /// </summary>
        public double SellPrice { get; set; }
        /// <summary>
        /// Diference Between SellPrice and TotalBoughtPrice
        /// </summary>
        public double Increase { get; set; }
        /// <summary>
        /// date of Tx Closed.
        /// </summary>
        public DateTime SellTime { get; set; }

    }
}
