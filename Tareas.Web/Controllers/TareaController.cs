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
  public class TareaController : Controller
  {
    private ITareaService service;

    public TareaController()
    {
      service = new TareaService();
    }

    //
    // GET: /Tarea/

    public ActionResult Index()
    {
      return View();
    }

    public void UpdateOrder(List<IdProyectosTareas> listado, FormCollection collection)
    {
      service.UpdateList(listado);
    }

    //
    // GET: /Tarea/Details/5

    public ActionResult Details(int id)
    {
      return View();
    }

    //
    // GET: /Tarea/Create

    public ActionResult Create()
    {
      return View();
    } 

    //
    // POST: /Tarea/Create

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
    // GET: /Tarea/Edit/5
 
    public ActionResult Edit(int id)
    {
        return View();
    }

    //
    // POST: /Tarea/Edit/5

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
    public int AjaxEdit(int? id, TareaExtendido tarea, FormCollection collection)
    {
      if (id.HasValue)
      {
        var tareaActual = service.GetById(id.Value);
        tareaActual.IdEstado = tarea.IdEstado;
        tareaActual.IdAsignado = tarea.IdAsignado;
        tareaActual.Descripcion = tarea.Descripcion;
        tareaActual.FechaEntrega = tarea.FechaEntrega;
        tareaActual.TiempoEstimado = TareaService.StringToTime(tarea.TiempoEstimadoString);
        service.Update(tareaActual);
        return 0;
      }
      else
      {
        Usuario usuario = new UsuarioService().GetByNombreUsuario(User.Identity.Name);
        Tarea tareaNueva = new Tarea() { 
          IdUsuarioCrea = usuario.Id,
          IdProyecto = tarea.IdProyecto,
          IdEstado = tarea.IdEstado,
          IdAsignado = tarea.IdAsignado,
          Descripcion = tarea.Descripcion,
          FechaEntrega = tarea.FechaEntrega,
          TiempoEstimado = TareaService.StringToTime(tarea.TiempoEstimadoString)
        };
        return service.Insert(tareaNueva);
      }
    }

    [HttpPost]
    public string LogTrabajoTarea(int id)
    {
      Usuario usuario = new UsuarioService().GetByNombreUsuario(User.Identity.Name);
      var logTrabajoTareaService = new LogTrabajoTareasService();
      logTrabajoTareaService.LogTrabajo(id, usuario.Id, DateTime.Now);
      return TareaService.TimeToString(logTrabajoTareaService.getTiempoPorTarea(id));
    }

    [HttpPost]
    public string CorregirTiempo(string tiempo)
    {
      int? valor = TareaService.StringToTime(tiempo);
      return valor.HasValue ? TareaService.TimeToString(valor.Value) : "";
    }

    //
    // GET: /Tarea/Delete/5
 
    public ActionResult Delete(int id)
    {
        return View();
    }

    //
    // POST: /Tarea/Delete/5

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
