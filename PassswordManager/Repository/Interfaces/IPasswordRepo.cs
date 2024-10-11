using PassswordManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassswordManager.Repository.Interfaces
{
    public interface IPasswordRepo
    {
        /// <summary>
        /// Adds a password to the database
        /// </summary>
        /// <param name="siteName">Site of the name the password belongs to</param>
        /// <param name="plainPassword">The plain password</param>
        /// <param name="masterPassword">The master key where the encryption gets derived from</param>
        /// <returns></returns>
        Task AddPassword(string siteName, string plainPassword, string masterPassword);

        /// <summary>
        /// Gets the passwords from the database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PasswordItem>> GetPasswords();
    }
}
