using System.Web.Mvc;

namespace CrawlerWebApp
{
    public static class HtmlHelpers
    {

        public static string SimpleLink(this HtmlHelper html, string url, string text, string target = "_self")
        {
            return string.Format("<a href=\"{0}\" target=\"{1}\">{2}</a>", url, target, text);
        }

    }
}