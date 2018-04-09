using System;
using System.Collections.Generic;

namespace SeleniumScraperASPnet.Model
{
    public class MarketSnapshot
    {
        public int SnapId { get; set; }
        public DateTime SnapTime { get; set; }

        public virtual List<Coin> Coins { get; set; }
    }
}