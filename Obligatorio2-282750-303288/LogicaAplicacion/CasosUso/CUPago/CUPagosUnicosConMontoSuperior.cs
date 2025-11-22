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
    public class CUPagosUnicosConMontoSuperior : ICUPagosUnicosConMontoSuperior
    {
        public IRepositorioPago RepoPago { get; set; }
        public CUPagosUnicosConMontoSuperior(IRepositorioPago repoPago)
        {
            RepoPago = repoPago;
        }

        public IEnumerable<Equipo> Ejecutar(decimal monto)
        {
            if (monto < 0)
                throw new ArgumentException("El monto no puede ser menor que 0");

            return RepoPago.PagosUnicosConMontoSuperior(monto);
        }
    }
}
