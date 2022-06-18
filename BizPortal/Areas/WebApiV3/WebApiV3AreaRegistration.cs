using System.Web.Mvc;

namespace BizPortal.Areas.WebApiV3
{
    public class WebApiV3AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WebApiV3";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WebApiV3_default",
                "WebApiV3/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}