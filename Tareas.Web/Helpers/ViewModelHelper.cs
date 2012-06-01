using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Crm.Lib.Services;
using System.Collections;

namespace Crm.Web.Helpers
{
    public static class ViewModelHelper
    {
        public static SelectList CreateSelectList(this ISelectListService service, string defaultText,
                                                     string selectedValue)
        {
            List<KeyValuePair<string, string>> list = service.GetIdValueList();

            if (defaultText != null)
                list.Insert(0, new KeyValuePair<string, string>(String.Empty, defaultText));

            return new SelectList(list, "Key", "Value", selectedValue);
        }

        public static MultiSelectList CreateMultipleSelectList(this ISelectListService service,
                                                                  IEnumerable selectedValues)
        {
            List<KeyValuePair<string, string>> list = service.GetIdValueList();

            return new MultiSelectList(list, "Key", "Value", selectedValues);
        }
    }
}