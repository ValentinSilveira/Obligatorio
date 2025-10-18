using CasosDeUsos.DTOs;
using CasosDeUsos.DTOs.PagosDTO;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LogicaAplicacion.Mappers
{
    public class MapperPago
    {


        public static Unico PagoUnicoDTOToPagoUnico(PagoUnicoDTO pagoUnicoDTO, Usuario usuario, Gasto gasto, MetodoPago metodoPago)
        {
            if (pagoUnicoDTO == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }

            return new Unico(gasto, usuario, metodoPago, pagoUnicoDTO.Descripcion, pagoUnicoDTO.Monto, pagoUnicoDTO.FechaPago, pagoUnicoDTO.Recibo);
        }


        public static Recurrente PagoRecurrenteDTOToPagoRecurrente(PagoRecurrenteDTO pagoRecurrenteDTO, Usuario usuario, Gasto gasto, MetodoPago metodoPago)
        {
            if (pagoRecurrenteDTO == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new Recurrente(gasto, usuario,metodoPago, pagoRecurrenteDTO.Descripcion, pagoRecurrenteDTO.Monto, pagoRecurrenteDTO.FechaDesde, pagoRecurrenteDTO.FechaHasta);
        }

        public static IEnumerable<ListadoPagoDTO> PagoToPagoListadoDTO(IEnumerable<Pago> pagos)
        {
            List<ListadoPagoDTO> listadoPagos = new List<ListadoPagoDTO>();
            foreach (Pago pago in pagos)
            {
                listadoPagos.Add(new ListadoPagoDTO()
                {
                    Id = pago.Id,
                    TipoPago = pago is Unico ? "Unico" : "Recurrente",
                    Tipo = pago.TipoGasto.Nombre,
                    Monto = pago.Monto,
                    FechaDesde = pago is Unico ? ((Unico)pago).FechaPago : ((Recurrente)pago).FechaDesde,
                    FechaHasta = pago is Recurrente ? ((Recurrente)pago).FechaHasta : null,
                    UsuarioNombre = pago.Usuario.Nombre,
                    GastoDescripcion = pago.TipoGasto.Descripcion,
                    MetodoPago = pago.Metodo.ToString(),
                    SaldoPendiente = pago.SaldoPendiente,
                    Descripcion = pago.Descripcion,
                    FechaPago = pago is Unico u ? u.FechaPago : (DateTime?)null,
                    Recibo = pago is Unico u2 ? u2.NroRecibo : null,
                });
            }
            return listadoPagos;
        }

        public static IEnumerable<SelectListItem> GetMetodosDePagoSelectList()
        {
            return Enum.GetValues(typeof(MetodoPago))
                       .Cast<MetodoPago>()
                       .Select(m => new SelectListItem
                       {
                           Value = ((int)m).ToString(),
                           Text = m.ToString()
                       });
        }

        public static DetallePagoDTO PagoToDetallePagoDTO(Pago pago)
        {
            if (pago == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new DetallePagoDTO
            {
                Id = pago.Id,
                TipoGasto = pago.TipoGasto.Nombre,
                Usuario = pago.Usuario.Nombre,
                MetodoPago = pago.Metodo.ToString(),
                Descripcion = pago.Descripcion,
                Monto = pago.Monto
            };
        }
    }
}
