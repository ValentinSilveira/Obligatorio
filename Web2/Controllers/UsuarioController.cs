using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using Web.Models.DTOs.UsuariosDTO;
using Web.Models.Usuarios;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        public string urlBase = "";
        public UsuarioController(IConfiguration configuracion)
        {
            urlBase = configuracion.GetValue<string>("UrlBase") + "UsuarioWebAPI";
        }
        // GET: UsuarioController        
        public ActionResult Index()
        {
            IEnumerable<ListadoUsuarioDTO> listadoUsuarios = new List<ListadoUsuarioDTO>();
            try
            {
                HttpClient client = new HttpClient();
                Task<HttpResponseMessage>tarea=client.GetAsync("https://localhost:7101/api/UsuarioWebAPI/Usuarios");
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> tareaContenido = contenido.ReadAsStringAsync();
                    tareaContenido.Wait();
                    string datos = tareaContenido.Result;
                    listadoUsuarios = JsonConvert.DeserializeObject<IEnumerable<ListadoUsuarioDTO>>(datos);
                }
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
            DetalleUsuarioDTO detalleUsuario = new DetalleUsuarioDTO();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");
                    Task<HttpResponseMessage> tarea = client.GetAsync($"api/UsuarioWebAPI/{id}");
                    tarea.Wait();
                    HttpResponseMessage resp = tarea.Result;
                    if (resp.IsSuccessStatusCode)
                    {
                        Task<DetalleUsuarioDTO> tareaContenido =
                        resp.Content.ReadFromJsonAsync<DetalleUsuarioDTO>();
                        tareaContenido.Wait();
                        detalleUsuario = tareaContenido.Result;
                    }
                    else
                    {
                        Task<string> tareaError = resp.Content.ReadAsStringAsync();
                        tareaError.Wait();
                        ViewBag.Mensaje = tareaError.Result;
                    }
                }
            }
            catch
            {
                ViewBag.Mensaje = "Error inesperado.";
            }

            return View(detalleUsuario);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            UsuarioDTO usuarioDTO = new UsuarioDTO();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");
                    Task<HttpResponseMessage> tareaRoles = client.GetAsync("api/UsuarioWebAPI/Roles");
                    tareaRoles.Wait();
                    HttpResponseMessage respRoles = tareaRoles.Result;
                    if (respRoles.IsSuccessStatusCode)
                    {
                        Task<List<ListadoRolDTO>> tareaContenidoRoles =
                        respRoles.Content.ReadFromJsonAsync<List<ListadoRolDTO>>();
                        tareaContenidoRoles.Wait();
                        usuarioDTO.Roles = tareaContenidoRoles.Result;
                    }

                    Task<HttpResponseMessage> tareaEquipos = client.GetAsync("api/UsuarioWebAPI/Equipos");
                    tareaEquipos.Wait();
                    HttpResponseMessage respEquipos = tareaEquipos.Result;
                    if (respEquipos.IsSuccessStatusCode)
                    {
                        Task<List<ListadoEquipoDTO>> tareaContenidoEquipos =
                        respEquipos.Content.ReadFromJsonAsync<List<ListadoEquipoDTO>>();
                        tareaContenidoEquipos.Wait();
                        usuarioDTO.Equipos = tareaContenidoEquipos.Result;
                    }
                }
            }
            catch
            {
                ViewBag.Mensaje = "Error al cargar datos.";
                usuarioDTO.Roles = new List<ListadoRolDTO>();
                usuarioDTO.Equipos = new List<ListadoEquipoDTO>();
            }
            return View(usuarioDTO);
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
                return View(usuarioDTO);

            try
            {
                HttpClient cliente = new HttpClient();

                string url = urlBase + "/Crear";

                Task<HttpResponseMessage> solicitud = cliente.PostAsJsonAsync(url, usuarioDTO);
                solicitud.Wait();

                HttpResponseMessage respuesta = solicitud.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    TempData["Exito"] = "Usuario creado correctamente.";
                    CargarCombos(usuarioDTO);
                    return RedirectToAction(nameof(Create));
                }
                else
                {
                    Task<string> body = respuesta.Content.ReadAsStringAsync();
                    body.Wait();

                    ViewBag.Mensaje = body.Result;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }

            CargarCombos(usuarioDTO);

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
            DetalleUsuarioDTO detalleUsuarioDTO = new DetalleUsuarioDTO();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");
                    Task<HttpResponseMessage> tarea =
                        client.GetAsync($"api/UsuarioWebAPI/{id}");
                    tarea.Wait();
                    HttpResponseMessage resp = tarea.Result;
                    if (resp.IsSuccessStatusCode)
                    {
                        detalleUsuarioDTO =
                            resp.Content.ReadFromJsonAsync<DetalleUsuarioDTO>().Result;
                    }
                    else
                    {
                        ViewBag.Mensaje = "Usuario no encontrado.";
                    }
                }
            }
            catch
            {
                ViewBag.Mensaje = "Error inesperado.";
            }

            return View(detalleUsuarioDTO);
        }        

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DetalleUsuarioDTO detalleUsuario)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");
                    Task<HttpResponseMessage> tarea =
                        client.DeleteAsync($"api/UsuarioWebAPI/Eliminar/{id}");
                    tarea.Wait();
                    HttpResponseMessage resp = tarea.Result;
                    if (resp.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    Task<string> tareaError = resp.Content.ReadAsStringAsync();
                    tareaError.Wait();
                    ViewBag.Mensaje = tareaError.Result;
                    return View(detalleUsuario);
                }
            }
            catch
            {
                ViewBag.Mensaje = "Error inesperado.";
                return View(detalleUsuario);
            }
        }

        [HttpGet]
        public ActionResult CambiarPassword(int id)
        {
            var dto = new CambiarPasswordDTO { UsuarioId = id };
            return View(dto);
        }

        [HttpPost]
        public ActionResult CambiarPassword(CambiarPasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            try
            {
                using (HttpClient client = new HttpClient())
                {                    
                    client.BaseAddress = new Uri("https://localhost:7101");

                    Task<HttpResponseMessage> solicitud =
                    client.PutAsJsonAsync("api/UsuarioWebAPI/CambiarPassword", dto);
                    solicitud.Wait();
                    HttpResponseMessage resp = solicitud.Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        TempData["Exito"] = "Contraseña actualizada correctamente.";
                        return View(new CambiarPasswordDTO
                        {
                            UsuarioId = dto.UsuarioId
                        });
                    }

                    Task<string> error = resp.Content.ReadAsStringAsync();
                    error.Wait();
                    ViewBag.Mensaje = error.Result;

                    return View(dto);
                }
            }
            catch
            {
                ViewBag.Mensaje = "Error inesperado.";
                return View(dto);
            }
        }

        private void CargarCombos(UsuarioDTO usuarioDTO)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlBase.Replace("UsuarioWebAPI", ""));

                    Task<HttpResponseMessage> tareaRoles = client.GetAsync("UsuarioWebAPI/Roles");
                    tareaRoles.Wait();
                    if (tareaRoles.Result.IsSuccessStatusCode)
                    {
                        var contenido = tareaRoles.Result.Content.ReadFromJsonAsync<List<ListadoRolDTO>>();
                        contenido.Wait();
                        usuarioDTO.Roles = contenido.Result;
                    }

                    Task<HttpResponseMessage> tareaEquipos = client.GetAsync("UsuarioWebAPI/Equipos");
                    tareaEquipos.Wait();
                    if (tareaEquipos.Result.IsSuccessStatusCode)
                    {
                        var contenido = tareaEquipos.Result.Content.ReadFromJsonAsync<List<ListadoEquipoDTO>>();
                        contenido.Wait();
                        usuarioDTO.Equipos = contenido.Result;
                    }
                }
            }
            catch
            {
                usuarioDTO.Roles = new List<ListadoRolDTO>();
                usuarioDTO.Equipos = new List<ListadoEquipoDTO>();
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }        
    }
}
