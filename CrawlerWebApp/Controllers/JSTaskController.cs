using CrawlerWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrawlerWebApp.Controllers
{
    public class JSTaskController : Controller
    {
        // GET: JSTask
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public JsonResult Example1()
        {
            var jsModel = new JSTaskModel();
            jsModel.Name = "Peter";
            jsModel.Age = 20;
            return Json(jsModel,JsonRequestBehavior.AllowGet);
        }
    }
}