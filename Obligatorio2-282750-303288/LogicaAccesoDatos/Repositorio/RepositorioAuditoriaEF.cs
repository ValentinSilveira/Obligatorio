using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorio
{
    public class RepositorioAuditoriaEF : IRepositorioAuditoria
    {
        public ObligatorioContexto Contexto { get; set; }

        public RepositorioAuditoriaEF(ObligatorioContexto contexto)
        {
            Contexto = contexto;
        }
        public void Add(Auditoria item)
        {
            Contexto.Auditorias.Add(item);
            Contexto.SaveChanges();
        }

        public IEnumerable<Auditoria> FindAll()
        {
            return Contexto.Auditorias;
        }

        public void Delete(Auditoria item)
        {
            throw new NotImplementedException();
        }

        public void Update(Auditoria item)
        {
            throw new NotImplementedException();
        }

        public Auditoria FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
