﻿namespace MVC_Frontend.Models
{
    public class Coin
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Change { get; set; }
        public double PChange { get; set; }
        public double MarketCap { get; set; }
        public double Volume { get; set; }
        public double Volume24H { get; set; }
        public double TotalVolume24H { get; set; }
        public double CirculatingSupply { get; set; }

        // navigation properties
        public int SnapId { get; set; }
        public virtual MarketSnapshot MarketSnapshot { get; set; }
    }
}