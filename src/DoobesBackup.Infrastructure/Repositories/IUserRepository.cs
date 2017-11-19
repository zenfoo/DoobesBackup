namespace DoobesBackup.Infrastructure.Repositories
{
    using DoobesBackup.Domain;

    public interface IUserRepository : IRepository<User>
    {
        User GetByUserName(string userName);
    }
}
