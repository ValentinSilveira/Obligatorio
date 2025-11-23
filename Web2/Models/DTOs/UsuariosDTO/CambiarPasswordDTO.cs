using System.ComponentModel.DataAnnotations;

namespace Web.Models.DTOs.UsuariosDTO
{
    public class CambiarPasswordDTO
    {
        public int UsuarioId { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string NuevaPassword { get; set; }
    }
}
