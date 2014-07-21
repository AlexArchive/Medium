using System.ComponentModel.DataAnnotations;

namespace GoBlog.Areas.Admin.Models
{
    public class PostInputModel
    {
        [Editable(false)]
        public string Slug { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public bool Draft { get; set; }
    }
}