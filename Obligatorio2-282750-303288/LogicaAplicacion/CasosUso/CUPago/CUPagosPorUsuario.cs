using CasosDeUsos.InterfacesCasosUsos.IPagoCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUPago
{
    public class CUPagosPorUsuario : ICUPagosPorUsuario
    {
        public IRepositorioPago RepoPago { get; set; }
        public IRepositorioUsuario RepoUsuario { get; set; }

        public CUPagosPorUsuario(IRepositorioPago repoPago, IRepositorioUsuario repoUsuario)
        {
            RepoPago = repoPago;
            RepoUsuario = repoUsuario;
        }
        public IEnumerable<Pago> Ejecutar(int id)
        {
            if (id <= 0) throw new PagoException("Id invalido");
            return RepoPago.PagosDeUsuarioDado(id);
        }
    }
}
