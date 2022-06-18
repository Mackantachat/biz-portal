using BizPortal.Utils.Helpers;
using System.Globalization;
using System.Threading;
using System.Web.Http.Controllers;

namespace BizPortal.Utils.Annotations
{
    public class InternationalizationAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            string language = (string)filterContext.RouteData.Values["language"] ?? "th";
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language);
        }
    }

    public class HttpInternationalizationAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string query = actionContext.Request.RequestUri.Query;
            var queryStrings = System.Web.HttpUtility.ParseQueryString(query);

            string language = "th";

            if (!string.IsNullOrEmpty(queryStrings["language"]))
            {
                language = queryStrings["language"];
            }
            else if (System.Web.HttpContext.Current.Request.UrlReferrer != null)
            {
                var routeData = RouteHelper.GetRouteDataByUrl(System.Web.HttpContext.Current.Request.UrlReferrer);
                if (routeData.Values["language"] != null)
                {
                    language = routeData.Values["language"].ToString();
                }
            }
            else
            {
                language = "th";
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(language);
        }
    }
}
