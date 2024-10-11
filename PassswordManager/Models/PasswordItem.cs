using PassswordManager.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
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

        private bool _isPasswordVisible = false;

        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set
            {
                if (_isPasswordVisible != value)
                {
                    _isPasswordVisible = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(EyeIcon));
                }
            }
        }

        /// <summary>
        /// Eye icon for the password visibility
        /// </summary>
        public string EyeIcon => IsPasswordVisible ? "eye_open_icon.png" : "eye_closed_icon.png";


        /// <summary>
        /// Event for property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Set the password for the item with the given master password and plain password
        /// </summary>
        /// <param name="plainPassword"></param>
        /// <param name="masterPassword"></param>
        public void SetPassword(string plainPassword, string masterPassword)
        {
            Salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
            var saltBytes = Convert.FromBase64String(Salt);
            EncryptedPassword = EncryptionService.Encrypt(plainPassword, masterPassword, saltBytes);
        }

        /// <summary>
        /// Get the password for the item with the given master password
        /// </summary>
        /// <param name="masterPassword"></param>
        /// <returns></returns>
        public string GetPassword(string masterPassword)
        {
            var saltBytes = Convert.FromBase64String(Salt);
            return EncryptionService.Decrypt(EncryptedPassword, masterPassword, saltBytes);
        }
    }
}
