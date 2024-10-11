using Microsoft.EntityFrameworkCore;
using PassswordManager.Models;
using PassswordManager.Repository.DBContext;
using PassswordManager.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassswordManager.Repository
{
    public class PasswordRepo: IPasswordRepo
    {
        private PasswordsContext _context;

        public PasswordRepo(PasswordsContext context) 
        {
            _context = context;
        }

        public async Task AddPassword(string siteName, string plainPassword, string masterPassword)
        {
            if (!string.IsNullOrEmpty(siteName) && !string.IsNullOrEmpty(plainPassword))
            {
                var passwordItem = new PasswordItem
                {
                    Name = siteName
                };
                passwordItem.SetPassword(plainPassword, masterPassword);

                _context.PasswordItems.Add(passwordItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Please fill in both fields");
            }
        }

        public async Task<IEnumerable<PasswordItem>> GetPasswords()
        {
            return await _context.PasswordItems.ToListAsync();
        }
    }
}
