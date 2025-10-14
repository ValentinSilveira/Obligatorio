using CasosDeUsos.DTOs.GastoDTO;
using CasosDeUsos.DTOs.GastoDTO.GastoDTO;
using CasosDeUsos.DTOs.GastosDTO;
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.CasosUso;
using LogicaAplicacion.CasosUso.CUGasto;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
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
        public ICUEditarGasto CUEditarGasto { get; set; }
        public IRepositorioAuditoria RepoAuditoria { get; set; }
        public GastoController(ICUAltaGasto cUAltaGasto, ICUListadoGasto cUListadoGasto, ICUBuscarGasto cUBuscarGasto
                                 , ICUEliminarGasto cUEliminarGasto, ICUEditarGasto cUEditarGasto, IRepositorioAuditoria repoAuditoria)
        {
            CUAltaGasto = cUAltaGasto;
            CUListadoGasto = cUListadoGasto;
            CUBuscarGasto = cUBuscarGasto;
            CUEliminarGasto = cUEliminarGasto;
            CUEditarGasto = cUEditarGasto;
            RepoAuditoria = repoAuditoria;
        }


        // GET: GastoController
        public ActionResult Index()
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Administracion"))
            {
                return RedirectToAction("AccesoDenegado");
            }
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
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administracion" || rol == "Empleado"))
            {
                return RedirectToAction("AccesoDenegado");
            }
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
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Administracion"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            return View();
        }

        // POST: GastoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GastoDTO gastoDTO)
        {
            string usuario = HttpContext.Session.GetString("UsuarioEmail") ?? "Desconocido";

            try
            {
                if (ModelState.IsValid)
                {
                    CUAltaGasto.Ejecutar(gastoDTO);
                    RepoAuditoria.Add(new Auditoria
                    {
                        Usuario = usuario,
                        Entidad = "Gasto",
                        Operacion = "Create",
                        Fecha = DateTime.Now,
                        Detalle = $"Se creó el gasto '{gastoDTO.Nombre}'"
                    });
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
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Administracion"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            try
            {
                if (id <= 0)
                {
                    ViewBag.Mensaje = "ID inválido.";
                    return RedirectToAction(nameof(Index));
                }
                DetalleGastoDTO detalleGasto = CUBuscarGasto.Ejecutar(id);

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
            string usuario = HttpContext.Session.GetString("UsuarioEmail") ?? "Desconocido";
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Administracion"))
            {
                return RedirectToAction("AccesoDenegado");
            }
            
            try
            {
                if (id > 0 && ModelState.IsValid)
                {
                    CUEditarGasto.Ejecutar(detalleGasto, id);
                    RepoAuditoria.Add(new Auditoria
                    {
                        Usuario = usuario,
                        Entidad = "Gasto",
                        Operacion = "Update",
                        Fecha = DateTime.Now,
                        Detalle = $"Se modificó el gasto '{detalleGasto.Nombre}' (ID: {id})"
                    });
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Mensaje = "Los datos no son correctos";
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

        // GET: GastoController/Delete/5
        public ActionResult Delete(int id)
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Administracion"))
            {
                return RedirectToAction("AccesoDenegado");
            }
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
            string usuario = HttpContext.Session.GetString("UsuarioEmail") ?? "Desconocido";
            try
            {
                if (id > 0)
                {
                    CUEliminarGasto.Ejecutar(id);
                    RepoAuditoria.Add(new Auditoria
                    {
                        Usuario = usuario,
                        Entidad = "Gasto",
                        Operacion = "Delete",
                        Fecha = DateTime.Now,
                        Detalle = $"Se eliminó el gasto '{detalleGasto.Nombre}' (ID: {id})"
                    });

                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Mensaje = "Id no válido";
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
