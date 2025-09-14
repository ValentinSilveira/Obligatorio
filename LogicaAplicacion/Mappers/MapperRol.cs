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
        public static IEnumerable<RolDTO> ListRolToListRolDTO(
             IEnumerable<Rol> Roles)
        {
            return Roles.Select(r => new RolDTO()
            {
                Id = r.Id,
                Descripcion = r.Descripcion
            });
        }
    }
}
