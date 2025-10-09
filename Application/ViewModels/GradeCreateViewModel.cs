
using Application.DTOs;
using Domain.Entities;

namespace Application.ViewModels
{
    public class GradeCreateViewModel
    {
        public IEnumerable<Student>? Students { get; set; }
        public IEnumerable<Subject>? Subjects { get; set; }
        public GradeDto? GradeDto { get; set; }
    }
}

