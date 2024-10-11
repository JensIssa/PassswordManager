using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PassswordManager.PasswordManagerService;
using PassswordManager.PasswordManagerService.Interfaces;
using PassswordManager.Repository;
using PassswordManager.Repository.DBContext;
using PassswordManager.Repository.Interfaces;

namespace PassswordManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddDbContext<PasswordsContext>(
                
                options => options
                    .UseSqlite("Filename=Passwords.db")
                    .EnableSensitiveDataLogging()
                );


            // Some DI stuff
            builder.Services.AddScoped<DbContext, PasswordsContext>();

            builder.Services.AddScoped<IPasswordRepo, PasswordRepo>();

            builder.Services.AddScoped<IPasswordService, PasswordService>();


            return builder.Build();
        }
    }
}
