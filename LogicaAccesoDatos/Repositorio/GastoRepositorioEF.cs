using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorio
{
    public class GastoRepositorioEF : IRepositorioGasto
    {
        public ObligatorioContexto Contexto { get; set; }
        public GastoRepositorioEF(ObligatorioContexto contexto)
        {
            Contexto = contexto;
        }
        public void Add(Gasto item)
        {
            item.Validar();
            Gasto gasto = FindById(item.Id);
            if (gasto == null)
            {
                Contexto.Gastos.Add(item);
                Contexto.SaveChanges();
            }
            else
            {
                throw new Exception("El gasto ya existe");
            }
        }

        public void Delete(Gasto item)
        {
            Contexto.Gastos.Remove(item);
            Contexto.SaveChanges();
        }

        public IEnumerable<Gasto> FindAll()
        {
            return Contexto.Gastos;
        }

        public Gasto FindById(int id)
        {
            return Contexto.Gastos
                .Where(g => g.Id == id)
                .SingleOrDefault();
        }

        public void Update(Gasto item)
        {
            var existente = Contexto.Gastos.Find(item.Id);
            if (existente == null)
                throw new Exception("El gasto no existe.");
            existente.Nombre = item.Nombre;
            existente.Descripcion = item.Descripcion;

            Contexto.SaveChanges();
        }
    }
}
