using CasosDeUsos.DTOs.PagosDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.InterfacesCasosUsos.IPagoCU
{
    public interface ICUAltaPagoRecurrente
    {
        void Ejecutar(PagoRecurrenteDTO pagoRecurrenteDTO);
    }
}
