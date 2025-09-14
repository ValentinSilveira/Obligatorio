using CasosDeUsos.DTOs;
using LogicaAplicacion.InterfacesCasosUsos;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class ListadoRoles : IListadoRoles
    {
        public IRepositorioRol RepoRol { get; set; }
        public ListadoRoles(IRepositorioRol repoRol)
        {
            RepoRol = repoRol;
        }
        public IEnumerable<RolDTO> Ejecutar()
        {
            IEnumerable<Rol> Roles = RepoRol.FindAll();
            return MapperRol.ListRolToListRolDTO(Roles);
        }
    }
}
