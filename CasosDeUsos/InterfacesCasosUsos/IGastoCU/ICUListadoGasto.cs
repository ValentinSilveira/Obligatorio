using CasosDeUsos.DTOs.GastoDTO.GastoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.InterfacesCasosUsos.IGastoCU
{
    public interface ICUListadoGasto
    {
        IEnumerable<ListadoGastoDTO> Ejecutar();
    }
}
