using CasosDeUsos.DTOs;
using CasosDeUsos.DTOs.PagosDTO;
using CasosDeUsos.InterfacesCasosUsos.IPagoCU;
using ExcepcionesPropias.ExcepcionesEntidades;
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
    public class CUAltaPagoUnico : ICUAltaPagoUnico
    {
        public IRepositorioPago RepoPago { get; set; }
        public IRepositorioUsuario RepoUsuario { get; set; }
        public IRepositorioGasto RepoGasto { get; set; }


        public CUAltaPagoUnico(IRepositorioPago repoPago, IRepositorioUsuario repoUsuario,IRepositorioGasto repoGasto)
        {
            RepoPago = repoPago;
            RepoUsuario = repoUsuario;
            RepoGasto = repoGasto;
        }


        public void Ejecutar(PagoUnicoAPIDTO dto)
        {
            Usuario usuario = RepoUsuario.FindById(dto.UsuarioId);
            if (usuario == null)
                throw new ArgumentException("El usuario no existe.");                        
            Gasto gasto = RepoGasto.FindById(dto.GastoId);
            if (gasto == null)
                throw new ArgumentException("El gasto no existe.");
            if (!Enum.TryParse(dto.MetodoPago, true, out MetodoPago metodoPago))
                throw new ArgumentException("Método de pago inválido.");
            if (string.IsNullOrWhiteSpace(dto.Recibo))
                throw new PagoException("El recibo es obligatorio.");
            if (dto.Recibo == "0" || dto.Recibo == "00")
                throw new PagoException("El número de recibo no puede ser 0.");
            if (RepoPago.ExisteRecibo(dto.Recibo))
                throw new PagoException("El número de recibo ya existe.");
            Unico pagoUnico = MapperPago.PagoUnicoDTOToPagoUnico(dto, usuario, gasto, metodoPago);
            RepoPago.Add(pagoUnico);
        }
    }
}
