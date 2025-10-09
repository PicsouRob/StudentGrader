using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class SubjectDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "El Codigo es obligatorio")]
        public string? Code { get; set; }

        [Required(ErrorMessage = "La Descripcion es obligatoria")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Las Creditos es obligatoria")]
        [Range(1, 5, ErrorMessage = "Las Creditos deben estar entre 1 y 5")]
        public int Credits { get; set; } = 1;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}

