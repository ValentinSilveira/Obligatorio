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
    public class PagoUnicoDTO
    {
        [DisplayName("Seleccione un Gasto")]
        public int GastoId { get; set; }
        public IEnumerable<ListadoGastoDTO> Gastos { get; set; } = new List<ListadoGastoDTO>();

        [DisplayName("Seleccione un Usuario")]
        public int UsuarioId { get; set; }
        public IEnumerable<ListadoUsuarioDTO> Usuarios { get; set; } = new List<ListadoUsuarioDTO>();
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El monto no puede ser 0")]
        public int Monto { get; set; }
        [DisplayName("Seleccione un Metodo de Pago")]
        public string MetodoPago { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaPago { get; set; }
        [Required(ErrorMessage = "El recibo es obligatorio")]
        [RegularExpression(@"^(?!0+$).+", ErrorMessage = "El número de recibo no puede ser 0")]
        public string Recibo { get; set; }
    }
}
