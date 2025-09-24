using Domain.Entities;

namespace Domain.Interfaces
{
	public interface IAuthService
	{
        Task<User> RegisterAsync(string name, string email, string password);
        Task<User> LoginAsync(string email, string password, bool rememberMe);
        Task<bool> SignOutAsync();
    }
}

