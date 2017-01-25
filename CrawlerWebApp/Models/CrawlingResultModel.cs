
using System.Collections.Generic;

namespace CrawlerWebApp.Models
{
    public class Board
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public List<Theme> Themes { get; set; }

    }

    public class Theme
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public List<Thread> Threads { get; set; }
    }

    public class Thread
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public string Content { get; set; }
        public string AuthorName { get; set; }
    }
}