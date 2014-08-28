namespace GoBlog.Authentication
{
    public interface IAuthenticator
    {
        bool Authenticated { get; }
        bool Authenticate(string username, string password);
        void Logout();
    }
}