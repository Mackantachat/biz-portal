using System;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using System.Globalization;
using System.Web.Routing;

namespace BizPortal.Utils.Annotations
{
    public class IntroSiteAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                string viewName = ConfigurationManager.AppSettings["IntroViewName"].ToString();

                if ((filterContext.ActionDescriptor.ControllerDescriptor.ControllerName != "Intro")
                    && (!string.IsNullOrEmpty(viewName)))
                {
                    var context = HttpContext.Current;

                    var mvcContext = new HttpContextWrapper(context);
                    //Overwrite the redirection uri
                    UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);
                    var routeData = RouteTable.Routes.GetRouteData(mvcContext);
                    //Get the current language  
                    RouteValueDictionary routeValues = new RouteValueDictionary();
                    var lang = routeData.Values["language"] ?? "th";

                    HttpCookie cookie = null;
                    if (context.Request.Cookies[ConfigurationValues.SHOW_INTRO_COOKIE] != null)
                        cookie = context.Request.Cookies[ConfigurationValues.SHOW_INTRO_COOKIE];

                    if ((cookie == null) || (cookie.Value != viewName))
                    {
                        string returnUrl = filterContext.HttpContext.Request.Url.PathAndQuery;
                        if (returnUrl != "/")
                        {
                            returnUrl = HttpUtility.UrlEncode(returnUrl);
                            filterContext.Result = new RedirectResult("~/" + lang + "/Intro?returnUrl=" + returnUrl);
                        }
                        else
                            filterContext.Result = new RedirectResult("~/" + lang + "/Intro");
                    }
                }
            }
            catch (Exception ex) { }
        }
    }
}
