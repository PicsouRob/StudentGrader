using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
	public class RegisterDto
	{
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public String Name { get; set; }

        [Required(ErrorMessage = "El Correo electronico es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public String Email { get; set; }

        [Required(ErrorMessage = "La Contraseña es obligaorio")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Debe ser por lo menos 8 caracteres.")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}

