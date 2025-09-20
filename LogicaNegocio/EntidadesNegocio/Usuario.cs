using ExcepcionesPropias.ExcepcionesEntidades;
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
    public class Usuario:IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Password Password { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        private Usuario() { }
        public Usuario(string email, string password) 
        {
            Email = email;
            Password = new Password(password);
            Validar();
        }

        public void Validar()
        {
            ValidarNombre();
            ValidarApellido();

        }

        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new UsuarioException("El nombre no puede estar vacío.");
            }

        }

        private void ValidarApellido()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new UsuarioException("El apellido no puede estar vacío.");
            }

        }

    }
}
