using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassswordManager.Repository.DBContext
{
    /// <summary>
    /// Factory for setting up the DB. MAUI complained about the PasswordsContext not having a parameterless constructor
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PasswordsContext>
    {
       public PasswordsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PasswordsContext>();
            optionsBuilder.UseSqlite("Filename=Passwords.db");

            return new PasswordsContext(optionsBuilder.Options);
        }
    }
}
