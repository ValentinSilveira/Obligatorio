using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.interfacesRepositorios
{
    public interface IRepositorioAuditoria
    {
        void Add(EntidadesNegocio.Auditoria item);
        IEnumerable<EntidadesNegocio.Auditoria> FindAll();
    }
}
