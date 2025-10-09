using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IStudentService
    {
        Task<bool> AddStudentAsync(Student student);
        Task<Student> GetStudentByIdAsync(string studentId);
        Task<Student> GetStudentByNameAsync(string name);
        Task<bool> UpdateStudentAsync(Student student);
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<bool> DeleteStudentAsync(string studentId);
        Task<string> GenerateUniqueStudentIdAsync();
    }
}

