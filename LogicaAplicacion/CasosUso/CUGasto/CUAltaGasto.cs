using CasosDeUsos.DTOs;
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUGastos
{
    public class CUAltaGasto :ICUAltaGasto
    {
        public IRepositorioGasto RepoGasto { get; set; }
        public CUAltaGasto(IRepositorioGasto repositorioGasto)
        {
            RepoGasto = repositorioGasto;
        }

        public void Ejecutar(GastoDTO gastoDTO)
        {
            Gasto gasto = MapperGasto.GastoDTOToGasto(gastoDTO);
            RepoGasto.Add(gasto);
        }
    }
}
