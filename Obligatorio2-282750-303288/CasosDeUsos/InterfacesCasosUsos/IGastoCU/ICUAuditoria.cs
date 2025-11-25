using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.InterfacesCasosUsos.IGastoCU
{
    public interface ICUAuditoria
    {
        IEnumerable<Auditoria> AuditoriasPorGasto(int idGasto);
        void RegistrarAuditoria(string usuario, string entidad, string operacion, string detalle, int? entidadId);
    }
}
