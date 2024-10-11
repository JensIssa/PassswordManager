using Microsoft.EntityFrameworkCore;
using PassswordManager.PasswordManagerService.Interfaces;
using PassswordManager.Repository.DBContext;
using System.Data.Entity;

namespace PassswordManager
{
    public partial class App : Application
    {
        private readonly IPasswordService _passwordService;
        private readonly PasswordsContext _dbContext;

        /// <summary>
        /// Starts the app and sets up the DB context and password service for the app to use. Also handles the migration and creation of the DB
        /// </summary>
        /// <param name="passwordService"></param>
        /// <param name="passwordsContext"></param>
        public App(IPasswordService passwordService, PasswordsContext passwordsContext)
        {
            InitializeComponent();
            _passwordService = passwordService;
            _dbContext = passwordsContext;
            _dbContext.Database.Migrate();
            _dbContext.Database.EnsureCreated();


            MainPage = new NavigationPage(new MainPage(_passwordService));
        }
    }
}
