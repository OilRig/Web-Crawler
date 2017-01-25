using System.Collections.Generic;

namespace CrawlerEngine
{
    public class BoardTheme
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public List<BoardThread> Threads;


        public BoardTheme()
        {
            Threads = new List<BoardThread>();
        }

    }
}