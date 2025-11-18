using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.DTOs.UsuariosDTO
{
    public class ListadoUsuarioDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public decimal TotalPagado { get; set; }
    }
}
