using System.Web.Mvc;
using System.Web.Routing;

namespace CrawlerWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //users/posts?filter
            routes.MapRoute(
                "UserActivityInfo",
                "Users/Posts/{userName}",
                new
                { controller = "History", action = "UserActivityInfo" }
            );

            routes.MapRoute(
                 "SchemaByBoardName",
                 "Boards/{boardName}",
                new { controller = "History", action = "SchemaByBoardName" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
