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
    public class CUListadoPagoPorFecha : ICUListadoPagoPorFecha
    {
        IRepositorioPago RepoPago { get; set; }
        public ICUListadoPagoPorFecha CUListadoPagosPorFechas { get; set; }

        public CUListadoPagoPorFecha(IRepositorioPago repoPago)
        {
            RepoPago = repoPago;
        }

        public IEnumerable<ListadoPagoDTO> Ejecutar(DateTime fechaDesde, DateTime fechaHasta)
        {
            IEnumerable<Pago> pagos = RepoPago.FindByRangoFechas(fechaDesde, fechaHasta);
            return MapperPago.PagoToPagoListadoDTO(pagos);
        }
    }
}
