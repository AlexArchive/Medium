using System.ComponentModel.DataAnnotations;

namespace Blog.Models.AdminModel
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Passwords must be at least 6 characters.")]
        public string Password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
    }
}