using CasosDeUsos.InterfacesCasosUsos.IPagoCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.CasosUso.CUPago;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoWebAPIController : ControllerBase
    {
        public ICUBuscarPago CUBuscarPago { get; set; }

        public PagoWebAPIController(ICUBuscarPago cuBuscarPago) 
        {
            CUBuscarPago = cuBuscarPago;
        }

        // GET: api/<PagoWebAPIController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try 
            {
                if(id <= 0) 
                {
                    return BadRequest("El id no es correcto");
                }
                return Ok(CUBuscarPago.Ejecutar(id));
            }
            catch (ArgumentNullException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch(PagoException ex) 
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, "Error");
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
