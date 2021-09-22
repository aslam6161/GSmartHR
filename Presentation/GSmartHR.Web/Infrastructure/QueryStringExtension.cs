using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSmartHR.Web.Infrastructure
{
    public static class QueryStringExtension
    {
        public static bool HasValue(string key)
        {
            Microsoft.Extensions.Primitives.StringValues queryVal;

            if (HttpContext.Current.Request.Query.TryGetValue(key, out queryVal))
            {
                return true;
            }

            return false;
        }

        public static bool ShowDisplayOrder()
        {
            return HasValue("showdisplayorder");
        }
    }
}
