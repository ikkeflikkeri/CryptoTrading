using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrading.Exchanges.Bittrex.Agent.Models
{
    public class MarketOrderHistoryInfo
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string FillType { get; set; }
        public string OrderType { get; set; }
    }
}
