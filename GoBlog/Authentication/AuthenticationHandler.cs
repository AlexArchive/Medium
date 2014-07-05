using System.Web.Security;

namespace GoBlog.Authentication
{
    public class AuthenticationHandler : IAuthenticationHandler
    {
        public bool Authenticate(string username, string password)
        {
            var authenticated = FormsAuthentication.Authenticate(username, password);
            if (authenticated)
                FormsAuthentication.SetAuthCookie(username, false);
            return authenticated;
        }
    }
}