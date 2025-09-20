using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.interfacesRepositorios
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        Usuario FindByEmailAndPassword(string name, string password);
    }
}
