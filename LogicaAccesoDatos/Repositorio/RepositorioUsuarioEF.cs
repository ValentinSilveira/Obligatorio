using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios.InterfacesUsuarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorio
{
    public class RepositorioUsuarioEF : IRepositorioUsuario
    {
        public ObligatorioContexto Contexto { get; set; }

        public RepositorioUsuarioEF(ObligatorioContexto contexto)
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
                .Include(u => u.Rol)
                .Where(c => c.Id == id)
                .SingleOrDefault();
        }

        public void Update(Usuario item)
        {
            throw new NotImplementedException();
        }

        public Usuario FindByEmailAndPassword(string email, string password)
        {
            return Contexto.Usuarios
                    .Include(u => u.Rol)
                    .FirstOrDefault(u => u.Email == email && u.Password.Valor == password);
        }
    }
}
