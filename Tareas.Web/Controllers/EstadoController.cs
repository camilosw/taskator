using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tareas.Lib.Services;

namespace Tareas.Web.Controllers
{
  [Authorize]
  public class EstadoController : Controller
  {
    private IEstadoService service;

    public EstadoController()
    {
      service = new EstadoService();
    }

    //
    // GET: /Estado/

    [HttpPost]
    public JsonResult List()
    {
      var listadoEstados = service.GetAll();
      var usuario = new UsuarioService().GetByNombreUsuario(User.Identity.Name);
      if (!usuario.Administrador) {
        listadoEstados.RemoveAll(e => e.TareaActiva == 0);
      }
      var estados = from e in listadoEstados
                    select new {
                      Id = e.Id.ToString(),
                      Nombre = e.Nombre,
                      Color = e.Color,
                      TareaActiva = e.TareaActiva
                    };
      return Json(estados);
    }
  }
}
