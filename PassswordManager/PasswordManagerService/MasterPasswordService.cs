using Konscious.Security.Cryptography;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PassswordManager.Services
{
    public static class MasterPasswordService
    {
        public static readonly string MasterPasswordFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "master_password.hash");


        /// <summary>
        /// Hashes and stores the master password in a file along with the salt. 
        /// </summary>
        /// <param name="masterPassword">The master password</param>
        public static void SetMasterPassword(string masterPassword)
        {
            var salt = GenerateSalt();

            var hashedPassword = HashPassword(masterPassword, salt);

            var combinedHash = Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hashedPassword);
            File.WriteAllText(MasterPasswordFilePath, combinedHash);
        }

        /// <summary>
        /// Verifies the master password by comparing the hash of the input password with the stored hash.
        /// </summary>
        /// <param name="inputPassword"></param>
        /// <returns>True if the input password matches the stored hash; otherwise, false</returns>
        public static bool VerifyMasterPassword(string inputPassword)
        {
            if (!File.Exists(MasterPasswordFilePath))
                return false;

            var storedHashData = File.ReadAllText(MasterPasswordFilePath).Split(':');

            if (storedHashData.Length != 2)
                return false;

            var storedSalt = Convert.FromBase64String(storedHashData[0]);
            var storedHash = Convert.FromBase64String(storedHashData[1]);

            var inputHash = HashPassword(inputPassword, storedSalt);

            return storedHash.SequenceEqual(inputHash);
        }

        /// <summary>
        /// Hashes the password using argon2id with the recommended parameters. from OWASP: https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html
        /// </summary>
        /// <param name="password">Password being hashed</param>
        /// <param name="salt">Random generated salt</param>
        /// <returns></returns>
        private static byte[] HashPassword(string password, byte[] salt)
        {
            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                argon2.Salt = salt;
                argon2.DegreeOfParallelism = 1;
                argon2.MemorySize = 19456;
                argon2.Iterations = 2;

                return argon2.GetBytes(32);
            }
        }

        /// <summary>
        /// Generates a random saltes with 16 bytes.
        /// </summary>
        /// <returns></returns>
        private static byte[] GenerateSalt()
        {
            var salt = new byte[16];
            RandomNumberGenerator.Fill(salt);
            return salt;
        }

    }
}
