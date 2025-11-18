using CasosDeUsos.DTOs.GastoDTO.GastoDTO;
using CasosDeUsos.DTOs.GastosDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.InterfacesCasosUsos.IGastoCU
{
    public interface ICUEditarGasto
    {
        void Ejecutar(DetalleGastoDTO detalleGasto, int id, string usuario);
    }
}
