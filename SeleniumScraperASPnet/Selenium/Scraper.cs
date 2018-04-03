using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumScraperASPnet.Model;

namespace SeleniumScraperASPnet.Selenium
{
    internal static class Scraper
    {
        private static readonly IWebDriver WebDriver;

        static Scraper()
        {
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("--headless");
            WebDriver = new ChromeDriver(option);
        }

        public static List<Coin> CompileSnapshot()
        {
            WebDriver.Navigate().GoToUrl("https://finance.yahoo.com/cryptocurrencies?offset=0&count=150");

            var (symbols, properties) = GetTableHeads();

            var coins = new List<Coin>();

            // create a list of all coins (with respective properties)
            for (var i = 1; i < symbols.Count + 1; i++)
            {
                var xPath = "//*[@id=\"scr-res-table\"]/table/tbody/tr[" +
                            i + "]/td[position() >= 2 and not(position() > 11)]";

                var results = WebDriver.FindElements(By.XPath(xPath));

                coins.Add(Extractor.ParseCoin(properties, results));
            }

            return coins;
        }

        private static (List<string> symbols, List<string> properties) GetTableHeads()
        {
            // acquire the stock symbols (vertical table header)
            var symbols = Symbols;

            // acquire the stock properties (horizontal table header)
            var properties = Properties;

            return (symbols, properties);
        }

        private static List<string> Properties
        {
            get
            {
                var headCols =
                    WebDriver.FindElements(
                        By.XPath("//*[@id=\"scr-res-table\"]/table/thead/tr/th[*]/span"));
                var properties = new List<string>();
                foreach (IWebElement c in headCols)
                    properties.Add(c.Text);
                return properties;
            }
        }

        private static List<string> Symbols
        {
            get
            {
                var headRows =
                    WebDriver.FindElements(
                        By.XPath("//*[@id=\"scr-res-table\"]/table/tbody/tr[*]/td[2]/a"));
                var symbols = new List<string>();
                foreach (IWebElement r in headRows)
                    symbols.Add(r.Text);
                return symbols;
            }
        }
    }
}