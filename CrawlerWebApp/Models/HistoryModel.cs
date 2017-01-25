using System.Collections.Generic;

namespace CrawlerWebApp.Models
{
    public class DbBoard
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public List<DbTheme> Themes { get; set; }

    }

    public class DbTheme
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public List<DbThread> Threads { get; set; }
    }

    public class DbThread
    {
        public string Name { get; set; }
        public string ThemeName { get; set; }
        public string Url { get; set; }
        public List<DbPost> Posts { get; set; }
    }

    public class DbPost
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public string ThreadName { get; set; }
        public string ThemeName { get; set; }
        public string FirstSymbols
        {
            get
            {
                return Content.Substring(0, 150);
            }
        }
    }

    public class DbUser
    {
        public string Name { get; set; }
        public List<DbPost> Posts { get; set; }
    }
}