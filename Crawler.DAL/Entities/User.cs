using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Crawler.DAL.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ThreadPost> ThreadPosts { get; set; }
    }
}