using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Recurrente : Pago
    {
        public DateTime FechaDesde {  get; set; }
        public DateTime FechaHasta { get; set; }
        public Recurrente(Gasto tipoGasto, Usuario usuario, string descripcion, int monto, DateTime fechaDesde, DateTime fechaHasta) : base(tipoGasto, usuario, descripcion, monto)
        {
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
        }
    }
}
