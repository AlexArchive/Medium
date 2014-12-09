using System.Web.Security;

namespace Medium.WebModel
{
    public class Authenticator : IAuthenticator
    {
        public bool AuthenticationSuccessful { get; private set; }
        
        public void Authenticate(string username, string password)
        {
            AuthenticationSuccessful = 
                FormsAuthentication.Authenticate(username, password);
            if (AuthenticationSuccessful)
            {
                FormsAuthentication.SetAuthCookie(username, true);
            }
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}