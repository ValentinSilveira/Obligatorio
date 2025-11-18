using CasosDeUsos.DTOs.GastosDTO;
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
    public class CUAltaGasto : ICUAltaGasto
    {        
        public IRepositorioGasto RepoGasto { get; set; }
        public ICUAuditoria CUAuditoria { get; set; }

        public CUAltaGasto(IRepositorioGasto repoGasto, ICUAuditoria cuAuditoria)
        {
            RepoGasto = repoGasto;
            CUAuditoria = cuAuditoria;
        }
        public void Ejecutar(GastoDTO gastoDTO, string usuario)
        {
            if (gastoDTO == null)
            {
                throw new ArgumentNullException("Datos inválidos");
            }
            if (RepoGasto.ExistePorNombre(gastoDTO.Nombre))
            {
                throw new GastoException($"Ya existe un gasto con el nombre '{gastoDTO.Nombre}'");
            }

            Gasto gasto = MapperGasto.GastoDTOToGasto(gastoDTO);
            RepoGasto.Add(gasto);

            CUAuditoria.RegistrarAuditoria(
                usuario,
                "Gasto",
                "Create",
                $"Se creó el gasto '{gasto.Nombre}' con ID {gasto.Id}"
            );
        }
    }
}
