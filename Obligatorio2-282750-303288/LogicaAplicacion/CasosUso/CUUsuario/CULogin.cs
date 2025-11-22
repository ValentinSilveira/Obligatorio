using CasosDeUsos.DTOs.UsuariosDTO;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.InterfacesCasosUsos;
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
    public class CULogin : ILogin
    {
        public IRepositorioUsuario RepoUsuarios { get; set; }

        public CULogin(IRepositorioUsuario repoUsuarios)
        {
            RepoUsuarios = repoUsuarios;
        }

        /*
        public UsuarioLogueadoDTO Ejecutar(string name, string password)
        {
            Usuario usuario = RepoUsuarios.FindByEmailAndPassword(name, password);
            return MapperUsuario.UsuarioToUsuarioListadoDTO(usuario);
        }
        */

        public UsuarioLogueadoDTO Ejecutar(UsuarioLoginDTO usuarioLoginDTO)
        {
            Usuario usuario = RepoUsuarios.FindByEmailAndPassword(usuarioLoginDTO.Email, usuarioLoginDTO.Password);
            if (usuario == null) throw new UsuarioException("Datos incorrectos");
            return MapperUsuario.UsuarioToUsuarioLogueadoDTO(usuario);
        }
    }
}
