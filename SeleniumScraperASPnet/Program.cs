using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumScraperASPnet
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("--headless");
            IWebDriver driver = new ChromeDriver(option);

            driver.Navigate().GoToUrl("https://finance.yahoo.com/cryptocurrencies?offset=0&count=150");

            var headRows =
                driver.FindElements(
                    By.XPath("//*[@id=\"scr-res-table\"]/table/tbody/tr[not(position() >5)]/td[2]/a"));
            var symbols = new List<string>();
            foreach (IWebElement r in headRows)
                symbols.Add(r.Text);
            
            var headCols =
                driver.FindElements(
                    By.XPath("//*[@id=\"scr-res-table\"]/table/thead/tr/th[*]/span"));
            var properties = new List<string>();
            foreach (IWebElement c in headCols)
                properties.Add(c.Text);
        }
    }
}