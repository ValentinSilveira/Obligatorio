using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<<< HEAD:CasosDeUsos/DTOs/UsuariosDTO/DetalleUsuarioDTO.cs
namespace CasosDeUsos.DTOs.UsuariosDTO
========
namespace CasosDeUsos.DTOs.DTOsUsuario
>>>>>>>> origin:CasosDeUsos/DTOs/DTOsUsuario/DetalleUsuarioDTO.cs
{
    public class DetalleUsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Apellido { get; set; }
        public string DescripcionRol { get; set; }
    }
}
