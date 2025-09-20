using CasosDeUsos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.InterfacesCasosUsos.IUsuarioCU
{
    public interface ICUAltaGasto
    {
        void Ejecutar(GastoDTO gastoDTO);
    }
}
