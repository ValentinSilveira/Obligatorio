using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorio
{
    public class RepositorioRolEF : IRepositorioRol
    {
        public ObligatorioContexto Contexto { get; set; }
        public RepositorioRolEF(ObligatorioContexto contexto)
        {
            Contexto = contexto;
        }
        public void Add(Rol item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Rol item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rol> FindAll()
        {
            return Contexto.Roles.ToList();
        }

        public Rol FindById(int id)
        {
            throw new NotImplementedException();
            //return Contexto.Roles
            //    .Where(c => c.Id == id)
            //    .SingleOrDefault();
        }

        public void Update(Rol item)
        {
            throw new NotImplementedException();
        }
    }
}
