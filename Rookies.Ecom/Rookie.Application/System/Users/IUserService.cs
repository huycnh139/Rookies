using Rookie.ViewModel.System.Users;

namespace Rookie.Application.System.Users
{
    public interface IUserService
    {
        Task<string> AuthencateAsync(LoginRequest request);
        Task<bool> RegisterAsync(RegisterRequest request);
        Task<bool> UpdateAsync(UserUpdateRequest request, Guid userId);
        Task<List<UserResponseDto>> GetAllUserAsync();
    }
}
