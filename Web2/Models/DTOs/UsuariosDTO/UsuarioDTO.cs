using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.DTOs.UsuariosDTO
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "la contraseña es obligatoria")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string Password { get; set; }
        [DisplayName("Seleccione un equipo")]
        [Required(ErrorMessage = "El equipo es obligatoria")]
        public int EquipoId { get; set; }
        public IEnumerable<ListadoEquipoDTO> Equipos { get; set; } = new List<ListadoEquipoDTO>();

        [DisplayName("Seleccione un Rol")]
        [Required(ErrorMessage = "El rol es obligatoria")]
        public int RolId { get; set; }
        public IEnumerable<ListadoRolDTO> Roles { get; set; } = new List<ListadoRolDTO>();
    }
}
