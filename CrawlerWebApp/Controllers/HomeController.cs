using Crawler.Logging.Intefaces;
using System;
using System.Web.Mvc;

namespace CrawlerWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppLogger _logger;
        public HomeController(IAppLogger logger)
        {
            _logger = logger;
        }

        //GET:Nome/Index
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HttpError404()
        {
            Exception exception = Server.GetLastError();

            _logger.LogError(exception, "Page not found");

            Server.ClearError();
            return View();
        }
        public ActionResult HttpError500()
        {
            Exception exception = Server.GetLastError();

            _logger.LogError(exception, "Internal error");

            Server.ClearError();
            return View();
        }
    }
}