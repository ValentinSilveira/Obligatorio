using CasosDeUsos.DTOs.UsuariosDTO;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperRol
    {
        public static IEnumerable<ListadoRolDTO> ListRolToListRolDTO(
             IEnumerable<Rol> Roles)
        {
            return Roles.Select(r => new ListadoRolDTO()
            {
                Id = r.Id,
                Descripcion = r.Descripcion
            });
        }
    }
}
