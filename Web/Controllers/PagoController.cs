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

        public ICUListadoGasto CUListadoGasto { get; set; }

        public PagoController(ICUAltaPagoUnico CuAltaPagoUnico,ICUAltaPagoRecurrente cUAltaPagoRecurrente, ICUListadoUsuario cUListadoUsuario, ICUListadoGasto cUListadoGasto)
        {
            CUAltaPagoUnico = CuAltaPagoUnico;
            CUAltaPagoRecurrente = cUAltaPagoRecurrente;
            CUListadoUsuario = cUListadoUsuario;
            CUListadoGasto = cUListadoGasto;
        }

        // GET: PagoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PagoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PagoController/Create
        public ActionResult Create()
        {
            PagoUnicoDTO UnicoDTO = new PagoUnicoDTO();
            try 
            {
                UnicoDTO.Usuarios = CUListadoUsuario.Ejecutar();
                UnicoDTO.Gastos = CUListadoGasto.Ejecutar();
            }
            catch(Exception ex) 
            {
                ViewBag.Mensaje = "Error";
            }
            return View(UnicoDTO);
        }

        // POST: PagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PagoUnicoDTO unicoDTO)
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
