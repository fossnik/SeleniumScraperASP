using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeleniumScraperASPnet.Model
{
    public class MarketSnapshot
    {
        [Key]
        public int SnapId { get; set; }
        public DateTime SnapTime { get; set; }

        public virtual List<Coin> Coins { get; set; }
    }
}