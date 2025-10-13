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
        public IRepositorioAuditoria RepoAuditoria { get; set; }
        public IRepositorioGasto RepoGasto { get; set; }
        public CUActualizarGasto(IRepositorioAuditoria repositorioAuditoria, IRepositorioGasto repoGasto)
        {
            RepoAuditoria = repositorioAuditoria;
            RepoGasto = repoGasto;
        }
        public void Ejecutar(Gasto gasto, string usuario)
        {
            RepoGasto.Update(gasto);
            var registro = new Auditoria
            {
                Usuario = usuario,
                Fecha = DateTime.Now,
                Operacion = "Update",
                Detalle = $"Se actualizó el gasto '{gasto.Nombre}' con Id {gasto.Id}",
            };
            RepoAuditoria.Add(registro);
        }
    }
}
