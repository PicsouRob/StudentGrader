
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class GradeService : IGradeService
    {
        private readonly AppDbContext _context;

        public GradeService(AppDbContext context)
        {
            _context = context;
        }

        public GradeHelper ManageGradeAsync(Grade grade)
        {
            {
                int averageGrades = (grade.Grade1 + grade.Grade2 + grade.Grade3 + grade.Grade4) / 4;
                decimal examContribution = grade.Exam * 0.3m;
                decimal totalContribution = (averageGrades * 0.7m) + examContribution;

                string classification = totalContribution >= 90 ? "A" : totalContribution >= 80 ? "B" : totalContribution >= 70 ? "C" : "F";
                string state = totalContribution >= 70 ? "approved" : "failed";

                return new GradeHelper
                {
                    TotalGrade = (int)totalContribution,
                    Classification = classification,
                    State = state
                };
            }
        }

        public async Task<bool> AddGradeAsync(Grade grade)
        {
            try
            {
                var gradeData = ManageGradeAsync(grade);
                grade.TotalGrade = gradeData.TotalGrade;
                grade.Classification = gradeData.Classification;
                grade.State = gradeData.State;

                await _context.Grade!.AddAsync(grade);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception.Message}");
            }
        }

        public async Task<Grade> GetGradeByIdAsync(int gradeId)
        {
            var grade = await _context.Grade
            .Include(g => g.Student)
            .Include(g => g.Subject)
            .FirstOrDefaultAsync(g => g.Id == gradeId);

            return grade!;
        }

        public async Task<bool> UpdateGradeAsync(Grade grade)
        {
            try
            {
                var existedGrade = await GetGradeByIdAsync(grade.Id!)
                    ?? throw new Exception("No existe calificacion con este id");

                var gradeData = ManageGradeAsync(grade);
                grade.TotalGrade = gradeData.TotalGrade;
                grade.Classification = gradeData.Classification;
                grade.State = gradeData.State;

                _context.Entry(existedGrade).CurrentValues.SetValues(grade);
                _context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception.Message}");
            }
        }

        public async Task<IEnumerable<Grade>> GetGradesAsync()
        {
            var grades = await _context.Grade
            .Include(g => g.Student)
            .Include(g => g.Subject)
            .ToListAsync();

            return grades;
        }

        public async Task<bool> DeleteGradeAsync(int gradeId)
        {
            try
            {
                var existedGrade = await GetGradeByIdAsync(gradeId)
                    ?? throw new Exception("No existe estudiante con esta matricula");

                _context.Grade!.Remove(existedGrade);
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

