using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassswordManager.Models
{
    public class PasswordItem
    {
        public string Name { get; set; }
        public string EncryptedPassword { get; set; }

        // Decrypt the password before displaying it
        public string GetDecryptedPassword()
        {
            return EncryptionService.Decrypt(EncryptedPassword);
        }

        // Encrypt the password before storing it
        public void SetPassword(string plainPassword)
        {
            EncryptedPassword = EncryptionService.Encrypt(plainPassword);
        }
    }

}
