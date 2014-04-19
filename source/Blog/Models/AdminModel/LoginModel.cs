using System.ComponentModel.DataAnnotations;

namespace Blog.Models.AdminModel
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}