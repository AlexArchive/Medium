using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Medium.DomainModel
{
    public class ConfigurationModel
    {
        [DisplayName("Title")]
        public string BlogTitle { get; set; }

        [DisplayName("About")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string AboutText { get; set; }

        [DisplayName("Posts per page")]
        public int PageSize { get; set; }
    }
}