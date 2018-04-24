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
        private readonly MarketSnapshotContext _context;

        public SnapshotsController()
        {
            _context = new MarketSnapshotContext();
        }

        // GET: All MarketSnapshot
        public ViewResult Index()
        {
            var snapshots = _context.MarketSnapshots.ToList();

            return View(snapshots);
        }

        public ActionResult Details(int id)
        {
            var snapshot = _context.MarketSnapshots.SingleOrDefault(c => c.SnapId == id);

            if (snapshot == null)
                return HttpNotFound();

            return View(snapshot);
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

            using (var db = new MarketSnapshotContext())
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