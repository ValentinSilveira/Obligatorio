using CasosDeUsos.DTOs.DTOsUsuario;
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios.InterfacesUsuarios;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUBuscarUsuario : ICUBuscarUsuario
    {
        public IRepositorioUsuario RepoUsuario { get; set; }

        public CUBuscarUsuario(IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
        }

        public CUBuscarUsuario()
        {
        }

        public DetalleUsuarioDTO Ejecutar(int id) 
        {
            Usuario usuario = RepoUsuario.FindById(id);
            if ( usuario != null) 
            {
                return MapperUsuario.UsuarioToDetalleUsuarioDTO(usuario);
            }
            else
            {
                throw new UsuarioException("No se encontró un cliente con ese id");
            }
        }
    }
}
