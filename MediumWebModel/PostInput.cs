using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Medium.WebModel
{
    public class PostInput
    {
        [HiddenInput(DisplayValue = false)]
        public string Slug { get; set; }

        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public bool Published { get; set; }
    }
}