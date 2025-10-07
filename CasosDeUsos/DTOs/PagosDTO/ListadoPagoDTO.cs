using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.DTOs.PagosDTO
{
    public class ListadoPagoDTO
    {
        public int Id { get; set; }
        [DisplayName("Tipo de pago")]
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        [DisplayName("Metodo de Pago")]
        public string MetodoPago { get; set; }

        [DisplayName("Fecha desde")]
        public DateTime FechaDesde { get; set; }
        [DisplayName("Fecha hasta")]
        public DateTime ? FechaHasta { get; set; }
        [DisplayName("Usuario")]
        public string UsuarioNombre { get; set; }
        [DisplayName("Tipo de gasto")]
        public string GastoDescripcion { get; set; }
        public string TipoPago { get; set; }
    }       
    
}