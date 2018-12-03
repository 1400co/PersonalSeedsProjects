using UmpaLumpaBTC.Common.Coinmarketcap;

namespace UmpaLumpaBTC.Persistence
{
    public interface ICurrencyDAO
    {
        BtcPrice InsertCurrency(BtcPrice btcPrice);
        BtcPrice GetCurrency(string currency);
    }
}