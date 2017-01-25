using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL.BusinessEntities
{
    public class BoardFoundRow
    {
        public string BoardName { get; set; }
        public string ThemeName { get; set; }
        public string ThreadName { get; set; }
        public string ThemeUrl { get; set; }
        public string ThreadUrl { get; set; }
    }
}
