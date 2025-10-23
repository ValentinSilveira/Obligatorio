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
        public void RegistrarAuditoria(string usuario, string entidad, string operacion, string detalle)
        {
            var auditoria = new Auditoria
            {
                Usuario = usuario,
                Entidad = entidad,
                Operacion = operacion,
                Fecha = DateTime.Now,
                Detalle = detalle
            };

            RepoAuditoria.Add(auditoria);
        }

        public IEnumerable<Auditoria> ListarAuditorias()
        {
            return RepoAuditoria.FindAll();
        }
    }
}
