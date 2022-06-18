using System;
using System.Web;
using System.Web.Mvc;
using EGA.Owin.Security.EGAOpenID;
using EGA.Owin.Security.Utils.Extensions;
using System.Web.Routing;
using BizPortal.Utils.Routes;
using System.Configuration;
using BizPortal.DAL;
using System.Linq;
using Microsoft.AspNet.Identity;
using EGA.EGA_CAS.Data.UserProfile;
using System.Security.Claims;
using System.Globalization;
using System.Threading;

namespace BizPortal.Utils.Annotations
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        // Custom property
        public string OpenIDUserType { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var mvcContext = new HttpContextWrapper(HttpContext.Current);
            if (mvcContext.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(OpenIDUserType))
            {
                var identity = mvcContext.User.Identity;
                var privilegeLevels = filterContext.HttpContext.User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.UserType);
                var authToken = identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.AuthToken);

                if (this.OpenIDUserType.Contains(privilegeLevels))
                {
                    if (!string.IsNullOrEmpty(authToken))
                    {
                        var db = new ApplicationDbContext();
                        var userId = identity.GetUserId();
                        var user = db.Users.Where(o => o.Id == userId).Single();
                        var service = identity.CreateOpenIDServiceInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"], bool.Parse(ConfigurationManager.AppSettings["TestMode"]));
                        //joaey เพิ่ม - 27/05/63
                        //เนื่องจากต้องการให้ล็อกอินด้วย dbdid ได้ จึงต้อง authen ด้วยbizid.egov 
                        //และเรียกservice openid production เท่านั้น
                        if (bool.Parse(ConfigurationManager.AppSettings["TestMode"]) && !String.IsNullOrEmpty(user.JuristicID) && user.JuristicID == "0105500002383")
                        { 
                            service = service = identity.CreateOpenIDServiceInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"], false);
                        }

                        var info = service.GetUser(authToken);

                        if (info != null)
                        {
                            user.Email = info.Email != null ? info.Email.StringValue : (user.Email ?? "");
                            user.EmailConfirmed = (info.Email != null && info.Email.VerifiedLevel.StartsWith("VerifiedLevel"));
                            user.PhoneNumber = info.Mobile != null ? info.Mobile.StringValue : (user.PhoneNumber ?? "");
                            user.PhoneNumberConfirmed = (info.Mobile != null && info.Mobile.VerifiedLevel.StartsWith("VerifiedLevel"));

                            identity.UpdateClaimValue(ClaimTypes.Email, user.Email);
                            identity.UpdateClaimValue(ClaimTypes.MobilePhone, user.PhoneNumber);

                            if (user.UserType == "Citizen")
                            {
                                var dto = (CitizenProfileDto)info;
                                if (dto.CitizenId != null && dto.CitizenId.VerifiedLevel.StartsWith("VerifiedLevel"))
                                {
                                    user.CitizenID = dto.CitizenId.StringValue;
                                    identity.UpdateClaimValue(EGAOpenIDAttributeExchangeType.CitizenID, user.CitizenID);
                                }
                            }
                            else if (user.UserType == "JuristicPerson")
                            {
                                var dto = (JuristicProfileDto)info;
                                if (dto.JuristicId != null && dto.JuristicId.VerifiedLevel.StartsWith("VerifiedLevel"))
                                {
                                    user.JuristicID = dto.JuristicId.StringValue;
                                    identity.UpdateClaimValue(EGAOpenIDAttributeExchangeType.JuristicID, user.JuristicID);
                                }
                            }
                            else if (user.UserType == "Foreigner")
                            {
                                var dto = (ForeignerProfileDto)info;
                                if (dto.PassportId != null && dto.PassportId.VerifiedLevel.StartsWith("VerifiedLevel"))
                                {
                                    user.PassportID = dto.PassportId.StringValue;
                                    identity.UpdateClaimValue(EGAOpenIDAttributeExchangeType.PassportID, user.PassportID);
                                }
                            }

                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    //filterContext.Result = new HttpUnauthorizedResult();
                    this.HandleUnauthorizedRequest(filterContext);
                }
            }
            else
            {
                //filterContext.Result = new HttpUnauthorizedResult();
                this.HandleUnauthorizedRequest(filterContext);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped);

            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        area = "Apps",
                        controller = "Account",
                        action = "ChooseType",
                        returnUrl = returnUrl
                    }
                    )
                );
        }
    }
}
