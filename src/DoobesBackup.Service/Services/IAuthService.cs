namespace DoobesBackup.Service.Services
{
    using DoobesBackup.Domain;
    using System.Threading.Tasks;

    public interface IAuthService
    {
        Task<User> Login(string username, string password);
        string GenerateUserToken(User authenticatedUser);
    }
}
