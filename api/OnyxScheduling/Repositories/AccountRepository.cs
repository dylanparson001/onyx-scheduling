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

        
        public async Task<List<User>> GetAllCustomers(string companyId)
        {
            return await _authDataContext.Users.Where(x => x.Role == "Customer" &&
                                                           x.CompanyId.ToUpper() == companyId.ToUpper()
            ).ToListAsync();
        }

        public async Task<List<User>> GetAllOfficeStaff(string companyId)
        {
            return await _authDataContext.Users.Where(x => x.Role == "Office" &&
                                                           x.CompanyId.ToUpper() == companyId.ToUpper()
                                                           ).ToListAsync();
        }

        public async Task<List<User>> GetAllTechnicians(string companyId)
        {
            return await _authDataContext.Users.Where(x => x.Role == "Field" &&
                                                           x.CompanyId.ToUpper() == companyId.ToUpper()
            ).ToListAsync();
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

        public async Task<User> GetUserFromUsername(string username, string companyId)
        {
            return await _authDataContext.Users.FirstOrDefaultAsync(x => x.UserName == username &&
                                                                         x.CompanyId.ToUpper() == companyId.ToUpper()
            );
        }
        
        public async Task<List<User>> GetUsers(int position, int take, string companyId)
        {
            var result = await _authDataContext.Users.Where(x => x.CompanyId.ToUpper() == companyId.ToUpper())
                .OrderBy(x => x.NormalizedUserName)
                .Skip(position)
                .Take(take)
                .ToListAsync();

            return result;
        }

        public async Task<int> GetCountUsers(string companyId)
        {
            var result = await _authDataContext.Users.Where(x => x.CompanyId.ToUpper() == companyId.ToUpper()).CountAsync();

            return result;
        }

        public async Task<List<User>> SearchUsernames(string username, string companyId)
        {
            var result = await _authDataContext.Users
                .Where(x => (x.NormalizedUserName.Contains(username.ToUpper()) ||
                             x.FirstName.ToUpper().Contains(username.ToUpper()) ||
                             x.LastName.ToUpper().Contains(username.ToUpper()))&&
                            x.CompanyId.ToUpper() == companyId.ToUpper()
                            )
                .OrderBy(x => x.NormalizedUserName)
                .ToListAsync();

            return result;
        }
    }
}
