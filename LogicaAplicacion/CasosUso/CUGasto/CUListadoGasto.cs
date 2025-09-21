using CasosDeUsos.DTOs.DTOsGasto;
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios.InterfacesGastos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUGasto
{
    public class CUListadoGasto : ICUListadoGasto
    {
        public IRepositorioGasto RepoGasto { get; set; }
        public ICUListadoGasto CUListadoGastos { get; set; }

        public CUListadoGasto(IRepositorioGasto repoGasto)
        {
            RepoGasto = repoGasto;
        }

        public IEnumerable<ListadoGastoDTO> Ejecutar()
        {
            IEnumerable<Gasto> gasto = RepoGasto.FindAll();
            return MapperGasto.GastoToGastoListadoDTO(gasto);
        }
    }
}
