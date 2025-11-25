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

namespace LogicaAplicacion.CasosUso.CUGasto
{
    public class CUEditarGasto : ICUEditarGasto
    {
        public IRepositorioGasto RepoGasto { get; set; }
        public ICUAuditoria CUAuditoria { get; set; }

        public CUEditarGasto(IRepositorioGasto repoGasto, ICUAuditoria cuAuditoria)
        {
            RepoGasto = repoGasto;
            CUAuditoria = cuAuditoria;
        }
        public void Ejecutar(DetalleGastoDTO detalleGasto, int id, string usuario)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id inválido");
            }
            if (detalleGasto == null)
            {
                throw new ArgumentNullException("Datos inválidos");
            }
            if (RepoGasto.ExistePorNombre(detalleGasto.Nombre, id))
            {
                throw new GastoException($"Ya existe un gasto con el nombre '{detalleGasto.Nombre}'");
            }    

            Gasto gasto = RepoGasto.FindById(id);
            if (gasto == null)
            {
                throw new Exception("No se encontró el gasto");
            }

            gasto.Nombre = detalleGasto.Nombre;
            gasto.Descripcion = detalleGasto.Descripcion;
            RepoGasto.Update(gasto);

            CUAuditoria.RegistrarAuditoria(
                usuario,
                "Gasto",
                "Edit",
                $"Se editó el gasto '{gasto.Nombre}' (ID {gasto.Id})",
                gasto.Id
            );
        }
    }
}
