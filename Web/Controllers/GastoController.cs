using CasosDeUsos.DTOs;
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.CasosUso;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class GastoController : Controller
    {
        public ICUAltaGasto CUAltaGasto { get; set; }
        public ICUListadoGasto CUListadoGasto { get; set; }
        public ICUBuscarGasto CUBuscarGasto { get; set; }
        public ICUEliminarGasto CUEliminarGasto { get; set; }
        //public ICUActualizarGasto CUActualizarGasto { get; set; }
        public GastoController(ICUAltaGasto cUAltaGasto, ICUListadoGasto cUListadoGasto, ICUBuscarGasto cUBuscarGasto
                                 , ICUEliminarGasto cUEliminarGasto /*ICUActualizarGasto cUActualizarGasto*/)
        {
            CUAltaGasto = cUAltaGasto;
            CUListadoGasto = cUListadoGasto;
            CUBuscarGasto = cUBuscarGasto;
            CUEliminarGasto = cUEliminarGasto;
            //CUActualizarGasto = cUActualizarGasto;
        }


        // GET: GastoController
        public ActionResult Index()
        {
            IEnumerable<ListadoGastoDTO> listadoGastos = new List<ListadoGastoDTO>();
            try
            {
                listadoGastos = CUListadoGasto.Ejecutar();
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
            DetalleGastoDTO detalleGasto = new DetalleGastoDTO();
            try
            {
                if (id > 0)
                {
                    detalleGasto = CUBuscarGasto.Ejecutar(id);
                }
                else
                {
                    throw new ArgumentException("Gasto no válido");
                }
            }
            catch (GastoException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (ArgumentNullException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (ArgumentException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(detalleGasto);
        }

        // GET: GastoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GastoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GastoDTO gastoDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CUAltaGasto.Ejecutar(gastoDTO);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Mensaje = "Datos incorrectos";
                }
            }
            catch (GastoException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(gastoDTO);
        }

        // GET: GastoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GastoController/Edit/5
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

        // GET: GastoController/Delete/5
        public ActionResult Delete(int id)
        {
            DetalleGastoDTO detalleGasto = new DetalleGastoDTO();
            try
            {
                if (id > 0)
                {
                    detalleGasto = CUBuscarGasto.Ejecutar(id);
                }
                else
                {
                    throw new ArgumentException("Id no válido");
                }
            }
            catch (UsuarioException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (ArgumentNullException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (ArgumentException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(detalleGasto);
        }

        // POST: GastoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DetalleGastoDTO detalleGasto)
        {
            try
            {
                if (id > 0)
                {
                    CUEliminarGasto.Ejecutar(id);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new ArgumentException("Id no válido");
                }
            }
            catch (GastoException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (ArgumentException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(detalleGasto);
        }
    }
}
