using Microsoft.EntityFrameworkCore;
using OnyxScheduling.Auth;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AuthDataContext _context;

        public AccountRepository(AuthDataContext context)
        {
            _context = context;
        }
        public Task<LoginDto> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public void Register(User user)
        {
            _context.Users.AddAsync(user);
            _context.SaveChanges();
        }

        public async Task<bool> UserExists(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
