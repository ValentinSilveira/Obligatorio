using CasosDeUsos.DTOs.DTOsUsuario;
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
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
            RepoUsuario.Add(usuario);
        }
    }
    
}
