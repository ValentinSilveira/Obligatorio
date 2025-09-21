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
    public class CUListadoUsuarios : ICUListadoUsuario
    {
        public IRepositorioUsuario RepoCliente { get; set; }
        public ICUListadoUsuario CUListadoUsuario { get; set; }

        public CUListadoUsuarios(IRepositorioUsuario repoCliente)
        {
            RepoCliente = repoCliente;
        }

        public IEnumerable<ListadoUsuarioDTO> Ejecutar()
        {
            IEnumerable<Usuario> usuarios = RepoCliente.FindAll();
            return MapperUsuario.UsuarioToUsuarioListadoDTO(usuarios);
        }
    }
}

