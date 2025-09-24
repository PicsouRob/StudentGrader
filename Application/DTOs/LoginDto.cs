using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El Correo electronico es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public String Email { get; set; }

        [Required(ErrorMessage = "La Contraseña es obligaorio")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "Recuerdame")]
        public bool RememberMe { get; set; }
    }
}

