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
            var pagos = RepoPago.FindByRangoFechas(fechaDesde, fechaHasta);

            foreach (var pago in pagos)
            {
                if (pago is Recurrente r)
                {
                    var fechaReferencia = new DateTime(fechaDesde.Year, fechaDesde.Month, 1);
                    int mesesRestantes = Math.Max(0, ((r.FechaHasta.Year - fechaReferencia.Year) * 12) + (r.FechaHasta.Month - fechaReferencia.Month));
                    pago.SaldoPendiente = mesesRestantes * r.Monto;
                }
                else
                {
                    pago.SaldoPendiente = 0;
                }
            }

            return MapperPago.PagoToPagoListadoDTO(pagos);
        }
    }
}
