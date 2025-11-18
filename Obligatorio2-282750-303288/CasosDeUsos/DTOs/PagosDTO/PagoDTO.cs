using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.DTOs.PagosDTO
{
    public class PagoDTO
    {
        public Gasto TipoGasto { get; set; }
        public Usuario Usuario { get; set; }
        public string Descripcion { get; set; }
        public int Monto { get; set; }
    }
}
