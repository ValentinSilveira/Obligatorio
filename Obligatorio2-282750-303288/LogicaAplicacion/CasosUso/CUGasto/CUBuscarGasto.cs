using CasosDeUsos.DTOs.GastoDTO.GastoDTO;
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using ExcepcionesPropias.ExcepcionesEntidades;
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
    public class CUBuscarGasto : ICUBuscarGasto
    {
        public IRepositorioGasto RepoGasto { get; set; }

        public CUBuscarGasto(IRepositorioGasto repoGasto)
        {
            RepoGasto = repoGasto;
        }
        public DetalleGastoDTO Ejecutar(int id)
        {
            Gasto gasto = RepoGasto.FindById(id);
            if (gasto != null)
            {
                return MapperGasto.GastoToDetalleGastoDTO(gasto);
            }
            else
            {
                throw new GastoException("No se encontró un Gasto con ese id");
            }
        }
    }
}
