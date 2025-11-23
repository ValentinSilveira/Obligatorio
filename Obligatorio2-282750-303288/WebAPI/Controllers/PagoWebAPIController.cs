using CasosDeUsos.DTOs.PagosDTO;
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using CasosDeUsos.InterfacesCasosUsos.IPagoCU;
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.Mappers;
using LogicaAplicacion.CasosUso.CUPago;
using LogicaAplicacion.CasosUso.CUUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoWebAPIController : ControllerBase
    {
        //comentario
        public ICUBuscarPago CUBuscarPago { get; set; }
        public ICUAltaPagoUnico CUAltaPagoUnico { get; set; }
        public ICUAltaPagoRecurrente CUAltaPagoRecurrente { get; set; }
        public ICUListadoUsuario CUListadoUsuario { get; set; }
        public ICUListadoPago CUListadoPago { get; set; }
        public ICUListadoGasto CUListadoGasto { get; set; }
        public ICUObtenerMetodoPago CUObtenerMetodoPago { get; set; }
        public ICUListadoPagoPorFecha CUFiltrarPagosPorFechas { get; set; }
        public ICUListadoPorPrecio CUListadoPorPrecio { get; set; }
        public ICUPagosPorUsuario CUPagosPorUsuario { get; set; }
        public ICUPagosUnicosConMontoSuperior CUPagosUnicosConMontoSuperior { get; set; }

        public PagoWebAPIController(ICUBuscarPago cuBuscarPago, ICUAltaPagoUnico CuAltaPagoUnico, ICUAltaPagoRecurrente cUAltaPagoRecurrente,
            ICUListadoUsuario cUListadoUsuario, ICUListadoGasto cUListadoGasto, ICUListadoPago cUListadoPago
            , ICUObtenerMetodoPago cUObtenerMetodoPago, ICUListadoPagoPorFecha cUFiltrarPagosPorFechas
            , ICUListadoPorPrecio cUListadoPorPrecio, ICUPagosPorUsuario cUPagosPorUsuario, ICUPagosUnicosConMontoSuperior cUPagosUnicosConMontoSuperior)
        {
            CUBuscarPago = cuBuscarPago;
            CUAltaPagoUnico = CuAltaPagoUnico;
            CUAltaPagoRecurrente = cUAltaPagoRecurrente;
            CUListadoUsuario = cUListadoUsuario;
            CUListadoGasto = cUListadoGasto;
            CUListadoPago = cUListadoPago;
            CUObtenerMetodoPago = cUObtenerMetodoPago;
            CUFiltrarPagosPorFechas = cUFiltrarPagosPorFechas;
            CUListadoPorPrecio = cUListadoPorPrecio;
            CUPagosPorUsuario = cUPagosPorUsuario;
            CUPagosUnicosConMontoSuperior = cUPagosUnicosConMontoSuperior;
        }

        
        // GET api/<PagoWebAPIController>/5
        /// <summary>
        /// Permite obtener detalles de un pago por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Administracion")]
        [HttpGet("pago/id/{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id no es correcto");
                }
                return Ok(CUBuscarPago.Ejecutar(id));
            }
            catch (PagoException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error");
            }
        }

        // GET api/<PagoWebAPIController>/3
        /// <summary>
        /// Permite obtener todos los pagos de un usuario dado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Administracion")]       
        [HttpGet("usuario/{id}")]
        public IActionResult GetPagosDeUsuario(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id no es correcto");
                }
                return Ok(CUPagosPorUsuario.Ejecutar(id));
            }
            catch (PagoException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error");
            }
        }

        // GET api/<PagoWebAPIController>/3
        /// <summary>
        /// Permite obtener los equipos en los que sus miembros hayan hecho pagos unicos con un monto superior al dado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize(Roles = "Administracion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        [HttpGet("equipos/monto/superior/{monto}")]
        public IActionResult GetEquiposConPagosUnicosConMontoSuperior(decimal monto)
        {
            try
            {
                if (monto <= 0)
                    return BadRequest("El monto debe ser mayor a cero.");

                var equipos = CUPagosUnicosConMontoSuperior.Ejecutar(monto);

                var equiposDTO = MapperEquipo.ListEquipoToListEquipoDTO(equipos);

                return Ok(equiposDTO);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="desde"></param>
        /// <param name="hasta"></param>
        /// <returns></returns>
        //[Authorize(Roles = "Administracion")]
        [HttpGet]
        public IActionResult ListarPorFecha([FromQuery] DateTime? desde, [FromQuery] DateTime? hasta)
        {
            try
            {
                if (!desde.HasValue || !hasta.HasValue)
                    return BadRequest("Debe ingresar ambas fechas.");

                var pagos = CUFiltrarPagosPorFechas.Ejecutar(desde.Value, hasta.Value);
                return Ok(pagos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al obtener pagos.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// //[Authorize(Roles = "Administracion")]
        [HttpPost("CrearUnico")]
        public IActionResult CrearUnico([FromBody] PagoUnicoAPIDTO dto)
        {
            try
            {
                CUAltaPagoUnico.Ejecutar(dto);
                return Ok("Pago único creado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// 
        //[Authorize(Roles = "Administracion")]
        [HttpPost("CrearRecurrente")]
        public IActionResult CrearRecurrente([FromBody] PagoRecurrenteAPIDTO dto)
        {
            try
            {
                string usuario = "Sistema";
                CUAltaPagoRecurrente.Ejecutar(dto);
                return Ok("Pago recurrente creado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minimo"></param>
        /// <returns></returns>
        //[Authorize(Roles = "Administracion")]
        [HttpGet("Precio")]
        public IActionResult PorPrecio([FromQuery] decimal minimo)
        {
            try
            {
                var pagos = CUListadoPorPrecio.Ejecutar(minimo);
                return Ok(pagos);
            }
            catch
            {
                return StatusCode(500, "Error al filtrar por precio.");
            }
        }

        // POST api/<PagoWebAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PagoWebAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PagoWebAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
