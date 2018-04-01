using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using SeleniumScraperASPnet.Model;

namespace SeleniumScraperASPnet.Selenium
{
    internal class Extractor
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
                coin.symbol = coinsProperties["Symbol"];
                coin.name = coinsProperties["Name"];
                //this.price = double.Parse(coinsProperties[@"Price (Intraday)"].Replace(",", ""));
                coin.change = double.Parse(coinsProperties["Change"].Replace("[%,]", ""));
                //this.pChange = double.Parse(coinsProperties["% Change"].Replace("[%,]", ""));
                coin.marketCap = ParseMagnitude(coinsProperties["Market Cap"]);
                coin.volume = ParseMagnitude(coinsProperties["Volume in Currency (Since 0:00 UTC)"]);
                coin.volume24h = ParseMagnitude(coinsProperties["Volume in Currency (24Hr)"]);
                coin.totalVolume24h = ParseMagnitude(coinsProperties["Total Volume All Currencies (24Hr)"]);
                coin.circulatingSupply = ParseMagnitude(coinsProperties["Circulating Supply"]);
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