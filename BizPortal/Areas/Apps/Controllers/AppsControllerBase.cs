using BizPortal.Controllers;
using BizPortal.Utils.Annotations;
using BizPortal.Utils.Helpers;
using EGA.Owin.Security.EGAOpenID;
using EGA.Owin.Security.Utils.Extensions;
using EGA.WS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Areas.Apps.Controllers
{
    public abstract class AppsControllerBase : BizPortal.Controllers.ControllerBase
    {
        private const string SESSION_OAUTH_TOKEN = "BizIDOAuthToken";

        public AppsControllerBase()
        {

        }

        [AuthorizeUser(OpenIDUserType = "JuristicPerson")]
        protected ActionResult GetOAuthData(string scope, string returnUrl, string denyCallbackUrl)
        {
            Dictionary<string, string> requestParameters = new Dictionary<string, string>();
            requestParameters.Add("scope", scope ?? string.Empty);
            TempData["RequestParameters"] = requestParameters;

            Dictionary<string, string> redirectParameters = new Dictionary<string, string>();
            redirectParameters.Add("DenyCallbackUrl", denyCallbackUrl ?? string.Empty);
            TempData["RedirectParameters"] = redirectParameters;


            return RedirectToAction("OAuth", "Account", new { area = "", returnUrl = returnUrl });
        }

        protected string GetUploadAbsolutePath(string subFolder = "")
        {
            string absPath = "~/Uploads/Apps/" + ControllerContext.RouteData.Values["controller"].ToString();
            if (!string.IsNullOrEmpty(subFolder))
                absPath += "/" + subFolder;
            return absPath;
        }

        protected string GetViewName()
        {
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            return string.Format("{0}_{1}", actionName, lang);
        }

        /// <summary>
        /// Default View Name format: ActionName_TwoLetterISOLanguageName eg. ExAction_th
        /// </summary>
        /// <returns></returns>
        [AuthorizeUser(OpenIDUserType = "JuristicPerson")]
        protected ViewResult InterView()
        {
            return View(GetViewName());
        }

        /// <summary>
        /// Default View Name format: ActionName_TwoLetterISOLanguageName eg. ExAction_th
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AuthorizeUser(OpenIDUserType = "JuristicPerson")]
        public ViewResult InterView(object model)
        {
            return View(GetViewName(), model);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            try
            {
                ViewBag.IdentityID = IdentityID;
                ViewBag.IdentityType = IdentityType;
            }
            catch
            {
                ViewBag.IdentityID = null;
                ViewBag.IdentityType = null;
            }
        }
    }
}