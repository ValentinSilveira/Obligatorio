using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using Web.Models.DTOs.GastosDTO;

namespace Web.Controllers
{
    public class GastoController : Controller
    {

        //Probando algo para merge 
        // GET: GastoController
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("Token") == null)
                return RedirectToAction("Login", "Home");

            IEnumerable<ListadoGastoDTO> listadoGastos = new List<ListadoGastoDTO>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");
                    string token = HttpContext.Session.GetString("Token");
                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                    Task<HttpResponseMessage> tarea = client.GetAsync("api/GastoWebAPI/GetGastos");
                    tarea.Wait();
                    HttpResponseMessage respuesta = tarea.Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        Task<List<ListadoGastoDTO>> tareaContenido =
                            respuesta.Content.ReadFromJsonAsync<List<ListadoGastoDTO>>();
                        tareaContenido.Wait();

                        listadoGastos = tareaContenido.Result;
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }

            return View(listadoGastos);
        }

        // GET: GastoController/Details/5
        public ActionResult Details(int id)
        {

            if (HttpContext.Session.GetString("Token") == null)
                return RedirectToAction("Login", "Home");

            DetalleGastoDTO dto = new DetalleGastoDTO();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");
                string token = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                Task<HttpResponseMessage> tarea =
                client.GetAsync($"/api/GastoWebAPI/{id}");
                tarea.Wait();

                var resp = tarea.Result;

                if (resp.IsSuccessStatusCode)
                {
                    Task<DetalleGastoDTO> leer =
                        resp.Content.ReadFromJsonAsync<DetalleGastoDTO>();
                    leer.Wait();

                    dto = leer.Result;
                }
                else
                {
                    ViewBag.Mensaje = "No se encontró el gasto.";
                }
            }

            return View(dto);
        }

        // GET: GastoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GastoController/Create
        [HttpPost]
        public ActionResult Create(GastoDTO dto)
        {

            if (HttpContext.Session.GetString("Token") == null)
                return RedirectToAction("Login", "Home");
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");
                string token = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                Task<HttpResponseMessage> tarea =
                client.PostAsJsonAsync("/api/GastoWebAPI/Crear", dto);

                tarea.Wait();

                HttpResponseMessage resp = tarea.Result;

                if (resp.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Gasto creado correctamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Mensaje = "El Gasto con ese nombre ya existe";
                    return View(dto);
                }
            }
        }

        // GET: GastoController/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Token") == null)
                return RedirectToAction("Login", "Home");
            try
            {
                if (id <= 0)
                {
                    ViewBag.Mensaje = "ID inválido.";
                    return RedirectToAction(nameof(Index));
                }
                DetalleGastoDTO detalleGasto = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");
                    string token = HttpContext.Session.GetString("Token");
                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                    Task<HttpResponseMessage> tarea = client.GetAsync($"/api/GastoWebAPI/{id}");
                    tarea.Wait();
                    HttpResponseMessage response = tarea.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Task<DetalleGastoDTO> tareaContenido =
                        response.Content.ReadFromJsonAsync<DetalleGastoDTO>();
                        tareaContenido.Wait();
                        detalleGasto = tareaContenido.Result;
                    }
                }
                if (detalleGasto == null)
                {
                    ModelState.Clear();
                    ViewBag.Exito = "Gasto creado correctamente";
                    return View(new GastoDTO());
                }
                return View(detalleGasto);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"Error al cargar el gasto: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: GastoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DetalleGastoDTO detalleGasto)
        {

            if (HttpContext.Session.GetString("Token") == null)
                return RedirectToAction("Login", "Home");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");
                string token = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                Task<HttpResponseMessage> tarea =
                client.PutAsJsonAsync($"/api/GastoWebAPI/Editar/{id}", detalleGasto);
                tarea.Wait();
                HttpResponseMessage resp = tarea.Result;
                if (resp.IsSuccessStatusCode)
                {
                    TempData["Exito"] = "Gasto actualizado correctamente";
                    return RedirectToAction("Index");
                }
                Task<string> tareaError = resp.Content.ReadAsStringAsync();
                tareaError.Wait();
                ViewBag.Mensaje = tareaError.Result;
                return View(detalleGasto);
            }
        }

        // GET: GastoController/Delete/5
        public ActionResult Delete(int id)
        {

            if (HttpContext.Session.GetString("Token") == null)
                return RedirectToAction("Login", "Home");
            DetalleGastoDTO dto = new DetalleGastoDTO();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");
                string token = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                Task<HttpResponseMessage> tarea =
                client.GetAsync($"/api/GastoWebAPI/{id}");
                tarea.Wait();
                if (tarea.Result.IsSuccessStatusCode)
                    dto = tarea.Result.Content.ReadFromJsonAsync<DetalleGastoDTO>().Result;
            }
            return View(dto);
        }

        // POST: GastoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DetalleGastoDTO detalleGasto)
        {

            if (HttpContext.Session.GetString("Token") == null)
                return RedirectToAction("Login", "Home");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");
                    string token = HttpContext.Session.GetString("Token");
                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                    string nombreGastoBorrado = detalleGasto.Nombre;
                    Task<HttpResponseMessage> tarea = client.DeleteAsync($"/api/GastoWebAPI/Eliminar/{id}");
                    tarea.Wait();
                    HttpResponseMessage resp = tarea.Result;
                    if (resp.IsSuccessStatusCode)
                    {
                        TempData["Exito"] = $"Gasto '{nombreGastoBorrado}' eliminado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    Task<string> tareaError = resp.Content.ReadAsStringAsync();
                    tareaError.Wait();
                    ViewBag.Mensaje = tareaError.Result;
                    Task<HttpResponseMessage> tareaDetalle =
                    client.GetAsync($"/api/GastoWebAPI/{id}");
                    tareaDetalle.Wait();
                    HttpResponseMessage respDetalle = tareaDetalle.Result;

                    if (respDetalle.IsSuccessStatusCode)
                    {
                        detalleGasto = respDetalle.Content
                            .ReadFromJsonAsync<DetalleGastoDTO>()
                            .Result;
                    }
                    return View(detalleGasto);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"Error al eliminar el gasto: {ex.Message}";
                return View(detalleGasto);
            }
        }

        public ActionResult AuditoriaGasto(int id)
        {
            if (HttpContext.Session.GetString("Token") == null)
                return RedirectToAction("Login", "Home");

            List<AuditoriaDTO> auditoria = new();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");
                string token = HttpContext.Session.GetString("Token");

                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

                var resp = client.GetAsync($"api/GastoWebAPI/gasto/{id}").Result;

                if (resp.IsSuccessStatusCode)
                {
                    string json = resp.Content.ReadAsStringAsync().Result;
                    auditoria = JsonConvert.DeserializeObject<List<AuditoriaDTO>>(json);
                }
                else if (resp.StatusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("Login", "Home");
                }
            }

            return View(auditoria);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}
