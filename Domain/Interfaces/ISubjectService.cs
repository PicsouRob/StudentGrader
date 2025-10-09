
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISubjectService
    {
        Task<bool> AddSubjectAsync(Subject subject);
        Task<Subject> GetSubjectByIdAsync(int subjectId);
        Task<Subject> GetSubjectByNameAsync(string name);
        Task<bool> UpdateSubjectAsync(Subject subject);
        Task<IEnumerable<Subject>> GetSubjectsAsync();
        Task<bool> DeleteSubjectAsync(int subjectId);
    }
}

