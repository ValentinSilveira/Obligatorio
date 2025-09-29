using CasosDeUsos.DTOs.PagosDTO;
using CasosDeUsos.InterfacesCasosUsos.IPagoCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUPago
{
    public class CUAltaPagoRecurrente : ICUAltaPagoRecurrente
    {
        public IRepositorioPago RepoPago { get; set; }
        public IRepositorioUsuario RepoUsuario { get; set; }
        public IRepositorioGasto RepoGasto { get; set; }


        public CUAltaPagoRecurrente(IRepositorioPago repoPago, IRepositorioUsuario repoUsuario, IRepositorioGasto repoGasto)
        {
            RepoPago = repoPago;
            RepoUsuario = repoUsuario;
            RepoGasto = repoGasto;
        }

        public void Ejecutar(PagoRecurrenteDTO pagoRecurrenteDTO)
        {
            Usuario usuario = RepoUsuario.FindById(pagoRecurrenteDTO.UsuarioId);
            Gasto gasto = RepoGasto.FindById(pagoRecurrenteDTO.GastoId);

            if (usuario != null && gasto != null)
            {
                Recurrente pagoRecurrente = MapperPago.PagoRecurrenteDTOToPagoRecurrente(pagoRecurrenteDTO, usuario, gasto);
                RepoPago.Add(pagoRecurrente);
            }
            else
            {
                throw new ArgumentNullException("El usuario o el gasto no son validos");

            }
        }
    }
}
