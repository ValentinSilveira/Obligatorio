using CasosDeUsos.DTOs.GastosDTO;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperAuditoria
    {
        public static AuditoriaDTO AuditoriaToDTO(Auditoria a)
        {
            return new AuditoriaDTO
            {
                Id = a.Id,
                Usuario = a.Usuario,
                Entidad = a.Entidad,
                Operacion = a.Operacion,
                Fecha = a.Fecha,
                Detalle = a.Detalle
            };
        }

        public static IEnumerable<AuditoriaDTO> ListToDTO(IEnumerable<Auditoria> lista)
        {
            return lista.Select(a => AuditoriaToDTO(a));
        }
    }
}
