using CasosDeUsos.DTOs.GastoDTO.GastoDTO;
using CasosDeUsos.DTOs.GastosDTO;
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Administracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class GastoWebAPIController : ControllerBase
    {
        public ICUAltaGasto CUAltaGasto { get; set; }
        public ICUListadoGasto CUListadoGasto { get; set; }
        public ICUBuscarGasto CUBuscarGasto { get; set; }
        public ICUEliminarGasto CUEliminarGasto { get; set; }
        public ICUEditarGasto CUEditarGasto { get; set; }
        public ICUAuditoria CUAuditoria { get; set; }
        public GastoWebAPIController(ICUAltaGasto cUAltaGasto, ICUListadoGasto cUListadoGasto, ICUBuscarGasto cUBuscarGasto
                                 , ICUEliminarGasto cUEliminarGasto, ICUEditarGasto cUEditarGasto, ICUAuditoria cUAuditoria)
        {
            CUAltaGasto = cUAltaGasto;
            CUListadoGasto = cUListadoGasto;
            CUBuscarGasto = cUBuscarGasto;
            CUEliminarGasto = cUEliminarGasto;
            CUEditarGasto = cUEditarGasto;
            CUAuditoria = cUAuditoria;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpGet("GetGastos")]
        public IActionResult GetGastos()
        {
            var gastos = CUListadoGasto.Ejecutar();
            var dto = gastos.Select(g => new ListadoGastoDTO
            {
                Id = g.Id,
                Nombre = g.Nombre
            });

            return Ok(dto);
        }

        [Authorize(Roles = "Administracion")]
        [HttpPost("Crear")]
        public IActionResult Crear([FromBody] GastoDTO dto)
        {
            try
            {
                string usuario = "UsuarioEjemplo"; 
                CUAltaGasto.Ejecutar(dto, usuario);

                return Ok("Gasto creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetGasto(int id)
        {
            var gasto = CUBuscarGasto.Ejecutar(id);

            if (gasto == null)
                return NotFound("Gasto no encontrado");

            var dto = new DetalleGastoDTO
            {
                Id = gasto.Id,
                Nombre = gasto.Nombre,
                Descripcion = gasto.Descripcion
            };

            return Ok(dto);
        }

        [HttpPut("Editar/{id}")]
        public IActionResult Editar(int id, [FromBody] DetalleGastoDTO dto)
        {
            string usuario = "Sistema";

            CUEditarGasto.Ejecutar(dto,id ,usuario);

            return Ok("Gasto actualizado correctamente");
        }

        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                string usuario = "Sistema";
                CUEliminarGasto.Ejecutar(id, usuario);

                return Ok("Gasto eliminado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno");
            }
        }

    }
}
