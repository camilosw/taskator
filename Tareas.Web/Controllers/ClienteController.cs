using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tareas.Lib.Services;

namespace Tareas.Web.Controllers
{
  [Authorize]
  public class ClienteController : Controller
  {
    private IClienteService service;

    public ClienteController()
    {
      service = new ClienteService();
    }

    //
    // GET: /Cliente/

    public ActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public JsonResult AjaxList()
    {
      var clientes = from c in service.GetAllActive()
                     select new
                     {
                       Id = c.Id,
                       Nombre = c.Nombre
                     };
      return Json(clientes);
    }
  }
}
