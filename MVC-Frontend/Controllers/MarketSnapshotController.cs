using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC_Frontend.Models;
using MVC_Frontend.Models.Selenium;

namespace MVC_Frontend.Controllers
{
    public class MarketSnapshotController : Controller
    {
        // GET: MarketSnapshot
        public ActionResult Index()
        {
            using (var db = new SnapshotContext())
            {
                var query = from s in db.MarketSnapshots orderby s.SnapId select s;

                return View(query);
            }
        }

        public ActionResult CompileSnapshot()
        {
            List<Coin> snapshot;
            using (MarketSnapshot)
            {
                try
                {
                    snapshot = Scraper.CompileSnapshot();
                    Scraper.QuitWebDriver();
                }
                catch (Exception e)
                {
                    Scraper.QuitWebDriver();
                    Console.WriteLine(e);
                    throw;
                }
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

            return View(snapshot);
        }

        public IDisposable MarketSnapshot { get; set; }
    }
}