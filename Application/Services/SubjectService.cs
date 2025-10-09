using System.Diagnostics;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly AppDbContext _context;

        public SubjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddSubjectAsync(Subject subject)
        {
            try
            {
                var existedSubject = await GetSubjectByNameAsync(subject.Name!);

                if (existedSubject != null)
                {
                    throw new Exception("El estudiante ya existe");
                }

                _context.Subject!.Add(subject);
                _context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception.Message}");
            }
        }

        public async Task<Subject> GetSubjectByNameAsync(string name)
        {
            var subject = await _context.Subject!.FirstOrDefaultAsync(s => s.Name == name);

            return subject!;
        }

        public async Task<Subject> GetSubjectByIdAsync(int subjectId)
        {
            var subject = await _context.Subject!.FirstOrDefaultAsync(s => s.Id == subjectId);

            return subject!;
        }

        public async Task<bool> UpdateSubjectAsync(Subject subject)
        {
            try
            {
                var existedSubject = await GetSubjectByIdAsync(subject.Id!)
                    ?? throw new Exception("No existe estudiante con esta matricula");

                _context.Entry(existedSubject).CurrentValues.SetValues(subject);
                _context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception.Message}");
            }
        }

        public async Task<IEnumerable<Subject>> GetSubjectsAsync()
        {
            var subjects = await _context.Subject!.ToListAsync();

            return subjects;
        }

        public async Task<bool> DeleteSubjectAsync(int subjectId)
        {
            try
            {
                var existedSubject = await GetSubjectByIdAsync(subjectId)
                    ?? throw new Exception("No existe estudiante con esta matricula");

                _context.Subject!.Remove(existedSubject);
                _context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception.Message}");
            }
        }
    }
}

