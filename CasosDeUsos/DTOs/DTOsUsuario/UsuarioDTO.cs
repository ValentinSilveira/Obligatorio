using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<<< HEAD:CasosDeUsos/DTOs/UsuariosDTO/UsuarioDTO.cs
namespace CasosDeUsos.DTOs.UsuariosDTO
========
namespace CasosDeUsos.DTOs.DTOsUsuario
>>>>>>>> origin:CasosDeUsos/DTOs/DTOsUsuario/UsuarioDTO.cs
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "la contraseña es obligatoria")]
        public string Contraseña { get; set; }

        public string Email { get; set; }
        public int RolId { get; set; }
    }
}
