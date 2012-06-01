using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Crm.Web.Helpers
{
    public static class ButtonHelpers
    {
        public static HtmlString LinkButton(this HtmlHelper helper, string buttonText, string actionName)
        {
            return LinkExtensions.ActionLink(helper, buttonText, actionName,
                null, new { @class = "fg-button ui-state-default ui-corner-all" });
        }

        public static HtmlString LinkButton(this HtmlHelper helper, string buttonText, string actionName, string controllerName)
        {
            return LinkExtensions.ActionLink(helper, buttonText, actionName, controllerName,
                null, new { @class = "fg-button ui-state-default ui-corner-all" });
        }

        public static HtmlString SubmitButton(this HtmlHelper helper, string value)
        {
            string tag = String.Format("<input type=\"submit\" value=\"{0}\" class=\"fg-button ui-state-default ui-corner-all\" />", value);
            return new HtmlString(tag);
        }

        public static HtmlString InputButton(this HtmlHelper helper, string value, string onClick)
        {
            string tag = String.Format("<input type=\"button\" value=\"{0}\" onclick=\"{1}\" class=\"fg-button ui-state-default ui-corner-all\" />", value, onClick);
            return new HtmlString(tag);
        }

        /// <summary>
        /// Crea el boton cancelar
        /// </summary>
        public static HtmlString CancelButton(this HtmlHelper helper, string buttonText, string actionName, string controllerName = null, object buttonHtmlAttributes = null)
        {
            // Crea el button
            var buttonBuilder = new TagBuilder("input");
            buttonBuilder.MergeAttribute("type", "button");
            buttonBuilder.MergeAttribute("value", buttonText);
            buttonBuilder.MergeAttribute("class", "fg-button ui-state-default ui-corner-all");
            string onclick = "window.location.pathname = '" +
                new UrlHelper(helper.ViewContext.RequestContext).Action(actionName, controllerName) + "'";

            buttonBuilder.MergeAttribute("onclick", onclick);
            buttonBuilder.MergeAttributes(new RouteValueDictionary(buttonHtmlAttributes));


            return new HtmlString(buttonBuilder.ToString(TagRenderMode.Normal));
        }

    }
}