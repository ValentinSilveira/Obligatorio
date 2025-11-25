using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
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
            if (HttpContext.Session.GetString("Token") != null)
            {
                int? usuarioId = HttpContext.Session.GetInt32("Usuario");
                IEnumerable<ListadoUsuarioDTO> listadoUsuarios = new List<ListadoUsuarioDTO>();
                try
                {
                    var token = HttpContext.Session.GetString("Token");

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                    Task<HttpResponseMessage> tarea = client.GetAsync("https://localhost:7101/api/UsuarioWebAPI/Usuarios");
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
                    else if ((int)respuesta.StatusCode == StatusCodes.Status401Unauthorized)
                    {
                        return RedirectToAction("Login", "Home");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = "Error";
                }

                return View(listadoUsuarios);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                DetalleUsuarioDTO detalleUsuario = new DetalleUsuarioDTO();
                try
                {
                    var token = HttpContext.Session.GetString("Token");

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:7101");
                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                    Task<HttpResponseMessage> tarea = client.GetAsync($"api/UsuarioWebAPI/{id}");
                    tarea.Wait();

                    HttpResponseMessage respuesta = tarea.Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        Task<DetalleUsuarioDTO> tareaContenido =
                        respuesta.Content.ReadFromJsonAsync<DetalleUsuarioDTO>();
                        tareaContenido.Wait();

                        detalleUsuario = tareaContenido.Result;
                    }
                    else if ((int)respuesta.StatusCode == StatusCodes.Status401Unauthorized)
                    {
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        Task<string> tareaError = respuesta.Content.ReadAsStringAsync();
                        tareaError.Wait();
                        ViewBag.Mensaje = tareaError.Result;
                    }
                }
                catch
                {
                    ViewBag.Mensaje = "Error inesperado.";
                }

                return View(detalleUsuario);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                UsuarioDTO usuarioDTO = new UsuarioDTO();

                try
                {
                    var token = HttpContext.Session.GetString("Token");

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:7101");
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);

                        Task<HttpResponseMessage> tareaRoles = client.GetAsync("api/UsuarioWebAPI/Roles");
                        tareaRoles.Wait();

                        HttpResponseMessage respuestaRoles = tareaRoles.Result;

                        if (respuestaRoles.IsSuccessStatusCode)
                        {
                            Task<List<ListadoRolDTO>> tareaContenidoRoles =
                                respuestaRoles.Content.ReadFromJsonAsync<List<ListadoRolDTO>>();
                            tareaContenidoRoles.Wait();

                            usuarioDTO.Roles = tareaContenidoRoles.Result;
                        }
                        else if ((int)respuestaRoles.StatusCode == StatusCodes.Status401Unauthorized)
                        {
                            return RedirectToAction("Login", "Home");
                        }

                        Task<HttpResponseMessage> tareaEquipos = client.GetAsync("api/UsuarioWebAPI/Equipos");
                        tareaEquipos.Wait();

                        HttpResponseMessage respuestaEquipo = tareaEquipos.Result;

                        if (respuestaEquipo.IsSuccessStatusCode)
                        {
                            Task<List<ListadoEquipoDTO>> tareaContenidoEquipos =
                                respuestaEquipo.Content.ReadFromJsonAsync<List<ListadoEquipoDTO>>();
                            tareaContenidoEquipos.Wait();

                            usuarioDTO.Equipos = tareaContenidoEquipos.Result;
                        }
                        else if ((int)respuestaEquipo.StatusCode == StatusCodes.Status401Unauthorized)
                        {
                            return RedirectToAction("Login", "Home");
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
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioDTO usuarioDTO)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                if (!ModelState.IsValid)
                    return View(usuarioDTO);

                try
                {
                    var token = HttpContext.Session.GetString("Token");

                    HttpClient cliente = new HttpClient();
                    cliente.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);

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
                    else if ((int)respuesta.StatusCode == StatusCodes.Status401Unauthorized)
                    {
                        return RedirectToAction("Login", "Home");
                    }

                    Task<string> body = respuesta.Content.ReadAsStringAsync();
                    body.Wait();
                    ViewBag.Mensaje = body.Result;
                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = ex.Message;
                }

                CargarCombos(usuarioDTO);

                return View(usuarioDTO);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                int? usuarioId = HttpContext.Session.GetInt32("Usuario");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                int? usuarioId = HttpContext.Session.GetInt32("Usuario");
                try
                {
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                DetalleUsuarioDTO detalleUsuarioDTO = new DetalleUsuarioDTO();

                try
                {
                    var token = HttpContext.Session.GetString("Token");

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:7101");
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);

                        Task<HttpResponseMessage> tarea =
                            client.GetAsync($"api/UsuarioWebAPI/{id}");
                        tarea.Wait();

                        HttpResponseMessage respuesta = tarea.Result;

                        if (respuesta.IsSuccessStatusCode)
                        {
                            detalleUsuarioDTO =
                                respuesta.Content.ReadFromJsonAsync<DetalleUsuarioDTO>().Result;
                        }
                        else if ((int)respuesta.StatusCode == StatusCodes.Status401Unauthorized)
                        {
                            return RedirectToAction("Login", "Home");
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
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]        
        public ActionResult Delete(int id, DetalleUsuarioDTO detalleUsuario)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                try
                {
                    var token = HttpContext.Session.GetString("Token");

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:7101");
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);

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
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public ActionResult CambiarPassword(int id)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                var dto = new CambiarPasswordDTO { UsuarioId = id };
                return View(dto);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult CambiarPassword(CambiarPasswordDTO dto)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                if (!ModelState.IsValid)
                    return View(dto);

                try
                {
                    var token = HttpContext.Session.GetString("Token");

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:7101");
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);

                        Task<HttpResponseMessage> solicitud =
                            client.PutAsJsonAsync("api/UsuarioWebAPI/CambiarPassword", dto);
                        solicitud.Wait();

                        HttpResponseMessage respuesta = solicitud.Result;

                        if (respuesta.IsSuccessStatusCode)
                        {
                            TempData["Exito"] = "Contraseña actualizada correctamente.";
                            return View(new CambiarPasswordDTO
                            {
                                UsuarioId = dto.UsuarioId
                            });
                        }
                        else if ((int)respuesta.StatusCode == StatusCodes.Status401Unauthorized)
                        {
                            return RedirectToAction("Login", "Home");
                        }

                        Task<string> error = respuesta.Content.ReadAsStringAsync();
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
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        private void CargarCombos(UsuarioDTO usuarioDTO)
        {
            int? usuarioId = HttpContext.Session.GetInt32("Usuario");
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
