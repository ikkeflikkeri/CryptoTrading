using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrading.Exchanges.Bittrex.Agent.Models
{
    public class OrderBookItem
    {
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
    }
}
