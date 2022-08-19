using Rookie.ViewModel.System.Users;

namespace Rookie.Ecom.Customer.Service
{
    public interface IUserApiClient
    {
        public Task<string> Authenticate(LoginRequest request);
    }
}
