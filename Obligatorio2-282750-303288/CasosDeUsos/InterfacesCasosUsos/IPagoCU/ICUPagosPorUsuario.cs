using CasosDeUsos.DTOs.PagosDTO;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.InterfacesCasosUsos.IPagoCU
{
    public interface ICUPagosPorUsuario
    {
        IEnumerable<ListadoPagoDTO> Ejecutar(int id);
    }
}
