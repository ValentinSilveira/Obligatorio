using CasosDeUsos.DTOs.DTOsUsuario;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.InterfacesCasosUsos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;
using Web.Models.Usuarios;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ILogin LoginUsuario { get; set; }

        public HomeController(ILogger<HomeController> logger, ILogin loginUsuario)
        {
            _logger = logger;
            LoginUsuario = loginUsuario;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
    }
}
