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

        // New properties
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

        public string EyeIcon => IsPasswordVisible ? "eye_open_icon.png" : "eye_closed_icon.png";

        // Event for property changes
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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
