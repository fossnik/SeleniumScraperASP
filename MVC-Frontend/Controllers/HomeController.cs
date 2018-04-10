using System.Web.Mvc;

namespace MVC_Frontend.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return
            View();
        }
    }
}