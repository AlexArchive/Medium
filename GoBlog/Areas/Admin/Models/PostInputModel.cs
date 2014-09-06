using System.ComponentModel.DataAnnotations;

namespace GoBlog.Areas.Admin.Models
{
    public class PostInputModel
    {
        [ScaffoldColumn(false)]
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Draft { get; set; }
    }
}