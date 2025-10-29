using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddStudentAsync(Student student)
        {
            try
            {
                var existedStudent = await GetStudentByNameAsync(student.Name!);

                if (existedStudent != null)
                {
                    throw new Exception("El estudiante ya existe");
                }

                _context.Student!.Add(student);
                _context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception.Message}");
            }
        }

        public async Task<Student> GetStudentByIdAsync(string studentId)
        {
            var student = await _context.Student!.FirstOrDefaultAsync(s => s.StudentId == studentId);

            return student!;
        }

        public async Task<Student> GetStudentByNameAsync(string name)
        {
            var student = await _context.Student!.FirstOrDefaultAsync(s => s.Name == name);

            return student!;
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            try
            {
                var existedStudent = await GetStudentByIdAsync(student.StudentId!) ??
                    throw new Exception("No existe estudiante con esta matricula");

                _context.Entry(existedStudent).CurrentValues.SetValues(student);
                _context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception.Message}");
            }
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            var students = await _context.Student!.ToListAsync();

            return students;
        }

        public async Task<bool> DeleteStudentAsync(string studentId)
        {
            try
            {
                var existedStudent = await GetStudentByIdAsync(studentId)
                    ?? throw new Exception("No existe estudiante con esta matricula");

                _context.Student!.Remove(existedStudent);
                _context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception.Message}");
            }
        }

        public async Task<string> GenerateUniqueStudentIdAsync()
        {
            Random random = new Random();
            string studentId;
            bool isUnique;

            do
            {
                string randomDigits = random.Next(0, 10000000).ToString("D7"); // 7-digit number
                studentId = "A0" + randomDigits;
                isUnique = !await _context.Student!.AnyAsync(s => s.StudentId == studentId);
            } while (!isUnique);

            return studentId;
        }
    }
}

