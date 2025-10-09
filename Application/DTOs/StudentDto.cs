using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public String? Name { get; set; }

        [Required(ErrorMessage = "El Correo electronico es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public String? Email { get; set; }

        // [Required(ErrorMessage = "La Matricula es obligatoria")]
        // [StringLength(9, MinimumLength = 9, ErrorMessage = "La matricula debe tener 9 caracteres")]
        public string? StudentId { get; set; }

        [Required(ErrorMessage = "El Estado es obligatorio")]
        public String? Status { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de entrada")]
        [Required(ErrorMessage = "La Fecha de entrada es obligatoria")]
        public DateTime? EnrollmentDate { get; set; } = DateTime.Now;

        [Phone]
        [Display(Name = "Numero de Telefono")]
        [Required(ErrorMessage = "El Numero de Telefono es obligatorio")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "El Numero de Telefono debe tener 10 caracteres")]
        public string? PhoneNumber { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}

