using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crawler.DAL.Entities
{
    public class ThreadPost
    {
        [Key]
        public int PostId { get; set; }


        public string PostContent { get; set; }

        public string AuthorName { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("BoardThread")]
        public int ThreadId { get; set; }
        public virtual BoardThread BoardThread { get; set; }
    }
}