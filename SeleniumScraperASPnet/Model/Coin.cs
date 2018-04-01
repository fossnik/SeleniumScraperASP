namespace SeleniumScraperASPnet.Model
{
    public class Coin
    {
        public int ID { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double change { get; set; }
        public double pChange { get; set; }
        public double marketCap { get; set; }
        public double volume { get; set; }
        public double volume24h { get; set; }
        public double totalVolume24h { get; set; }
        public double circulatingSupply { get; set; }
    }
}