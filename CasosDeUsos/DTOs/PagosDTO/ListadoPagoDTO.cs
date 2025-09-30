using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.DTOs.PagosDTO
{
    public class ListadoPagoDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string UsuarioNombre { get; set; }
        public string GastoDescripcion { get; set; }
        //public decimal SaldoPendiente { get; set; }
    }       
    
}