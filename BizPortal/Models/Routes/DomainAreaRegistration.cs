using BizPortal.Utils.Routes;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace BizPortal.Models.Routes
{
    public abstract class DomainAreaRegistration : AreaRegistration
    {
        public virtual string Controller
        {
            get
            {
                return "Home";
            }
        }

        public virtual string Action
        {
            get
            {
                return "Index";
            }
        }

        public virtual string Language
        {
            get
            {
                return "th";
            }
        }

        public virtual string Domain
        {
            get
            {
                Uri serviceUri = new Uri(ConfigurationManager.AppSettings["ServicesDomain"]);
                return serviceUri.Host;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            bool applyDomainRoute = bool.Parse(ConfigurationManager.AppSettings["ApplyDomainRoute"]);

            if (applyDomainRoute)
            {

                context.Routes.Add("DomainRoute_" + AreaName, new DomainRoute(
                    Domain,                                                // Domain with parameters
                    "{language}/" + AreaName + "/{controller}/{action}/{id}",                    // URL with parameters
                    new { controller = Controller, action = Action, id = UrlParameter.Optional, language = Language }, // Parameter defaults
                    new { language = @"^[A-Za-z]{2}$" },
                    new string[] { "BizPortal.Areas." + AreaName + ".Controllers" },
                    AreaName));
            }
            else
            {
                context.MapRoute(
                AreaName + "_default",
                "{language}/" + AreaName + "/{controller}/{action}/{id}",
                new { controller = Controller, action = Action, id = UrlParameter.Optional, language = Language }, new { language = @"^[A-Za-z]{2}$" });
            }
        }
    }
}