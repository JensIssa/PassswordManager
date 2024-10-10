using PassswordManager.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace PassswordManager.Models
{
    public class PasswordItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EncryptedPassword { get; set; }

        [Required]
        public string Salt { get; set; }

        public void SetPassword(string plainPassword, string masterPassword)
        {
            Salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
            var saltBytes = Convert.FromBase64String(Salt);
            EncryptedPassword = EncryptionService.Encrypt(plainPassword, masterPassword, saltBytes);
        }

        public string GetPassword(string masterPassword)
        {
            var saltBytes = Convert.FromBase64String(Salt);
            return EncryptionService.Decrypt(EncryptedPassword, masterPassword, saltBytes);
        }
    }
}
