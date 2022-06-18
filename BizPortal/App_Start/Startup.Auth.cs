using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using EGA.Owin.Security.EGAOpenID;
using EGA.Owin.Security.EGAOAuth;
using System.Configuration;
using BizPortal.Models;
using BizPortal.DAL;
using System.Collections.Generic;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using BizPortal.Utils.Routes;
using MB.Owin.Logging.Log4Net;
using Microsoft.Owin.Logging;

namespace BizPortal
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseLog4Net();//from Web.config
            var logger = app.CreateLogger<Startup>();
            logger.WriteInformation("OWIN Loggong is started.");

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            CookieAuthenticationProvider provider = new CookieAuthenticationProvider
            {
                // Enables the application to validate the security stamp when the user logs in.
                // This is a security feature which is used when you change a password or add an external login to your account.  
                OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(60),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
            };
            var originalHandler = provider.OnApplyRedirect;
            bool applyDomainRoute = bool.Parse(ConfigurationManager.AppSettings["ApplyDomainRoute"]);

            provider.OnApplyRedirect = context =>
                    {
                        // http://stackoverflow.com/questions/21168955/mvc-5-how-to-define-owin-loginpath-with-localized-routes

                        var mvcContext = new HttpContextWrapper(HttpContext.Current);
                        //Overwrite the redirection uri
                        UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);
                        var routeData = RouteTable.Routes.GetRouteData(mvcContext);
                        //Get the current language  
                        RouteValueDictionary routeValues = new RouteValueDictionary();
                        routeValues.Add("language", routeData.Values["language"]);
                        DomainRoute domainRoute = routeData.Route as DomainRoute;

                        if (mvcContext.User.Identity.IsAuthenticated)
                        {
                            //Reuse the RetrunUrl
                            Uri uri = new Uri(context.RedirectUri);
                            string returnUrl = HttpUtility.ParseQueryString(uri.Query)[context.Options.ReturnUrlParameter];
                            routeValues.Add(context.Options.ReturnUrlParameter, returnUrl);

                            if (routeData.DataTokens["area"] != null && routeData.DataTokens["area"].ToString() == "Apps")
                            {
                                routeValues.Add("area", "Apps");
                                //context.RedirectUri = url.Action("ChooseType", "Account", routeValues);
                                context.RedirectUri = url.Action("Login", "Account", routeValues);
                            }
                            else
                            {
                                routeValues.Add("area", "");
                                context.RedirectUri = url.Action("UnAuthorized", "Account", routeValues);
                            }
                        }
                        else
                        {
                            routeValues.Add(context.Options.ReturnUrlParameter, context.Request.Uri.PathAndQuery);
                            if ((routeData.DataTokens["area"] != null && routeData.DataTokens["area"].ToString() == "Apps") ||
                                (routeData.Values["area"] == null && routeData.Values["controller"].ToString() == "Quiz") ||
                                (routeData.Values["area"] == null && routeData.Values["controller"].ToString() == "Track"))
                            {
                                routeValues.Add("area", "Apps");

                                if (mvcContext.Request.QueryString["Citizen"] != null && mvcContext.Request.QueryString["Citizen"] == "true")
                                {
                                    routeValues.Add("authType", "EGA OpenID Citizen");
                                    context.RedirectUri = url.Action("Login", "Account", routeValues);
                                }
                                else
                                {
                                    context.RedirectUri = url.Action("ChooseType", "Account", routeValues);
                                }
                            }
                            else
                            {
                                if (domainRoute != null)
                                {
                                    Uri serviceUri = new Uri(ConfigurationManager.AppSettings["ServicesDomain"]);
                                    if (serviceUri.Host == mvcContext.Request.Url.Host)
                                    {
                                        routeValues.Add("area", "Apps");
                                        context.RedirectUri = url.Action("ChooseType", "Account", routeValues);
                                    }
                                    else
                                    {
                                        routeValues.Add("area", "");
                                        context.RedirectUri = url.Action("Login", "Account", routeValues);
                                    }
                                }
                                else
                                {
                                    if(routeData.DataTokens["area"] != null && routeData.DataTokens["area"].ToString() == "Manage")
                                    {
                                        routeValues.Add("area", "Apps");
                                        context.RedirectUri = url.Action("ChooseType", "Account", routeValues);
                                    }
                                    else
                                    {
                                        // Redirect to OpenID login page
                                        routeValues.Add("area", "");
                                        context.RedirectUri = url.Action("Login", "Account", routeValues);
                                    }
                                }
                            }
                        }

                        originalHandler.Invoke(context);
                    };


            //https://www.jamessturtevant.com/posts/ASPNET-Identity-Cookie-Authentication-Timeouts/
            string currentLang = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var cookieOptions = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //LoginPath = new PathString(string.Format("/{0}/Account/UnAuthorized", currentLang)),
                LoginPath = new PathString(string.Format("/{0}?showMsg={1}", currentLang, ShowMsgType.Unauthorized)),
                Provider = provider,
                ExpireTimeSpan = TimeSpan.FromMinutes(60),
                SlidingExpiration = true,
            };

            if (applyDomainRoute)
            {
                // ถ้าบังคับใช้ domain
                cookieOptions.CookieDomain = ConfigurationManager.AppSettings["CookieAuthDomain"];
                cookieOptions.CookieName = ConfigurationManager.AppSettings["CookieNameAuthDomain"];
            }

            app.UseCookieAuthentication(cookieOptions);
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            bool juristicEmailVer = false;

            app.UseEGAOpenIDAuthentication(new EGAOpenIDAuthenticationOptions()
            {
                CustomOpenIDUrl = () =>
                {
                    var openIDUrl = ConfigurationManager.AppSettings["OpenIDUrl"] + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                    return openIDUrl;
                },
                AttributeRequests = new List<AttributeRequest>()
                    {
                        new AttributeRequest(WellKnownAttributes.Contact.Email, bool.TryParse(ConfigurationManager.AppSettings["JuristicEmailVerification"], out juristicEmailVer) ? juristicEmailVer : false),
                        new AttributeRequest(WellKnownAttributes.Name.FullName, true),
                        new AttributeRequest(WellKnownAttributes.Name.Prefix, true),
                        new AttributeRequest(WellKnownAttributes.Name.First, true),
                        new AttributeRequest(WellKnownAttributes.Name.Last, true),
                        new AttributeRequest(EGAOpenIDAttributeExchangeType.UserID, true),
                        new AttributeRequest(WellKnownAttributes.Name.Alias, true),
                        new AttributeRequest(EGAOpenIDAttributeExchangeType.UserType, true),
                        new AttributeRequest(EGAOpenIDAttributeExchangeType.CitizenID, false),
                        new AttributeRequest(EGAOpenIDAttributeExchangeType.JuristicID, false),
                        new AttributeRequest(EGAOpenIDAttributeExchangeType.PassportID, false),
                        new AttributeRequest(EGAOpenIDAttributeExchangeType.OrganizationID, false),
                        new AttributeRequest(EGAOpenIDAttributeExchangeType.AuthToken, true),
                        new AttributeRequest(WellKnownAttributes.Contact.Phone.Mobile, false)
                    }
            });

            bool citizenEmailVer = false;
            bool citizenIDVer = false;
            bool citizenMobileVer = false;

            app.UseEGAOpenIDAuthentication(new EGAOpenIDAuthenticationOptions("EGA OpenID Citizen")
            {
                CustomOpenIDUrl = () =>
                {
                    var openIDUrl = ConfigurationManager.AppSettings["CitizenOpenIDUrl"] + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                    return openIDUrl;
                },
                CallbackPath = new PathString("/signin-egaopenid-citizen"),
                AttributeRequests = new List<AttributeRequest>()
                    {   new AttributeRequest(EGAOpenIDAttributeExchangeType.CitizenID, bool.TryParse(ConfigurationManager.AppSettings["CitizenIDVerification"], out citizenIDVer) ? citizenIDVer : false),
                        new AttributeRequest(WellKnownAttributes.Contact.Email, bool.TryParse(ConfigurationManager.AppSettings["CitizenEmailVerification"], out citizenEmailVer) ? citizenEmailVer : false),
                        new AttributeRequest(WellKnownAttributes.Contact.Phone.Mobile, bool.TryParse(ConfigurationManager.AppSettings["CitizenMobilePhoneVerification"], out citizenMobileVer) ? citizenMobileVer : false),
                        new AttributeRequest(WellKnownAttributes.Name.FullName, true),
                        new AttributeRequest(WellKnownAttributes.Name.Prefix, false),
                        new AttributeRequest(WellKnownAttributes.Name.First, true),
                        new AttributeRequest(WellKnownAttributes.Name.Last, true),
                        new AttributeRequest(EGAOpenIDAttributeExchangeType.UserID, true),
                        new AttributeRequest(WellKnownAttributes.Name.Alias, true),
                        new AttributeRequest(EGAOpenIDAttributeExchangeType.UserType, true),                 
                        new AttributeRequest(EGAOpenIDAttributeExchangeType.JuristicID, false),
                        new AttributeRequest(EGAOpenIDAttributeExchangeType.PassportID, false),
                        new AttributeRequest(EGAOpenIDAttributeExchangeType.OrganizationID, false),
                        new AttributeRequest(EGAOpenIDAttributeExchangeType.AuthToken, true),
                       
                    },
                SetNullApplicationStoreParam = true
            });

            app.UseEGAOAuthAuthentication(new EGAOAuthAuthenticationOptions()
            {
                ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"],
                ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"],
                CustomOAuthUrl = () =>
                {
                    var oAuthUrl = ConfigurationManager.AppSettings["OAuthUrl"];
                    oAuthUrl = oAuthUrl.Replace("{language}", System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
                    return oAuthUrl;
                },
                CustomOAuthXmlUrl = () =>
                {
                    var oAuthXmlUrl = ConfigurationManager.AppSettings["OAuthXmlUrl"];
                    oAuthXmlUrl = oAuthXmlUrl.Replace("{language}", System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
                    return oAuthXmlUrl;
                }
            });

            app.MapSignalR();

            //https://weblog.west-wind.com/posts/2011/Feb/11/HttpWebRequest-and-Ignoring-SSL-Certificate-Errors
            System.Net.ServicePointManager.ServerCertificateValidationCallback += delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                  System.Security.Cryptography.X509Certificates.X509Chain chain,
                  System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true; // **** Always accept
            };

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}