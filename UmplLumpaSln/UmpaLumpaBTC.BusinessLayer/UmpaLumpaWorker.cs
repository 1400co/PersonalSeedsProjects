using System;
using System.Linq;
using UmpaLumpaBTC.Common;
using UmpaLumpaBTC.Common.Coinmarketcap;
using UmpaLumpaBTC.Persistence;

namespace UmpaLumpaBTC.BusinessLayer
{
    public class UmpaLumpaWorker
    {
        protected IMarketProvider _providers;
        protected ICurrencyDAO _currencyDAO;
        protected IStrategyDAO _strategyDAO;
        protected ITransactionLayer _transactionDAO;

        public UmpaLumpaWorker(IMarketProvider providers, ICurrencyDAO currencyDAO, IStrategyDAO strategyDAO, ITransactionLayer transactionDAO)
        {
            this._providers = providers;
            this._currencyDAO = currencyDAO;
            this._strategyDAO = strategyDAO;
            this._transactionDAO = transactionDAO;
        }

        public void UpdateCurrentBalance()
        {
            //step 1 Get current Value 
            var currentCoins = this._providers.GetCoinsBalance();
            var coin = currentCoins.FirstOrDefault(p => p.symbol == "XRP");
            if (coin == null)
            {
                return;
            }

            var strategy = _strategyDAO.GetCurrencyStrategy(coin.name);

            //Step 2 Ask for open Transactions
            if (_transactionDAO.IsOpenTransaction())
            {
                SmartSell(coin, strategy);
            }
            else
            {
                SmartBuyout(coin, strategy);
            }
            //step3 Be Happy
        }


        public void SmartBuyout(BtcPrice btcPrice, Strategy strategy)
        {
            if (Convert.ToDouble(btcPrice.price_btc) <= strategy.Floor)
            {
                //
                _providers.BuyCoins(btcPrice.name, Convert.ToDouble(btcPrice.price_btc), strategy.BtcBag);


            }
        }

        public void SmartSell(BtcPrice btcPrice, Strategy strategy)
        {
            var trans = _transactionDAO.GetTransaction();
            var btcCurrentPrice = Convert.ToDouble(btcPrice.price_btc);
            var btcSellPrice = (btcCurrentPrice * strategy.Discount);

            if (btcSellPrice < trans.BoughtPrice)
            {
                btcSellPrice = btcCurrentPrice;
            }

            if (btcCurrentPrice <= (strategy.Profit * btcCurrentPrice))
            {
                //Venda
                _providers.SellCoins(btcPrice.name, btcSellPrice, trans.BuyAmmount);
            }
        }


    }
}
