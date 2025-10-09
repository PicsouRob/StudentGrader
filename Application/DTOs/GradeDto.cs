
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class GradeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Estudiante es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El Estudiante debe estar entre 1 y {0}")]
        public int StudentId { get; set; } = 0;

        [Required(ErrorMessage = "La Asignatura es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La Asignatura debe estar entre 1 y {0}")]
        public int SubjectId { get; set; } = 0;

        [Range(0, 100, ErrorMessage = "La Calificacion debe estar entre 0 y 100")]
        [Required(ErrorMessage = "La Calificacion 1 es obligatoria")]
        public int Grade1 { get; set; } = 0;

        [Range(0, 100, ErrorMessage = "La Calificacion debe estar entre 0 y 100")]
        [Required(ErrorMessage = "La Calificacion 2 es obligatoria")]
        public int Grade2 { get; set; } = 0;

        [Range(0, 100, ErrorMessage = "La Calificacion debe estar entre 0 y 100")]
        [Required(ErrorMessage = "La Calificacion 3 es obligatoria")]
        public int Grade3 { get; set; } = 0;

        [Range(0, 100, ErrorMessage = "La Calificacion debe estar entre 0 y 100")]
        [Required(ErrorMessage = "La Calificacion es obligatoria")]
        public int Grade4 { get; set; } = 0;

        [Required(ErrorMessage = "El Examen es obligatorio")]
        public int Exam { get; set; }

        // With Value A, B, C, F
        // [Required(ErrorMessage = "La Calificacion total es obligatoria")]
        // public int TotalGrade { get; set; }

        // // With value approved or failed
        // [Required(ErrorMessage = "La Clasificacion es obligatoria")]
        // public string? Classification { get; set; }

        // [Required(ErrorMessage = "El Estado es obligatorio")]
        // public string? State { get; set; }

        // public DateTime? CreatedAt { get; set; } = DateTime.Now;

        // public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}

