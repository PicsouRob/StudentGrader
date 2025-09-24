using Domain.Entities;

namespace Domain
{
	public interface IAuthService
	{
        Task<Student> RegisterAsync(string name, string email, string password);
        Task<Student> LoginAsync(string email, string password);
    }
}

