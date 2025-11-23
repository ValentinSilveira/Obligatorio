using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.DTOs.UsuariosDTO
{
    public class UsuarioLogueadoDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public string NombreUsuario { get; set; }
        public string Token { get; set; }
    }
}
