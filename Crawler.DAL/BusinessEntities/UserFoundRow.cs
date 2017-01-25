using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL.BusinessEntities
{
    public class UserFoundRow
    {
        public string Name { get; set; }
        public string PostContent { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string ThemeName { get; set; }
        public string ThreadName { get; set; }
    }
}
