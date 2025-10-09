
namespace Domain.Entities
{
    public class Grade
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public int Grade1 { get; set; }

        public int Grade2 { get; set; }

        public int Grade3 { get; set; }

        public int Grade4 { get; set; }

        public int Exam { get; set; }

        public int TotalGrade { get; set; }

        public string? Classification { get; set; }

        public string? State { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}

