using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tareas.Web.Controllers
{
    [Authorize]
    public class MenuPrincipalController : Controller
    {
        //
        // GET: /MenuPrincipal/

        [ChildActionOnly]
        public ActionResult MenuPrincipal()
        {
            var items = new List<MenuItem>
            {
                new MenuItem{ Text = "Proyectos", Action = "Index", Controller = "Proyecto", Selected=false },
                new MenuItem{ Text = "Personas", Action = "Index", Controller = "Usuario", Selected = false}
            };

            string action = ControllerContext.ParentActionViewContext.RouteData.Values["action"].ToString();
            string controller = ControllerContext.ParentActionViewContext.RouteData.Values["controller"].ToString();

            foreach (var item in items)
            {
                if (item.Controller == controller && item.Action == action)
                {
                    item.Selected = true;
                }
            }

            return PartialView(items);
        }

        public class MenuItem
        {
            public string Text { get; set; }
            public string Action { get; set; }
            public string Controller { get; set; }
            public bool Selected { get; set; }
        }
    }
}
