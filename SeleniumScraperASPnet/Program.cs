using System;
using System.Collections.Generic;
using SeleniumScraperASPnet.Model;

namespace SeleniumScraperASPnet
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            List<Coin> snapshot = Selenium.Capture.CompileSnapshot();

            Environment.Exit(0);
        }
    }
}