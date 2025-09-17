using CasosDeUsos.DTOs;
using ExcepcionesPropias.ExcepcionesEntidades;
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

        public UsuarioController(IListadoRoles listadoRoles,
            ILogin loginUsuario
            )
        {            
            ListadoRoles = listadoRoles;
            LoginUsuario = loginUsuario;
        }
        // GET: UsuarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
            UsuarioViewModel usuarioVM = new UsuarioViewModel();
            return View(usuarioVM);

        }
        [HttpPost]
        public ActionResult Login(UsuarioViewModel usuarioVM)
        {
            try
            {
                UsuarioLoginDTO usuarioDTO = LoginUsuario.Ejecutar(usuarioVM.Email,
                    usuarioVM.Password);
                if (usuarioDTO != null)
                {
                    HttpContext.Session.SetString("Rol", usuarioDTO.Descripcion);
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
