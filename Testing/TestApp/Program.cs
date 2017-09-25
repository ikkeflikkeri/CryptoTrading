using CryptoTrading.Calculations.Indicators;
using CryptoTrading.Exchanges.Bittrex.Agent;
using System;
using System.Configuration;
using System.Linq;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bittrex = new BittrexAgent();
            bittrex.SetApiKey(ConfigurationManager.AppSettings["BittrexApiKey"], ConfigurationManager.AppSettings["BittrexApiSecret"]);

            var markets = bittrex.GetMarkets().Result.Where(m => m.MarketName.Contains("ETH-") || m.MarketName.Contains("BTC-"));

            foreach (var market in markets)
            {
                var result = bittrex.GetMarketSummary(market.MarketName);

                if (result == null)
                    continue;

                var volume = result.Result[0].BaseVolume;

                if (volume < 250)
                    continue;

                var ticks = bittrex.GetTicks(market.MarketName, "thirtyMin");

                var rsi = new RsiIndicator().Calculate(ticks);

                if(rsi >= 70 || rsi < 30)
                    Console.WriteLine(market.MarketName + " - " + rsi);
            }
        }
    }
}
