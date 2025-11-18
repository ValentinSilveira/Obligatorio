using CasosDeUsos.DTOs.PagosDTO;
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
    public class CUListadoPorPrecio : ICUListadoPorPrecio
    {
        IRepositorioPago RepoPago { get; set; }
        public CUListadoPorPrecio(IRepositorioPago repoPago)
        {
            RepoPago = repoPago;
        }
        public IEnumerable<ListadoPagoDTO> Ejecutar (decimal precioMinimo)
        {
            IEnumerable<Pago> pagos = RepoPago.FindByRangoPrecio(precioMinimo);
            return MapperPago.PagoToPagoListadoDTO(pagos);
        }
    }
}
