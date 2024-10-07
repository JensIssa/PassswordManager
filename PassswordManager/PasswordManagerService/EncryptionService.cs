using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

public static class EncryptionService
{
    private static readonly byte[] salt = Encoding.UTF8.GetBytes("some-random-salt");

    private static byte[] DeriveKey(string password, byte[] salt)
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

    public static string Encrypt(string plainText, string masterPassword)
    {
        using (Aes aesAlg = Aes.Create())
        {
            var key = DeriveKey(masterPassword, salt);
            aesAlg.Key = key;
            aesAlg.IV = new byte[16];

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    public static string Decrypt(string cipherText, string masterPassword)
    {
        using (Aes aesAlg = Aes.Create())
        {
            var key = DeriveKey(masterPassword, salt);
            aesAlg.Key = key;
            aesAlg.IV = new byte[16];

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
