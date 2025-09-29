using CasosDeUsos.DTOs.DTOsGasto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.InterfacesCasosUsos.IGastoCU
{
    public interface ICUModificarGasto
    {
        void Ejecutar(DetalleGastoDTO detalleGastoDTO, int id);
    }
}
