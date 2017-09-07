using CryptoTrading.Exchanges.Bittrex.Agent;
using System.Configuration;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bittrex = new BittrexAgent();
            bittrex.SetApiKey(ConfigurationManager.AppSettings["BittrexApiKey"], ConfigurationManager.AppSettings["BittrexApiSecret"]);

            var buyOrderId = bittrex.CreateBuyOrder("BTC-ETH", 0.1m, 0.01m).Result.Uuid;
            var sellOrderId = bittrex.CreateSellOrder("BTC-ETH", 0.1m, 1m).Result.Uuid;

            var openOrders = bittrex.GetOpenOrders("BTC-ETH");
            var order = bittrex.GetOrder(sellOrderId);

            bittrex.CancelOrder(buyOrderId);
            bittrex.CancelOrder(sellOrderId);

            var neoBalance = bittrex.GetBalance("NEO");
            var ethDepositAddress = bittrex.GetDepositAddress("ETH");

            var orderHistory = bittrex.GetOrderHistory("BTC-ETH");
            var withdrawalHistory = bittrex.GetWithdrawalHistory("ETH");
            var depositHistory = bittrex.GetDepositHistory("ETH");

            var ticks = bittrex.GetTicks("BTC-ETH", "thirtyMin");
        }
    }
}
