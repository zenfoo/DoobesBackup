namespace DoobesBackup.Service.Services
{
    using DoobesBackup.Domain;

    public interface IAuthService
    {
        User Login(string username, string password);
    }
}
