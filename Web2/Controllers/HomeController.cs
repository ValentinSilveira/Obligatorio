using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;
using Web.Models.DTOs.UsuariosDTO;
using Web.Models.Usuarios;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101/");

                    var tarea = client.PostAsJsonAsync(
                        "api/UsuarioWebAPI/Login",
                        new { Email = usuarioVM.Email, Password = usuarioVM.Password }
                    );

                    tarea.Wait(); 
                    var response = tarea.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var tareaContenido = response.Content.ReadFromJsonAsync<UsuarioLoginDTO>();
                        tareaContenido.Wait();

                        var usuarioDTO = tareaContenido.Result;

                        HttpContext.Session.SetString("Rol", usuarioDTO.NombreRol);
                        HttpContext.Session.SetString("UsuarioEmail", usuarioDTO.Email);

                        return RedirectToAction("Index", "Home");
                    }
                }

                ViewBag.Mensaje = "Credenciales incorrectas.";
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error al conectar con la API: " + ex.Message;
            }

            return View(usuarioVM);
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login");
        }
    }
}
