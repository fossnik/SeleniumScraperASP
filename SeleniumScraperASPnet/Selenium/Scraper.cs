using System;
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
            option.AddArguments("--headless", "--disable-javascript");
            WebDriver = new ChromeDriver(option);
        }

        public static List<Coin> CompileSnapshot()
        {
            const string url = "https://finance.yahoo.com/cryptocurrencies?offset=0&count=150";

            WebDriver.Navigate().GoToUrl(url);

            if (WebDriver.Title == string.Empty)
                throw new Exception("[Null Page Title] Probable Page-Access Fault\n- " + url + " -\n");

            var (symbols, properties) = GetTableHeads();

            var coins = new List<Coin>();

            // create a list of all coins (with respective properties)
            for (var row = 1; row < symbols.Count + 1; row++)
            {
                var xPath = "//*[@id=\"scr-res-table\"]/table/tbody/tr[" +
                            row + "]/td[position() >= 2 and not(position() > 11)]";

                var results = WebDriver.FindElements(By.XPath(xPath));

                coins.Add(Extractor.ParseCoin(properties, results));
            }

            return coins;
        }

        public static void QuitWebDriver()
        {
            WebDriver.Quit();
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