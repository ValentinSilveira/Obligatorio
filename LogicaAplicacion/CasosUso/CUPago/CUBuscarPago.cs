using CasosDeUsos.DTOs.PagosDTO;
using CasosDeUsos.InterfacesCasosUsos.IPagoCU;
using ExcepcionesPropias.ExcepcionesEntidades;
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
    public class CUBuscarPago : ICUBuscarPago
    {
        public IRepositorioPago RepoPago { get; set; }

        public CUBuscarPago (IRepositorioPago repoPago)
        {
            RepoPago = repoPago;
        }

        public CUBuscarPago() { }
        public DetallePagoDTO Ejecutar(int id)
        {
            Pago pago = RepoPago.FindById(id);
            if(pago != null) 
            {
                return MapperPago.PagoToDetallePagoDTO(pago);
            }
            else 
            {
                throw new PagoException("No se encontró un pago con ese id");
            }
        }
    }
}
