using CasosDeUsos.DTOs;
using CasosDeUsos.DTOs.UsuariosDTO;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    internal class MapperEquipo
    {
        public static IEnumerable<ListadoEquipoDTO> ListEquipoToListEquipoDTO(
             IEnumerable<Equipo> Equipos)
        {
            return Equipos.Select(e => new ListadoEquipoDTO()
            {
                Id = e.Id,
                Nombre = e.Nombre,
            });
        }
    }
}
