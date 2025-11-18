using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorio
{
    public class RepositorioEquipoEF : IRepositorioEquipo
    {
        public ObligatorioContexto Contexto { get; set; }
        public RepositorioEquipoEF(ObligatorioContexto contexto)
        {
            Contexto = contexto;
        }
        public void Add(Equipo item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Equipo item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Equipo> FindAll()
        {
            return Contexto.Equipos.ToList();
        }

        public Equipo FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Equipo item)
        {
            throw new NotImplementedException();
        }
    }
}
