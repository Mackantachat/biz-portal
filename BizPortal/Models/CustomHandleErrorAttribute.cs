using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace BizPortal.Models
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //CustomErrorsSection customErrorsSection = ConfigurationManager.GetSection("system.web/customErrors") as CustomErrorsSection;
            HttpException httpEx = filterContext.Exception as HttpException;
            if (httpEx != null)// && customErrorsSection != null && customErrorsSection.Mode != CustomErrorsMode.Off)
            {
                if (httpEx.GetHttpCode() == 404) // Not Found
                {
                    filterContext.Result = new RedirectResult(string.Format("~/{0}/Error/PageNotFound", CultureInfo.CurrentCulture.TwoLetterISOLanguageName));
                }
            }
            //else
            //{
                base.OnException(filterContext);
            //}
        }
    }
}