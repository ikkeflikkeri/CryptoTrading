using CryptoTrading.Exchanges.Bittrex.Agent.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using CryptoTrading.Exchanges.Bittrex.Agent.Models;
using CryptoTrading.Common.Models;

namespace CryptoTrading.Exchanges.Bittrex.Agent
{
    public class BittrexAgent : IBittrexAgent
    {
        private const string BittrexApiUri = "https://bittrex.com/api/v1.1/";

        private string ApiKey;
        private string ApiSecret;

        public void CancelOrder(string orderId)
        {
            GetApiCall<Response<string>>("market/cancel", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("uuid", orderId)
            }, true);
        }

        public CreateBuyOrderResponse CreateBuyOrder(string market, decimal quantity, decimal price)
        {
            return GetApiCall<CreateBuyOrderResponse>("market/buylimit", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("market", market),
                new KeyValuePair<string, string>("quantity", quantity.ToString(CultureInfo.InvariantCulture)),
                new KeyValuePair<string, string>("rate", price.ToString(CultureInfo.InvariantCulture))
            }, true);
        }

        public CreateSellOrderResponse CreateSellOrder(string market, decimal quantity, decimal price)
        {
            return GetApiCall<CreateSellOrderResponse>("market/selllimit", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("market", market),
                new KeyValuePair<string, string>("quantity", quantity.ToString(CultureInfo.InvariantCulture)),
                new KeyValuePair<string, string>("rate", price.ToString(CultureInfo.InvariantCulture))
            }, true);
        }

        public GetBalanceResponse GetBalance(string currency)
        {
            return GetApiCall<GetBalanceResponse>("account/getbalance", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("currency", currency)
            }, true);
        }

        public GetBalancesResponse GetBalances()
        {
            return GetApiCall<GetBalancesResponse>("account/getbalances", true);
        }

        public GetCurrenciesResponse GetCurrencies()
        {
            return GetApiCall<GetCurrenciesResponse>("public/getcurrencies");
        }

        public GetDepositAddressResponse GetDepositAddress(string currency)
        {
            return GetApiCall<GetDepositAddressResponse>("account/getdepositaddress", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("currency", currency)
            }, true);
        }

        public GetDepositHistoryResponse GetDepositHistory(string currency)
        {
            return GetApiCall<GetDepositHistoryResponse>("account/getdeposithistory", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("currency", currency)
            }, true);
        }

        public GetMarketOrderBookResponse GetMarketOrderBook(string market)
        {
            return GetApiCall<GetMarketOrderBookResponse>("public/getorderbook", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("market", market),
                new KeyValuePair<string, string>("type", "both")
            });
        }

        public GetMarketOrderHistoryResponse GetMarketOrderHistory(string market)
        {
            return GetApiCall<GetMarketOrderHistoryResponse>("public/getmarkethistory", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("market", market)
            });
        }

        public GetMarketsResponse GetMarkets()
        {
            return GetApiCall<GetMarketsResponse>("public/getmarkets");
        }

        public GetMarketSummariesResponse GetMarketSummaries()
        {
            return GetApiCall<GetMarketSummariesResponse>("public/getmarketsummaries");
        }

        public GetMarketSummaryResponse GetMarketSummary(string market)
        {
            return GetApiCall<GetMarketSummaryResponse>("public/getmarketsummary", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("market", market)
            });
        }

        public GetMarketTickerResponse GetMarketTicker(string market)
        {
            return GetApiCall<GetMarketTickerResponse>("public/getticker", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("market", market)
            });
        }

        public GetOpenOrdersResponse GetOpenOrders(string market)
        {
            return GetApiCall<GetOpenOrdersResponse>("market/getopenorders", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("market", market)
            }, true);
        }

        public GetOrderResponse GetOrder(string orderId)
        {
            return GetApiCall<GetOrderResponse>("account/getorder", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("uuid", orderId)
            }, true);
        }

        public GetOrderHistoryResponse GetOrderHistory(string market)
        {
            return GetApiCall<GetOrderHistoryResponse>("account/getorderhistory", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("market", market)
            }, true);
        }

        public GetWithdrawalHistoryResponse GetWithdrawalHistory(string currency)
        {
            return GetApiCall<GetWithdrawalHistoryResponse>("account/getwithdrawalhistory", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("currency", currency)
            }, true);
        }

        public void Withdraw(string currency, string address, decimal quantity)
        {
            GetApiCall<Response<string>>("account/withdraw", new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("currency", currency),
                new KeyValuePair<string, string>("quantity", quantity.ToString(CultureInfo.InvariantCulture)),
                new KeyValuePair<string, string>("address", address)
            }, true);
        }

        public void SetApiKey(string key, string secret)
        {
            ApiKey = key;
            ApiSecret = secret;
        }

        public List<MarketTick> GetTicks(string market, string interval)
        {
            string method = "pub/market/GetTicks?marketName=" + market + "&tickInterval=" + interval + "&_=" + DateTime.Now.Ticks.ToString();

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://bittrex.com/Api/v2.0/");

                    var response = client.GetAsync(method).Result;

                    if (response.IsSuccessStatusCode)
                        return response.Content.ReadAsAsync<GetTicksResponse>().Result.Result.Select(MapToMarketTick).ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return new List<MarketTick>();
                }
            }

            return new List<MarketTick>();
        }

        private MarketTick MapToMarketTick(GetTicksInfo tick)
        {
            return new MarketTick
            {
                Open = tick.O,
                Close = tick.C,
                High = tick.H,
                Low = tick.L,
                Time = tick.T,
                Volume = tick.V
            };
        }

        private T GetApiCall<T>(string method, bool isPrivateCall = false)
        {
            return GetApiCall<T>(method, new List<KeyValuePair<string, string>>(), isPrivateCall);
        }

        private T GetApiCall<T>(string method, List<KeyValuePair<string, string>> parameters, bool isPrivateCall = false)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BittrexApiUri);

                    if(isPrivateCall)
                    {
                        parameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
                        parameters.Add(new KeyValuePair<string, string>("nonce", DateTime.Now.Ticks.ToString()));
                    }

                    if(parameters != null)
                    {
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            method += (i == 0 ? "?" : "&") + parameters[i].Key + "=" + parameters[i].Value;
                        }
                    }

                    if (isPrivateCall)
                        client.DefaultRequestHeaders.Add("apisign", GetHmac(client.BaseAddress + method, ApiSecret));

                    var response = client.GetAsync(method).Result;

                    if (response.IsSuccessStatusCode)
                        return response.Content.ReadAsAsync<T>().Result;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return default(T);
                }
            }

            return default(T);
        }

        private string GetHmac(string uri, string secret)
        {
            var encoding = Encoding.UTF8;

            using (HMACSHA512 hmac = new HMACSHA512(encoding.GetBytes(secret)))
            {
                var msg = encoding.GetBytes(uri);
                var hash = hmac.ComputeHash(msg);
                return BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
            }
        }
    }
}
