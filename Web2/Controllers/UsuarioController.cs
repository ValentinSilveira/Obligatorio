using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Models.DTOs.UsuariosDTO;
using Web.Models.Usuarios;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        private bool UsuarioEsAdminGer()
        {
            return HttpContext.Session.GetString("Rol") == "Administracion" || HttpContext.Session.GetString("Rol") == "Gerente";
        }

        // GET: UsuarioController
        
        public ActionResult Index()
        {
            if (!UsuarioEsAdminGer())
                return RedirectToAction("Login", "Home");
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
                //ViewBag.Mensaje = "Error";
            }

            return View(listadoUsuarios);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            if (!UsuarioEsAdminGer())
                return RedirectToAction("Login", "Home");
            DetalleUsuarioDTO detalleUsuario = new DetalleUsuarioDTO();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");

                    HttpResponseMessage resp =
                        client.GetAsync($"api/UsuarioWebAPI/{id}").Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        detalleUsuario = resp.Content.ReadFromJsonAsync<DetalleUsuarioDTO>().Result;
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se encontró el usuario.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(detalleUsuario);
        }

        // GET: UsuarioController/Create
        public async Task<ActionResult> Create()
        {
            if (!UsuarioEsAdminGer())
                return RedirectToAction("Login", "Home");
            UsuarioDTO usuarioDTO = new UsuarioDTO();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");

                    HttpResponseMessage respRoles =
                        client.GetAsync("api/UsuarioWebAPI/Roles").Result;

                    if (respRoles.IsSuccessStatusCode)
                    {
                        usuarioDTO.Roles = respRoles.Content
                            .ReadFromJsonAsync<List<ListadoRolDTO>>()
                            .Result;
                    }

                    HttpResponseMessage respEquipos =
                        client.GetAsync("api/UsuarioWebAPI/Equipos").Result;

                    if (respEquipos.IsSuccessStatusCode)
                    {
                        usuarioDTO.Equipos = respEquipos.Content
                            .ReadFromJsonAsync<List<ListadoEquipoDTO>>()
                            .Result;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error al cargar los datos";
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
            if (!UsuarioEsAdminGer())
                return RedirectToAction("Login", "Home");
            if (!ModelState.IsValid)
                return View(usuarioDTO);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");

                    HttpResponseMessage resp =
                        client.PostAsJsonAsync("api/UsuarioWebAPI/Crear", usuarioDTO)
                        .Result;

                    if (resp.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));

                    ViewBag.Mensaje = "Error al crear usuario.";
                    return View(usuarioDTO);
                }
            }
            catch
            {
                ViewBag.Mensaje = "Error inesperado al crear usuario.";
                return View(usuarioDTO);
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
            if (!UsuarioEsAdminGer())
                return RedirectToAction("Login", "Home");
            DetalleUsuarioDTO detalleUsuarioDTO = new DetalleUsuarioDTO();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                var resp = client.GetAsync($"api/UsuarioWebAPI/{id}").Result;

                if (resp.IsSuccessStatusCode)
                    detalleUsuarioDTO = resp.Content.ReadFromJsonAsync<DetalleUsuarioDTO>().Result;
            }

            return View(detalleUsuarioDTO);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DetalleUsuarioDTO detalleUsuario)
        {
            if (!UsuarioEsAdminGer())
                return RedirectToAction("Login", "Home");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");

                    HttpResponseMessage resp =
                        client.DeleteAsync($"api/UsuarioWebAPI/Eliminar/{id}").Result;

                    if (resp.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));

                    ViewBag.Mensaje = "Error al eliminar usuario.";
                    return View(detalleUsuario);
                }
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
