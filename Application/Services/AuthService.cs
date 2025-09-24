using Domain;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<Student> _passwordHasher;

        public AuthService(AppDbContext context, IPasswordHasher<Student> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Student> RegisterAsync(string name, string email, string password)
        {
            try
            {
                var existedStudent = await _context.Student!.FirstOrDefaultAsync(e => e.Email == email);

                if (existedStudent != null) throw new Exception("El correo ya esta en uso.");

                var newStudent = new Student
                {
                    Name = name,
                    Email = email,
                    HashedPassword = _passwordHasher.HashPassword(null, password)
                };

                _context.Student!.Add(newStudent);
                await _context.SaveChangesAsync();

                return newStudent;
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception.Message}");
            }
        }

        public async Task<Student> LoginAsync(string email, string password)
        {
            try
            {
                var existedStudent = await _context.Student!.FirstOrDefaultAsync(e => e.Email == email);

                if (existedStudent == null) throw new Exception("No existe estudiante con este correo.");

                if (existedStudent != null && _passwordHasher.VerifyHashedPassword(null, existedStudent.HashedPassword!, password) == PasswordVerificationResult.Failed)
                    throw new Exception("Contraseeña incorrecta");

                return existedStudent!;
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception.Message}");
            }
        }
    }
}

