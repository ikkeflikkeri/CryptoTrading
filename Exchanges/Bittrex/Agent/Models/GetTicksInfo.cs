using System;

namespace CryptoTrading.Exchanges.Bittrex.Agent.Models
{
    public class GetTicksInfo
    {
        public decimal BV { get; set; } //?
        public decimal C { get; set; } //Current
        public decimal H { get; set; } //High
        public decimal L { get; set; } //Low
        public decimal O { get; set; } //?
        public DateTime T { get; set; } //Time
        public decimal V { get; set; } //Volume
    }
}
