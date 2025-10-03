using CasosDeUsos.DTOs.UsuariosDTO;
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.CasosUso;
using LogicaAplicacion.InterfacesCasosUsos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Usuarios;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        public IListadoRoles ListadoRoles { get; set; }        
        public ICUAltaUsuario CUAltaUsuario { get; set; }
        public ICUListadoUsuario CUListadoUsuario { get; set; }
        public ICUBuscarUsuario CUBuscarUsuario { get; set; }
        public ICUEliminarUsuario CUEliminarUsuario { get; set; }


        public UsuarioController(IListadoRoles listadoRoles, ICUAltaUsuario cUAltaUsuario, ICUListadoUsuario cUListadoUsuario, ICUBuscarUsuario cUBuscarUsuario
,                                  ICUEliminarUsuario cUEliminarUsuario )
        {
            ListadoRoles = listadoRoles;
            CUAltaUsuario = cUAltaUsuario;
            CUListadoUsuario = cUListadoUsuario;
            CUBuscarUsuario = cUBuscarUsuario;
            CUEliminarUsuario = cUEliminarUsuario;
        }

        // GET: UsuarioController
        public ActionResult Index()
        {
            var rol = HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administracion" || rol == "Empleado"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            IEnumerable<ListadoUsuarioDTO> listadoUsuarios = new List<ListadoUsuarioDTO>();
            try
            {
                listadoUsuarios = CUListadoUsuario.Ejecutar();
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }

            return View(listadoUsuarios);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            var rol = HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administracion" || rol == "Empleado"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            DetalleUsuarioDTO detalleUsuario = new DetalleUsuarioDTO();
            try
            {
                if (id > 0)
                {
                    detalleUsuario = CUBuscarUsuario.Ejecutar(id);
                }
                else
                {
                    throw new ArgumentException("Id no válido");
                }
            }
            catch (UsuarioException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (ArgumentNullException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (ArgumentException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(detalleUsuario);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            var rol = HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administracion" || rol == "Empleado"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioDTO usuarioDTO)
        {            
            try
            {
                if (ModelState.IsValid)
                {
                    CUAltaUsuario.Ejecutar(usuarioDTO);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Mensaje = "Datos incorrectos";
                }
            }
            catch (UsuarioException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(usuarioDTO);
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            var rol = HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administracion" || rol == "Empleado"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            DetalleUsuarioDTO detalleUsuario = new DetalleUsuarioDTO();
            try
            {
                if (id > 0)
                {
                    detalleUsuario = CUBuscarUsuario.Ejecutar(id);
                }
                else
                {
                    throw new ArgumentException("Id no válido");
                }
            }
            catch (UsuarioException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (ArgumentNullException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (ArgumentException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(detalleUsuario);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DetalleUsuarioDTO detalleUsuario)
        {
            var rol = HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administracion" || rol == "Empleado"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            try
            {
                if (id > 0)
                {
                    CUEliminarUsuario.Ejecutar(id);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new ArgumentException("Id no válido");
                }
            }
            catch (UsuarioException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (ArgumentException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(detalleUsuario);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login");
        }
    }
}
