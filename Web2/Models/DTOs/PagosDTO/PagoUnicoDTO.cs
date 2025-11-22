using Web.Models.DTOs.GastosDTO;
using Web.Models.DTOs.UsuariosDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.DTOs.PagosDTO
{
    public class PagoUnicoDTO
    {
        [Required(ErrorMessage = "Debe seleccionar un gasto")]
        [DisplayName("Seleccione un Gasto")]
        public int GastoId { get; set; }
        public IEnumerable<ListadoGastoDTO> Gastos { get; set; } = new List<ListadoGastoDTO>();

        [Required(ErrorMessage = "Debe seleccionar un usuario")]
        [DisplayName("Seleccione un Usuario")]
        public int UsuarioId { get; set; }
        public IEnumerable<ListadoUsuarioDTO> Usuarios { get; set; } = new List<ListadoUsuarioDTO>();

        [Required(ErrorMessage = "Debe ingresar una descripción")]
        public string Descripcion { get; set; }
        
        [Required(ErrorMessage = "Debe ingresar un monto")]
        [Range(1, int.MaxValue, ErrorMessage = "Monto debe ser mayor a 0")]
        public int Monto { get; set; }

        [DisplayName("Seleccione un metodo de pago")]
        [Required(ErrorMessage = "Debe seleccionar un método de pago")]
        public string MetodoPago { get; set; }

        [DisplayName("Seleccione una fecha de pago")]
        [Required(ErrorMessage = "Debe seleccionar la fecha de pago")]
        [DataType(DataType.Date)]
        public DateTime FechaPago { get; set; }

        [Required(ErrorMessage = "Debe ingresar un recibo")]
        public string Recibo { get; set; }
    }
}
