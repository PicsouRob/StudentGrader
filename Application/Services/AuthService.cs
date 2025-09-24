using Infrastructure.Data;
using Domain.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System.Security.Claims;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IMapper _mapper;

        public AuthService(
            UserManager<Users> userManager, IMapper mapper,
            SignInManager<Users> signInManager
        ) {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<User> RegisterAsync(string name, string email, string password)
        {
            try
            {
                var existedStudent = await _userManager.FindByEmailAsync(email);

                if (existedStudent != null) throw new Exception("El correo ya esta en uso.");

                var newIdentityUser = new Users
                {
                    FullName = name,
                    Email = email,
                    UserName = email,
                };

                var result = await _userManager.CreateAsync(newIdentityUser, password);

                if(!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));

                    throw new Exception($"Error al registrar: {errors}");
                }

                return _mapper.Map<User>(newIdentityUser);
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception.Message}");
            }
        }

        public async Task<User> LoginAsync(string email, string password, bool rememberMe)
        {
            try
            {
                var findedUser = await _userManager.FindByEmailAsync(email);

                if (findedUser == null) throw new Exception("No existe usuario con este correo.");

                var result = await _signInManager.PasswordSignInAsync(findedUser, password, rememberMe, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    throw new Exception("Contraseña incorrecta.");
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.GivenName, findedUser.FullName ?? string.Empty)
                };

                var addedClaimResult = await _userManager.AddClaimsAsync(findedUser, claims);

                await _signInManager.RefreshSignInAsync(findedUser);

                return _mapper.Map<User>(findedUser);
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception.Message}");
            }
        }

        public async Task<bool> SignOutAsync()
        {
            await _signInManager.SignOutAsync();

            return true;
        }
    }
}

