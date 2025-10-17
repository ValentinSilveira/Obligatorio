using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Unico : Pago, IValidable
    {        
        public DateTime FechaPago { get; set; }
        public string NroRecibo { get; set; }
        public Unico(Gasto tipoGasto, Usuario usuario, MetodoPago metodo, string descripcion, int monto, DateTime fechaPago, string nroRecibo) : base(tipoGasto, usuario, metodo, descripcion, monto)
        {
            FechaPago = fechaPago;
            NroRecibo = nroRecibo;
            Validar();
        }
        protected Unico() : base() { }

        public void Validar()
        {
            ValidarFechaPago();
            ValidarRecibo();
        }

        private void ValidarFechaPago()
        {
            if (FechaPago == null)
            {
                throw new PagoException("Debe indicar una fecha valida");
            }
        }

        private void ValidarRecibo()
        {
            if (NroRecibo == null)
            {
                throw new PagoException("Debe indicar el número de recibo");
            }
            if (NroRecibo == "0")
            {
                throw new PagoException("El número de recibo no puede ser 0.");
            }
        }
    }
}
