namespace GoBlog.Authentication
{
    public interface IAuthenticationService
    {
        bool Authenticated { get; }
        bool Authenticate(string username, string password);
        void Logout();
    }
}