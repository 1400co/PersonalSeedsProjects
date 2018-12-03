using UmpaLumpaBTC.Common;

namespace UmpaLumpaBTC.Persistence
{
    public interface IStrategyDAO
    {
        Strategy InsertCurrency(Strategy btcPrice);
        Strategy GetCurrencyStrategy(string currency);
    }
}