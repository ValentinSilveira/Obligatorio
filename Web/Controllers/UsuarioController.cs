using CasosDeUsos.DTOs;
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
        public ILogin LoginUsuario { get; set; }
        public ICUAltaUsuario CUAltaUsuario { get; set; }
        public ICUListadoUsuario CUListadoUsuario { get; set; }
        public ICUBuscarUsuario CUBuscarUsuario { get; set; }


        public UsuarioController(IListadoRoles listadoRoles, ILogin loginUsuario, ICUAltaUsuario cUAltaUsuario, ICUListadoUsuario cUListadoUsuario, ICUBuscarUsuario cUBuscarUsuario
            )
        {
            ListadoRoles = listadoRoles;
            LoginUsuario = loginUsuario;
            CUAltaUsuario = cUAltaUsuario;
            CUListadoUsuario = cUListadoUsuario;
            CUBuscarUsuario = cUBuscarUsuario;
        }

        // GET: UsuarioController
        public ActionResult Index()
        {
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
            DetalleUsuarioDTO detalleCliente = new DetalleUsuarioDTO();
            try
            {
                if (id > 0)
                {
                    detalleCliente = CUBuscarUsuario.Ejecutar(id);
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
            return View(detalleCliente);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
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
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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

        [HttpGet]
        public ActionResult Login()
        {
            UsuarioLoginViewModel usuarioVM = new UsuarioLoginViewModel();
            return View(usuarioVM);
        }
        [HttpPost]
        public ActionResult Login(UsuarioLoginViewModel usuarioVM)
        {
            try
            {
                UsuarioLoginDTO usuarioDTO = LoginUsuario.Ejecutar(usuarioVM.Email,
                    usuarioVM.Password);
                if (usuarioDTO != null)
                {
                    HttpContext.Session.SetString("Rol", usuarioDTO.NombreRol);
                    HttpContext.Session.SetString("Email", usuarioDTO.Email);
                    return RedirectToAction("Index", "Home");
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
            return View();

        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login");
        }
    }
}
