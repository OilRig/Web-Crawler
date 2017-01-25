using System.Collections.Generic;

namespace CrawlerEngine
{
    public class BoardThread
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public List<ThreadPost> PostsList { get; set; }

        public BoardThread()
        {
            PostsList = new List<ThreadPost>();
        }
    }
}
