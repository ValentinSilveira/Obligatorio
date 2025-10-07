using CasosDeUsos.DTOs;
using CasosDeUsos.DTOs.PagosDTO;
using CasosDeUsos.DTOs.UsuariosDTO;
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using CasosDeUsos.InterfacesCasosUsos.IPagoCU;
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public PagoController(ICUAltaPagoUnico CuAltaPagoUnico, ICUAltaPagoRecurrente cUAltaPagoRecurrente,
            ICUListadoUsuario cUListadoUsuario, ICUListadoGasto cUListadoGasto, ICUListadoPago cUListadoPago
            , ICUObtenerMetodoPago cUObtenerMetodoPago, ICUListadoPago cUListadoPagos)
        {
            CUAltaPagoUnico = CuAltaPagoUnico;
            CUAltaPagoRecurrente = cUAltaPagoRecurrente;
            CUListadoUsuario = cUListadoUsuario;
            CUListadoGasto = cUListadoGasto;
            CUListadoPago = cUListadoPago;
            CUObtenerMetodoPago = cUObtenerMetodoPago;
            CUListadoPagos = cUListadoPagos;
        }

        // GET: PagoController
        public ActionResult Index(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            try
            {
                IEnumerable<ListadoPagoDTO> pagos = CUListadoPago.Ejecutar();
                if (fechaDesde.HasValue && fechaHasta.HasValue)
                {
                    pagos = CUListadoPago.Ejecutar()
                .Where(p =>
                    p.FechaDesde >= fechaDesde.Value &&
                    p.FechaDesde <= fechaHasta.Value);
                }

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

        // GET: PagoController/Details/5
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
                UnicoDTO.MetodoPago = string.Join(", ", CUObtenerMetodoPago.Ejecutar());
            }
            catch(Exception ex) 
            {
                ViewBag.Mensaje = "Error";
            }
            return View(UnicoDTO);
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
            catch(PagoException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch(Exception ex) 
            {
                ViewBag.Mensaje = "Error";
            }
            return View(unicoDTO);
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
                    CUAltaPagoRecurrente.Ejecutar(recurrenteDTO);
                    TempData["Exito"] = "Pago recurrente creado correctamente.";
                    return RedirectToAction(nameof(CreatePagoRecurrente));
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
        public ActionResult FiltrarPorMonto(decimal? montoMinimo)
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            try
            {
                IEnumerable<ListadoUsuarioDTO> usuarios = new List<ListadoUsuarioDTO>();

                if (montoMinimo.HasValue && montoMinimo.Value > 0)
                {
                    var pagos = CUListadoPago.Ejecutar();
                    var pagosUnicos = pagos
                        .Where(p => p.TipoPago == "Unico" && p.Monto > montoMinimo.Value);

                    var usuariosConPagosAltos = pagosUnicos
                        .GroupBy(p => new { p.UsuarioNombre, p.Id })
                        .Select(g => new ListadoUsuarioDTO
                        {
                            Id = g.Key.Id,
                            Nombre = g.Key.UsuarioNombre,
                            TotalPagado = g.Sum(x => x.Monto)
                        })
                        .OrderByDescending(u => u.TotalPagado);

                    usuarios = usuariosConPagosAltos;
                }

                ViewBag.MontoMinimo = montoMinimo;

                return View(usuarios);
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Error al filtrar usuarios por monto.";
                return View(new List<ListadoUsuarioDTO>());
            }
        }
    }
}
