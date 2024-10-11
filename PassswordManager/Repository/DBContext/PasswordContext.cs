using Microsoft.EntityFrameworkCore;
using PassswordManager.Models;
using System.Reflection;

namespace PassswordManager.Repository.DBContext
{
    public class PasswordsContext : DbContext
    {
        public PasswordsContext(DbContextOptions<PasswordsContext> options)
            : base(options)
        {
        }

        public DbSet<PasswordItem> PasswordItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
