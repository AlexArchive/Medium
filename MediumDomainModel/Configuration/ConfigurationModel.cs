using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Medium.DomainModel.Configuration
{
    public class ConfigurationModel
    {
        [DisplayName("Title")]
        [Required]
        public string BlogTitle { get; set; }

        [DisplayName("About")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Required]
        public string AboutText { get; set; }

        [DisplayName("Posts per page")]
        [Required]
        [Range(0, 50)]
        public int PageSize { get; set; }
    }
}