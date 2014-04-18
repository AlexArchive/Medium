using System.Web.Security;

namespace Blog.Core.Security
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        public bool Authenticate(string username, string password)
        {
            var authenticated = FormsAuthentication.Authenticate(username, password);

            if (authenticated)
                FormsAuthentication.SetAuthCookie(username, true);

            return authenticated;
        }
    }
}