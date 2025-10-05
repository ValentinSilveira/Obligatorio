using CasosDeUsos.DTOs.UsuariosDTO;
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUListadoEquipos : ICUListadoEquipo
    {
        public IRepositorioEquipo RepoEquipo { get; set; }
        public ICUListadoEquipo CUListadoEquipo { get; set; }
        public CUListadoEquipos(IRepositorioEquipo repoEquipo)
        {
            RepoEquipo = repoEquipo;
        }
        public IEnumerable<ListadoEquipoDTO> Ejecutar()
        {
            IEnumerable<Equipo> Equipos = RepoEquipo.FindAll();
            return MapperEquipo.ListEquipoToListEquipoDTO(Equipos);
        }
    }
}
