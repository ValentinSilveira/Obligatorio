using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUGastos
{
    public class CUEliminarGasto : ICUEliminarGasto
    {
        public IRepositorioGasto RepoGasto { get; set; }
        public ICUAuditoria CUAuditoria { get; set; }

        public CUEliminarGasto(IRepositorioGasto repoGasto, ICUAuditoria cuAuditoria)
        {
            RepoGasto = repoGasto;
            CUAuditoria = cuAuditoria;
        }
        public void Ejecutar(int id, string usuario)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El id es incorrecto");
            }

            Gasto gasto = RepoGasto.FindById(id);
            if (gasto == null)
            {
                throw new GastoException("El gasto con ese id no existe");
            }

            bool tienePagos = RepoGasto.TienePagosAsociados(gasto.Id);
            if (tienePagos)
                throw new GastoException("No se puede eliminar el gasto porque tiene pagos asociados.");
            
            RepoGasto.Delete(gasto);

            CUAuditoria.RegistrarAuditoria(
                usuario,
                "Gasto",
                "Delete",
                $"Se eliminó el gasto '{gasto.Nombre}' (ID {gasto.Id})"
            );
        }
    }
}
