using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Recurrente : Pago, IValidable
    {
        public DateTime FechaDesde {  get; set; }
        public DateTime FechaHasta { get; set; }
        public Recurrente(Gasto tipoGasto, Usuario usuario, MetodoPago metodo, string descripcion, int monto, DateTime fechaDesde, DateTime fechaHasta) : base(tipoGasto, usuario,metodo, descripcion, monto)
        {
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            Validar();
        }
        protected Recurrente() :base(){}
        

        public void Validar() 
        {
            ValidarFechaInicio();
            ValidarFechaFin();
            if (FechaDesde > FechaHasta)
            {
                throw new PagoException("La fecha de inicio debe ser menor a la fecha de fin");
            }
        }

        private void ValidarFechaInicio() 
        {
            if(FechaDesde == null) 
            {
                throw new PagoException("Debe indicar la fecha de inicio");
            }
        }

        private void ValidarFechaFin() 
        {
            if (FechaHasta == null)
            {
                throw new PagoException("Debe indicar la fecha de fin");
            }
        }        
    }
}
