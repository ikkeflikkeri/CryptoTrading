using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrading.Exchanges.Bittrex.Agent.Models
{
    public class OrderBookInfo
    {
        public List<OrderBookItem> Buy { get; set; }
        public List<OrderBookItem> Sell { get; set; }
    }
}
