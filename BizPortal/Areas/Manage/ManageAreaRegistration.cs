using BizPortal.Models.Routes;
using BizPortal.Utils.Routes;
using System.Configuration;
using System.Web.Mvc;

namespace BizPortal.Areas.Manage
{
    public class ManageAreaRegistration : DomainAreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Manage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            bool applyDomainRoute = bool.Parse(ConfigurationManager.AppSettings["ApplyDomainRoute"]);

            if (applyDomainRoute)
            {

                context.Routes.Add("DomainRoute_" + AreaName, new DomainRoute(
                    Domain,                                                // Domain with parameters
                    "{language}/BackOffice/{controller}/{action}/{id}",                    // URL with parameters
                    new { controller = Controller, action = Action, id = UrlParameter.Optional, language = Language }, // Parameter defaults
                    new { language = @"^[A-Za-z]{2}$" },
                    new string[] { "BizPortal.Areas." + AreaName + ".Controllers" },
                    AreaName));
            }
            else
            {
                context.MapRoute(
                AreaName + "_default",
                "{language}/BackOffice/{controller}/{action}/{id}",
                new { controller = Controller, action = Action, id = UrlParameter.Optional, language = Language }, new { language = @"^[A-Za-z]{2}$" });
            }
        }
    }
}