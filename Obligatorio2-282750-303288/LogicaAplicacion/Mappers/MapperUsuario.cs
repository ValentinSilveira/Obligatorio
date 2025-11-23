using CasosDeUsos.DTOs.UsuariosDTO;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.CasosUso;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    internal class MapperUsuario
    {
        public static Usuario UsuarioDTOToUsuario(UsuarioApiDTO usuarioDTO) 
        {
            if(usuarioDTO == null) 
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            Usuario usuario = new Usuario(usuarioDTO.Password, usuarioDTO.Apellido, usuarioDTO.Nombre)
            {
                RolId = usuarioDTO.RolId,
                EquipoId = usuarioDTO.EquipoId
            };

            return usuario;
        }
        public static IEnumerable<ListadoUsuarioDTO> UsuarioToUsuarioListadoDTO(IEnumerable<Usuario> Usuarios)
        {
            List<ListadoUsuarioDTO> listadoClientes = new List<ListadoUsuarioDTO>();

            foreach (Usuario usuario in Usuarios)
            {
                listadoClientes.Add(new ListadoUsuarioDTO()
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Nombre = usuario.NombreUsuario,
                });

            }
            return listadoClientes;
        }
        public static UsuarioLogueadoDTO UsuarioToUsuarioLogueadoDTO(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new UsuarioException("El usuario y/o la password es incorrecta");

            }
            return new UsuarioLogueadoDTO()
            {
                Id = usuario.Id,
                Email = usuario.Email,
                NombreUsuario = usuario.NombreUsuario,
                Rol = usuario?.Rol?.Descripcion,
            };

        }
        public static DetalleUsuarioDTO UsuarioToDetalleUsuarioDTO(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new DetalleUsuarioDTO()
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Nombre = usuario.NombreUsuario,
                Apellido = usuario.Apellido,
                Rol = usuario?.Rol?.Descripcion,
                Equipo = usuario.Equipo.Nombre
            };            
            
        }        
    }
}
