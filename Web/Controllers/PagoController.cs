using CasosDeUsos.DTOs;
using CasosDeUsos.DTOs.PagosDTO;
using CasosDeUsos.DTOs.UsuariosDTO;
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using CasosDeUsos.InterfacesCasosUsos.IPagoCU;
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAccesoDatos.Repositorio;
using LogicaAplicacion.Mappers;
using LogicaNegocio.interfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static LogicaAplicacion.CasosUso.CUPago.CUListadoPago;

namespace Web.Controllers
{
    public class PagoController : Controller
    {
        public ICUAltaPagoUnico CUAltaPagoUnico { get; set; }
        public ICUAltaPagoRecurrente CUAltaPagoRecurrente { get; set; }
        public ICUListadoUsuario CUListadoUsuario { get; set; }
        public ICUListadoPago CUListadoPago { get; set; }
        public ICUListadoGasto CUListadoGasto { get; set; }
        public ICUObtenerMetodoPago CUObtenerMetodoPago { get; set; }
        public ICUListadoPago CUListadoPagos { get; set; }
        public ICUListadoPagoPorFecha CUFiltrarPagosPorFechas { get; set; }
        public ICUListadoPorPrecio CUListadoPorPrecio { get; set; }



        public PagoController(ICUAltaPagoUnico CuAltaPagoUnico, ICUAltaPagoRecurrente cUAltaPagoRecurrente,
            ICUListadoUsuario cUListadoUsuario, ICUListadoGasto cUListadoGasto, ICUListadoPago cUListadoPago
            , ICUObtenerMetodoPago cUObtenerMetodoPago, ICUListadoPago cUListadoPagos, ICUListadoPagoPorFecha cUFiltrarPagosPorFechas
            , ICUListadoPorPrecio cUListadoPorPrecio)
        {
            CUAltaPagoUnico = CuAltaPagoUnico;
            CUAltaPagoRecurrente = cUAltaPagoRecurrente;
            CUListadoUsuario = cUListadoUsuario;
            CUListadoGasto = cUListadoGasto;
            CUListadoPago = cUListadoPago;
            CUObtenerMetodoPago = cUObtenerMetodoPago;
            CUListadoPagos = cUListadoPagos;
            CUFiltrarPagosPorFechas = cUFiltrarPagosPorFechas;
            CUListadoPorPrecio = cUListadoPorPrecio;
        }

        // GET: PagoController
        public ActionResult Index(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || rol != "Gerente")
                return RedirectToAction("AccesoDenegado");

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

            var pagos = CUFiltrarPagosPorFechas.Ejecutar(fechaDesde.Value, fechaHasta.Value);

            ViewBag.FechaDesde = fechaDesde?.ToString("yyyy-MM-dd");
            ViewBag.FechaHasta = fechaHasta?.ToString("yyyy-MM-dd");

            if (!pagos.Any())
                ViewBag.Mensaje = "No se encontraron pagos en el rango de fechas ingresado.";

            return View(pagos);
        }

        public ActionResult RangoPrecio(decimal? montoMinimo)
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || rol != "Gerente")
                return RedirectToAction("AccesoDenegado");

            IEnumerable<ListadoPagoDTO> pagos = new List<ListadoPagoDTO>();
            bool filtroAplicado = false;

            try
            {
                bool filtroIntentado = Request.Query.Count > 0;
                if (!montoMinimo.HasValue)
                {
                    if (filtroIntentado)
                    {
                        ViewBag.Mensaje = "Debe ingresar un monto mínimo para filtrar.";
                    }

                    ViewBag.MontoMinimo = montoMinimo;
                    ViewBag.FiltroAplicado = false;
                    return View(pagos);
                }
                filtroAplicado = true;
                pagos = CUListadoPorPrecio.Ejecutar(montoMinimo.Value);

                ViewBag.MontoMinimo = montoMinimo;
                ViewBag.FiltroAplicado = filtroAplicado;

                if (!pagos.Any())
                {
                    ViewBag.Mensaje = $"No se encontraron pagos con monto mayor a {montoMinimo.Value}.";
                }
                return View(pagos);
            }
            catch
            {
                ViewBag.Mensaje = "Error al filtrar pagos por monto.";
                ViewBag.FiltroAplicado = false;
                return View(new List<ListadoPagoDTO>());
            }
        }

        public ActionResult Details(int id)
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            return View();
        }

        // GET: PagoController/CreatePagoUnico
        public ActionResult CreatePagoUnico()
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administracion" || rol == "Empleado"))
                return RedirectToAction("AccesoDenegado");

            var UnicoDTO = new PagoUnicoDTO();
            UnicoDTO.FechaPago = DateTime.Now.Date;
            UnicoDTO.Usuarios = CUListadoUsuario.Ejecutar();
            UnicoDTO.Gastos = CUListadoGasto.Ejecutar();

            
            ViewBag.Usuarios = new SelectList(UnicoDTO.Usuarios, "Id", "Nombre");
            ViewBag.Gastos = new SelectList(UnicoDTO.Gastos, "Id", "Nombre");

            return View(UnicoDTO);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePagoUnico(PagoUnicoDTO unicoDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CUAltaPagoUnico.Ejecutar(unicoDTO);
                    TempData["Exito"] = "Pago único creado correctamente.";
                    return RedirectToAction(nameof(CreatePagoUnico));
                }
            }
            catch (PagoException ex)
            {
                ViewBag.Mensaje = ex.Message;
                ModelState.AddModelError(nameof(unicoDTO.Recibo), ex.Message);
            }
            catch
            {
                ViewBag.Mensaje = "Ocurrió un error inesperado al crear el pago.";
            }

            // Vuelvo a llenar la lista y marco la opción seleccionada
            unicoDTO.Usuarios = CUListadoUsuario.Ejecutar();
            unicoDTO.Gastos = CUListadoGasto.Ejecutar();
            ViewBag.Usuarios = new SelectList(unicoDTO.Usuarios, "Id", "Nombre", unicoDTO.UsuarioId);
            ViewBag.Gastos = new SelectList(unicoDTO.Gastos, "Id", "Nombre", unicoDTO.GastoId);

            return View(unicoDTO);
        }

        // GET: PagoController/CreatePagoRecurrente
        public ActionResult CreatePagoRecurrente()
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administracion" || rol == "Empleado"))
                return RedirectToAction("AccesoDenegado");

            var recurrenteDTO = new PagoRecurrenteDTO
            {
                FechaDesde = DateTime.Now.Date,
                FechaHasta = DateTime.Now.Date,
                Usuarios = CUListadoUsuario.Ejecutar(),
                Gastos = CUListadoGasto.Ejecutar()
            };

            ViewBag.Usuarios = new SelectList(recurrenteDTO.Usuarios, "Id", "Nombre");
            ViewBag.Gastos = new SelectList(recurrenteDTO.Gastos, "Id", "Nombre");

            return View(recurrenteDTO);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePagoRecurrente(PagoRecurrenteDTO recurrenteDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (recurrenteDTO.FechaDesde.Month == recurrenteDTO.FechaHasta.Month &&
                        recurrenteDTO.FechaDesde.Year == recurrenteDTO.FechaHasta.Year)
                    {
                        ViewBag.Mensaje = "La fecha de inicio y la fecha de fin no pueden estar en el mismo mes.";
                    }
                    else if (recurrenteDTO.FechaDesde > recurrenteDTO.FechaHasta)
                    {
                        ViewBag.Mensaje = "La fecha de inicio no puede ser mayor que la fecha de fin.";
                    }
                    else
                    {
                        CUAltaPagoRecurrente.Ejecutar(recurrenteDTO);
                        TempData["Exito"] = "Pago recurrente creado correctamente.";
                        return RedirectToAction(nameof(CreatePagoRecurrente));
                    }
                }
            }
            catch (PagoException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch
            {
                ViewBag.Mensaje = "Ocurrió un error al crear el pago.";
            }

            // Vuelvo a llenar la lista y marcar la selección
            recurrenteDTO.Usuarios = CUListadoUsuario.Ejecutar();
            recurrenteDTO.Gastos = CUListadoGasto.Ejecutar();
            ViewBag.Usuarios = new SelectList(recurrenteDTO.Usuarios, "Id", "Nombre", recurrenteDTO.UsuarioId);
            ViewBag.Gastos = new SelectList(recurrenteDTO.Gastos, "Id", "Nombre", recurrenteDTO.GastoId);

            return View(recurrenteDTO);
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
    }
}
