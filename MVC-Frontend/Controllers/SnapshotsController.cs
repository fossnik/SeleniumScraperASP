using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC_Frontend.Models;
using MVC_Frontend.Models.Selenium;

namespace MVC_Frontend.Controllers
{
    public class SnapshotsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SnapshotsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: All MarketSnapshot
        public ViewResult Index()
        {
            var snapshots = _context.MarketSnapshots.ToList();

            return View(snapshots);
        }

        // Run Selenium
        public ActionResult CompileSnapshot()
        {
            List<Coin> snapshot;
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

            using (var db = new ApplicationDbContext())
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
    }
}