using ExcepcionesPropias.ExcepcionesEntidades;
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
            item.Validar();
            Usuario usuario = FindByEmailAndPassword(item.Email);
            if (usuario == null)
            {
                Contexto.Usuarios.Add(item);
                Contexto.SaveChanges();
            }
            else
            {
                throw new UsuarioException("El rut ya existe");
            }
        }

        private Usuario FindByEmailAndPassword(string email)
        {
            return Contexto.Usuarios
                .Where(c => c.Email == email)
                .SingleOrDefault();
        }

        public void Delete(Usuario item)
        {
            Contexto.Usuarios.Remove(item);
            Contexto.SaveChanges();
        }

        public IEnumerable<Usuario> FindAll()
        {
            return Contexto.Usuarios;
        }

        public Usuario FindById(int id)
        {
            return Contexto.Usuarios
                .Where(c => c.Id == id)
                .SingleOrDefault();
        }

        public void Update(Usuario item)
        {
            throw new NotImplementedException();
        }

        public Usuario FindByEmailAndPassword(string name, string password)
        {
            throw new NotImplementedException();
        }
    }
}
