using CasosDeUsos.DTOs;
using LogicaAplicacion.InterfacesCasosUsos;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class Login : ILogin
    {
        public IRepositorioUsuario RepoUsuarios { get; set; }

        public Login(IRepositorioUsuario repoUsuarios)
        {
            RepoUsuarios = repoUsuarios;
        }

        public UsuarioLoginDTO Ejecutar(string name, string password)
        {
            Usuario usuario = RepoUsuarios.FindByEmailAndPassword(name, password);
            return MapperUsuario.UsuarioToUsuarioListadoDTO(usuario);
        }
    }
}
