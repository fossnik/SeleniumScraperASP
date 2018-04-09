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
            
            List<Coin> snapshot = Selenium.Scraper.CompileSnapshot();

            using (var db = new SnapshotContext())
            {
                // build dbContext object from the list of coin objects procured via selenium
                var marketSnapshot = new MarketSnapshot {Coins = snapshot};

                // append to database
                db.MarketSnapshots.Add(marketSnapshot);
                db.SaveChanges();

                var query = from b in db.MarketSnapshots orderby b.SnapId select b;
                foreach (var marketSnapshot1 in query)
                {
                    Console.WriteLine(marketSnapshot1.SnapId);
                }
            }

            Environment.Exit(0);
        }
    }
}