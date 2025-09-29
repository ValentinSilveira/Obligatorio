using CasosDeUsos.DTOs;
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

        public void Ejecutar(PagoUnicoDTO pagoUnicoDTO)
        {
            Usuario usuario = RepoUsuario.FindById(pagoUnicoDTO.UsuarioId);
            Gasto gasto = RepoGasto.FindById(pagoUnicoDTO.GastoId);

            if(usuario != null && gasto != null) 
            {
                Unico pagoUnico = MapperPago.PagoUnicoDTOToPagoUnico(pagoUnicoDTO, usuario, gasto);
                RepoPago.Add(pagoUnico);
            }
            else 
            {
                throw new ArgumentNullException("El usuario o el gasto no son validos");
            }
            
        }
    }
}
