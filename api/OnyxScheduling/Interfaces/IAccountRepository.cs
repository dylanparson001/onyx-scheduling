using OnyxScheduling.Auth;
using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> UserExists(string username);
        Task<LoginDto> Login(LoginDto loginDto);
        void Register(User user);
    }
}
