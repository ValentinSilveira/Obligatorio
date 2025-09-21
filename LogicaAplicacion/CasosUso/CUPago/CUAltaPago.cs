using CasosDeUsos.DTOs;
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
    public class CUAltaPago : ICUAltaPago
    {
        public IRepositorioPago RepoPago { get; set; }

        public CUAltaPago(IRepositorioPago repoPago)
        {
            RepoPago = repoPago;
        }

        public void Ejecutar(PagoDTO pagoDTO)
        {
            /*
            Pago pago = MapperPago.PagoDTOToPago(pagoDTO);
            RepoPago.Add(pago);
            */
        }
    }
}
