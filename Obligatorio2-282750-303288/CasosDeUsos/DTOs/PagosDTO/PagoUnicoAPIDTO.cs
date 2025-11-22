using CasosDeUsos.DTOs.GastoDTO.GastoDTO;
using CasosDeUsos.DTOs.GastosDTO;
using CasosDeUsos.DTOs.UsuariosDTO;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.DTOs.PagosDTO
{
    public class PagoUnicoAPIDTO
    {
        public int GastoId { get; set; }
        public int UsuarioId { get; set; }

        public string Descripcion { get; set; }
        public int Monto { get; set; }

        public string MetodoPago { get; set; }

        public DateTime FechaPago { get; set; }
        public string Recibo { get; set; }
    }
}
