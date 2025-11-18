using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.interfacesRepositorios
{
    public interface IRepositorioPago : IRepositorio<Pago>
    {
        IEnumerable<Pago> FindByRangoFechas(DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Pago> FindByRangoPrecio(decimal precioMinimo);
        bool ExisteRecibo(string nroRecibo);
        IEnumerable<Pago> PagosDeUsuarioDado(int idUsuario);
        IEnumerable<Equipo> PagosUnicosConMontoSuperior(int monto);
    }
}
