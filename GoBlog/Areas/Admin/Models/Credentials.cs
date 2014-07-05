using System.ComponentModel.DataAnnotations;

namespace GoBlog.Areas.Admin.Models
{
    public class CredentialsModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}