using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using Web.Models.DTOs.GastosDTO;
using Web.Models.DTOs.PagosDTO;
using Web.Models.DTOs.UsuariosDTO;

namespace Web.Controllers
{
    public class PagoController : Controller
    {
        public ActionResult Index(DateTime? fechaDesde, DateTime? fechaHasta)
        {
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

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");

                    string url = $"api/PagoWebAPI?desde={fechaDesde:yyyy-MM-dd}&hasta={fechaHasta:yyyy-MM-dd}";

                    Task<HttpResponseMessage> tarea = client.GetAsync(url);
                    tarea.Wait();

                    HttpResponseMessage resp = tarea.Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        Task<string> tJson = resp.Content.ReadAsStringAsync();
                        tJson.Wait();

                        pagos = JsonConvert.DeserializeObject<List<ListadoPagoDTO>>(tJson.Result);
                    }
                }
            }
            catch
            {
                ViewBag.Mensaje = "Error";
            }

            if (!pagos.Any())
                ViewBag.Mensaje = "No se encontraron pagos en el rango de fechas ingresado.";

            return View(pagos);
        }

        public ActionResult RangoPrecio(decimal? montoMinimo)
        {
            if (!montoMinimo.HasValue)
            {
                ViewBag.Mensaje = Request.Query.Count > 0 ? "Debe ingresar un monto." : null;
                return View(new List<ListadoPagoDTO>());
            }

            List<ListadoPagoDTO> pagos = new List<ListadoPagoDTO>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");

                    Task<HttpResponseMessage> tarea = client.GetAsync($"api/PagoWebAPI/Precio?minimo={montoMinimo}");
                    tarea.Wait();

                    HttpResponseMessage resp = tarea.Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        Task<string> tJson = resp.Content.ReadAsStringAsync();
                        tJson.Wait();

                        pagos = JsonConvert.DeserializeObject<List<ListadoPagoDTO>>(tJson.Result);
                    }
                }
            }
            catch
            {
                ViewBag.Mensaje = "Error";
            }

            if (!pagos.Any())
                ViewBag.Mensaje = $"No hay pagos mayores a {montoMinimo}.";

            return View(pagos);
        }

        public ActionResult Details(int id)
        {
            DetallePagoDTO dto = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");

                    Task<HttpResponseMessage> tarea = client.GetAsync($"api/PagoWebAPI/pago/id/{id}");
                    tarea.Wait();

                    HttpResponseMessage resp = tarea.Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        Task<string> tJson = resp.Content.ReadAsStringAsync();
                        tJson.Wait();

                        dto = JsonConvert.DeserializeObject<DetallePagoDTO>(tJson.Result);
                    }
                }
            }
            catch
            {
                ViewBag.Mensaje = "Error";
            }

            if (dto == null)
                ViewBag.Mensaje = "No se encontró el pago.";

            return View(dto);
        }

        public ActionResult CreatePagoUnico()
        {
            PagoUnicoDTO dto = new PagoUnicoDTO { FechaPago = DateTime.Now.Date };

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                Task<HttpResponseMessage> tareaG = client.GetAsync("api/GastoWebAPI/GetGastos");
                tareaG.Wait();
                if (tareaG.Result.IsSuccessStatusCode)
                {
                    Task<string> tJson = tareaG.Result.Content.ReadAsStringAsync();
                    tJson.Wait();
                    dto.Gastos = JsonConvert.DeserializeObject<List<ListadoGastoDTO>>(tJson.Result);
                }
            }
            ViewBag.Gastos = new SelectList(dto.Gastos, "Id", "Nombre");

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePagoUnico(PagoUnicoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                CargarListasParaFormulario(dto);
                return View(dto);
            }

            int? usuarioId = HttpContext.Session.GetInt32("Usuario");
            if (usuarioId == null)
            {
                TempData["Error"] = "Debe iniciar sesión nuevamente.";
                return RedirectToAction("Login", "Home");
            }

            PagoUnicoDTO apiDto = new PagoUnicoDTO
            {
                UsuarioId = usuarioId.Value,
                GastoId = dto.GastoId,
                Descripcion = dto.Descripcion,
                Monto = dto.Monto,
                MetodoPago = dto.MetodoPago,
                FechaPago = dto.FechaPago,
                Recibo = dto.Recibo
            };

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                Task<HttpResponseMessage> tarea = client.PostAsJsonAsync("api/PagoWebAPI/CrearUnico", apiDto);
                tarea.Wait();

                HttpResponseMessage resp = tarea.Result;

                if (resp.IsSuccessStatusCode)
                {
                    TempData["Exito"] = "Pago único creado correctamente.";
                    return RedirectToAction(nameof(CreatePagoUnico));
                }

                CargarListasParaFormulario(dto);

                Task<string> tJson = resp.Content.ReadAsStringAsync();
                tJson.Wait();
                ViewBag.Mensaje = tJson.Result;
            }

            return View(dto);
        }

        public ActionResult CreatePagoRecurrente()
        {
            PagoRecurrenteDTO dto = new PagoRecurrenteDTO
            {
                FechaDesde = DateTime.Now.Date,
                FechaHasta = DateTime.Now.Date
            };

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                Task<HttpResponseMessage> tareaG = client.GetAsync("api/GastoWebAPI/GetGastos");
                tareaG.Wait();
                if (tareaG.Result.IsSuccessStatusCode)
                {
                    Task<string> tJson = tareaG.Result.Content.ReadAsStringAsync();
                    tJson.Wait();
                    dto.Gastos = JsonConvert.DeserializeObject<List<ListadoGastoDTO>>(tJson.Result);
                }              
            }
            ViewBag.Gastos = new SelectList(dto.Gastos, "Id", "Nombre");
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePagoRecurrente(PagoRecurrenteDTO dto)
        {
            if (!ModelState.IsValid)
            {
                CargarListasParaFormulario(dto);
                return View(dto);
            }            

            if (dto.FechaDesde.Month == dto.FechaHasta.Month &&
                dto.FechaDesde.Year == dto.FechaHasta.Year)
            {
                ViewBag.Mensaje = "Los meses no pueden ser iguales.";
                CargarListasParaFormulario(dto);
                return View(dto);
            }

            if (dto.FechaDesde > dto.FechaHasta)
            {
                ViewBag.Mensaje = "Fecha inicio no puede ser mayor.";
                CargarListasParaFormulario(dto);
                return View(dto);
            }

            int? usuarioId = HttpContext.Session.GetInt32("Usuario");
            if (usuarioId == null)
            {
                TempData["Error"] = "Debe iniciar sesión nuevamente.";
                return RedirectToAction("Login", "Home");
            }
            dto.UsuarioId = usuarioId.Value;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                Task<HttpResponseMessage> tarea = client.PostAsJsonAsync("api/PagoWebAPI/CrearRecurrente", dto);
                tarea.Wait();
                HttpResponseMessage resp = tarea.Result;

                if (resp.IsSuccessStatusCode)
                {
                    TempData["Exito"] = "Pago recurrente creado correctamente.";
                    return RedirectToAction(nameof(CreatePagoRecurrente));
                }                
                CargarListasParaFormulario(dto);
                Task<string> tJson = resp.Content.ReadAsStringAsync();
                tJson.Wait();
                ViewBag.Mensaje = tJson.Result;
            }

            return View(dto);
        }

        public ActionResult PagosPorUsuario()
        {
            int? usuarioId = HttpContext.Session.GetInt32("Usuario");

            if (usuarioId == null)
            {
                TempData["Error"] = "Debe iniciar sesión.";
                return RedirectToAction("Login", "Home");
            }

            List<ListadoPagoDTO> pagos = new List<ListadoPagoDTO>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                Task<HttpResponseMessage> tarea =
                    client.GetAsync($"api/PagoWebAPI/usuario/{usuarioId.Value}");
                tarea.Wait();

                HttpResponseMessage resp = tarea.Result;

                if (resp.IsSuccessStatusCode)
                {
                    var json = resp.Content.ReadAsStringAsync().Result;
                    pagos = JsonConvert.DeserializeObject<List<ListadoPagoDTO>>(json);
                }
                else
                {
                    var json = resp.Content.ReadAsStringAsync().Result;
                    ViewBag.Mensaje = json;
                }
            }

            if (!pagos.Any())
                ViewBag.Mensaje = "El usuario no tiene pagos registrados.";

            return View(pagos);
        }

        public ActionResult PagosUnicosMontoSuperior(decimal? monto)
        {
            ViewBag.Monto = monto;

            if (!monto.HasValue || monto.Value <= 0)
            {
                ViewBag.Mensaje = Request.Query.Count > 0 ? "Debe ingresar un monto válido." : null;
                return View(new List<EquipoDTO>());
            }

            List<EquipoDTO> equipos = new List<EquipoDTO>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                Task<HttpResponseMessage> tarea = client.GetAsync($"api/PagoWebAPI/equipos/monto/superior/{monto.Value}");
                tarea.Wait();

                HttpResponseMessage resp = tarea.Result;

                if (resp.IsSuccessStatusCode)
                {
                    Task<string> tJson = resp.Content.ReadAsStringAsync();
                    tJson.Wait();
                    equipos = JsonConvert.DeserializeObject<List<EquipoDTO>>(tJson.Result);
                }
                else
                {
                    Task<string> tJson = resp.Content.ReadAsStringAsync();
                    tJson.Wait();
                    ViewBag.Mensaje = tJson.Result;
                }
            }

            if (!equipos.Any())
                ViewBag.Mensaje = $"No existen equipos cuyos empleados hayan realizado pagos únicos mayores a {monto}.";

            return View(equipos);
        }

        private void CargarListasParaFormulario(PagoRecurrenteDTO dto)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                var gastosResp = client.GetAsync("api/GastoWebAPI/GetGastos").Result;
                if (gastosResp.IsSuccessStatusCode)
                {
                    var json = gastosResp.Content.ReadAsStringAsync().Result;
                    dto.Gastos = JsonConvert.DeserializeObject<List<ListadoGastoDTO>>(json);
                }
            }

            ViewBag.Gastos = new SelectList(dto.Gastos, "Id", "Nombre", dto.GastoId);
        }

        private void CargarListasParaFormulario(PagoUnicoDTO dto)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                var gastosResp = client.GetAsync("api/GastoWebAPI/GetGastos").Result;
                if (gastosResp.IsSuccessStatusCode)
                {
                    var json = gastosResp.Content.ReadAsStringAsync().Result;
                    dto.Gastos = JsonConvert.DeserializeObject<List<ListadoGastoDTO>>(json);
                }
            }
            ViewBag.Gastos = new SelectList(dto.Gastos, "Id", "Nombre", dto.GastoId);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }

}