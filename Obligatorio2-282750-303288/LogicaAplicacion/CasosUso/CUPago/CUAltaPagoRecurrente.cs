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


        public void Ejecutar(PagoRecurrenteAPIDTO pagoRecurrenteDTO)
        {
            Usuario usuario = RepoUsuario.FindById(pagoRecurrenteDTO.UsuarioId);
            if (usuario == null)
                throw new ArgumentException("El usuario no existe.");

            
            Gasto gasto = RepoGasto.FindById(pagoRecurrenteDTO.GastoId);
            if (gasto == null)
                throw new ArgumentException("El gasto no existe.");

            
            if (!Enum.TryParse(pagoRecurrenteDTO.MetodoPago, true, out MetodoPago metodoPago))
                throw new ArgumentException("Método de pago inválido.");

            
            Recurrente pagoRecurrente = MapperPago.PagoRecurrenteDTOToPagoRecurrente(
                pagoRecurrenteDTO, usuario, gasto, metodoPago);

            
            RepoPago.Add(pagoRecurrente);
        }     
    }
}
