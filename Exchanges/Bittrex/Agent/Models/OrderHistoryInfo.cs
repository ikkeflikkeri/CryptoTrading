using System;

namespace CryptoTrading.Exchanges.Bittrex.Agent.Models
{
    public class OrderHistoryInfo
    {
        public string OrderUuid { get; set; }
        public string Exchange { get; set; }
        public DateTime TimeStamp { get; set; }
        public string OrderType { get; set; }
        public decimal Limit { get; set; }
        public decimal Quantity { get; set; }
        public decimal QuantityRemaining { get; set; }
        public decimal Commission { get; set; }
        public decimal Price { get; set; }
        public decimal? PricePerUnit { get; set; }
        public bool IsConditional { get; set; }
        public bool ImmediateOrCancel { get; set; }
    }
}
