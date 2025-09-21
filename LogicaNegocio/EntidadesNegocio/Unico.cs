using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Unico: Pago
    {
        public DateTime FechaPago { get; set; }
        public string NroRecibo { get; set; }
        public Unico(Gasto tipoGasto, Usuario usuario,MetodoPago metodo, string descripcion, int monto, DateTime fechaPago,string nroRecibo) : base(tipoGasto, usuario,metodo, descripcion, monto)
        {
            FechaPago = fechaPago;
            NroRecibo = nroRecibo;
        }
    }
}
