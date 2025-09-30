using CasosDeUsos.DTOs.GastoDTO.GastoDTO;
using CasosDeUsos.DTOs.PagosDTO;
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using CasosDeUsos.InterfacesCasosUsos.IPagoCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUPago
{
    public class CUListadoPago : ICUListadoPago
    {
        public IRepositorioPago RepoPago { get; set; }
        public ICUListadoPago CUListadoPagos { get; set; }

        public CUListadoPago(IRepositorioPago repoPago)
        {
            RepoPago = repoPago;
        }

        public IEnumerable<ListadoPagoDTO> Ejecutar()
        {
            IEnumerable<Pago> pago = RepoPago.FindAll();
            return MapperPago.PagoToPagoListadoDTO(pago);
        }
    }
}
