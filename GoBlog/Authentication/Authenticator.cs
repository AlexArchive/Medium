using System.Web;
using System.Web.Security;

namespace GoBlog.Authentication
{
    public class Authenticator : IAuthenticator
    {
        public bool Authenticated
        {
            get { return HttpContext.Current.User.Identity.IsAuthenticated; }
        }

        public bool Authenticate(string username, string password)
        {
            var authenticated = FormsAuthentication.Authenticate(username, password);
            if (authenticated)
                FormsAuthentication.SetAuthCookie(username, false);
            return authenticated;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}