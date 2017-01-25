using System.Web.Mvc;

namespace CrawlerWebApp.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            ViewBag.StartTime = HttpContext.Application["StartTime"];
            ViewBag.BrowserName = Session["BrowserName"];
            ViewBag.BrowserVersion = Session["BrowserVersion"];
            ViewBag.CurrentRequestTime = HttpContext.Application["CurrentRequestTime"];
            ViewBag.RequestsAmount = HttpContext.Application["Requests"];
            ViewBag.SessionStart = Session["StartTime"];
            return View();
        }
    }
}