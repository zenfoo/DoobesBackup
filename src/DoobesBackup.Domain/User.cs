namespace DoobesBackup.Domain
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
