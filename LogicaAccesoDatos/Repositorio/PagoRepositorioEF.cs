using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios.InterfacesPagos;
using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }

        public Pago FindById(int id)
        {
            return Contexto.Pagos.Where(p => p.Id == id).SingleOrDefault();                    
        }

        public void Update(Pago item)
        {
            throw new NotImplementedException();
        }
    }
}
