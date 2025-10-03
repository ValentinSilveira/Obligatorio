using CasosDeUsos.DTOs.GastoDTO.GastoDTO;
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUGasto
{
    public class CUEditarGasto : ICUEditarGasto
    {
        public IRepositorioGasto RepoGasto { get; set; }

        public CUEditarGasto(IRepositorioGasto repoGasto)
        {
            RepoGasto = repoGasto;
        }

        public CUEditarGasto(){}

        public void Ejecutar(DetalleGastoDTO detalleGasto, int id)
        {
            if( id <= 0)
            {
                throw new ArgumentException("Id inválido");
            }
            if(detalleGasto == null)
            {
               throw new ArgumentNullException("Datos inválidos");
            }
            Gasto gasto = RepoGasto.FindById(id);
            gasto.Nombre = detalleGasto.Nombre;
            gasto.Descripcion = detalleGasto.Descripcion;
            RepoGasto.Update(gasto);
        }
    }
}
