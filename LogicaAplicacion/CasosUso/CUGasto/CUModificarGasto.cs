using CasosDeUsos.DTOs.DTOsGasto;
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios.InterfacesGastos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUGasto
{
    public class CUModificarGasto : ICUModificarGasto
    {
        public IRepositorioGasto RepoGasto  { get; set; }

        public CUModificarGasto(IRepositorioGasto repoGasto)
        {
            RepoGasto = repoGasto;
        }

        public void Ejecutar(DetalleGastoDTO detalleGastoDTO, int id)
        {
            if (id <= 0) throw new ArgumentException("El gasto es incorrecto");
            if (detalleGastoDTO == null) throw new ArgumentNullException("Datos incorrectos");
            Gasto gasto = RepoGasto.FindById(id);
            gasto.Nombre = detalleGastoDTO.Nombre;
            gasto.Descripcion = detalleGastoDTO.Descripcion;
            RepoGasto.Update(gasto);
        }
    }
}
