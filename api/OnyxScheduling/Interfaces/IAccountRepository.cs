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
        public Task UpdateUserInfo(User newUser);
    }
}
