using CasosDeUsos.DTOs.UsuariosDTO;
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using LogicaAplicacion.InterfacesCasosUsos;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuarios
{
    public class ListadoRoles : ICUListadoRol
    {
        public IRepositorioRol RepoRol { get; set; }
        public ICUListadoRol CUListadoRoles { get; set; }
        
        public ListadoRoles(IRepositorioRol repoRol)
        {
            RepoRol = repoRol;
        }
        public IEnumerable<ListadoRolDTO> Ejecutar()
        {
            IEnumerable<Rol> Roles = RepoRol.FindAll();
            return MapperRol.ListRolToListRolDTO(Roles);
        }
    }
}
