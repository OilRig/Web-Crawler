using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crawler.DAL.Entities
{
    public class BoardTheme
    {
        [Key]
        public int ThemeId { get; set; }

        public string Subject { get; set; }

        [StringLength(255)]
        [Index(IsUnique = true)]
        public string Url { get; set; }


        [ForeignKey("Board")]
        public int BoardId { get; set; }
        public virtual Board Board { get; set; }

        public virtual ICollection<BoardThread> BoardThreads { get; set; }
    }
}
