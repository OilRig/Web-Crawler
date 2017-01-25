using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crawler.DAL.Entities
{
    public class Board
    {
        [Key]
        public int BoardId { get; set; }
        public string Name { get; set; }

        [StringLength(255)]
        [Index(IsUnique = true)]
        public string Url { get; set; }


        public virtual ICollection<BoardTheme> BoardThemes { get; set; }
    }
}