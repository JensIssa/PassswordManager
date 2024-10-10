using Microsoft.EntityFrameworkCore;
using PassswordManager.Models;
using System.IO;

namespace PassswordManager.Data
{
    public class PasswordsContext : DbContext
    {
        public DbSet<PasswordItem> PasswordItems { get; set; }

        private string _dbPath;

        public PasswordsContext()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _dbPath = Path.Combine(folder, "passwords.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }
    }
}
