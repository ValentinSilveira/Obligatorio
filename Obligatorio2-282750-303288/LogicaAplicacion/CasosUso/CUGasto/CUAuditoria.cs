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
    public class CUAuditoria : ICUAuditoria
    {
        public IRepositorioAuditoria RepoAuditoria { get; set; }
        public CUAuditoria(IRepositorioAuditoria repoAuditoria)
        {
            RepoAuditoria = repoAuditoria;
        }
        public void RegistrarAuditoria(string usuario, string entidad, string operacion, string detalle, int? entidadId)
        {
            var auditoria = new Auditoria
            {
                Usuario = usuario,
                Entidad = entidad,
                Operacion = operacion,
                Fecha = DateTime.Now,
                Detalle = detalle,
                EntidadId = entidadId
            };

            RepoAuditoria.Add(auditoria);
        }

        public IEnumerable<Auditoria> ListarAuditorias()
        {
            return RepoAuditoria.FindAll();
        }

        public IEnumerable<Auditoria> AuditoriasPorGasto(int idGasto)
        {
            return RepoAuditoria
                .FindAll()
                .Where(a => a.Entidad == "Gasto" && a.EntidadId == idGasto)
                .OrderByDescending(a => a.Fecha);
        }
    }
}
