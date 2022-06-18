using BizPortal.Utils.Routes;
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace BizPortal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            bool applyDomainRoute = bool.Parse(ConfigurationManager.AppSettings["ApplyDomainRoute"]);

            if (applyDomainRoute)
            {
                Uri serviceUri = new Uri(ConfigurationManager.AppSettings["BizDomain"]);

                routes.Add("DomainRoute", new DomainRoute(
                    serviceUri.Host,                                    // Domain with parameters
                    "{language}/{controller}/{action}/{id}",                    // URL with parameters
                    new { controller = "Home", action = "Index", id = UrlParameter.Optional, language = "th" }, // Parameter defaults
                    new { language = @"^[A-Za-z]{2}$" },
                    new string[] { "BizPortal.Controllers" }));

                serviceUri = new Uri(ConfigurationManager.AppSettings["ServicesDomain"]);
                routes.Add("DomainRoute_Account", new DomainRoute(
                    serviceUri.Host,                                    // Domain with parameters
                    "{language}/Account/{action}/{id}",                    // URL with parameters
                    new { controller = "Account", action = "Index", id = UrlParameter.Optional, language = "th" }, // Parameter defaults
                    new { language = @"^[A-Za-z]{2}$" },
                    new string[] { "BizPortal.Controllers" }));

                routes.Add("DomainRoute_File", new DomainRoute(
                    serviceUri.Host,                                    // Domain with parameters
                    "{language}/File/{action}/{id}",                    // URL with parameters
                    new { controller = "File", action = "Index", id = UrlParameter.Optional, language = "th" }, // Parameter defaults
                    new { language = @"^[A-Za-z]{2}$" },
                    new string[] { "BizPortal.Controllers" }));

                serviceUri = new Uri(ConfigurationManager.AppSettings["ServicesDomain"]);
                routes.Add("DomainRoute_Tracking", new DomainRoute(
                    serviceUri.Host,                                    // Domain with parameters
                    "{language}/Track/{action}/{id}",                    // URL with parameters
                    new { controller = "Track", action = "Index", id = UrlParameter.Optional, language = "th" }, // Parameter defaults
                    new { language = @"^[A-Za-z]{2}$" },
                    new string[] { "BizPortal.Controllers" }));

                serviceUri = new Uri(ConfigurationManager.AppSettings["ServicesDomain"]);
                routes.Add("DomainRoute_Quiz", new DomainRoute(
                    serviceUri.Host,                                    // Domain with parameters
                    "{language}/Quiz/{id}",                             // URL with parameters
                    new { controller = "Quiz", action = "Group", id = UrlParameter.Optional, language = "th" }, // Parameter defaults
                    new { language = @"^[A-Za-z]{2}$" },
                    new string[] { "BizPortal.Controllers" }));

                routes.Add("DomainRoute_Intro", new DomainRoute(
                    serviceUri.Host,                                    // Domain with parameters
                    "{language}/Intro/{action}/{id}",                   // URL with parameters
                    new { controller = "Intro", action = "Index", id = UrlParameter.Optional, language = "th" }, // Parameter defaults
                    new { language = @"^[A-Za-z]{2}$" },
                    new string[] { "BizPortal.Controllers" }));
            }
            else
            {
                routes.MapRoute("DefaultQuiz", "{language}/Quiz/{id}",
                    new { controller = "Quiz", action = "Group", id = UrlParameter.Optional, language = "th" },
                    new { language = @"^[A-Za-z]{2}$" },
                    new string[] { "BizPortal.Controllers" });

                routes.MapRoute("DefaultLocalized", "{language}/{controller}/{action}/{id}", 
                    new { controller = "Home", action = "Index", id = UrlParameter.Optional, language = "th" },
                    new { language = @"^[A-Za-z]{2}$" },
                    new string[] { "BizPortal.Controllers" });
            }
        }
    }
}
