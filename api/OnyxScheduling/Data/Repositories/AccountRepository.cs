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
    }
}
