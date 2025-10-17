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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Rol")) ||
                HttpContext.Session.GetString("Rol") != "Gerente")
                return RedirectToAction("AccesoDenegado");
            try
            {
                IEnumerable<ListadoPagoDTO> pagos;

                if (fechaDesde.HasValue && fechaHasta.HasValue)
                    pagos = CUFiltrarPagosPorFechas.Ejecutar(fechaDesde.Value, fechaHasta.Value);
                else
                    pagos = CUListadoPago.Ejecutar();

                ViewBag.FechaDesde = fechaDesde?.ToString("yyyy-MM-dd");
                ViewBag.FechaHasta = fechaHasta?.ToString("yyyy-MM-dd");

                return View(pagos);
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Error al obtener pagos";
                return View(new List<ListadoPagoDTO>());
            }
        }

        public ActionResult RangoPrecio(decimal? montoMinimo)
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || rol != "Gerente")
                return RedirectToAction("AccesoDenegado");

            try
            {
                IEnumerable<ListadoPagoDTO> pagos = new List<ListadoPagoDTO>();
                bool filtroAplicado = false;

                if (montoMinimo.HasValue && montoMinimo.Value > 0)
                {
                    filtroAplicado = true;
                    pagos = CUListadoPorPrecio.Ejecutar(montoMinimo.Value);
                }

                ViewBag.MontoMinimo = montoMinimo;
                ViewBag.FiltroAplicado = filtroAplicado;

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
            {
                return RedirectToAction("AccesoDenegado");
            }
            PagoUnicoDTO UnicoDTO = new PagoUnicoDTO();
            try
            {
                UnicoDTO.FechaPago = DateTime.Now.Date;
                UnicoDTO.Usuarios = CUListadoUsuario.Ejecutar();
                UnicoDTO.Gastos = CUListadoGasto.Ejecutar();
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(UnicoDTO);
        }

        // POST: PagoController/CreatePagoUnico
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
            catch (Exception)
            {
                ViewBag.Mensaje = "Ocurrió un error inesperado al crear el pago.";
            }
            unicoDTO.Gastos = CUListadoGasto.Ejecutar();
            unicoDTO.Usuarios = CUListadoUsuario.Ejecutar();
            unicoDTO.FechaPago = unicoDTO.FechaPago == default ? DateTime.Now.Date : unicoDTO.FechaPago;
            return View(unicoDTO);
        }

        // GET: PagoController/CreatePagoRecurrente
        public ActionResult CreatePagoRecurrente()
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administracion" || rol == "Empleado"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            PagoRecurrenteDTO recurrenteDTO = new PagoRecurrenteDTO();
            try
            {
                recurrenteDTO.FechaDesde = DateTime.Now.Date;
                recurrenteDTO.FechaHasta = DateTime.Now.Date;
                recurrenteDTO.Usuarios = CUListadoUsuario.Ejecutar();
                recurrenteDTO.Gastos = CUListadoGasto.Ejecutar();
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(recurrenteDTO);
        }

        // POST: PagoController/CreatePagoRecurrente
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
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            recurrenteDTO.Usuarios = CUListadoUsuario.Ejecutar();
            recurrenteDTO.Gastos = CUListadoGasto.Ejecutar();
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
