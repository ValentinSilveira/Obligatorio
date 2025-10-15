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
    public class Usuario:IValidable
    {
        public int Id { get; private set; }
        public string Email { get; private set; }
        public Password Password { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
        public int EquipoId { get; set; }
        public Equipo Equipo { get; set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        private Usuario() { }
        public Usuario(string password, string apellido, string nombre) 
        {
            Nombre = nombre;
            Apellido = apellido;
            Password = new Password(password);
            Email = GenerarEmail(nombre, apellido);
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
        private string GenerarEmail(string nombre, string apellido, string agregado = "")
        {
            string nomb = nombre.Substring(0, Math.Min(3, nombre.Length)).ToLower();
            string apel = apellido.Substring(0, Math.Min(3, apellido.Length)).ToLower();
            return $"{nomb}{apel}@laEmpresa.com".ToLower(); ;
        }
    }
}
