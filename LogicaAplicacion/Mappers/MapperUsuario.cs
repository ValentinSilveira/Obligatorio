using CasosDeUsos.DTOs;
using ExcepcionesPropias.ExcepcionesEntidades;
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
            return new Usuario(usuarioDTO.Email, usuarioDTO.Contrasenia);
        }

        public static UsuarioLoginDTO UsuarioToUsuarioListadoDTO(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new UsuarioException("El usuario y/o la password es incorrecta");

            }
            return new UsuarioLoginDTO()
            {
                Email = usuario.Email.Valor,
                Descripcion = usuario.Rol.Descripcion
            };
        }
    }
}
