<<<<<<< HEAD
﻿using CasosDeUsos.DTOs.GastoDTO.GastoDTO;
=======
﻿using CasosDeUsos.DTOs.DTOsGasto;
>>>>>>> origin
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios.InterfacesGastos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<< HEAD
namespace LogicaAplicacion.CasosUso.CUGastos
=======
namespace LogicaAplicacion.CasosUso.CUGasto
>>>>>>> origin
{
    public class CUBuscarGasto : ICUBuscarGasto
    {
        public IRepositorioGasto RepoGasto { get; set; }

        public CUBuscarGasto(IRepositorioGasto repoGasto)
        {
            RepoGasto = repoGasto;
        }

        public CUBuscarGasto()
        {
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
