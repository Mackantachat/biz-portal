using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Controllers
{
    public class IntroController : ControllerBase
    {
        // GET: /Intro/
        public ActionResult Index(string returnUrl)
        {
            string returnUrlEN = returnUrl;

            if (!Url.IsLocalUrl(returnUrl) || string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Action("Index", "Home", new { area = "", language = "th" });
                returnUrlEN = Url.Action("Index", "Home", new { area = "", language = "en" });
            }

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.ReturnUrlEN = returnUrlEN;

            string viewName = ConfigurationManager.AppSettings["IntroViewName"].ToString();

            HttpCookie cookie = null;
            if (Request.Cookies[ConfigurationValues.SHOW_INTRO_COOKIE] == null)
            {
                cookie = new HttpCookie(ConfigurationValues.SHOW_INTRO_COOKIE);

                bool applyDomainRoute = bool.Parse(ConfigurationManager.AppSettings["ApplyDomainRoute"]);
                if (applyDomainRoute)
                {
                    string domain = ConfigurationManager.AppSettings["CookieAuthDomain"];
                    cookie.Domain = domain;
                }
            }
            else
            {
                cookie = Request.Cookies[ConfigurationValues.SHOW_INTRO_COOKIE];
                if (cookie.Value == viewName)
                    return Redirect(returnUrl);
            }
            cookie.Value = viewName;
            Response.Cookies.Add(cookie);

            return View(viewName);
        }

        public ActionResult Detail(string viewName, string returnUrl) 
        {
            string returnUrlEN = returnUrl;

            if (!Url.IsLocalUrl(returnUrl) || string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Action("Index", "Home", new { area = "", language = "th" });
                returnUrlEN = Url.Action("Index", "Home", new { area = "", language = "en" });
            }

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.ReturnUrlEN = returnUrlEN;

            return View(viewName);
        }
    }
}