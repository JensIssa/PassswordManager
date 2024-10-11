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
