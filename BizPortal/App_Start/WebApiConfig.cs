using BizPortal.Extensions;
using BizPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;

namespace BizPortal
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{namespace}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IHttpControllerSelector), new WebApiControllerVersioningSelector(config));
        }

        public static void RegisterWebApiFilters(HttpFilterCollection filters)
        {
            filters.Add(new CustomExceptionFilterAttribute());
        }
    }
}
