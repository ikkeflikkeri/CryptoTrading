using CryptoTrading.Exchanges.Bittrex.Agent.Responses;

namespace CryptoTrading.Exchanges.Bittrex.Agent
{
    interface IBittrexAgent
    {
        GetMarketsResponse GetMarkets();
        GetCurrenciesResponse GetCurrencies();
        GetMarketTickerResponse GetMarketTicker(string market);
        GetMarketSummariesResponse GetMarketSummaries();
        GetMarketSummaryResponse GetMarketSummary(string market);
        GetMarketOrderBookResponse GetMarketOrderBook(string market);
        GetMarketOrderHistoryResponse GetMarketOrderHistory(string market);
        CreateBuyOrderResponse CreateBuyOrder(string market, decimal quantity, decimal price);
        CreateSellOrderResponse CreateSellOrder(string market, decimal quantity, decimal price);
        void CancelOrder(string orderId);
        GetOpenOrdersResponse GetOpenOrders(string market);
        GetBalancesResponse GetBalances();
        GetBalanceResponse GetBalance(string currency);
        GetDepositAddressResponse GetDepositAddress(string currency);
        void Withdraw(string currency, string address, decimal quantity);
        GetOrderResponse GetOrder(string orderId);
        GetOrderHistoryResponse GetOrderHistory(string market);
        GetWithdrawalHistoryResponse GetWithdrawalHistory(string currency);
        GetDepositHistoryResponse GetDepositHistory(string currency);
        void SetApiKey(string key, string secret);
        GetTicksResponse GetTicks(string market, string interval);
    }
}