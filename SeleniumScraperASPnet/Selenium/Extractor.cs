using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using SeleniumScraperASPnet.Model;

namespace SeleniumScraperASPnet.Selenium
{
    internal static class Extractor
    {
        public static Coin ParseCoin(List<string> properties, ReadOnlyCollection<IWebElement> values)
        {
            Coin coin = new Coin();
            var coinsProperties = new Dictionary<string, string>();

            for (var index = 0; index < values.Count; index++)
            {
                coinsProperties[properties[index]] = values[index].Text;
            }

            try
            {
                coin.Symbol = coinsProperties["Symbol"];
                coin.Name = coinsProperties["Name"];
                coin.Price = double.Parse(coinsProperties["Price (Intraday)"]);
                coin.Change = double.Parse(coinsProperties["Change"].Replace("%", ""));
                coin.PChange = double.Parse(coinsProperties["% Change"].Replace("%", ""));
                coin.MarketCap = ParseMagnitude(coinsProperties["Market Cap"]);
                coin.Volume = ParseMagnitude(coinsProperties["Volume in Currency (Since 0:00 UTC)"]);
                coin.Volume24H = ParseMagnitude(coinsProperties["Volume in Currency (24Hr)"]);
                coin.TotalVolume24H = ParseMagnitude(coinsProperties["Total Volume All Currencies (24Hr)"]);
                coin.CirculatingSupply = ParseMagnitude(coinsProperties["Circulating Supply"]);
            }
            catch
            {
                throw new Exception("Values Collation Error: Could not find Value");
            }

            return coin;
        }
        
        private static double ParseMagnitude(string s)
        {
            var input = Regex.Replace(s, "[^0-9.MBT]", "");

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
                    try
                    {
                        return double.Parse(input);
                    }
                    catch
                    {
                        throw new Exception("Magnitude Conversion Failure");                        
                    }
            }
        }
    }
}