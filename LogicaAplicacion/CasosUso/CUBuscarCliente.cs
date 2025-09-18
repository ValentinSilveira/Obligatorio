using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class CUBuscarCliente
    {
        public IRepositorioUsuario RepoUsuario { get; set; }

        public CUBuscarCliente(IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
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
