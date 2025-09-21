using CasosDeUsos.DTOs.DTOsUsuario;
using LogicaAplicacion.InterfacesCasosUsos;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios.InterfacesUsuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CULogin : ILogin
    {
        public IRepositorioUsuario RepoUsuarios { get; set; }

        public CULogin(IRepositorioUsuario repoUsuarios)
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
