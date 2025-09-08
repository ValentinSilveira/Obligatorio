using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.DTOs
{
    public class EquipoDTO
    {
        public string Nombre;
        public List<Usuario> Usuarios { get; set; }
    }
}
