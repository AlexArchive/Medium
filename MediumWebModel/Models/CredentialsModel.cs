using System.ComponentModel.DataAnnotations;

namespace Medium.WebModel.Models
{
    public class CredentialsModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}