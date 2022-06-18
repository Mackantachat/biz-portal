using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EGA.Owin.Security.EGAOpenID;
using EGA.Owin.Security.Utils.Extensions;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using System.Configuration;
using BizPortal.ViewModels;
using BizPortal.Utils.Helpers;

namespace BizPortal.Models
{
    public class CustomWebViewPage<TModel> : WebViewPage<TModel>
    {
        public string IdentityID
        {
            get
            {
                //return "0100000000000"; /*//--//
                UserTypeEnum identityType = IdentityType;
                if (identityType == UserTypeEnum.Juristic)
                    return User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.JuristicID);
                else if (identityType == UserTypeEnum.Citizen)
                    return User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.CitizenID);
                else if (identityType == UserTypeEnum.Foreigner)
                    return User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.PassportID);
                else
                    return string.Empty;
                //--//*/
            }
        }

        public UserTypeEnum IdentityType
        {
            get
            {
                string userType = User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.UserType, "Anonymous");
                return EnumUtils.GetEnum<UserTypeEnum>(userType, true);
            }
        }

        public string IdentityFullName
        {
            get
            {
                return User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.FullName);
            }
        }

        public string IdentityPrefix
        {
            get
            {
                return User.Identity.GetClaimValueOrDefault(WellKnownAttributes.Name.Prefix);
            }
        }

        public string IdentityFirstname
        {
            get
            {
                return User.Identity.GetClaimValueOrDefault(WellKnownAttributes.Name.First);
            }
        }

        public string IdentityLastname
        {
            get
            {
                return User.Identity.GetClaimValueOrDefault(WellKnownAttributes.Name.Last);
            }
        }

        public string IdentityFullnameEN
        {
            get
            {
                return User.Identity.GetClaimValueOrDefault(OpenIdAttributeExchangeType.FullNameEN);
            }
        }

        public string IdentityPrefixEN
        {
            get
            {
                return User.Identity.GetClaimValueOrDefault(OpenIdAttributeExchangeType.PrefixEN);
            }
        }

        public string IdentityFirstnameEN
        {
            get
            {
                return User.Identity.GetClaimValueOrDefault(OpenIdAttributeExchangeType.FirstEN);
            }
        }

        public string IdentityLastnameEN
        {
            get
            {
                return User.Identity.GetClaimValueOrDefault(OpenIdAttributeExchangeType.LastEN);
            }
        }

        public string IdentityProvider
        {
            get
            {
                return User.Identity.GetClaimValueOrDefault(OpenIdAttributeExchangeType.LoginProvider);
            }
        }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public string AreaName { get; set; }

        public bool ApplyDomainRoute { get; set; }

        public string BizDomain { get; set; }

        public string ServicesDomain { get; set; }

        public bool ShowGovChannelHeader()
        {
            if (!string.IsNullOrEmpty(AreaName) && AreaName.ToLower() == "landing")
            {
                return true;
            }
            //else if (User.Identity.IsAuthenticated && (IdentityType == UserTypeEnum.Citizen || IdentityType == UserTypeEnum.Foreigner))
            //{
            //    return true;
            //}

            return false;
        }

        protected override void InitializePage()
        {
            if (ViewContext != null)
            {
                ActionName = ViewContext.RouteData.Values["action"].ToString();
                ControllerName = ViewContext.RouteData.Values["controller"].ToString();
                AreaName = ViewContext.RouteData.DataTokens["area"] != null ? ViewContext.RouteData.DataTokens["area"].ToString() : string.Empty;
            }

            ApplyDomainRoute = bool.Parse(ConfigurationManager.AppSettings["ApplyDomainRoute"]);
            if (ApplyDomainRoute)
            {
                BizDomain = ConfigurationManager.AppSettings["BizDomain"];
                ServicesDomain = ConfigurationManager.AppSettings["ServicesDomain"];
            }

            base.InitializePage();
        }

        public override void Execute()
        {

        }
    }
}