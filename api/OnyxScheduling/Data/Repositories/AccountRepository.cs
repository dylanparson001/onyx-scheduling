using System.Runtime.Loader;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OnyxScheduling.Auth;
using OnyxScheduling.Dtos;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        private readonly AuthDataContext _authDataContext;

        public AccountRepository(DataContext context, AuthDataContext authDataContext)
        {
            _context = context;
            _authDataContext = authDataContext;
        }

        
        public async Task<List<User>> GetAllCustomers()
        {
            return await _authDataContext.Users.Where(x => x.Role == "Customer").ToListAsync();
        }

        public async Task<List<User>> GetAllOfficeStaff()
        {
            return await _authDataContext.Users.Where(x => x.Role == "Office").ToListAsync();
        }

        public async Task<List<User>> GetAllTechnicians()
        {
            return await _authDataContext.Users.Where(x => x.Role == "Field").ToListAsync();
        }

        public async Task<User> GetCustomersFromCustomerId(string customerId)
        {
            return await _authDataContext.Users.FirstOrDefaultAsync(x => x.Id == customerId);
        }

        public async Task<User> GetTechnciainsFromTechId(string techniciainId)
        {
            return await _authDataContext.Users.FirstOrDefaultAsync(x => x.Id == techniciainId);
        }

        public async Task UpdateUserInfo(string userId, UserDto newUser)
        {
            var currentUser = await _authDataContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            currentUser.UserName = newUser.UserName;
            currentUser.NormalizedUserName = newUser.UserName.ToUpper();
            currentUser.FirstName = newUser.FirstName;
            currentUser.LastName = newUser.LastName;
            await _authDataContext.SaveChangesAsync();
        }

        public async Task<User> GetUserInfo(string userId)
        {
            return await _authDataContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public Task<User> GetUserRole(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
