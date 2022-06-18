using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using BizPortal.DAL;
using BizPortal.Utils.Extensions;
using EGA.Owin.Security.EGAOpenID;
using EGA.Owin.Security.Utils.Extensions;
using BizPortal.ViewModels;
using Newtonsoft.Json.Serialization;
using BizPortal.Utils.Annotations;
using System.Threading;
using EGA.WS;
using System.Configuration;
using SharpRaven;
using SharpRaven.Data;
using System.Threading.Tasks;
using BizPortal.ViewModels.V2;
using Mapster;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using Microsoft.AspNet.Identity;
using BizPortal.Models;
using System.Globalization;
using Microsoft.AspNet.Identity.EntityFramework;
using BizPortal.Utils.Helpers;

namespace BizPortal.Areas.WebApi.Controllers
{
    [HttpInternationalization]
    public abstract class ApiControllerBase : ApiController
    {
        private ApplicationDbContext _db;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private HttpContextWrapper _context;
        private EGAWSAPI _api;
        private RavenClient _ravenClient;
        private List<ApplicationRole> _roles;

        public EGAWSAPI Api
        {
            get
            {
                if (_api == null)
                {
                    _api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);
                }

                return _api;
            }
        }

        public RavenClient RavenClient
        {
            get
            {
                if (_ravenClient == null)
                {
                    _ravenClient = new RavenClient("https://f8fd6431dbd9485a9caeaba3110bf977:5d05890d51d94581a55b4401e11df255@sentry.io/138851");
                }

                return _ravenClient;
            }
        }

        public HttpContextWrapper Context
        {
            get
            {
                if (_context == null)
                    _context = new HttpContextWrapper(HttpContext.Current);
                return _context;
            }
            private set
            {
                _context = value;
            }
        }

        public ApplicationDbContext DB
        {
            get
            {
                if (_db == null)
                    _db = new ApplicationDbContext();
                return _db;// ?? Context.GetOwinContext().Get<ApplicationDbContext>();
            }
            protected set
            {
                _db = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? Context.GetOwinContext().Get<ApplicationSignInManager>();
            }
            protected set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>(DB));
                //return _userManager ?? Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            protected set
            {
                _userManager = value;
            }
        }

        public List<ApplicationRole> Roles
        {
            get
            {
                if (_roles == null)
                    _roles = DB.Roles.Where(e => e.Order > 0).ToList();
                return _roles;
            }
            protected set
            {
                _roles = value;
            }
        }

        public string CurrentUserID
        {
            get
            {
                return User.Identity.GetUserId();
            }
        }

        public string OrganizationID
        {
            get
            {
                //string orgCode = ConfigurationManager.AppSettings["TestOrgranizationID"].ToString();
                //if (Convert.ToBoolean(ConfigurationManager.AppSettings["EnableTestOrgranizationID"]))
                //{
                //    if (BizPortal.Controllers.ControllerBase.testUserOrgCode.ContainsKey(User.Identity.Name))
                //    {
                //        orgCode = BizPortal.Controllers.ControllerBase.testUserOrgCode[User.Identity.Name];
                //    }
                //}
                //else
                //{
                string orgCode = User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.OrganizationID);
                //}
                return orgCode;
            }
        }

        public string IdentityID
        {
            get
            {
                UserTypeEnum identityType = IdentityType;
                if (identityType == UserTypeEnum.Juristic)
                    return User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.JuristicID);
                else if (identityType == UserTypeEnum.Citizen)
                    return User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.CitizenID);
                else if (identityType == UserTypeEnum.GovernmentAgent)
                    return User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.CitizenID);
                else if (identityType == UserTypeEnum.Foreigner)
                    return User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.PassportID);
                else
                    return string.Empty;
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

        public string CitizenBirthDate
        {
            get
            {
                DateTime date = DateTime.MinValue;
                return DateTime.TryParseExact(User.Identity.GetBirthDate(), "yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en"), DateTimeStyles.None, out date) ? date.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th")) : "";
            }
        }

        public UserTypeEnum IdentityType
        {
            get
            {
                string userType = User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.UserType);
                return EnumUtils.GetEnum<UserTypeEnum>(userType, true);
            }
        }

        public string IdentityProvider
        {
            get
            {
                return User.Identity.GetClaimValueOrDefault(OpenIdAttributeExchangeType.LoginProvider);
            }
        }

        public JuristicRequestFileUploadType[] getJuristicRequestFileUploadType(string applicationSysName)
        {
            if (true || applicationSysName == "SSO Register Employer") // ปรับให้ดึงจาก Database
                return new JuristicRequestFileUploadType[] { new JuristicRequestFileUploadType { Name = "ใบแจ้งการประเมินเงินสมทบกองทุนเงินทดแทนประจำปี", Code = "C01" }, new JuristicRequestFileUploadType { Name = "ใบรับรองการขึ้นทะเบียนนายจ้าง", Code = "C02" } };
            else
                return new JuristicRequestFileUploadType[] { };
        }

        public IdentityAddress GetCGDIdentityAddress(UserTypeEnum identityType, ApplicationRequestViewModel model)
        {
            var address = new IdentityAddress();

            if (identityType == UserTypeEnum.Juristic)
            {
                var addressinfo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION");

                address.Floor = addressinfo.ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS");
                address.Building = addressinfo.ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS");
                address.RoomNo = addressinfo.ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS");
                address.Address = addressinfo.ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS");
                address.Moo = addressinfo.ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS");
                address.Soi = addressinfo.ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS");
                address.Road = addressinfo.ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS");
                address.TumbolID = addressinfo.ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS");
                address.AmphurID = addressinfo.ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS");
                address.ProvinceID = addressinfo.ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS");
                address.Tumbol = addressinfo.ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT");
                address.Amphur = addressinfo.ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT");
                address.Province = addressinfo.ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT");
                address.PostCode = addressinfo.ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS");
            }
            else
            {
                var addressinfo = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION");

                address.Floor = addressinfo.ThenGetStringData("ADDRESS_FLOOR_CITIZEN_ADDRESS");
                address.Building = addressinfo.ThenGetStringData("ADDRESS_BUILDING_CITIZEN_ADDRESS");
                address.RoomNo = addressinfo.ThenGetStringData("ADDRESS_ROOMNO_CITIZEN_ADDRESS");
                address.Address = addressinfo.ThenGetStringData("ADDRESS_CITIZEN_ADDRESS");
                address.Moo = addressinfo.ThenGetStringData("ADDRESS_MOO_CITIZEN_ADDRESS");
                address.Soi = addressinfo.ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS");
                address.Road = addressinfo.ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS");
                address.TumbolID = addressinfo.ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS");
                address.AmphurID = addressinfo.ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS");
                address.ProvinceID = addressinfo.ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS");
                address.Tumbol = addressinfo.ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT");
                address.Amphur = addressinfo.ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT");
                address.Province = addressinfo.ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT");
                address.PostCode = addressinfo.ThenGetStringData("ADDRESS_POSTCODE_CITIZEN_ADDRESS");
            }

            if (address.ProvinceID == "10" && address.AmphurID == "09" && address.TumbolID == "06")
            {
                // แก้พระโขนงใต้ id ไม่ตรงกับทากรมบัญชีกลาง
                address.TumbolID = "10";
            }

            return address;
        }

        public OrganizationInfomation GetOrganizationInfomation(string OrgCode)
        {
            var info = new OrganizationInfomation();
            var org = DB.OrganizationTranslations.Where(e => e.OrgCode == OrgCode).ToList();
            var th = org.Where(e => e.LanguageID == 1).FirstOrDefault();
            var en = org.Where(e => e.LanguageID == 2).FirstOrDefault();

            if (th != null)
            {
                info.OrgNameTH = th.OrgName;
                info.OrgAddressTH = th.Address;
            }

            if (en != null)
            {
                info.OrgNameEN = en.OrgName;
                info.OrgAddressEN = en.Address;
            }

            return info;
        }

        public void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }

        public string CurrentCulture { get { return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName; } }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        protected void CaptureError(Exception ex, bool throwEventAfterCaptured = false)
        {
            Task.Run(() =>
            {
                try
                {
                    RavenClient.Capture(new SentryEvent(ex));
                }
                catch (Exception sentryEx)
                {

                }
            });

            if (throwEventAfterCaptured)
            {
                throw ex;
            }
        }

        protected void CaptureError(Exception ex, bool throwEventAfterCaptured = false, Dictionary<string, object> additionalData = null)
        {
            try
            {
                if (additionalData != null)
                {
                    foreach (var item in additionalData)
                    {
                        if (ex.Data[item.Key] != null)
                            ex.Data[item.Key] = item.Value;
                        else
                            ex.Data.Add(item.Key, item.Value);
                    }
                }
            }
            catch (Exception e) { }

            CaptureError(ex, throwEventAfterCaptured);
        }

        protected void CaptureError(Exception ex, bool throwEventAfterCaptured = false, Dictionary<string, object> additionalData = null, ApplicationRequestViewModel model = null)
        {
            try
            {
                if (additionalData == null)
                {
                    additionalData = new Dictionary<string, object>();
                }

                if (model != null)
                {
                    ApplicationRequestViewModelSerializable serializable = new ApplicationRequestViewModelSerializable();
                    TypeAdapter.Adapt(model, serializable);

                    string key = "Model";
                    if (additionalData.ContainsKey(key))
                        additionalData[key] = serializable;
                    else
                        additionalData.Add(key, serializable);
                }
            }
            catch (Exception e) { }

            CaptureError(ex, throwEventAfterCaptured, additionalData);
        }
    }

    public class OrganizationInfomation
    {
        public string OrgNameTH { get; set; }

        public string OrgNameEN { get; set; }

        public string OrgAddressTH { get; set; }

        public string OrgAddressEN { get; set; }

        public string OrgPhoneNumber { get; set; }
    }
}
