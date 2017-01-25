using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crawler.DAL.Entities
{
    public class BoardThread
    {
        [Key]
        public int ThreadId { get; set; }
        public string Subject { get; set; }

        [StringLength(255)]
        [Index(IsUnique = true)]
        public string Url { get; set; }

        [ForeignKey("BoardTheme")]
        public int ThemeId { get; set; }
        public virtual BoardTheme BoardTheme { get; set; }


        public virtual ICollection<ThreadPost> ThreadPosts { get; set; }

    }
}