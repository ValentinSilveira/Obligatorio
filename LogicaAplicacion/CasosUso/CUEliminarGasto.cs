using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class CUEliminarGasto : ICUEliminarGasto
    {
        public IRepositorioGasto RepoGasto { get; set; }

        public CUEliminarGasto(IRepositorioGasto repoGasto)
        {
            RepoGasto = repoGasto;
        }

        public void Ejecutar(int id)
        {
            Gasto gasto = RepoGasto.FindById(id);
            if (id > 0)
            {
                if (gasto != null)
                {
                    RepoGasto.Delete(gasto);
                }
                else
                {
                    throw new GastoException("El gasto con ese id no existe");
                }
            }
            else
            {
                throw new ArgumentException("El id es incorrecto");
            }

        }
    }
}
