using System.ComponentModel.DataAnnotations;

namespace GoBlog.Areas.Admin.Models
{
    public class PostInputModel
    {
        // Concern: Seems like the intuitive choice. My hope is that this attribute will stop the user from
        // editing the slug using a HTTP tool.
        [Editable(false)]
        public string Slug { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}