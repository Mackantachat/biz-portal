using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace BizPortal.Utils
{
    public static class Utility
    {
        public static string UpdateLanguage(this UrlHelper url, string lang)
        {
            string controller = url.RequestContext.RouteData.Values["controller"].ToString();
            string action = url.RequestContext.RouteData.Values["action"].ToString();

            url.RequestContext.RouteData.Values["language"] = lang;
            return url.Action(action, controller, url.RequestContext.RouteData.Values);
        }

        public static bool isCurrentCultureIsThai()
        {
            bool isThai = false;
            if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower() == "th")
            {
                isThai = true;
            }
            return isThai;
        }
    }
}
