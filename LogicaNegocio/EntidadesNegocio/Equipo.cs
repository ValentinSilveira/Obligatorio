using LogicaNegocio.InterfacesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Equipo : IValidable
    {
        public int Id { get; set; }
        public string Nombre;
        public List<Usuario> Usuarios { get; set; }

        public Equipo(string nombre, List<Usuario> usuarios)
        {
            Nombre = nombre;
            Usuarios = usuarios;
            Validar();
        }

        public Equipo() { }

        private void Validar()
        {
        }
    }
}
