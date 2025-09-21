using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public abstract class Pago:IValidable
    {
        public int Id { get; set; }
        public Gasto TipoGasto { get; set; }
        public Usuario Usuario { get; set; }

        public MetodoPago Metodo {  get; set; }
        public string Descripcion {  get; set; }
        public int Monto { get; set; }

        public Pago(Gasto tipoGasto, Usuario usuario, MetodoPago metodo, string descripcion, int monto)
        {
            TipoGasto = tipoGasto;
            Usuario = usuario;
            Metodo = metodo;
            Descripcion = descripcion;
            Monto = monto;
            Validar();
        }

        public Pago() { }

        public void Validar()
        {
            ValidarUsuario();
            ValidarGasto();
            ValidarDescripcion();
            ValidarMonto();
            ValidarMetodo();
        }

        private void ValidarUsuario() 
        {
            if(Usuario == null)
            {
                throw new ArgumentNullException("No hay ningun usuario asignado al pago");
            }
        }

        private void ValidarGasto() 
        {
            if(TipoGasto == null) 
            {
                throw new ArgumentNullException("No hay un gasto asignado");
            }
        }

        private void ValidarDescripcion() 
        {
            if (string.IsNullOrEmpty(Descripcion)) 
            {
                throw new PagoException("Debe incluir una descripción");
            }
        }

        private void ValidarMonto() 
        {
            if(Monto < 1) 
            {
                throw new PagoException("El monto debe ser mayor que 0");
            }
        }

        private void ValidarMetodo() 
        {
            if(Metodo.ToString() != "CREDITO" || Metodo.ToString() != "EFECTIVO") 
            {
                throw new PagoException("Debe seleccionar un método de pago válido");
            }
        }
    }
}
