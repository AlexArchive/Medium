using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Medium.WebModel
{
    public class PostInput
    {
        [HiddenInput(DisplayValue = false)]
        public string Slug { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public bool Published { get; set; }
    }
}