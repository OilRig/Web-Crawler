using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CrawlerWebApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["StartTime"] = DateTime.Now;
            Application["Requests"] = 0;
        }

        protected void Session_Start()
        {
            Session["StartTime"] = DateTime.Now.ToLongTimeString();
            Session["BrowserName"] = Request.Browser.Browser;
            Session["BrowserVersion"] = Request.Browser.Version;

        }
        protected void Application_BeginRequest()
        {
            Application["CurrentRequestTime"] = DateTime.Now.ToLongTimeString();
            var counter = (int)Application["Requests"];
            counter++;
            Application["Requests"] = counter;

        }
        protected void Application_EndRequest()
        {

        }

        protected void Application_End()
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            HttpException httpException = exception as HttpException;

            if (httpException != null)
            {
                string action;

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        action = "HttpError404";
                        break;
                    case 500:
                        action = "HttpError500";
                        break;
                    default:
                        action = "General";
                        break;
                }

                Response.Redirect(string.Format("~/Home/{0}", action));
            }
        }
    }
}
