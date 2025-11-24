using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesNegocio;
using LogicaNegocio.ValueObjects.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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
        //dfsgdfsgsdf 
        public string NombreUsuario { get; private set; }
        public string Apellido { get; private set; }
        private Usuario() { }
        public Usuario(string password, string apellido, string nombre) 
        {
            NombreUsuario = nombre;
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
            if (string.IsNullOrEmpty(NombreUsuario))
            {
                throw new UsuarioException("El nombre no puede estar vacío.");
            }

        }

        private void ValidarApellido()
        {
            if (string.IsNullOrEmpty(NombreUsuario))
            {
                throw new UsuarioException("El apellido no puede estar vacío.");
            }
        }
        private string GenerarEmail(string nombre, string apellido, string agregado = "")
        {
            string nomb = nombre.Substring(0, Math.Min(3, nombre.Length)).ToLower();
            string apel = apellido.Substring(0, Math.Min(3, apellido.Length)).ToLower();
            nomb = RemoverAcentos(nomb);
            apel = RemoverAcentos(apel);
            string correo = $"{nomb}{apel}{agregado}@laempresa.com".ToLower();
            return correo;
        }
        private string RemoverAcentos(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return texto;
            string normalizado = texto.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (char c in normalizado)
            {
                UnicodeCategory categoria = CharUnicodeInfo.GetUnicodeCategory(c);
                if (categoria != UnicodeCategory.NonSpacingMark)
                {
                    if (c == 'ñ') sb.Append('n');
                    else if (c == 'Ñ') sb.Append('N');
                    else if (c == 'ü') sb.Append('u');
                    else if (c == 'Ü') sb.Append('U');
                    else sb.Append(c);
                }
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
