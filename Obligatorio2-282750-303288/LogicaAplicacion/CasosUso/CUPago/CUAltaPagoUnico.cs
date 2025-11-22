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


        public void Ejecutar(PagoUnicoAPIDTO pagoUnicoDTO)
        {
            Usuario usuario = RepoUsuario.FindById(pagoUnicoDTO.UsuarioId);
            Gasto gasto = RepoGasto.FindById(pagoUnicoDTO.GastoId);

            if (usuario != null && gasto != null)
            {
                // Convertir el string del DTO a enum MetodoPago
                if (!Enum.TryParse(pagoUnicoDTO.MetodoPago, true, out MetodoPago metodoPago))
                {
                    throw new ArgumentException("Método de pago inválido");
                }
                if (string.IsNullOrWhiteSpace(pagoUnicoDTO.Recibo))
                    throw new PagoException("El recibo es obligatorio.");

                if (pagoUnicoDTO.Recibo == "0" || pagoUnicoDTO.Recibo == "00")
                    throw new PagoException("El número de recibo no puede ser 0.");

                if (RepoPago.ExisteRecibo(pagoUnicoDTO.Recibo))
                    throw new PagoException("El número de recibo ya existe.");
                                
                Unico pagoUnico = MapperPago.PagoUnicoDTOToPagoUnico(pagoUnicoDTO, usuario, gasto, metodoPago);

                RepoPago.Add(pagoUnico);
            }
            else
            {
                throw new ArgumentNullException("El usuario o el gasto no son válidos");
            }
        }        
    }
}
