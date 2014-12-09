namespace Medium.WebModel
{
    public interface IAuthenticator
    {
        bool AuthenticationSuccessful { get; }
        void Authenticate(string username, string password);
        void Logout();
    }
}