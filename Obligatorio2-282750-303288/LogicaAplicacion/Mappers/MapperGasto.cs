using CasosDeUsos.DTOs.GastoDTO.GastoDTO;
using CasosDeUsos.DTOs.GastosDTO;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    internal class MapperGasto
    {
        public static Gasto GastoDTOToGasto(GastoDTO gastoDTO) 
        {
            if(gastoDTO == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new Gasto(gastoDTO.Nombre, gastoDTO.Descripcion);
        }

        public static DetalleGastoDTO GastoToDetalleGastoDTO(Gasto gasto)
        {
            if (gasto == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new DetalleGastoDTO()
            {
                Id = gasto.Id,
                Nombre = gasto.Nombre,
                Descripcion = gasto.Descripcion

            };

        }
        public static IEnumerable<ListadoGastoDTO> GastoToGastoListadoDTO(IEnumerable<Gasto> Gastos)
        {
            List<ListadoGastoDTO> listadoGastos = new List<ListadoGastoDTO>();

            foreach (Gasto gasto in Gastos)
            {
                listadoGastos.Add(new ListadoGastoDTO()
                {
                    Id = gasto.Id,                    
                    Nombre = gasto.Nombre,
                    Descripcion = gasto.Descripcion
                });

            }
            return listadoGastos;
        }
        
        public static Gasto DetalleGastoDTOToGasto(DetalleGastoDTO detalleGasto)
        {
            if (detalleGasto == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new Gasto(detalleGasto.Nombre, detalleGasto.Descripcion);
        }
    }
}
