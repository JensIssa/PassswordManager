using Konscious.Security.Cryptography;
using System.IO;
using System.Text;

namespace PassswordManager.Services
{
    public static class MasterPasswordService
    {
        public static readonly string MasterPasswordFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "master_password.hash");


        public static void SetMasterPassword(string masterPassword)
        {
            var hashedPassword = HashPassword(masterPassword);
            File.WriteAllText(MasterPasswordFilePath, Convert.ToBase64String(hashedPassword));
        }

        public static bool VerifyMasterPassword(string inputPassword)
        {
            if (!File.Exists(MasterPasswordFilePath))
                return false;

            var storedHash = Convert.FromBase64String(File.ReadAllText(MasterPasswordFilePath));
            var inputHash = HashPassword(inputPassword);

            return storedHash.SequenceEqual(inputHash);
        }


        private static byte[] HashPassword(string password)
        {
            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                argon2.Salt = Encoding.UTF8.GetBytes("unique-master-salt");
                argon2.DegreeOfParallelism = 1;
                argon2.MemorySize = 19456;
                argon2.Iterations = 2;

                return argon2.GetBytes(32);
            }
        }
    }
}
