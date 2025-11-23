using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.DTOs.UsuariosDTO
{
    public class UsuarioCambiarPasswordDTO
    {
        public int UsuarioId { get; set; }

        [Required]
        [MinLength(8)]
        public string NuevaPassword { get; set; }
    }
}
