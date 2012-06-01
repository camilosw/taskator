using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tareas.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static HtmlString Css(this HtmlHelper helper, string fileName, string media = "screen")
        {
            string path = new UrlHelper(helper.ViewContext.RequestContext).Content("~/Content");
            string tag = String.Format("<link href='{0}/{1}' rel='stylesheet' type='text/css' media='{2}'/>\n",
                                    path, helper.AttributeEncode(fileName), media);
            return new HtmlString(tag);
        }

        public static HtmlString Script(this HtmlHelper helper, string fileName)
        {
            string path = new UrlHelper(helper.ViewContext.RequestContext).Content("~/Scripts");
            string tag = String.Format("<script src='{0}/{1}' type='text/javascript'></script>\n", path, helper.AttributeEncode(fileName));
            return new HtmlString(tag);
        }

        public static HtmlString Image(this HtmlHelper helper, string fileName, string alt = "", string attributes = "")
        {
            string path = new UrlHelper(helper.ViewContext.RequestContext).Content("~/Content/images");
            string tag = String.Format("<img src='{0}/{1}' alt='{2}' {3}/>",
                path, helper.AttributeEncode(fileName), helper.AttributeEncode(alt), helper.AttributeEncode(attributes));
            return new HtmlString(tag);
        }
    }
}