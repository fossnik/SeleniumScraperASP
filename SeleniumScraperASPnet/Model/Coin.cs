using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace SeleniumScraperASPnet.Model
{
    internal class Coin
    {
        public int ID { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public double price { get; }
        public double change { get; set; }
        public double pChange { get; }
        public double marketCap { get; set; }
        public double volume { get; set; }
        public double volume24h { get; set; }
        public double totalVolume24h { get; set; }
        public double circulatingSupply { get; set; }
        
        public Coin(List<string> properties, ReadOnlyCollection<IWebElement> values)
        {
            var coinsProperties = new Dictionary<string, string>();

            for (var index = 0; index < values.Count; index++)
            {
                coinsProperties[properties[index]] = values[index].Text;
            }

            try
            {
                this.symbol = coinsProperties["Symbol"];
                this.name = coinsProperties["Name"];
                //this.price = double.Parse(coinsProperties[@"Price (Intraday)"].Replace(",", ""));
                this.change = double.Parse(coinsProperties["Change"].Replace("[%,]", ""));
                //this.pChange = double.Parse(coinsProperties["% Change"].Replace("[%,]", ""));
                this.marketCap = ParseMagnitude(coinsProperties["Market Cap"]);
                this.volume = ParseMagnitude(coinsProperties["Volume in Currency (Since 0:00 UTC)"]);
                this.volume24h = ParseMagnitude(coinsProperties["Volume in Currency (24Hr)"]);
                this.totalVolume24h = ParseMagnitude(coinsProperties["Total Volume All Currencies (24Hr)"]);
                this.circulatingSupply = ParseMagnitude(coinsProperties["Circulating Supply"]);
            }
            catch
            {
                throw new Exception("Values Collation Error: Could not find Value");
            }
        }
        private static double ParseMagnitude(string s)
        {
            var input = Regex.Replace(s, "[^0-9.MBT]", "");
            Regex regex = new Regex(@"[\\D.]+");
            Match match = regex.Match(input);

            // M B and T for Millions, Billions, and Trillions. (eg 142.43B	=== 142,000,000,000)
            switch (input[(input.Length - 1)])
            {
                case 'M':
                    return double.Parse(input.Replace("M", "")) * 1000000;
                case 'B':
                    return double.Parse(input.Replace("B", "")) * 1000000000;
                case 'T':
                    return double.Parse(input.Replace("T", "")) * 1000000000000;
                default:
                    if (match.Success)
                        throw new Exception("Magnitude Conversion Failure - Invalid Non-digit Characters");
                    else
                        return double.Parse(input);
            }
        }
    }
}