using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class EncryptionService
{
    private static readonly string encryptionKey = "RANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOMRANDOM";
    public static string Encrypt(string plainText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            var key = Encoding.UTF8.GetBytes(encryptionKey);
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

    public static string Decrypt(string cipherText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            var key = Encoding.UTF8.GetBytes(encryptionKey);
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
