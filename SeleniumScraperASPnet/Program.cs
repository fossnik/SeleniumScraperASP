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
            
            List<Coin> snapshot = Selenium.Scraper.CompileSnapshot();

            foreach (var coinObject in snapshot)
                Console.WriteLine(coinObject.Name);

            Environment.Exit(0);
        }
    }
}