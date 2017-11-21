namespace DoobesBackup.Service.Services
{
    using DoobesBackup.Domain;
    using DoobesBackup.Framework;
    using DoobesBackup.Infrastructure.Repositories;
    using DoobesBackup.Service.Configuration;
    using System;
    using System.Threading.Tasks;
    using System.IdentityModel.Tokens;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;

    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;
        private readonly AppConfiguration appConfig;

        public AuthService(AppConfiguration appConfig, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.appConfig = appConfig;
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

            RSAParameters keyParams;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    keyParams = rsa.ExportParameters(true);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            RsaSecurityKey key = new RsaSecurityKey(keyParams);

            var signingKey = this.GetSigningKey();

            var signingCreds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, authenticatedUser.UserName),
                new Claim(ClaimTypes.Role, "Administrator")
            }, "Custom");

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claimsIdentity,
                SigningCredentials = signingCreds,
                Issuer = "http://tokenissuer.doobes.com",
                Audience = "http://backup.doobes.com"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
            var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);
            return signedAndEncodedToken;
        }

        public bool ValidateUserToken(string token, out IEnumerable<Claim> claims)
        {
            claims = Enumerable.Empty<Claim>(); // Default response

            var tokenValidationParams = new TokenValidationParameters()
            {
                ValidAudiences = new string[]
                {
                    "http://backup.doobes.com"
                },
                ValidIssuers = new string[]
                {
                    "http://tokenissuer.doobes.com"
                },
                IssuerSigningKey = this.GetSigningKey()
            };

            SecurityToken validatedToken;
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out validatedToken);

                // Assert that the role claim is defined
                AssertThat.IsTrue(principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role && c.Value == "Administrator") != null);
                claims = principal.Claims;
                return true;
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch (Exception)
            {
                // TODO: log
                throw;
            }
        }

        private SecurityKey GetSigningKey()
        {
            return new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appConfig.Auth.SigningSecret));
        }
    }
}
