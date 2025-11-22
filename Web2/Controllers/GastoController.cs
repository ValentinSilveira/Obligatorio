using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models.DTOs.GastosDTO;

namespace Web.Controllers
{    
    public class GastoController : Controller
    {

        private bool UsuarioEsAdmin()
        {
            return HttpContext.Session.GetString("Rol") == "Administracion";
        }

        // GET: GastoController
        public ActionResult Index()
        {
            if (!UsuarioEsAdmin())
                return RedirectToAction("Login", "Home");
            IEnumerable<ListadoGastoDTO> listadoGastos = new List<ListadoGastoDTO>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");

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
            if (!UsuarioEsAdmin())
                return RedirectToAction("Login", "Home");
            DetalleGastoDTO dto = new DetalleGastoDTO();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

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
            if (!UsuarioEsAdmin())
                return RedirectToAction("Login", "Home");
            return View();
        }

        // POST: GastoController/Create
        [HttpPost]
        public ActionResult Create(GastoDTO dto)
        {
            if (!UsuarioEsAdmin())
                return RedirectToAction("Login", "Home");
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

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
                    ViewBag.Mensaje = "Error al crear el gasto";
                    return View(dto);
                }
            }
        }

        // GET: GastoController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!UsuarioEsAdmin())
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
                    ViewBag.Mensaje = "No se encontró el gasto.";
                    return RedirectToAction(nameof(Index));
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
            if (!UsuarioEsAdmin())
                return RedirectToAction("Login", "Home");
            string usuario = HttpContext.Session.GetString("UsuarioEmail") ?? "Desconocido";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");
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
            if (!UsuarioEsAdmin())
                return RedirectToAction("Login", "Home");
            DetalleGastoDTO dto = new DetalleGastoDTO();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");
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
            if (!UsuarioEsAdmin())
                return RedirectToAction("Login", "Home");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");
                    Task<HttpResponseMessage> tarea = client.DeleteAsync($"/api/GastoWebAPI/Eliminar/{id}");
                    tarea.Wait();
                    HttpResponseMessage resp = tarea.Result;
                    if (resp.IsSuccessStatusCode)
                    {
                        TempData["Exito"] = "Gasto eliminado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    Task<string> tareaError = resp.Content.ReadAsStringAsync();
                    tareaError.Wait();
                    ViewBag.Mensaje = tareaError.Result;
                    return View(detalleGasto);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"Error al eliminar el gasto: {ex.Message}";
                return View(detalleGasto);
            }
        }
    }
}
