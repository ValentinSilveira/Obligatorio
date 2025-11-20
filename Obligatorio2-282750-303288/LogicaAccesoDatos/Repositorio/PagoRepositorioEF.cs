using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorio
{
    public class PagoRepositorioEF : IRepositorioPago
    {
        public ObligatorioContexto Contexto { get; set; }

        public PagoRepositorioEF(ObligatorioContexto contexto) 
        {
            Contexto = contexto;
        }
        public void Add(Pago item)
        {
            item.Validar();
            Pago pago = FindById(item.Id);
            if (pago == null)
            {
                Contexto.Pagos.Add(item);
                Contexto.SaveChanges();
            }
            else
            {
                throw new Exception("El Pago ya existe");
            }
        }

        public void Delete(Pago item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pago> FindAll()
        {
            return Contexto.Pagos
                .Include(p => p.TipoGasto)
                .Include(p => p.Usuario);
        }

        public Pago FindById(int id)
        {
            return Contexto.Pagos
                .Include(p => p.TipoGasto)
                .Include(p => p.Usuario)
                .Where(p => p.Id == id).SingleOrDefault();                    
        }

        public void Update(Pago item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pago> FindByRangoFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            if (fechaDesde > fechaHasta)
                throw new PagoException("La fecha de inicio debe ser menor a la fecha de fin");

            var todosLosPagos = Contexto.Pagos
                .Include(p => p.TipoGasto)
                .Include(p => p.Usuario)
                .ToList();

            var filtrados = todosLosPagos.Where(p =>
                (p is Unico u && u.FechaPago >= fechaDesde && u.FechaPago <= fechaHasta)
                || (p is Recurrente r && r.FechaDesde <= fechaHasta && r.FechaHasta >= fechaDesde)
            );

            return filtrados;
        }
        public IEnumerable<Pago> FindByRangoPrecio(decimal montoMinimo)
        {
            return Contexto.Pagos
                .Include(p => p.TipoGasto)
                .Include(p => p.Usuario)
                .Where(p => p is Unico && p.Monto > montoMinimo)
                .OrderBy(p => ((Unico)p).FechaPago)
                .ToList();
        }
        public bool ExisteRecibo(string nroRecibo)
        {
            return Contexto.Pagos
                .Any(p => EF.Property<string>(p, "TipoPago") == "Unico"
                       && ((Unico)p).NroRecibo == nroRecibo);
        }

        /*
        Permitirá obtener el detalle de los pagos, incluyendo los tipos de gasto. Deberá controlar que el usuario
        utilizado para el filtro sea el usuario que envía la solicitud.
       */

        public IEnumerable<Pago> PagosDeUsuarioDado(int idUsuario)
        {
            return Contexto.Pagos
                .Include(p => p.TipoGasto)
                .Include(p => p.Usuario)
                .Where(p => p.Usuario.Id == idUsuario)
                .ToList();
        }

        /*
            Dado un monto, permitirá obtener todos los equipos en los que sus empleados hayan realizado pagos únicos
            por un monto superior al dado. Los equipos no deberán repetirse y deberán estar ordenados por nombre en
            forma descendente. 
        */

        public IEnumerable<Equipo> PagosUnicosConMontoSuperior(decimal monto)
        {
            return Contexto.Pagos
                .Include(p => p.Usuario)
                .ThenInclude(u => u.Equipo)
                .Where(p => p is Unico && p.Monto > monto)
                .Select(p => p.Usuario.Equipo)
                .Distinct()
                .OrderByDescending(e => e.Nombre);
        }
    }
}
