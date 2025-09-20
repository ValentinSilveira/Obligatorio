using LogicaNegocio.InterfacesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Pago:IValidable
    {
        public int Id { get; set; }
        public Gasto TipoGasto { get; set; }
        public Usuario Usuario { get; set; }
        public string Descripcion {  get; set; }
        public int Monto { get; set; }

        public Pago(Gasto tipoGasto, Usuario usuario, string descripcion, int monto)
        {
            TipoGasto = tipoGasto;
            Usuario = usuario;
            Descripcion = descripcion;
            Monto = monto;
            Validar();
        }

        public Pago() { }

        private void Validar()
        {
            
        }
    }
}
