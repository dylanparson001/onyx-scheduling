using OnyxScheduling.Dtos;
using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces
{
    public interface IAccountRepository
    {
        public Task<List<User>> GetAllCustomers(string companyId);
        public Task<List<User>> GetAllOfficeStaff(string companyId);
        public Task<List<User>> GetAllTechnicians(string companyId);
        public Task<User> GetCustomersFromCustomerId(string customerId);
        public Task<User> GetTechnciainsFromTechId(string techniciainsId);
        public Task UpdateUserInfo(string userId, UserDto newUser);
        public Task<User> GetUserInfo(string userId);
        public Task<User> GetUserRole(string userId);
        public Task<User> GetUserFromUsername(string username, string companyId);
        public Task<List<User>> GetUsers(int position, int take, string companyId);
        public Task<int> GetCountUsers(string companyId);
        public Task<List<User>> SearchUsernames(string username, string companyId);
    }
}
