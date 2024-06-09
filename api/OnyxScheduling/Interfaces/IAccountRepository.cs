using OnyxScheduling.Dtos;
using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces
{
    public interface IAccountRepository
    {
        public Task<List<User>> GetAllCustomers();
        public Task<List<User>> GetAllOfficeStaff();
        public Task<List<User>> GetAllTechnicians();
        public Task<User> GetCustomersFromCustomerId(string customerId);
        public Task<User> GetTechnciainsFromTechId(string techniciainsId);
        public Task UpdateUserInfo(string userId, UserDto newUser);
        public Task<User> GetUserInfo(string userId);
        public Task<User> GetUserRole(string userId);
        public Task<User> GetUserFromUsername(string username);
        public Task<List<User>> GetUsers(int position, int take);
        public Task<int> GetCountUsers();
        public Task<List<User>> SearchUsernames(string username);
    }
}
