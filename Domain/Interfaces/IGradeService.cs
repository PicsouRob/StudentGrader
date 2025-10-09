using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGradeService
    {
        Task<bool> AddGradeAsync(Grade grade);
        Task<Grade> GetGradeByIdAsync(int gradeId);
        Task<bool> UpdateGradeAsync(Grade grade);
        Task<IEnumerable<Grade>> GetGradesAsync();
        Task<bool> DeleteGradeAsync(int gradeId);
        GradeHelper ManageGradeAsync(Grade grade);
    }
}

