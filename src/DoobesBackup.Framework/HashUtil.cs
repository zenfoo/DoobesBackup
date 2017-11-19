namespace DoobesBackup.Framework
{
    using System;

    public class HashUtil
    {
        /// <summary>
        /// Generate a hash for the password
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SlowHash(string input)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(input, 14);
        }

        public static bool VerifySlowHash(string password, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
        }

        public static string Hash(string input)
        {
            throw new NotImplementedException();
        }
    }
}
