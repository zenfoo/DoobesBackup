namespace DoobesBackup.Infrastructure.Repositories
{
    using DoobesBackup.Domain;
    using System.Threading.Tasks;

    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUserName(string userName);
    }
}
