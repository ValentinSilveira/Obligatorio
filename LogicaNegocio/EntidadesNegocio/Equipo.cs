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
        private static int s_ultId;
        public string Nombre;
        public List<Usuario> Usuarios { get; set; }

        public Equipo(string nombre, List<Usuario> usuarios)
        {
            Id = s_ultId++;
            Nombre = nombre;
            Usuarios = usuarios;
            Validar();
        }

        private void Validar()
        {
        }
    }
}
