using PassswordManager.Repository.Interfaces;
using PassswordManager.Models;
using PassswordManager.PasswordManagerService.Interfaces;

namespace PassswordManager.PasswordManagerService
{
    public class PasswordService: IPasswordService
    {
        private IPasswordRepo _repo;
        public PasswordService(IPasswordRepo repo) 
        {
            _repo = repo;
        }

        public async Task AddPassword(string siteName, string plainPassword, string masterPassword)
        {
            await _repo.AddPassword(siteName, plainPassword, masterPassword);
        }

        public async Task<IEnumerable<PasswordItem>> GetPasswords()
        {
            return await _repo.GetPasswords();
        }
    }
}
