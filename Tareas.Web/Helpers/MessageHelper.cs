using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Crm.Web.Helpers
{
    /// <summary>
    /// Muestra el mensaje informativo o de error que se ponga en TempData
    /// </summary>
    public static class MessageHelper
    {
        public static HtmlString Message(this HtmlHelper helper)
        {
            StringBuilder output = new StringBuilder();

            if (helper.ViewContext.TempData["InfoMessage"] != null)
            {
                StringBuilder tag = new StringBuilder();
                tag.AppendLine("<div class='infoMessage ui-widget ui-state-highlight ui-corner-all'>");
                tag.AppendLine("<span class='ui-icon ui-icon-info'></span>");
                tag.AppendLine(helper.ViewContext.TempData["InfoMessage"].ToString());
                tag.AppendLine("</div>");
                output.AppendLine(tag.ToString());
            }

            if (helper.ViewContext.TempData["ErrorMessage"] != null)
            {
                StringBuilder tag = new StringBuilder();
                tag.AppendLine("<div class='infoMessage ui-widget ui-state-error ui-corner-all'>");
                tag.AppendLine("<span class='ui-icon ui-icon-alert'></span>");
                tag.AppendLine(helper.ViewContext.TempData["ErrorMessage"].ToString());
                tag.AppendLine("</div>");
                output.AppendLine(tag.ToString());
            }

            if (helper.ViewContext.TempData["ExceptionMessage"] != null)
            {
                StringBuilder tag = new StringBuilder();
                tag.AppendLine("<div class='infoMessage ui-widget ui-state-error ui-corner-all'>");
                tag.AppendLine("<span class='ui-icon ui-icon-alert'></span>");
                tag.AppendLine(helper.ViewContext.TempData["ExceptionMessage"].ToString());
                tag.AppendLine("</div>");
                output.AppendLine(tag.ToString());
            }

            return new HtmlString(output.ToString());
        }
    }
}
