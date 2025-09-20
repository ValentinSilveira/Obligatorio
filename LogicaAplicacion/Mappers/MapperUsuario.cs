using CasosDeUsos.DTOs;
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
        public static Usuario UsuarioDTOToUsuario(UsuarioDTO usuarioDTO) 
        {
            if(usuarioDTO == null) 
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new Usuario(usuarioDTO.Email, usuarioDTO.Contraseña);
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
                    Nombre = usuario.Nombre,
                });

            }
            return listadoClientes;
        }

        public static UsuarioLoginDTO UsuarioToUsuarioListadoDTO(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new UsuarioException("El usuario y/o la password es incorrecta");

            }
            return new UsuarioLoginDTO()
            {
                Email = usuario.Email,
                NombreRol = usuario.Rol.Descripcion
            };
        }

        public static CasosDeUsos.DTOs.DetalleUsuarioDTO ClienteToDetalleClienteDTO(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new CasosDeUsos.DTOs.DetalleUsuarioDTO()
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido
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
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                DescripcionRol = usuario.Rol.Descripcion
            };
            
        }
    }
}
