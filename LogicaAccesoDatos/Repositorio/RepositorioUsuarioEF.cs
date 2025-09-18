using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorio
{
    public class RepositorioUsuarioEF : IRepositorioUsuario
    {
        public EmpresaContexto Contexto { get; set; }
        public RepositorioUsuarioEF(EmpresaContexto contexto)
        {
            Contexto = contexto;
        }

        public void Add(Usuario item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> FindAll()
        {
            throw new NotImplementedException();
        }

        public Usuario FindById(int id)
        {
            return Contexto.Usuarios
                .Where(c => c.Id == id)
                .SingleOrDefault();
        }

        public void Update(Usuario item, int id)
        {
            throw new NotImplementedException();
        }

        public Usuario FindByEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool ExisteEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
