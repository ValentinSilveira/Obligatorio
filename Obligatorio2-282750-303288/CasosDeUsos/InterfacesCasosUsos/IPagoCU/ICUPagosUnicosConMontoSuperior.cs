using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.InterfacesCasosUsos.IPagoCU
{
    public interface ICUPagosUnicosConMontoSuperior
    {
        IEnumerable<Equipo> Ejecutar(int monto);
    }
}
