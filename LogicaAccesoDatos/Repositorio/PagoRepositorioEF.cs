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
            List<Pago> pagos = Contexto.Pagos
                .Include(p => p.TipoGasto)
                .Include(p => p.Usuario)
                .ToList();

            IEnumerable<Pago> filtrados = pagos.Where(p =>
                (p is Unico u && u.FechaPago >= fechaDesde && u.FechaPago <= fechaHasta)
                || (p is Recurrente r && r.FechaDesde <= fechaHasta && r.FechaHasta >= fechaDesde)
            );
            foreach (Pago pago in filtrados)
            {
                if (pago is Recurrente r)
                {
                    DateTime fechaReferencia = new DateTime(fechaDesde.Year, fechaDesde.Month, 1);

                    if (fechaReferencia < r.FechaHasta)
                    {
                        int mesesRestantes = ((r.FechaHasta.Year - fechaReferencia.Year) * 12)
                                           + (r.FechaHasta.Month - fechaReferencia.Month);

                        if (mesesRestantes < 0)
                            mesesRestantes = 0;

                        pago.SaldoPendiente = mesesRestantes * r.Monto;
                    }
                    else
                    {
                        pago.SaldoPendiente = 0;
                    }
                }
                else
                {
                    pago.SaldoPendiente = 0;
                }
            }

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
    }
}
