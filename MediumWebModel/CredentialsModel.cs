using System.ComponentModel.DataAnnotations;

namespace Medium.WebModel
{
    public class CredentialsModel
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}