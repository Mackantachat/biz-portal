using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Models
{
    public class ForceServiceDomain : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool applyDomainRoute = bool.Parse(ConfigurationManager.AppSettings["ApplyDomainRoute"]);
            if (applyDomainRoute)
            {
                string serviceDomain = ConfigurationManager.AppSettings["ServicesDomain"];
                Uri serviceUri = new Uri(serviceDomain);
                if (filterContext.HttpContext.Request.Url.Host.ToLower() != serviceUri.Host.ToLower())
                {
                    filterContext.Result = new RedirectResult(string.Format("{0}{1}", serviceDomain, filterContext.HttpContext.Request.Url.PathAndQuery));
                }
                else
                {
                    base.OnActionExecuting(filterContext);
                }
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}