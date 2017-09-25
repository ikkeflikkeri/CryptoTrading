using CryptoTrading.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrading.Calculations.Indicators
{
    public class RsiIndicator
    {
        public decimal Calculate(List<MarketTick> ticks, int length = 14)
        {
            if (ticks.Count < length + 1)
                return decimal.MinValue;

            var orderedTicks = ticks.OrderBy(t => t.Time).ToList();

            decimal avgGain = 0;
            decimal avgLoss = 0;

            for (int i = ticks.Count - length; i < ticks.Count; i++)
            {
                var change = orderedTicks[i].Close - orderedTicks[i - 1].Close;

                if (change > 0)
                    avgGain += change;
                else
                    avgLoss -= change;
            }

            avgGain /= length;
            avgLoss /= length;

            return 100 - 100 / (1 + avgGain / avgLoss);
        }
    }
}
