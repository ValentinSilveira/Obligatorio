using CasosDeUsos.DTOs;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    internal class MapperRol
    {
        public static Rol RolDTOToRol(RolDTO rolDTO)
        {
            if (rolDTO == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new Rol(rolDTO.Descripcion);
        }
    }
}
