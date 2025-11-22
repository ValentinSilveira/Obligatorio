using CasosDeUsos.DTOs.UsuariosDTO;
using CasosDeUsos.InterfacesCasosUsos.IPagoCU;
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.CasosUso.CUPago;
using LogicaAplicacion.InterfacesCasosUsos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Token;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioWebAPIController : ControllerBase
    {
        public ICUListadoRol CUListadoRoles { get; set; }
        public ICUAltaUsuario CUAltaUsuario { get; set; }
        public ICUListadoUsuario CUListadoUsuario { get; set; }
        public ICUBuscarUsuario CUBuscarUsuario { get; set; }
        public ICUEliminarUsuario CUEliminarUsuario { get; set; }
        public ICUListadoEquipo CUListadoEquipo { get; set; }
        public ILogin LoginUsuario { get; set; }

        public UsuarioWebAPIController(ICUListadoRol listadoRoles, ICUAltaUsuario cUAltaUsuario, ICUListadoUsuario cUListadoUsuario, ICUBuscarUsuario cUBuscarUsuario,
                                ICUEliminarUsuario cUEliminarUsuario, ICUListadoEquipo cUListadoEquipo, ILogin loginUsuario)
        {
            CUListadoRoles = listadoRoles;
            CUAltaUsuario = cUAltaUsuario;
            CUListadoUsuario = cUListadoUsuario;
            CUBuscarUsuario = cUBuscarUsuario;
            CUEliminarUsuario = cUEliminarUsuario;
            CUListadoEquipo = cUListadoEquipo;
            LoginUsuario = loginUsuario;
        }

        // GET api/<UsuarioWebAPIController>/5
        /// <summary>
        /// Permite obtener detalles de un pago por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        // GET api/<UsuarioWebAPIController>/5
        /// <summary>
        /// Listado de usuarios
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = "Administracion")]        
        [HttpGet("Usuarios")]
        public IActionResult Get()
        {
            try
            {
                return Ok(CUListadoUsuario.Ejecutar());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize(Roles = "Administracion")]
        [HttpGet("{id}")]
        public IActionResult GetUsuarioById(int id)
        {
            try
            {
                var usuario = CUBuscarUsuario.Ejecutar(id);

                if (usuario == null)
                    return NotFound("Usuario no encontrado");

                return Ok(usuario);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        //[Authorize(Roles = "Administracion")]
        [HttpPost("Crear")]
        public IActionResult CrearUsuario([FromBody] UsuarioDTO dto)
        {
            try
            {
                string usuario = "Sistema";
                CUAltaUsuario.Ejecutar(dto);
                return Ok("Usuario creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize(Roles = "Administracion")]
        [HttpDelete("Eliminar/{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            try
            {
                string usuario = "Sistema";
                CUEliminarUsuario.Ejecutar(id);

                return Ok("Usuario eliminado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = "Administracion")]
        [HttpGet("Roles")]
        public IActionResult GetRoles()
        {
            try
            {
                return Ok(CUListadoRoles.Ejecutar());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = "Administracion")]
        [HttpGet("Equipos")]
        public IActionResult GetEquipos()
        {
            try
            {
                return Ok(CUListadoEquipo.Ejecutar());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<UsuarioWebAPIController>/5
        /// <summary>
        /// Login Usuario con Token
        /// </summary>
        /// <returns></returns>
        /// 

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost]
        public IActionResult Login([FromBody] UsuarioLoginDTO usuarioLoginDTO)
        {
            try
            {
                if (usuarioLoginDTO == null)
                {
                    return BadRequest("Datos incorrectos");
                }
                UsuarioLogueadoDTO usuarioLogueadoDTO = LoginUsuario.Ejecutar(usuarioLoginDTO);
                if (usuarioLogueadoDTO != null)
                {
                    usuarioLogueadoDTO.Token = ManejadorToken.CrearToken(usuarioLogueadoDTO);
                    return Ok(usuarioLogueadoDTO);
                }
                else return BadRequest("Datos incorrectos");

            }
            catch (UsuarioException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(55, "Error");
            }
        }
    }
}
