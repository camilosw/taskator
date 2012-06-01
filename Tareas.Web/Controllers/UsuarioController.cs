using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tareas.Lib.Services;
using Tareas.Lib.Models;

namespace Tareas.Web.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private IUsuarioService service;

        public UsuarioController()
        {
            service = new UsuarioService();
        }

        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult List()
        {
            Usuario usuario = service.GetByNombreUsuario(User.Identity.Name);
            var usuarios = from e in service.GetAllActivos()
                          select new
                          {
                              Id = e.Id,
                              Nombre = e.NombreUsuario,
                              Autenticado = e.Id == usuario.Id
                          };
            return Json(usuarios);
        }

        [HttpPost]
        public JsonResult ListTareas()
        {
            var tareas = service.GetConTareas();
            var tarea = tareas.Find(o => o.NombreUsuario == User.Identity.Name);
            tareas.Remove(tarea);
            tareas.Insert(0, tarea);
            var usuarios = from u in tareas
                           select new
                           {
                               Id = u.Id,
                               Nombre = u.NombreUsuario,
                               Tareas = from t in u.Tareas
                                        select new
                                        {
                                            Id = t.Id,
                                            IdProyecto = t.IdProyecto,
                                            NombreProyecto = t.NombreProyecto,
                                            IdEstado = t.IdEstado,
                                            IdAsignado = t.IdAsignado,
                                            UsuarioCrea = t.UsuarioCrea,
                                            Descripcion = t.Descripcion,
                                            FechaEntrega = t.FechaEntrega.HasValue ?
                                                           t.FechaEntrega.Value.ToString("M/d/yyyy") :
                                                           null,
                                            TiempoEstimado = t.TiempoEstimado.HasValue ?
                                                             TareaService.TimeToString(t.TiempoEstimado.Value) :
                                                             null,
                                            TiempoEjecutado = t.TiempoEjecutado.HasValue ?
                                                              TareaService.TimeToString((int)t.TiempoEjecutado.Value) :
                                                              null,
                                            FechaCreada = t.Creada.ToString("M/d/yyyy")
                                        }
                           };

            return Json(usuarios);
        }

        //
        // GET: /Usuario/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Usuario/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Usuario/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Usuario/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Usuario/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Usuario/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Usuario/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
