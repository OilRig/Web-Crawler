using System.Collections.Generic;

namespace CrawlerEngine
{
    public class Board
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public List<BoardTheme> Themes { get; set; }

        public Board(string bName, string bUrl)
        {
            Initialize(bName, bUrl);
        }
        public Board()
        {

        }

        public void Initialize(string bName, string bUrl)
        {
            Name = bName;
            Url = bUrl;
            Themes = new List<BoardTheme>();
        }

    }
}

