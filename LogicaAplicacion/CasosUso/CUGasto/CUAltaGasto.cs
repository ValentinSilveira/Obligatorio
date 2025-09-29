<<<<<<< HEAD
﻿using CasosDeUsos.DTOs.GastosDTO;
=======
﻿using CasosDeUsos.DTOs.DTOsGasto;
>>>>>>> origin
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
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
