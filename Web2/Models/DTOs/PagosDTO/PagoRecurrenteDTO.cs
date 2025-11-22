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
    public class PagoRecurrenteDTO
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

        [Required(ErrorMessage = "Debe seleccionar un método de pago")]
        [DisplayName("Seleccione un metodo de pago")]
        public string MetodoPago { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la fecha desde")]
        [DataType(DataType.Date)]
        [DisplayName("Seleccione fecha inicial")]
        public DateTime FechaDesde { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la fecha hasta")]
        [DataType(DataType.Date)]
        [DisplayName("Seleccione fecha final")]
        public DateTime FechaHasta { get; set; }
    }
}
