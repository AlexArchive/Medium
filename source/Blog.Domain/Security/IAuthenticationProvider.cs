namespace Blog.Domain.Security
{
    public interface IAuthenticationProvider
    {
        bool Authenticate(string username, string password);
        void SignOut();
    }
}
