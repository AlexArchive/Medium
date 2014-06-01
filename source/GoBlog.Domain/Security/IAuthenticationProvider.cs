namespace GoBlog.Domain.Security
{
    public interface IAuthenticationProvider
    {
        bool Authenticate(string username, string password);
        void SignOut();
    }
}
