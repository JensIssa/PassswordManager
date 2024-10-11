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
        Task AddPassword(string siteName, string plainPassword, string masterPassword);
        Task<IEnumerable<PasswordItem>> GetPasswords();
    }
}
