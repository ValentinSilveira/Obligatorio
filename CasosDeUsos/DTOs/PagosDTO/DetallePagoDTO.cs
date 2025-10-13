using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.DTOs.PagosDTO
{
    public class DetallePagoDTO
    {
        public int Id { get; set; }
        public string TipoGasto { get; set; }
        public string Usuario { get; set; }
        public string MetodoPago { get; set; }
        public string Descripcion { get; set; }
        public int Monto { get; set; }
    
    } 
}
