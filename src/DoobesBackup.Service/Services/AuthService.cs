namespace DoobesBackup.Service.Services
{
    using DoobesBackup.Domain;
    using DoobesBackup.Framework;
    using DoobesBackup.Infrastructure.Repositories;
    using System;
    using System.Threading.Tasks;

    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;

        public AuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        
        public async Task<User> Login(string userName, string password)
        {
            // Lookup the username
            var user = await this.userRepository.GetByUserName(userName);

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
            // This is a hash for "password"
            HashUtil.VerifySlowHash("notgoingtousethisvalue", "$2a$14$dX6l/KBxB/fIDcbEoHVuveqLB7kAX87yzrxvnoD3shF/gC8Ll28J2");
            return null;
        }

        /// <summary>
        /// Generate a JWT token
        /// </summary>
        /// <param name="authenticatedUser"></param>
        /// <returns></returns>
        public string GenerateUserToken(User authenticatedUser)
        {
            throw new NotImplementedException();
        }
    }
}
