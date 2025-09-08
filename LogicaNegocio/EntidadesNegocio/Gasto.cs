using LogicaNegocio.InterfacesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Gasto : IValidable
    {
        public int Id { get; set; }
        private static int s_ultId;
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Gasto(string nombre, string descripcion)
        {
            Id = s_ultId++;
            Nombre = nombre;
            Descripcion = descripcion;
            Validar();
        }

        private void Validar()
        {
           
        }
    }
}
