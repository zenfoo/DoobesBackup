namespace DoobesBackup.Service.Services
{
    using DoobesBackup.Domain;
    using DoobesBackup.Framework;
    using DoobesBackup.Infrastructure.Repositories;

    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;

        public AuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        
        public User Login(string userName, string password)
        {
            // Lookup the username
            var user = this.userRepository.GetByUserName(userName);

            // Verify the user exists and authenticate
            if (user != null)
            {
                // Hash the password using the salt from the user and check if they are the same
                if (HashUtil.VerifySlowHash(password, user.PasswordHash))
                {
                    return user;
                }

                return null;
            }

            // If the user does not exist then fake a password verification so we are less susceptible to a timing attack
            HashUtil.VerifySlowHash("notgoingtousethisvalue", "thisisabadhashvalue");
            return null;
        }
    }
}
