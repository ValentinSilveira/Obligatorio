using LogicaNegocio.InterfacesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Rol : IValidable
    {
        public int Id { get; set; }
        private static int s_ultId;
        public string Descripcion { get; set; }

        public Rol(string descripcion) 
        {
            Id = s_ultId++;
            Descripcion = descripcion;
            Validar();
        }

        private void Validar()
        {
        }
    }
}
