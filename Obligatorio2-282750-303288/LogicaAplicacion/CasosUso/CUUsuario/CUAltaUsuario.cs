using CasosDeUsos.DTOs.UsuariosDTO;
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuarios
{
    public class CUAltaUsuario : ICUAltaUsuario
    {
        public IRepositorioUsuario RepoUsuario { get; set; }

        public CUAltaUsuario(IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
        }   

        public void Ejecutar(UsuarioDTO usuarioDTO)
        {
            Usuario usuario = MapperUsuario.UsuarioDTOToUsuario(usuarioDTO);
            if (RepoUsuario.ExisteEmail(usuario.Email))
            {
                string nuevoEmail;
                var random = new Random();

                do
                {
                    int numeros = random.Next(1000, 9999);
                    nuevoEmail = usuario.Nombre.Substring(0, Math.Min(3, usuario.Nombre.Length)).ToLower() +
                                 usuario.Apellido.Substring(0, Math.Min(3, usuario.Apellido.Length)).ToLower() +
                                 numeros + "@laempresa.com";

                } while (RepoUsuario.ExisteEmail(nuevoEmail));

                typeof(Usuario)
                    .GetProperty("Email")
                    .SetValue(usuario, nuevoEmail);
            }
            RepoUsuario.Add(usuario);
        }
    }
    
}
