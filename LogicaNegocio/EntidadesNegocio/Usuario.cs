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
    public class Usuario:IValidable, IEquatable<Usuario>
    {
        public int Id { get; private set; }
        public Email Email { get; set; }
        public Contrasenia Contrasenia { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public Usuario() { }

        public Usuario(string email) 
        {
            Email = new Email(email);
        }

        public Usuario(string email, string contrasenia) 
        {
            Email = new Email(email);
            Contrasenia = new Contrasenia(contrasenia);
        }


        public void Validar() { }

        public bool Equals(Usuario? other)
        {
            throw new NotImplementedException();
        }
    }
}
