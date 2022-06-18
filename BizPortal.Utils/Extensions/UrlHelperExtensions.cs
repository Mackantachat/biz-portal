using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BizPortal.Utils.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string AbsolutePath(this UrlHelper urlHelper, string relativePath)
        {
            Uri uri = null;
            if (Uri.TryCreate(urlHelper.RequestContext.HttpContext.Request.Url, relativePath, out uri))
                return uri.ToString();
            else
                return urlHelper.RequestContext.HttpContext.Request.Url.ToString();
        }

        public static string AbsoluteAction(this UrlHelper urlHelper,
                                            string actionName,
                                            string controllerName)
        {
            return AbsolutePath(urlHelper, urlHelper.Action(actionName, controllerName));
        }

        public static string AbsoluteAction(this UrlHelper urlHelper,
                                            string actionName,
                                            string controllerName,
                                            object routeValues)
        {
            return AbsolutePath(urlHelper, urlHelper.Action(actionName, controllerName, routeValues));
        }
    }

}
