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
  public class ProyectoController : Controller
  {
    private IProyectoService service;

    public ProyectoController()
    {
      service = new ProyectoService();
    }

    //
    // GET: /Proyecto/

    public ActionResult Index()
    {
      return View();
    }

    //
    // GET: /Proyecto/

    public JsonResult List()
    {
      var proyectos = from p in service.GetVisibleConTareas()
                      select new
                      {
                          Id = p.Id,
                          Nombre = p.Nombre,
                          IdCliente = p.IdCliente,
                          CodigoPresupuesto = p.CodigoPresupuesto,
                          Tareas = from t in p.Tareas
                                    select new
                                    {
                                      Id = t.Id,
                                      IdProyecto = t.IdProyecto,
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

      return Json(proyectos);
    }

    public void UpdateOrder(List<int> idProyectos, FormCollection collection)
    {
      service.UpdateList(idProyectos);
    }

    //
    // GET: /Proyecto/Details/5

    public ActionResult Details(int id)
    {
      return View();
    }

    //
    // GET: /Proyecto/Create

    public ActionResult Create()
    {
      return View();
    } 

    //
    // POST: /Proyecto/Create

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
    // GET: /Proyecto/Edit/5
 
    public ActionResult Edit(int id)
    {
      return View();
    }

    //
    // POST: /Proyecto/Edit/5

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

    [HttpPost]
    public int AjaxEdit(int id, string nombre, int? idCliente, string codigoPresupuesto)
    {
      if (id == -1)
      {
        var proyecto = new Proyecto();
        proyecto.Nombre = nombre;
        proyecto.IdCliente = idCliente;
        proyecto.CodigoPresupuesto = codigoPresupuesto;
        id = service.Insert(proyecto);
      }
      else
      {
        var proyecto = service.GetById(id);
        proyecto.Nombre = nombre;
        proyecto.IdCliente = idCliente;
        proyecto.CodigoPresupuesto = codigoPresupuesto;
        service.Update(proyecto);
      }

      return id;
    }

    //
    // GET: /Proyecto/Delete/5
 
    public ActionResult Delete(int id)
    {
        return View();
    }

    //
    // POST: /Proyecto/Delete/5

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
