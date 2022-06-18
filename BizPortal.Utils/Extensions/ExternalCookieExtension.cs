using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizPortal.Utils.Extensions
{
    public static class ExternalCookieExtension
    {
        public static void UseExternalSignInCookie(this IAppBuilder app, string externalAuthenticationType, string cookiePrefix)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }

            app.SetDefaultSignInAsAuthenticationType(externalAuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = externalAuthenticationType,
                AuthenticationMode = AuthenticationMode.Passive,
                CookieManager = new SystemWebCookieManager(),
                CookieName = cookiePrefix + externalAuthenticationType,
                ExpireTimeSpan = TimeSpan.FromMinutes(5),
            });
        }
    }
}