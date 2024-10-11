using PassswordManager.Models;


namespace PassswordManager.PasswordManagerService.Interfaces
{
    public interface IPasswordService
    {
        Task AddPassword(string siteName, string plainPassword, string masterPassword);
        Task<IEnumerable<PasswordItem>> GetPasswords();
    }
}
