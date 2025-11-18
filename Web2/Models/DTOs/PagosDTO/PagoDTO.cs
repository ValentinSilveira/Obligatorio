using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.DTOs.PagosDTO
{
    public class PagoDTO
    {
        public string TipoGasto { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }
        public int Monto { get; set; }
    }
}
