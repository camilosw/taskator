using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm.Web.Helpers
{
    public static class FormatHelper
    {
        public static string ToString(this bool value, string ifTrue, string ifFalse)
        {
            return value ? ifTrue : ifFalse;
        }

        public static string ToString(this bool? value, string ifTrue, string ifFalse, string ifNull)
        {
            if (!value.HasValue) return ifNull;
            return value.Value ? ifTrue : ifFalse;
        }
    }
}