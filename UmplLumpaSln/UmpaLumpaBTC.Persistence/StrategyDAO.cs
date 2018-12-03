using UmpaLumpaBTC.Common;

namespace UmpaLumpaBTC.Persistence
{
    public class StrategyDAO : IStrategyDAO
    {
        public Strategy InsertCurrency(Strategy btcPrice)
        {

            return new Strategy();
        }

        public Strategy GetCurrencyStrategy(string currency)
        {

            return new Strategy() { Floor = 0.000091, Profit = 1.003 };
        }
    }
}
