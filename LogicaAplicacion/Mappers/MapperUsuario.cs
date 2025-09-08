using CasosDeUsos.DTOs;
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
            return new Usuario(usuarioDTO.Nombre, usuarioDTO.Apellido, usuarioDTO.Contrasenia, usuarioDTO.Email);
        }
    }
}
