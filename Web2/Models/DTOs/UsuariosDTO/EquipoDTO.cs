using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.DTOs.UsuariosDTO
{
    public class EquipoDTO
    {
        public string Nombre;
        public List<UsuarioDTO> Usuarios { get; set; }
    }
}
