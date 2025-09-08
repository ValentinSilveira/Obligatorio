using CasosDeUsos.DTOs;
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
    }
}
