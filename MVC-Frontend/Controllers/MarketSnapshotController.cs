using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeleniumScraperASPnet.Model;

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
    }
}