using System;
using System.Collections.Generic;
using SeleniumScraperASPnet.Model;

namespace SeleniumScraperASPnet
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // Linux Environment:
            // please note that it is necessary to set the environment varible "TERM" to "xterm"
            try
            {
                List<Coin> snapshot = Selenium.Scraper.CompileSnapshot();

                Console.WriteLine("Scraped " + snapshot.Count + " coins\n");
                foreach (var coinObject in snapshot)
                    Console.Write(coinObject.Name);

                Selenium.Scraper.QuitWebDriver();
            }
            catch (Exception e)
            {
                Selenium.Scraper.QuitWebDriver();
                Console.WriteLine(e);
                throw;
            }


            Environment.Exit(0);
        }
    }
}