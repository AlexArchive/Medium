namespace GoBlog.Authentication
{
    public interface IAuthenticationHandler
    {
        bool Authenticate(string username, string password);
    }
}