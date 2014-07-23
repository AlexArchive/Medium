using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoBlog.Persistence.Entities
{
    public class Tag
    {
        [Key]
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}