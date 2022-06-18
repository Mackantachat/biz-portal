using System.Web.Mvc;

namespace BizPortal.Areas.WebApiV2
{
    public class WebApiV2AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WebApiV2";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WebApiV2_default",
                "WebApiV2/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}