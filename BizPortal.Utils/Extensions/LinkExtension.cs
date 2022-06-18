using BizPortal.Utils.Routes;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System;

namespace BizPortal.Utils.Extensions
{
    public static class LinkExtensions
    {
        public static string BizAction(this UrlHelper urlHelper, string actionName, string controllerName, object routeValues, bool encryptQuery = false)
        {
            string path = encryptQuery ? urlHelper.EncodedAction(actionName, controllerName, routeValues) : urlHelper.Action(actionName, controllerName, routeValues);

            bool applyDomainRoute = bool.Parse(ConfigurationManager.AppSettings["ApplyDomainRoute"]);
            if (applyDomainRoute)
            {
                string domain = ConfigurationManager.AppSettings["BizDomain"];
                return domain + path;
            }
            else
            {
                return path;
            }
        }

        public static string ServiceAction(this UrlHelper urlHelper, string actionName, string controllerName, object routeValues, bool encryptQuery = false)
        {
            string path = encryptQuery ? urlHelper.EncodedAction(actionName, controllerName, routeValues) : urlHelper.Action(actionName, controllerName, routeValues);

            bool applyDomainRoute = bool.Parse(ConfigurationManager.AppSettings["ApplyDomainRoute"]);
            if (applyDomainRoute)
            {
                string domain = ConfigurationManager.AppSettings["ServicesDomain"];
                return domain + path;
            }
            else
            {
                return path;
            }
        }

        public static string ServiceAction(this System.Web.Http.Routing.UrlHelper urlHelper, string actionName, string controllerName, string area = "", string id = "")
        {

            bool applyDomainRoute = bool.Parse(ConfigurationManager.AppSettings["ApplyDomainRoute"]);

            if (applyDomainRoute)
            {
                string domain = ConfigurationManager.AppSettings["ServicesDomain"];
                return domain + urlHelper.Route("WebApiV2_default", new { controller = controllerName, action = actionName, area = area, id = id }).Replace("WebApiV2", "th");
            }
            else
            {
                return urlHelper.Link("WebApiV2_default", new { controller = controllerName, action = actionName, area = area, id = id }).Replace("WebApiV2", "th");
            }
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary(), requireAbsoluteUrl);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(), requireAbsoluteUrl);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary(), requireAbsoluteUrl);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, routeValues, new RouteValueDictionary(), requireAbsoluteUrl);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), requireAbsoluteUrl);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, routeValues, htmlAttributes, requireAbsoluteUrl);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, controllerName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), requireAbsoluteUrl);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool requireAbsoluteUrl)
        {
            if (requireAbsoluteUrl)
            {
                HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
                RouteData routeData = RouteTable.Routes.GetRouteData(currentContext);

                routeData.Values["controller"] = controllerName;
                routeData.Values["action"] = actionName;

                DomainRoute domainRoute = routeData.Route as DomainRoute;
                if (domainRoute != null)
                {
                    DomainData domainData = domainRoute.GetDomainData(new RequestContext(currentContext, routeData), routeData.Values);
                    return htmlHelper.ActionLink(linkText, actionName, controllerName, domainData.Protocol, domainData.HostName, domainData.Fragment, routeData.Values, null);
                }
            }
            return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
        }

        public static string EncodedAction(this UrlHelper urlHelper, string actionName, string controllerName, object routeValues)
        {
            string url = urlHelper.Action(actionName, controllerName, routeValues);
            var uri = new Uri(urlHelper.AbsolutePath(url));
            var queryString = uri.Query;

            if (!string.IsNullOrEmpty(queryString))
            {
                var encrypted = Encrypt(queryString);
                return uri.GetLeftPart(UriPartial.Path).Replace(uri.GetLeftPart(UriPartial.Authority), "") + "?q=" + HttpUtility.UrlEncode(encrypted);
            }
            else
            {
                return url;
            }
        }

        private static string Encrypt(string plainText)
        {
            string key = "ega+bizportal";
            byte[] EncryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            EncryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByte = Encoding.UTF8.GetBytes(plainText);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }
    }
}
