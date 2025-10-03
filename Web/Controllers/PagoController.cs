using CasosDeUsos.DTOs;
using CasosDeUsos.DTOs.PagosDTO;
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

        public PagoController(ICUAltaPagoUnico CuAltaPagoUnico, ICUAltaPagoRecurrente cUAltaPagoRecurrente,
            ICUListadoUsuario cUListadoUsuario, ICUListadoGasto cUListadoGasto, ICUListadoPago cUListadoPago
            , ICUObtenerMetodoPago cUObtenerMetodoPago)
        {
            CUAltaPagoUnico = CuAltaPagoUnico;
            CUAltaPagoRecurrente = cUAltaPagoRecurrente;
            CUListadoUsuario = cUListadoUsuario;
            CUListadoGasto = cUListadoGasto;
            CUListadoPago = cUListadoPago;
            CUObtenerMetodoPago = cUObtenerMetodoPago;
        }

        // GET: PagoController
        public ActionResult Index()
        {
            var rol = HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            try
            {
                var pagos = CUListadoPago.Ejecutar();
                return View(pagos);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error al obtener pagos";
                return View(new List<ListadoPagoDTO>());
            }
        }

        // GET: PagoController/Details/5
        public ActionResult Details(int id)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            return View();
        }

        // GET: PagoController/CreatePagoUnico
        public ActionResult CreatePagoUnico()        
        {
            var rol = HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administracion" || rol == "Empleado"))
            {
                return RedirectToAction("AccesoDenegado");
            }

            PagoUnicoDTO UnicoDTO = new PagoUnicoDTO();
            try 
            {
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
            var rol = HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administracion" || rol == "Empleado"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            PagoRecurrenteDTO recurrenteDTO = new PagoRecurrenteDTO();
            try
            {
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
                    return RedirectToAction(nameof(Index));
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
                    return RedirectToAction(nameof(Index));
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
    }
}
