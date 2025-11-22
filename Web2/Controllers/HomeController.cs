using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Web.Models;
using Web.Models.DTOs.UsuariosDTO;
using Web.Models.Usuarios;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public string urlBase = "";
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            urlBase = configuration.GetValue<string>("urlBase") + "UsuarioWebAPI";
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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UsuarioLoginDTO usuarioDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient cliente = new HttpClient();
                    Task<HttpResponseMessage> solicitud = cliente.PostAsJsonAsync(urlBase, usuarioDTO);
                    solicitud.Wait();
                    HttpResponseMessage respuesta = solicitud.Result;
                    if (respuesta.IsSuccessStatusCode)
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> body = contenido.ReadAsStringAsync();
                        string datos = body.Result;
                        UsuarioLogueadoDTO usuarioLogueadoDTO = JsonConvert.DeserializeObject<UsuarioLogueadoDTO>(datos);
                        if (usuarioLogueadoDTO != null)
                        {
                            HttpContext.Session.SetString("Rol", usuarioLogueadoDTO.NombreRol);
                            HttpContext.Session.SetString("Token", usuarioLogueadoDTO.Token);
                            return RedirectToAction("Index", "Usuario");

                        }
                        else
                        {
                            ViewBag.Mensaje = "Datos incorrectos";
                        }

                    }
                    else
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> body = contenido.ReadAsStringAsync();
                        string datos = body.Result;
                        ViewBag.Mensaje = datos;
                    }

                }
            }
            catch (Exception ex)
            {

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
