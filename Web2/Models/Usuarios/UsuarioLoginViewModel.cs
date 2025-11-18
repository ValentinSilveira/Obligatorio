using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Web.Models.Roles;

namespace Web.Models.Usuarios
{
    public class UsuarioLoginViewModel
    {
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
