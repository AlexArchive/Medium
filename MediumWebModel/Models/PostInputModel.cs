using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Medium.WebModel.Models
{
    public class PostInputModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Slug { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Required(ErrorMessage = "You must specify at least one tag.")]
        public string Tags { get; set; }

        public bool Published { get; set; }
    }
}