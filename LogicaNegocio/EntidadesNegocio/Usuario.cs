using LogicaNegocio.InterfacesNegocio;
using LogicaNegocio.ValueObjects.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Usuario:IValidable
    {
        public int Id { get; private set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Contrasenia Contrasenia { get; set; }
        public Email Email { get; set; }
        private static int s_ultId;

            public Usuario(string nombre, string apellido, string contrasenia, string email) 
            {
                Nombre = nombre;
                Apellido = apellido;
                Contrasenia = new Contrasenia(contrasenia);
                Email = new Email(nombre,apellido);
                Id = s_ultId++;
                Validar();
            }

        public void Validar() { }

    }
}
