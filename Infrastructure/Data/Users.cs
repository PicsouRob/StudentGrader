
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
	public class Users : IdentityUser
	{
        public string? FullName { get; set; }
    }
}

