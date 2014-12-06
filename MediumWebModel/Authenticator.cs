using System.Web.Security;

namespace Medium.WebModel
{
    public class Authenticator
    {
        public void Authenticate(string username, string password)
        {
            AuthenticationSuccessful = FormsAuthentication.Authenticate(
                username, password);
            if (AuthenticationSuccessful)
            {
                FormsAuthentication.SetAuthCookie(username, true);
            }
        }

        public bool AuthenticationSuccessful { get; set; }
    }
}