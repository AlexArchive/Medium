namespace GoBlog.Authentication
{
    public interface IAuthenticator
    {
        bool Authenticated { get; }
        void Authenticate(string username, string password);
        void Logout();
    }
}