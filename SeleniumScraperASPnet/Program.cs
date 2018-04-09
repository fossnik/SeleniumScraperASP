using System;
using System.Collections.Generic;
using System.Linq;
using SeleniumScraperASPnet.Model;

namespace SeleniumScraperASPnet
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // Linux Environment:
            // please note that it is necessary to set the environment varible "TERM" to "xterm"

            List<Coin> snapshot;
            try
            {
                snapshot = Selenium.Scraper.CompileSnapshot();

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

            using (var db = new SnapshotContext())
            {
                // build dbContext object from the list of coin objects procured via selenium
                var marketSnapshot = new MarketSnapshot
                {
                    Coins = snapshot,
                    SnapTime = DateTime.Now
                };

                // append to database
                db.MarketSnapshots.Add(marketSnapshot);
                db.SaveChanges();
            }

            Environment.Exit(0);
        }
    }
}