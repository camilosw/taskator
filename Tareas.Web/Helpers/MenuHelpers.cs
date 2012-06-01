using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Crm.Lib.Services;
using System.Text;

namespace Crm.Web.Helpers
{
    public static class MenuHelpers
    {
        public static HtmlString Menu(this HtmlHelper helper)
        {
            var moduloActivo = helper.ViewContext.RequestContext.HttpContext.Request.Path.Remove(0, 1);
            moduloActivo = moduloActivo.Split('/')[0];
            var modulos = new ModuloService().Get();

            var menu = new StringBuilder();
            foreach (var modulo in modulos)
            {
                var menuBuilder = new TagBuilder("li");
                if (modulo.Nombre == moduloActivo || 
                    (String.IsNullOrWhiteSpace(moduloActivo) && modulo.Nombre == "Home"))
                    menuBuilder.AddCssClass("current");
                menuBuilder.InnerHtml =
                    LinkExtensions.ActionLink(helper, modulo.Etiqueta, "Index", modulo.Nombre,
                                              new { area = String.Empty }, null).ToHtmlString();
                menu.AppendLine(menuBuilder.ToString(TagRenderMode.Normal));
            }
            
            return new HtmlString(menu.ToString());
        }
    }
}