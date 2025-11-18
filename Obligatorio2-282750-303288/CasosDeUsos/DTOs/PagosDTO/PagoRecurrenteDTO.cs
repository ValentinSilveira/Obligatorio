using CasosDeUsos.DTOs.GastoDTO.GastoDTO;
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
    public class PagoRecurrenteDTO
    {
        [DisplayName("Seleccione un Gasto")]
        public int GastoId { get; set; }
        public IEnumerable<ListadoGastoDTO> Gastos { get; set; } = new List<ListadoGastoDTO>();

        [DisplayName("Seleccione un Usuario")]
        public int UsuarioId { get; set; }
        public IEnumerable<ListadoUsuarioDTO> Usuarios { get; set; } = new List<ListadoUsuarioDTO>();
        [Required(ErrorMessage = "Debe incluir una descripción")]
        public string Descripcion { get; set; }
        public int Monto { get; set; }
        [DisplayName("Seleccione un Metodo de Pago")]
        public string MetodoPago { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaDesde { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaHasta { get; set; }
    }
}
