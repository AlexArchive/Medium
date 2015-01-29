using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Medium.DomainModel
{
    public class ConfigurationModel
    {
        public string BlogTitle { get; set; }

        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string AboutText { get; set; } 
    }
}