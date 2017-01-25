using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrawlerWebApp.Startup))]
namespace CrawlerWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
