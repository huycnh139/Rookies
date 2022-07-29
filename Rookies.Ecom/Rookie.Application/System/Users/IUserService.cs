using Rookie.ViewModel.System.Users;

namespace Rookie.Application.System.Users
{
    public interface IUserService
    {
        Task<string> Authencate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
        Task<bool> Update(UserUpdateRequest request, Guid userId);
    }
}
