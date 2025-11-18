using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using Web.Models.DTOs.GastosDTO;
using Web.Models.DTOs.PagosDTO;
using Web.Models.DTOs.UsuariosDTO;

namespace Web.Controllers
{
    public class PagoController : Controller
    {
        private bool UsuarioEsGer()
        {
            return HttpContext.Session.GetString("Rol") == "Gerente";
        }
        private bool Usuario()
        {
            return HttpContext.Session.GetString("Rol") == "Administracion" || HttpContext.Session.GetString("Rol") == "Gerente" || HttpContext.Session.GetString("Rol") == "Empleado";
        }
        
        // GET: PagoController
        public ActionResult Index(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            if (!UsuarioEsGer())
                return RedirectToAction("Login", "Home");
            bool filtroIntentado = Request.Query.Count > 0;

            if (!fechaDesde.HasValue || !fechaHasta.HasValue)
            {
                if (filtroIntentado)
                    ViewBag.Error = "Debe ingresar ambas fechas para filtrar.";

                ViewBag.FechaDesde = fechaDesde?.ToString("yyyy-MM-dd");
                ViewBag.FechaHasta = fechaHasta?.ToString("yyyy-MM-dd");

                return View(new List<ListadoPagoDTO>());
            }

            if (fechaDesde > fechaHasta)
            {
                ViewBag.Error = "La fecha desde no puede ser mayor a la fecha hasta.";
                ViewBag.FechaDesde = fechaDesde?.ToString("yyyy-MM-dd");
                ViewBag.FechaHasta = fechaHasta?.ToString("yyyy-MM-dd");
                return View(new List<ListadoPagoDTO>());
            }

            ViewBag.FechaDesde = fechaDesde?.ToString("yyyy-MM-dd");
            ViewBag.FechaHasta = fechaHasta?.ToString("yyyy-MM-dd");

            List<ListadoPagoDTO> pagos = new List<ListadoPagoDTO>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                string url = "/api/PagoWebAPI?desde=" +
                             fechaDesde.Value.ToString("yyyy-MM-dd") +
                             "&hasta=" +
                             fechaHasta.Value.ToString("yyyy-MM-dd");

                Task<HttpResponseMessage> tarea = client.GetAsync(url);
                tarea.Wait();

                HttpResponseMessage response = tarea.Result;

                if (response.IsSuccessStatusCode)
                {
                    Task<List<ListadoPagoDTO>> tareaLeer =
                        response.Content.ReadFromJsonAsync<List<ListadoPagoDTO>>();
                    tareaLeer.Wait();
                    List<ListadoPagoDTO> resultado = tareaLeer.Result;
                    pagos = resultado;
                }
            }
            if (pagos.Count == 0)
                ViewBag.Mensaje = "No se encontraron pagos en el rango de fechas ingresado.";

            return View(pagos);
        }
                
        public ActionResult RangoPrecio(decimal? montoMinimo)
        {
            if (!UsuarioEsGer())
                return RedirectToAction("Login", "Home");
            if (!montoMinimo.HasValue)
            {
                ViewBag.Mensaje = Request.Query.Count > 0 ? "Debe ingresar un monto." : null;
                return View(new List<ListadoPagoDTO>());
            }

            List<ListadoPagoDTO> pagos = new();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                var resp = client.GetAsync($"/api/PagoWebAPI/Precio?minimo={montoMinimo}").Result;

                if (resp.IsSuccessStatusCode)
                    pagos = resp.Content.ReadFromJsonAsync<List<ListadoPagoDTO>>().Result;
            }

            if (!pagos.Any()) ViewBag.Mensaje = $"No hay pagos mayores a {montoMinimo}.";
            return View(pagos);
        }
        
        public ActionResult Details(int id)
        {
            if (!UsuarioEsGer())
                return RedirectToAction("Login", "Home");
            DetallePagoDTO dto = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                var resp = client.GetAsync($"/api/PagoWebAPI/{id}").Result;

                if (resp.IsSuccessStatusCode)
                    dto = resp.Content.ReadFromJsonAsync<DetallePagoDTO>().Result;
            }

            if (dto == null) ViewBag.Mensaje = "No se encontró el pago.";
            return View(dto);
        }

        // GET: PagoController/CreatePagoUnico
        
        public ActionResult CreatePagoUnico()
        {
            if (!Usuario())
                return RedirectToAction("Login", "Home");
            PagoUnicoDTO dto = new() { FechaPago = DateTime.Now.Date };

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                var respG = client.GetAsync("api/GastoWebAPI/GetGastos").Result;
                if (respG.IsSuccessStatusCode)
                    dto.Gastos = respG.Content.ReadFromJsonAsync<List<ListadoGastoDTO>>().Result;

                var respU = client.GetAsync("api/UsuarioWebAPI/Usuarios").Result;
                if (respU.IsSuccessStatusCode)
                    dto.Usuarios = respU.Content.ReadFromJsonAsync<List<ListadoUsuarioDTO>>().Result;
            }

            ViewBag.Usuarios = new SelectList(dto.Usuarios, "Id", "Nombre");
            ViewBag.Gastos = new SelectList(dto.Gastos, "Id", "Nombre");
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult CreatePagoUnico(PagoUnicoDTO dto)
        {
            if (!Usuario())
                return RedirectToAction("Login", "Home");
            if (!ModelState.IsValid)
            {
                ViewBag.Usuarios = new SelectList(dto.Usuarios, "Id", "Nombre", dto.UsuarioId);
                ViewBag.Gastos = new SelectList(dto.Gastos, "Id", "Nombre", dto.GastoId);
                return View(dto);
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");
                var resp = client.PostAsJsonAsync("/api/PagoWebAPI/CrearUnico", dto).Result;

                if (resp.IsSuccessStatusCode)
                {
                    TempData["Exito"] = "Pago único creado correctamente.";
                    return RedirectToAction(nameof(CreatePagoUnico));
                }

                ViewBag.Mensaje = resp.Content.ReadAsStringAsync().Result;
            }

            return View(dto);
        }

        // GET: PagoController/CreatePagoRecurrente
        public ActionResult CreatePagoRecurrente()
        {
            if (!Usuario())
                return RedirectToAction("Login", "Home");
            PagoRecurrenteDTO dto = new()
            {
                FechaDesde = DateTime.Now.Date,
                FechaHasta = DateTime.Now.Date
            };
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                var respG = client.GetAsync("api/GastoWebAPI/GetGastos").Result;
                if (respG.IsSuccessStatusCode)
                    dto.Gastos = respG.Content.ReadFromJsonAsync<List<ListadoGastoDTO>>().Result;

                var respU = client.GetAsync("api/UsuarioWebAPI/Usuarios").Result;
                if (respU.IsSuccessStatusCode)
                    dto.Usuarios = respU.Content.ReadFromJsonAsync<List<ListadoUsuarioDTO>>().Result;
            }


            ViewBag.Usuarios = new SelectList(dto.Usuarios, "Id", "Nombre");
            ViewBag.Gastos = new SelectList(dto.Gastos, "Id", "Nombre");

            return View(dto);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePagoRecurrente(PagoRecurrenteDTO dto)
        {
            if (!Usuario())
                return RedirectToAction("Login", "Home");
            if (!ModelState.IsValid)
            {
                ViewBag.Usuarios = new SelectList(dto.Usuarios, "Id", "Nombre", dto.UsuarioId);
                ViewBag.Gastos = new SelectList(dto.Gastos, "Id", "Nombre", dto.GastoId);
                return View(dto);
            }
                        
            if (dto.FechaDesde.Month == dto.FechaHasta.Month &&
                dto.FechaDesde.Year == dto.FechaHasta.Year)
            {
                ViewBag.Mensaje = "Los meses no pueden ser iguales.";
                return View(dto);
            }

            if (dto.FechaDesde > dto.FechaHasta)
            {
                ViewBag.Mensaje = "Fecha inicio no puede ser mayor.";
                return View(dto);
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");
                var resp = client.PostAsJsonAsync("/api/PagoWebAPI/CrearRecurrente", dto).Result;

                if (resp.IsSuccessStatusCode)
                {
                    TempData["Exito"] = "Pago recurrente creado correctamente.";
                    return RedirectToAction(nameof(CreatePagoRecurrente));
                }

                ViewBag.Mensaje = resp.Content.ReadAsStringAsync().Result;
            }

            return View(dto);
        }

        // GET: PagoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        // POST: PagoController/Edit/5
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
        // GET: PagoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        // POST: PagoController/Delete/5
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

        public ActionResult PagosPorUsuario(int? id)
        {
            if (!UsuarioEsGer())
                return RedirectToAction("Login", "Home");

            ViewBag.IdUsuario = id;

            if (!id.HasValue)
            {
                ViewBag.Mensaje = Request.Query.Count > 0 ? "Debe ingresar un ID válido." : null;
                return View(new List<ListadoPagoDTO>());
            }

            if (id.Value <= 0)
            {
                ViewBag.Mensaje = "Debe ingresar un ID válido.";
                return View(new List<ListadoPagoDTO>());
            }

            List<ListadoPagoDTO> pagos = new();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                var resp = client.GetAsync($"/api/PagoWebAPI/usuario/{id}").Result;

                if (resp.IsSuccessStatusCode)
                    pagos = resp.Content.ReadFromJsonAsync<List<ListadoPagoDTO>>().Result;
                else
                    ViewBag.Mensaje = resp.Content.ReadAsStringAsync().Result;
            }

            if (!pagos.Any())
                ViewBag.Mensaje = "El usuario no tiene pagos registrados.";

            return View(pagos);
        }

        public ActionResult PagosUnicosMontoSuperior(int? monto)
        {
            if (!UsuarioEsGer())
                return RedirectToAction("Login", "Home");

            ViewBag.Monto = monto;

            if (!monto.HasValue)
            {
                ViewBag.Mensaje = Request.Query.Count > 0 ? "Debe ingresar un monto válido." : null;
                return View(new List<ListadoPagoDTO>());
            }
            if (monto.Value <= 0)
            {
                ViewBag.Mensaje = "Debe ingresar un monto valido.";
                return View(new List<ListadoPagoDTO>());
            }

            List<ListadoPagoDTO> pagos = new();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                var resp = client.GetAsync($"/api/PagoWebAPI/pago/monto/superior/{monto.Value}").Result;

                if (resp.IsSuccessStatusCode)
                    pagos = resp.Content.ReadFromJsonAsync<List<ListadoPagoDTO>>().Result;
                else
                    ViewBag.Mensaje = resp.Content.ReadAsStringAsync().Result;
            }

            if (!pagos.Any())
                ViewBag.Mensaje = $"No existen pagos únicos con monto mayor a {monto}.";

            return View(pagos);
        }
    }
}
