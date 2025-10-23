using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUGasto
{
    public class CUActualizarGasto : ICUActualizarGasto
    {
        public IRepositorioGasto RepoGasto { get; set; }
        public ICUAuditoria CUAuditoria { get; set; }

        public CUActualizarGasto(IRepositorioGasto repoGasto, ICUAuditoria cuAuditoria)
        {
            RepoGasto = repoGasto;
            CUAuditoria = cuAuditoria;
        }
        public void Ejecutar(Gasto gasto, string usuario)
        {
            if (gasto == null)
            {
                throw new ArgumentNullException("Datos inválidos");
            }

            RepoGasto.Update(gasto);

            CUAuditoria.RegistrarAuditoria(
                usuario,
                "Gasto",
                "Update",
                $"Se actualizó el gasto '{gasto.Nombre}' con Id {gasto.Id}"
            );
        }
    }
}
