using CasosDeUsos.DTOs;
using CasosDeUsos.DTOs.PagosDTO;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperPago
    {
        
        public static Unico PagoUnicoDTOToPagoUnico(PagoUnicoDTO pagoUnicoDTO, Usuario usuario, Gasto gasto) 
        {
            if (pagoUnicoDTO == null) 
            {
               throw new ArgumentNullException("Datos incorrectos");
            }
           return new Unico(gasto,usuario,pagoUnicoDTO.Descripcion,pagoUnicoDTO.Monto,pagoUnicoDTO.FechaPago,pagoUnicoDTO.Recibo);
        }
       
        public static Recurrente PagoRecurrenteDTOToPagoRecurrente(PagoRecurrenteDTO pagoRecurrenteDTO, Usuario usuario, Gasto gasto) 
        {
            if(pagoRecurrenteDTO == null) 
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new Recurrente(gasto, usuario, pagoRecurrenteDTO.Descripcion, pagoRecurrenteDTO.Monto, pagoRecurrenteDTO.FechaDesde, pagoRecurrenteDTO.FechaHasta);
        }
    }
}
