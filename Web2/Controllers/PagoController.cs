using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
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
            if (HttpContext.Session.GetString("Token") != null)
            {
                int? usuarioId = HttpContext.Session.GetInt32("Usuario");
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
                        string token = HttpContext.Session.GetString("Token");
                        client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                        string url = $"api/PagoWebAPI?desde={fechaDesde:yyyy-MM-dd}&hasta={fechaHasta:yyyy-MM-dd}";
                        Task<HttpResponseMessage> tarea = client.GetAsync(url);
                        tarea.Wait();
                        HttpResponseMessage respuesta = tarea.Result;
                        if (respuesta.IsSuccessStatusCode)
                        {
                            Task<string> tJson = respuesta.Content.ReadAsStringAsync();
                            tJson.Wait();

                            pagos = JsonConvert.DeserializeObject<List<ListadoPagoDTO>>(tJson.Result);
                        }
                        else if ((int)respuesta.StatusCode == StatusCodes.Status401Unauthorized)
                        {
                            return RedirectToAction("Login", "Home");
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
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult RangoPrecio(decimal? montoMinimo)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                int? usuarioId = HttpContext.Session.GetInt32("Usuario");
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
                        string token = HttpContext.Session.GetString("Token");
                        client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                        Task<HttpResponseMessage> tarea = client.GetAsync($"api/PagoWebAPI/Precio?minimo={montoMinimo}");
                        tarea.Wait();

                        HttpResponseMessage respuesta = tarea.Result;

                        if (respuesta.IsSuccessStatusCode)
                        {
                            Task<string> tJson = respuesta.Content.ReadAsStringAsync();
                            tJson.Wait();

                            pagos = JsonConvert.DeserializeObject<List<ListadoPagoDTO>>(tJson.Result);
                        }
                        else if ((int)respuesta.StatusCode == StatusCodes.Status401Unauthorized)
                        {
                            return RedirectToAction("Login", "Home");
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
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Details(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            if (token == null)
                return RedirectToAction("Login", "Home");

            DetallePagoDTO dto = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");
                
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var resp = client.GetAsync($"api/PagoWebAPI/pago/id/{id}").Result;

                if (resp.IsSuccessStatusCode)
                {
                    var json = resp.Content.ReadAsStringAsync().Result;
                    dto = JsonConvert.DeserializeObject<DetallePagoDTO>(json);
                }
                else if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
            }

            if (dto == null)
                ViewBag.Mensaje = "No se encontró el pago.";

            return View(dto);
        }

        public ActionResult CreatePagoUnico()
        {
            var token = HttpContext.Session.GetString("Token");
            if (token == null)
                return RedirectToAction("Login", "Home");

            PagoUnicoDTO dto = new PagoUnicoDTO
            {
                FechaPago = DateTime.Now.Date
            };

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var resp = client.GetAsync("api/GastoWebAPI/GetGastos").Result;

                if (resp.IsSuccessStatusCode)
                {
                    var json = resp.Content.ReadAsStringAsync().Result;
                    dto.Gastos = JsonConvert.DeserializeObject<List<ListadoGastoDTO>>(json);
                }
                else if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
            }

            ViewBag.Gastos = new SelectList(dto.Gastos, "Id", "Nombre");
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePagoUnico(PagoUnicoDTO dto)
        {
            if (HttpContext.Session.GetString("Token") != null)
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

                string token = HttpContext.Session.GetString("Token");
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");
                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                    Task<HttpResponseMessage> tarea = client.PostAsJsonAsync("api/PagoWebAPI/CrearUnico", apiDto);
                    tarea.Wait();

                    HttpResponseMessage respuesta = tarea.Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        TempData["Exito"] = "Pago único creado correctamente.";
                        return RedirectToAction(nameof(CreatePagoUnico));
                    }
                    else if ((int)respuesta.StatusCode == StatusCodes.Status401Unauthorized)
                    {
                        return RedirectToAction("Login", "Home");
                    }

                    CargarListasParaFormulario(dto);

                    Task<string> tJson = respuesta.Content.ReadAsStringAsync();
                    tJson.Wait();
                    ViewBag.Mensaje = tJson.Result;
                }

                return View(dto);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult CreatePagoRecurrente()
        {
            var token = HttpContext.Session.GetString("Token");

            if (token == null)
                return RedirectToAction("Login", "Home");

            PagoRecurrenteDTO dto = new PagoRecurrenteDTO
            {
                FechaDesde = DateTime.Now.Date,
                FechaHasta = DateTime.Now.Date
            };

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var resp = client.GetAsync("api/GastoWebAPI/GetGastos").Result;

                if (resp.IsSuccessStatusCode)
                {
                    var json = resp.Content.ReadAsStringAsync().Result;
                    dto.Gastos = JsonConvert.DeserializeObject<List<ListadoGastoDTO>>(json);
                }
                else if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
            }

            ViewBag.Gastos = new SelectList(dto.Gastos, "Id", "Nombre");
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePagoRecurrente(PagoRecurrenteDTO dto)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                int? usuarioId = HttpContext.Session.GetInt32("Usuario");
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

                if (usuarioId == null)
                {
                    TempData["Error"] = "Debe iniciar sesión nuevamente.";
                    return RedirectToAction("Login", "Home");
                }

                PagoRecurrenteDTO apiDto = new PagoRecurrenteDTO
                {
                    UsuarioId = usuarioId.Value,
                    GastoId = dto.GastoId,
                    Descripcion = dto.Descripcion,
                    Monto = dto.Monto,
                    MetodoPago = dto.MetodoPago,
                    FechaDesde = dto.FechaDesde,
                    FechaHasta = dto.FechaHasta
                };

                string token = HttpContext.Session.GetString("Token");
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7101");
                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                    Task<HttpResponseMessage> tarea =
                    client.PostAsJsonAsync("api/PagoWebAPI/CrearRecurrente", apiDto);
                    tarea.Wait();
                    HttpResponseMessage respuesta = tarea.Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        TempData["Exito"] = "Pago recurrente creado correctamente.";
                        return RedirectToAction(nameof(CreatePagoRecurrente));
                    }
                    else if ((int)respuesta.StatusCode == StatusCodes.Status401Unauthorized)
                    {
                        return RedirectToAction("Login", "Home");
                    }
                    CargarListasParaFormulario(dto);

                    Task<string> tJson = respuesta.Content.ReadAsStringAsync();
                    tJson.Wait();
                    ViewBag.Mensaje = tJson.Result;
                }
                return View(dto);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult PagosPorUsuario()
        {
            var token = HttpContext.Session.GetString("Token");
            if (token == null)
                return RedirectToAction("Login", "Home");

            List<ListadoPagoDTO> pagos = new();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101");

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var resp = client.GetAsync("api/PagoWebAPI/mis-pagos").Result;

                if (resp.IsSuccessStatusCode)
                {
                    var json = resp.Content.ReadAsStringAsync().Result;
                    pagos = JsonConvert.DeserializeObject<List<ListadoPagoDTO>>(json);
                }
                else if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
            }

            if (!pagos.Any())
                ViewBag.Mensaje = "El usuario no tiene pagos registrados.";

            return View(pagos);
        }

        public ActionResult PagosUnicosMontoSuperior(decimal? monto)
        {
            if (HttpContext.Session.GetString("Token") != null)
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
                    string token = HttpContext.Session.GetString("Token");
                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                    Task<HttpResponseMessage> tarea = client.GetAsync($"api/PagoWebAPI/equipos/monto/superior/{monto.Value}");
                    tarea.Wait();

                    HttpResponseMessage respuesta = tarea.Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        Task<string> tJson = respuesta.Content.ReadAsStringAsync();
                        tJson.Wait();
                        equipos = JsonConvert.DeserializeObject<List<EquipoDTO>>(tJson.Result);
                    }
                    else if ((int)respuesta.StatusCode == StatusCodes.Status401Unauthorized)
                    {
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        Task<string> tJson = respuesta.Content.ReadAsStringAsync();
                        tJson.Wait();
                        ViewBag.Mensaje = tJson.Result;
                    }
                }

                if (!equipos.Any())
                    ViewBag.Mensaje = $"No existen equipos cuyos empleados hayan realizado pagos únicos mayores a {monto}.";

                return View(equipos);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
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