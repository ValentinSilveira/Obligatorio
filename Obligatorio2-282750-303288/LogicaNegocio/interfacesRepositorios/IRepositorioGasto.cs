using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.interfacesRepositorios
{
    public interface IRepositorioGasto : IRepositorio<Gasto>
    {
        bool TienePagosAsociados(int idGasto);
        bool ExistePorNombre(string nombre, int? idExcluido = null);
    }
}
