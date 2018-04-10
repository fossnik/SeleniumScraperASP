using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Frontend.Models
{
    public class MarketSnapshot
    {
        [Key]
        public int SnapId { get; set; }
        public DateTime SnapTime { get; set; }

        public virtual List<Coin> Coins { get; set; }
    }
}