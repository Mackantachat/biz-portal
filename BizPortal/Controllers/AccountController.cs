using BizPortal.DAL.Migrations;
using BizPortal.DAL.MongoDB;
using BizPortal.Hubs;
using BizPortal.Integrated;
using BizPortal.Models;
using BizPortal.SeedPermit.APP_ORGANIC_NEW_SECTION_GROUP;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using EGA.Owin.Security.EGAOAuth;
using EGA.Owin.Security.EGAOAuth.Models;
using EGA.Owin.Security.EGAOpenID;
using EGA.Owin.Security.Utils.Extensions;
using Flurl.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Controllers
{
    [Authorize]
    public class AccountController : ControllerBase
    {
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private string NDIDUrl;

        public AccountController()
        {
            NDIDUrl = ConfigurationManager.AppSettings["NDIDUrl"];
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            NDIDUrl = ConfigurationManager.AppSettings["NDIDUrl"];
        }

        #region[OpenID]

        [Authorize]
        public async Task<ActionResult> InitUserData(string returnUrl, int status = 0)
        {
            if (status == 1 || status == 2)
            {
                var authToken = User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.AuthToken);

                if (!string.IsNullOrEmpty(authToken))
                {
                    var service = User.Identity.CreateOpenIDServiceInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"], bool.Parse(ConfigurationManager.AppSettings["TestMode"]));
                    var info = service.GetUser(User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.AuthToken));
                    if (info != null)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        if (info.Email != null)
                        {
                            user.Email = info.Email.StringValue;
                            user.EmailConfirmed = info.Email.VerifiedLevel.StartsWith("VerifiedLevel");
                            User.Identity.UpdateClaimValue(ClaimTypes.Email, user.Email);
                            await UserManager.UpdateAsync(user);
                        }
                    }
                }
            }

            string redirect = string.Empty;
            if (status == 1)
                redirect = Url.Action("Index", "Home", new { showMsg = ShowMsgType.ProfileUpdated.GetEnumStringValue(), returnUrl = returnUrl });
            else if (status == 0)
                redirect = Url.Action("Index", "Home", new { showMsg = ShowMsgType.FailedToUpdate.GetEnumStringValue(), returnUrl = returnUrl });
            else
            {
                if (!string.IsNullOrEmpty(returnUrl))
                    redirect = returnUrl;
                else
                    redirect = Url.Action("Index", "Home");
            }

            return Redirect(redirect);
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "", string authType = "EGA OpenID")
        {
            //ViewBag.ReturnUrl = returnUrl;
            //return View();

            // Request a redirect to the external login provider
            return new ChallengeResult(authType, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl, language = CurrentCulture }));
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return Redirect(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            /*
             * Try to fix the error, External Login Info is null after callback
             * Google Keywords: GetExternalLoginInfoAsync returns null sometime
             * URL: https://stackoverflow.com/a/36903338
             */
            ControllerContext.HttpContext.Session.RemoveAll();

            if (provider.Contains("NDID"))
            {
                return RedirectToAction("NDIDLogin", "Account", new { ReturnUrl = returnUrl });
            }
            else
            {
                // Request a redirect to the external login provider
                return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
            }
        }

        [Authorize]
        public ActionResult OAuth(string returnUrl)
        {
            var requestParameters = TempData["RequestParameters"] as Dictionary<string, string>;
            var redirectParameters = TempData["RedirectParameters"] as Dictionary<string, string>;

            return new EGAOAuthChallengeResult(Url.Action("OAuthCallback", "Account", new { ReturnUrl = returnUrl }), requestParameters, redirectParameters);
        }

        [Authorize]
        public async Task<ActionResult> OAuthCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();

            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            string oAuthData = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOAuthAuthenticationClaimType.OAuthDataXml);
            string AccessToken = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOAuthAuthenticationClaimType.AccessToken);
            User.Identity.AddClaimValue(EGAOAuthAuthenticationClaimType.OAuthDataXml, oAuthData);
            User.Identity.AddClaimValue(EGAOAuthAuthenticationClaimType.AccessToken, AccessToken);

            return Redirect(returnUrl);
        }

        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl, string token = "")
        {
            //var user = await UserManager.FindByNameAsync("egatest");
            //await SignInManager.SignInAsync(user, true, true);  
            //return UserLogInRedirect(user, returnUrl);

            ExternalLoginInfo loginInfo = null;
            AuthenticationManager.SignOut();

            if (!string.IsNullOrEmpty(token))
            {
                loginInfo = await NDIDGetExternalLoginInfo(token);
            }
            else
            {
                loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            }

            if (loginInfo == null)
            {
                notifyMsg(NotifyMsgType.Error, "ไม่สามารถเข้าสู่ระบบได้กรุณาลองใหม่อีกครั้ง");

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                if (loginInfo.Login.LoginProvider == UserProviderEnum.NDID.GetEnumStringValue())
                {

                    var citizenId = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.CitizenID);
                    var userId = DB.Users.Where(e => e.CitizenID == citizenId).Select(e => e.Id).FirstOrDefault();
                    if (userId != null)
                    {
                        await UserManager.AddLoginAsync(userId, new UserLoginInfo("NDID Citizen", citizenId));
                    }
                }
            }

            var loginProvider = loginInfo.Login.LoginProvider; // Login passed from what provider.
            var userType = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.UserType);

            if (userType == UserTypeEnum.Citizen.GetEnumStringValue())
            {
                if (loginProvider == UserProviderEnum.NDID.GetEnumStringValue())
                {
                    loginInfo.Login.LoginProvider = "NDID Citizen";
                }
                else
                {
                    loginInfo.Login.LoginProvider = "EGA OpenID Citizen";
                }
            }
            else
            {
                loginInfo.Login.LoginProvider = "EGA OpenID";
            }

            string identity = string.Empty;
            if (userType == UserTypeEnum.Citizen.GetEnumStringValue())
            {
                identity = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.CitizenID);
            }
            else if (userType == UserTypeEnum.Juristic.GetEnumStringValue())
            {
                identity = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.JuristicID);
            }
            else if (userType == UserTypeEnum.GovernmentAgent.GetEnumStringValue())
            {
                identity = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.CitizenID);
            }
            else if (userType == UserTypeEnum.Foreigner.GetEnumStringValue())
            {
                identity = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.PassportID);
            }

            if (string.IsNullOrEmpty(identity) || identity == "-")
            {
                notifyMsg(NotifyMsgType.Warning, "กรุณายืนยันข้อมูลส่วนบุคคลก่อนเข้าใช้งานระบบ");
                return RedirectToAction("LogOff", new { returnUrl = Url.BizAction("Index", "Home", new { area = "", showMsg = ShowMsgType.NeedValidate.GetEnumStringValue() }), provider = (loginInfo == null ? "" : loginInfo.Login.LoginProvider) });
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = UserManager.Find(loginInfo.Login);

            if (user == null)
            {
                user = UserManager.FindByName(loginInfo.DefaultUserName);

                // ลบ login ที่มี providerkey ไม่ตรงกับ openid
                if (user != null)
                {
                    var logins = UserManager.GetLogins(user.Id).Where(e => e.LoginProvider == loginProvider).ToList();
                    foreach (var login in logins)
                    {
                        UserManager.RemoveLogin(user.Id, login);
                    }

                    UserManager.AddLogin(user.Id, loginInfo.Login);
                }
            }
            else
            {
                user.PhoneNumber = loginInfo.ExternalIdentity.GetClaimValueOrDefault(WellKnownAttributes.Contact.Phone.Mobile);
                user.Email = loginInfo.ExternalIdentity.GetClaimValueOrDefault(WellKnownAttributes.Contact.Email);
                user.LastestLoginProvider = loginProvider;
                SetExternalLoginInfo(user, loginInfo, false);
                UpdateUnlinkedJuristicRequestStatus(user.Id, user.JuristicID);
            }

            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return UserLogInRedirect(user, returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:

                    try
                    {
                        new System.Net.Mail.MailAddress(loginInfo.Email);
                    }
                    catch
                    {
                        loginInfo.Email = null;
                    }

                    var newUser = new ApplicationUser { UserName = loginInfo.DefaultUserName, Email = loginInfo.Email, LastestLoginProvider = loginProvider };
                    var createResult = await UserManager.CreateAsync(newUser);

                    if (createResult.Succeeded)
                    {
                        createResult = await UserManager.AddLoginAsync(newUser.Id, loginInfo.Login);
                        if (createResult.Succeeded)
                        {
                            SetExternalLoginInfo(newUser, loginInfo, true);
                            UpdateUnlinkedJuristicRequestStatus(newUser.Id, newUser.JuristicID);
                            await SignInManager.SignInAsync(newUser, isPersistent: false, rememberBrowser: false);

                            return UserLogInRedirect(newUser, returnUrl);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(returnUrl))
                            {
                                return RedirectToAction("LogOff", new { returnUrl = returnUrl });
                            }
                            else
                            {
                                return RedirectToAction("LogOff", new { returnUrl = Url.BizAction("Index", "Home", new { area = "", showMsg = ShowMsgType.InvalidAccount.GetEnumStringValue() }) });
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return RedirectToAction("LogOff", new { returnUrl = returnUrl });
                        }
                        else
                        {
                            return RedirectToAction("LogOff", new { returnUrl = Url.BizAction("Index", "Home", new { showMsg = ShowMsgType.InvalidAccount.GetEnumStringValue() }) });
                        }
                    }
            }
        }

        // POST: /Account/LogOff
        [AllowAnonymous]
        public ActionResult LogOff(string returnUrl = null, string provider = null)
        {
            var providerDomain = string.Empty;
            var redirect = string.Empty;

            var userId = User.Identity.GetUserId();
            var claimUser = User as ClaimsPrincipal;

            if (userId != null && claimUser != null) 
            {
                foreach (var claim in claimUser.Claims)
                {
                    UserManager.RemoveClaim(userId, claim);
                }
            }


            AuthenticationManager.SignOut();

            if (User.Identity.IsAuthenticated)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());

                if (!string.IsNullOrEmpty(user.LastestLoginProvider))
                {
                    if (user.LastestLoginProvider == "EGA OpenID Citizen")
                    {
                        providerDomain = ConfigurationManager.AppSettings["CitizenOpenIDUrl"] + "Account/LogOff?returnUrl=";
                    }
                    else if (user.LastestLoginProvider == "EGA OpenID")
                    {
                        providerDomain = ConfigurationManager.AppSettings["OpenIDUrl"] + "Account/LogOff?returnUrl=";
                    }
                }

                if (string.IsNullOrEmpty(providerDomain))
                {
                    if (IdentityType == UserTypeEnum.Citizen)
                    {
                        providerDomain = ConfigurationManager.AppSettings["CitizenOpenIDUrl"] + "Account/LogOff?returnUrl=";
                    }
                    else
                    {
                        providerDomain = ConfigurationManager.AppSettings["OpenIDUrl"] + "Account/LogOff?returnUrl=";
                    }
                }
            }
            else if (!string.IsNullOrEmpty(provider))
            {
                if (!string.IsNullOrEmpty(provider))
                {
                    if (provider == "EGA OpenID Citizen")
                    {
                        providerDomain = ConfigurationManager.AppSettings["CitizenOpenIDUrl"] + "Account/LogOff?returnUrl=";
                    }
                    else if (provider == "EGA OpenID")
                    {
                        providerDomain = ConfigurationManager.AppSettings["OpenIDUrl"] + "Account/LogOff?returnUrl=";
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }


            if (!string.IsNullOrEmpty(returnUrl))
            {
                redirect = string.Format("{0}{1}", providerDomain, ServerHelper.ResolveServerUrl(returnUrl));
            }
            else
            {
                redirect = string.Format("{0}{1}", providerDomain, Url.AbsoluteAction("Index", "Home"));
            }

            if (!string.IsNullOrEmpty(redirect))
            {
                return Redirect(redirect);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        public ActionResult UnAuthorized(string returnUrl = null)
        {
            return Redirect(Url.BizAction("Index", "Home", new { showMsg = ShowMsgType.Unauthorized }));
        }

        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private void UpdateUnlinkedJuristicRequestStatus(string userId, string juristicID)
        {
            var requests = DB.JuristicApplicationStatusRequests.Where(o => o.JuristicID == juristicID && o.NotLinkedToJuristicUser).ToList();
            foreach (var request in requests)
            {
                request.NotLinkedToJuristicUser = false;
                request.CreatedUserID = userId;
            }

            // ดึงข้อมูลที่พักไว้ใน LabourRegisStatus มาบันทึกเข้ารายการร้องขอ
            List<LabourRegisStatus> labours = DB.LabourRegisStatus.Where(o => o.JuristicID == juristicID).ToList();
            foreach (var l in labours)
            {
                JuristicApplicationStatusRequestSubmitViewModel request = new JuristicApplicationStatusRequestSubmitViewModel()
                {
                    JuristicID = l.JuristicID,
                    FileIDs = new List<BizPortal.Utils.FileMetadata>(),
                    ApplicationID = l.ApplicationID,
                    Remark = l.RefCode,
                    CustomAppStatusID = l.Status ? (int)ApplicationStatusEnum.COMPLETED : (int)ApplicationStatusEnum.REJECTED,
                    IsSubmit = true,
                    DisableSendingEmail = true
                };
                request.Save();
            }
            DB.LabourRegisStatus.RemoveRange(labours);
            DB.SaveChanges();
        }

        private void SetExternalLoginInfo(ApplicationUser user, ExternalLoginInfo loginInfo, bool isNewUser)
        {
            var claims = SetUserClaim(loginInfo);

            user.FullName = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.FullName);
            user.Prefix = loginInfo.ExternalIdentity.GetClaimValueOrDefault(WellKnownAttributes.Name.Prefix);
            user.Firstname = loginInfo.ExternalIdentity.GetClaimValueOrDefault(WellKnownAttributes.Name.First);
            user.Lastname = loginInfo.ExternalIdentity.GetClaimValueOrDefault(WellKnownAttributes.Name.Last);
            user.AuthToken = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.AuthToken);

            foreach (var claim in claims)
            {
                user.Claims.Add(claim);
            }

            if (isNewUser)
            {
                user.UserType = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.UserType);

                if (user.UserType != "GovernmentAgent")
                {
                    if (UserManager.IsInRole(user.Id, ConfigurationValues.ROLES_ORG_AGENT_NAME))
                    {
                        UserManager.RemoveFromRole(user.Id, ConfigurationValues.ROLES_ORG_AGENT_NAME);
                    }
                }
            }

            switch (user.UserType)
            {
                case "Citizen":
                    user.CitizenID = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.CitizenID);
                    break;
                case "JuristicPerson":
                    user.JuristicID = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.JuristicID);
                    break;
                case "Foreigner":
                    user.PassportID = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.PassportID);
                    break;
                case "GovernmentAgent":
                    user.CitizenID = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.CitizenID);

                    if (isNewUser)
                    {
                        if (!UserManager.IsInRole(user.Id, ConfigurationValues.ROLES_ORG_AGENT_NAME))
                        {
                            UserManager.AddToRole(user.Id, ConfigurationValues.ROLES_ORG_AGENT_NAME);
                        }

                        string orgCode = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.OrganizationID);

                        if (string.IsNullOrEmpty(user.OrgCode))
                        {
                            user.OrgCode = orgCode;
                            if (!string.IsNullOrEmpty(orgCode))
                            {
                                if (!DB.Organizations.Where(o => o.OrgCode == orgCode).Any())
                                {
                                    //EGovServices svc = new EGovServices(ConfigurationManager.AppSettings["EgovServiceUrl"]);
                                    EGovServices svc = new EGovServices();
                                    Integrated.Models.Organization egovOrg = svc.GetOrganizationByOrgCode(orgCode, "th-TH");
                                    if (egovOrg != null)
                                    {
                                        Organization org = new Organization()
                                        {
                                            OrgCode = egovOrg.Code,
                                            OrgSysName = egovOrg.Name,
                                            MinistryCode = egovOrg.MinistryCode,
                                            DepartmentCode = egovOrg.DepartmentCode,
                                            DivisionCode = egovOrg.DivisionCode,
                                            LogoUrl = egovOrg.LogoURL,
                                            Url = egovOrg.Url,
                                            OrganizationTranslations = new HashSet<OrganizationTranslation>()
                                        };
                                        DB.Organizations.Add(org);
                                        OrganizationTranslation orgTH = new OrganizationTranslation()
                                        {
                                            LanguageID = DB.Languages.Where(o => o.TwoLetterISOLanguageName == "th").Select(o => o.LanguageID).Single(),
                                            OrgName = egovOrg.Name,
                                            Address = egovOrg.Address,
                                            Abbreviation = egovOrg.Abbreviation
                                        };
                                        org.OrganizationTranslations.Add(orgTH);

                                        egovOrg = svc.GetOrganizationByOrgCode(orgCode, "en-US");
                                        if (egovOrg != null)
                                        {
                                            OrganizationTranslation orgEN = new OrganizationTranslation()
                                            {
                                                LanguageID = DB.Languages.Where(o => o.TwoLetterISOLanguageName == "en").Select(o => o.LanguageID).Single(),
                                                OrgName = egovOrg.Name,
                                                Address = egovOrg.Address,
                                                Abbreviation = egovOrg.Abbreviation
                                            };
                                            org.OrganizationTranslations.Add(orgEN);
                                        }

                                        DB.SaveChanges();

                                        user.OrgCode = orgCode;
                                    }
                                }
                            }
                        }
                    }

                    break;
            }

            UserManager.Update(user);
        }

        private List<IdentityUserClaim> SetUserClaim(ExternalLoginInfo loginInfo)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["DGA_WS_URL"]);
            var request = new RestRequest(ConfigurationManager.AppSettings["OpenIDProfileUrl"], Method.POST);
            var claims = new List<IdentityUserClaim>();

            // set external callback claims
            //foreach (var claim in loginInfo.ExternalIdentity.Claims)
            //{
            //    claims.Add(new IdentityUserClaim { ClaimType = claim.Type, ClaimValue = claim.Value });
            //}

            // set profile service claims
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Consumer-Key", ConfigurationManager.AppSettings["ConsumerKey"]);
            request.AddHeader("Token", Api.AccessToken);
            request.AddJsonBody(new { personalToken = loginInfo.ExternalIdentity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.AuthToken) });

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var obj = JObject.Parse(response.Content);

                claims.Add(new IdentityUserClaim 
                {
                    ClaimType = OpenIdAttributeExchangeType.LoginProvider, ClaimValue = loginInfo.Login.LoginProvider 
                });

                claims.Add(new IdentityUserClaim
                {
                    ClaimType = OpenIdAttributeExchangeType.LoginProviderId,
                    ClaimValue = (obj["Result"]["UserId"] == null || obj["Result"]["UserId"].ToString() == "") ? "" : obj["Result"]["UserId"].ToString()
                });

                claims.Add(new IdentityUserClaim
                {
                    ClaimType = OpenIdAttributeExchangeType.LoginProviderUserName,
                    ClaimValue = (obj["Result"]["UserName"] == null || obj["Result"]["UserName"].ToString() == "") ? "" : obj["Result"]["UserName"].ToString()
                });

                claims.Add(new IdentityUserClaim
                {
                    ClaimType = OpenIdAttributeExchangeType.BirthDate,
                    ClaimValue = (obj["Result"]["DateOfBirth"] == null || obj["Result"]["DateOfBirth"].ToString() == "") ? "" : obj["Result"]["DateOfBirth"]["Value"].ToString()
                });

                claims.Add(new IdentityUserClaim
                {
                    ClaimType = OpenIdAttributeExchangeType.HouseNumber,
                    ClaimValue = (obj["Result"]["HouseNumber"] == null || obj["Result"]["HouseNumber"].ToString() == "") ? "" : obj["Result"]["HouseNumber"]["Value"].ToString()
                });

                claims.Add(new IdentityUserClaim
                {
                    ClaimType = OpenIdAttributeExchangeType.VillageName,
                    ClaimValue = (obj["Result"]["VillageName"] == null || obj["Result"]["VillageName"].ToString() == "") ? "" : obj["Result"]["VillageName"]["Value"].ToString()
                });

                claims.Add(new IdentityUserClaim
                {
                    ClaimType = OpenIdAttributeExchangeType.Moo,
                    ClaimValue = (obj["Result"]["Moo"] == null || obj["Result"]["Moo"].ToString() == "") ? "" : obj["Result"]["Moo"]["Value"].ToString()
                });

                claims.Add(new IdentityUserClaim
                {
                    ClaimType = OpenIdAttributeExchangeType.Road,
                    ClaimValue = (obj["Result"]["Road"] == null || obj["Result"]["Road"].ToString() == "") ? "" : obj["Result"]["Road"]["Value"].ToString()
                });

                claims.Add(new IdentityUserClaim
                {
                    ClaimType = OpenIdAttributeExchangeType.Soi,
                    ClaimValue = (obj["Result"]["Soi"] == null || obj["Result"]["Soi"].ToString() == "") ? "" : obj["Result"]["Soi"]["Value"].ToString()
                });

                claims.Add(new IdentityUserClaim
                {
                    ClaimType = OpenIdAttributeExchangeType.SubDistrict,
                    ClaimValue = (obj["Result"]["SubDistrict"] == null || obj["Result"]["SubDistrict"].ToString() == "") ? "" : obj["Result"]["SubDistrict"]["Value"].ToString()
                });

                claims.Add(new IdentityUserClaim
                {
                    ClaimType = OpenIdAttributeExchangeType.District,
                    ClaimValue = (obj["Result"]["District"] == null || obj["Result"]["District"].ToString() == "") ? "" : obj["Result"]["District"]["Value"].ToString()
                });

                claims.Add(new IdentityUserClaim
                {
                    ClaimType = OpenIdAttributeExchangeType.Province,
                    ClaimValue = (obj["Result"]["Province"] == null || obj["Result"]["Province"].ToString() == "") ? "" : obj["Result"]["Province"]["Value"].ToString()
                });

                claims.Add(new IdentityUserClaim
                {
                    ClaimType = OpenIdAttributeExchangeType.PostCode,
                    ClaimValue = (obj["Result"]["PostCode"] == null || obj["Result"]["PostCode"].ToString() == "") ? "" : obj["Result"]["PostCode"].ToString()
                });

                claims.Add(new IdentityUserClaim
                {
                    ClaimType = OpenIdAttributeExchangeType.GeoCode,
                    ClaimValue = (obj["Result"]["GeoCode"] == null || obj["Result"]["GeoCode"].ToString() == "") ? "" : obj["Result"]["GeoCode"]["Value"].ToString()
                });
            }

            return claims;
        }

        private ActionResult UserLogInRedirect(ApplicationUser user, string returnUrl)
        {
            if (user.UserType == UserTypeEnum.GovernmentAgent.GetEnumStringValue() || user.Roles.Count > 0)
            {
                var modulatorRoleId = DB.Roles.Where(e => e.Name == ConfigurationValues.ROLES_MODULATOR_NAME).Select(e => e.Id).FirstOrDefault();
                var signerRoleId = DB.Roles.Where(e => e.Name == ConfigurationValues.ROLES_DOCUMENT_SIGNER_NAME).Select(e => e.Id).FirstOrDefault();

                if (user.Roles.Any(e => e.RoleId == modulatorRoleId) && user.Roles.Count == 1)
                {
                    return (ActionResult)Redirect(Url.ServiceAction("Index", "Article", new { area = "Manage", language = CurrentCulture }));
                }
                else if (user.Roles.Any(e => e.RoleId == signerRoleId) && user.Roles.Count == 1)
                {
                    return (ActionResult)Redirect(Url.BizAction("Index", "Signing", new { area = "Apps", language = CurrentCulture }));
                }
                else
                {
                    return (ActionResult)Redirect(Url.ServiceAction("Index", "ApplicationStatus", new { area = "Manage", language = CurrentCulture }));
                }
            }
            else if (user.UserType == UserTypeEnum.Citizen.GetEnumStringValue() || user.UserType == UserTypeEnum.Juristic.GetEnumStringValue())
            {
                if (!string.IsNullOrEmpty(returnUrl) && (returnUrl.ToLower().Contains("apps/singleform") || returnUrl.ToLower().Contains("track/receipt") || returnUrl.ToLower().Contains("business") || returnUrl.ToLower().Contains("track/detail")))
                {
                    return (ActionResult)Redirect(returnUrl);
                }
                else
                {
                    return (ActionResult)Redirect(Url.BizAction("Dashboard", "Track", new { area = "", language = CurrentCulture }));
                }
            }
            else
            {
                return !string.IsNullOrEmpty(returnUrl) ? (ActionResult)Redirect(returnUrl) : (ActionResult)Redirect(Url.BizAction("Index", "Home", new { area = "", showMsg = ShowMsgType.InvalidAccount.GetEnumStringValue(), language = CurrentCulture }));
            }
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion

        #region[NDID]

        /// <summary>
        ///  TODO: NDID
        ///  1. ส่งคำสั่งไป clean data ที่ได้จาก AS 
        ///  2. ส่งคำสั่งไป closed request
        ///  3. คุม state การ login
        /// </summary>

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> NDIDLogin(string returnUrl)
        {
            var availableProvider = string.IsNullOrEmpty(ConfigurationManager.AppSettings["NDIDProvider"]) ? null : ConfigurationManager.AppSettings["NDIDProvider"].Split(',').ToList();

            ViewBag.IDP = await NDIDGetIDP(availableProvider);
            ViewBag.ReturnUrl = returnUrl;
            return View("NDIDLogin");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> NDIDLogin(string connectionId, string identityId, string provider, string providerCode, string returnUrl)
        {
            // ยิง req ไปหา NDID Rp Api
            var referenceId = Guid.NewGuid().ToString();
            var identityNamespace = NDIDIdentityNamespace.CitizenId.GetEnumStringValue();
            var mode = (int)NDIDRequestStatusMode.Normal;
            var serviceIds = ConfigurationManager.AppSettings["NDIDServiceID"].Split(',').ToList();
            var minIdp = int.Parse(ConfigurationManager.AppSettings["NDIDMinIDP"]);
            var minAs = int.Parse(ConfigurationManager.AppSettings["NDIDMinAS"]);
            var minIAL = float.Parse(ConfigurationManager.AppSettings["NDIDMinIAL"]);
            var minAAL = float.Parse(ConfigurationManager.AppSettings["NDIDMinAAL"]);
            var timeout = int.Parse(ConfigurationManager.AppSettings["NDIDTimeout"]);
            var callbackUrl = string.IsNullOrEmpty(ConfigurationManager.AppSettings["NDIDCallbackUrl"]) ? Url.Action("NDIDLoginCallback", "Account", null, Request.Url.Scheme) : ConfigurationManager.AppSettings["NDIDCallbackUrl"] + "/" + CurrentCulture + "/Account/NDIDLoginCallback";
            var requestList = await NDIDGetAS(serviceIds, providerCode.Split(',').ToList(), minAs, minIAL, minAAL);
            var message = string.Format(ConfigurationManager.AppSettings["NDIDRequestMessage"], referenceId);

            if (requestList != null && referenceId.Count() > 1)
            {
                var response = await NDIDRequestIdentity(connectionId, identityId, identityNamespace, provider, referenceId, mode, minIdp, minIAL, minAAL, requestList, message, callbackUrl, returnUrl, timeout);

                if (response != null && !string.IsNullOrEmpty(response.request_id))
                {
                    response.reference_id = referenceId;
                    return Json(new { Status = true, Message = "Successfully", Result = response }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Status = false, Message = "Failed", Result = response }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Status = false, Message = "Failed : Not found availabel AS" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> NDIDLoginCallback(string connectionId, string returnUrl)
        {
            var postData = "";
            var callbackData = NDIDGetCallbackData(Request, out postData);

            if (callbackData != null && callbackData.IsResponse)
            {
                var hub = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NDIDHub>();
                var requestStatus = NDIDGetRequestStatus(callbackData.request_id, callbackData.status, returnUrl, callbackData.closed, callbackData.timed_out, out string status, out string message);
                hub.Clients.Client(connectionId).AuthenCallback(new { Status = status, Message = message, Result = requestStatus });

                NDIDLoginTransaction.Add(callbackData.request_id, NDIDTransactionType.RPCallback, "rp_callback_" + status, message, postData);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> NDIDCheckRequestStatus(string connectionId, string requestId, string returnUrl)
        {
            var response = await NDIDCheckRequestIdentityStatus(requestId);

            if (response != null)
            {
                var hub = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NDIDHub>();
                var requestStatus = NDIDGetRequestStatus(response.request_id, response.status, returnUrl, response.closed, response.timed_out, out string status, out string message);
                hub.Clients.Client(connectionId).AuthenCallback(new { Status = status, Message = message, Result = requestStatus });
            }

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        private async Task<NDIDRequestIdentityResponse> NDIDRequestIdentity(string connectionId, string identityId, string identityNamespace, string provider, string referenceId, int mode, int minIdp, float minIAL, float minAAL, List<NDIDRequestList> requestList, string message, string callBackUrl, string returnUrl, int requestTimeOut = 600)
        {
            var idps = provider.Split(',').ToList();
            var requestMessage = message;
            var callbackUrl = string.Format("{0}?connectionId={1}&returnUrl={2}", callBackUrl, connectionId, returnUrl);
            var postUrl = string.Format("{0}/rp/requests/{1}/{2}", NDIDUrl, identityNamespace, identityId);
            var postData = new NDIDRequestIdentity
            {
                mode = mode,
                reference_id = referenceId,
                idp_id_list = idps,
                callback_url = callbackUrl,
                request_message = requestMessage,
                min_ial = minIAL,
                min_aal = minAAL,
                min_idp = minIdp,
                request_timeout = requestTimeOut,
                data_request_list = requestList
            };

            try
            {
                var response = await postUrl.PostJsonAsync(postData).ReceiveJson<NDIDRequestIdentityResponse>();
                NDIDLoginTransaction.Add(identityId, referenceId, response.request_id, "re_request_success", "Successfully", JsonConvert.SerializeObject(postData));
                return response;
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync();
                NDIDLoginTransaction.Add(identityId, referenceId, null, "re_request_error", ex.ToString(), JsonConvert.SerializeObject(error));
            }
            catch (Exception ex)
            {
                NDIDLoginTransaction.Add(identityId, referenceId, null, "re_request_error", ex.Message, ex.ToString());
            }

            return null;
        }

        private async Task<NDIDIdentity> NDIDGetIdentity(string requestId)
        {
            try
            {
                var response = await (string.Format("{0}/rp/requests/data/{1}", NDIDUrl, requestId)).GetJsonAsync<List<NDIDGetData>>();

                if (response != null)
                {
                    // clean data หลังเรียกข้อมูลมาใช้แล้ว
                    if (bool.Parse(ConfigurationManager.AppSettings["NDIDIsCleanRequestData"]))
                    {
                        var nodeId = ConfigurationManager.AppSettings["NDIDNodeId"];
                        NDIDCleanIdentity(requestId, nodeId);
                    }

                    NDIDLoginTransaction.Add(requestId, NDIDTransactionType.RPGetData, "rp_getdata_success", "Sucessfully", JsonConvert.SerializeObject(response));

                    if (response.Where(e => e.service_id == NDIDServiceId.IdentityInfo.GetEnumStringValue()).Any())
                    {
                        var identityInfo = response.Where(e => e.service_id == NDIDServiceId.IdentityInfo.GetEnumStringValue()).Select(e => e.data).FirstOrDefault();
                        return JsonConvert.DeserializeObject<NDIDIdentity>(identityInfo);
                    }
                    else
                    {
                        var identityInfoBasicData = response.Where(e => e.service_id == NDIDServiceId.IdentityInfoBasic.GetEnumStringValue()).Select(e => e.data).FirstOrDefault();
                        var identityContactData = response.Where(e => e.service_id == NDIDServiceId.IdentityContact.GetEnumStringValue()).Select(e => e.data).FirstOrDefault();
                        var identityInfo = JsonConvert.DeserializeObject<NDIDIdentity>(identityInfoBasicData);
                        var identityContact = JsonConvert.DeserializeObject<NDIDIdentityContact>(identityContactData);

                        identityInfo.customer_contact = identityContact.customer_contact;
                        return identityInfo;
                    }
                }
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync();
                NDIDLoginTransaction.Add(requestId, NDIDTransactionType.RPGetData, "rp_getdata_error", ex.ToString(), JsonConvert.SerializeObject(error));
            }
            catch (Exception ex)
            {
                NDIDLoginTransaction.Add(requestId, NDIDTransactionType.RPGetData, "rp_getdata_error", ex.Message, ex.ToString());
            }

            return null;
        }

        private async Task<List<NDIDIdp>> NDIDGetIDP(List<string> availableProvider)
        {
            try
            {
                var response = await (NDIDUrl + "/utility/idp").GetJsonAsync<List<NDIDIdpResponse>>();

                if (response != null)
                {
                    var idp = response.GroupBy(e => new { e.Detail.company_code, e.Detail.marketing_name_en, e.Detail.marketing_name_th })
                                 .Select(e => new NDIDIdp
                                 {
                                     NameEN = e.Key.marketing_name_en,
                                     NameTH = e.Key.marketing_name_th,
                                     Code = e.Key.company_code,
                                     NodeIds = e.Select(s => s.node_id).ToList(),
                                     MaxIAL = e.Select(s => s.max_ial).OrderByDescending(s => s).FirstOrDefault(),
                                     MaxAAL = e.Select(s => s.max_aal).OrderByDescending(s => s).FirstOrDefault(),
                                     ImageUrl = "/Content/Images/bankLogo/" + e.Key.marketing_name_en + ".png"
                                 })
                                .OrderBy(e => e.NameTH)
                                .ToList();

                    if (availableProvider != null && availableProvider.Count > 0)
                    {
                        var aidp = idp.Where(e => availableProvider.Contains(e.NameEN)).ToList();
                        var fordup = aidp.FirstOrDefault();
                        var dup = new NDIDIdp
                        {
                            NameEN = fordup.NameEN,
                            NameTH = fordup.NameTH,
                            Code = fordup.Code,
                            NodeIds = fordup.NodeIds,
                            MaxIAL = fordup.MaxIAL,
                            MaxAAL = fordup.MaxIAL,
                            ImageUrl = "/Content/Images/bankLogo/THAI ID2.png"
                        };

                        aidp.Add(dup);
                        return aidp;
                    }
                    else
                    {
                        return idp;
                    }
                }
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync();
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        private async Task<List<NDIDRequestList>> NDIDGetAS(List<string> serviceIds, List<string> providerCodes, int minAs, float minIAL, float minAAL)
        {
            var requestList = new List<NDIDRequestList>();

            try
            {
                foreach (var serviceId in serviceIds)
                {
                    var response = await (NDIDUrl + "/utility/as/" + serviceId).GetJsonAsync<List<NDIDAs>>();

                    if (response != null)
                    {
                        var asIdList = response.Where(e => e.Detail != null
                                                        && providerCodes.Contains(e.Detail.company_code)
                                                        && e.min_aal <= minAAL
                                                        && e.min_ial <= minIAL)
                                            .Select(e => e.node_id)
                                            .ToList();

                        if (asIdList.Count > 0)
                        {
                            requestList.Add(new NDIDRequestList
                            {
                                service_id = serviceId,
                                as_id_list = asIdList,
                                min_as = minAs,
                                request_params = ""
                            });
                        }
                    }
                }
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync();
            }
            catch (Exception ex)
            {

            }

            return requestList;
        }

        private async Task<ExternalLoginInfo> NDIDGetExternalLoginInfo(string requestId)
        {
            var identity = await NDIDGetIdentity(requestId);

            if (identity != null)
            {
                var loginInfo = new ExternalLoginInfo();
                var loginProviderKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(identity.identifier.card_number));

                loginInfo.ExternalIdentity = new ClaimsIdentity(
                                    DefaultAuthenticationTypes.ExternalCookie,
                                    ClaimsIdentity.DefaultNameClaimType,
                                    ClaimsIdentity.DefaultRoleClaimType);


                loginInfo.Login = new UserLoginInfo(UserProviderEnum.NDID.GetEnumStringValue(), loginProviderKey);
                loginInfo.DefaultUserName = identity.identifier.card_number;
                loginInfo.Email = identity.customer_contact.email_addr;
                loginInfo.ExternalIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, identity.identifier.card_number ?? ""));
                loginInfo.ExternalIdentity.AddClaim(new Claim(ClaimTypes.Name, identity.identifier.card_number ?? ""));
                loginInfo.ExternalIdentity.AddClaim(new Claim(ClaimTypes.Email, identity.customer_contact.email_addr ?? ""));

                loginInfo.ExternalIdentity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.UserType, UserTypeEnum.Citizen.GetEnumStringValue() ?? ""));
                loginInfo.ExternalIdentity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.CitizenID, identity.identifier.card_number ?? ""));
                loginInfo.ExternalIdentity.AddClaim(new Claim(OpenIdAttributeExchangeType.BirthDate, identity.birth_date ?? ""));

                // Thai Name
                loginInfo.ExternalIdentity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.FullName, identity.customer_info_th.thai_full_name ?? ""));
                loginInfo.ExternalIdentity.AddClaim(new Claim(WellKnownAttributes.Name.Prefix, identity.customer_info_th.thai_title ?? ""));
                loginInfo.ExternalIdentity.AddClaim(new Claim(WellKnownAttributes.Name.First, identity.customer_info_th.thai_first_name ?? ""));
                loginInfo.ExternalIdentity.AddClaim(new Claim(WellKnownAttributes.Name.Last, identity.customer_info_th.thai_last_name ?? ""));

                // Eng Name
                loginInfo.ExternalIdentity.AddClaim(new Claim(OpenIdAttributeExchangeType.FullNameEN, identity.customer_info_en.en_full_name ?? ""));
                loginInfo.ExternalIdentity.AddClaim(new Claim(OpenIdAttributeExchangeType.PrefixEN, identity.customer_info_en.en_title ?? ""));
                loginInfo.ExternalIdentity.AddClaim(new Claim(OpenIdAttributeExchangeType.FirstEN, identity.customer_info_en.en_first_name ?? ""));
                loginInfo.ExternalIdentity.AddClaim(new Claim(OpenIdAttributeExchangeType.LastEN, identity.customer_info_en.en_last_name ?? ""));

                //// Card Contact
                //loginInfo.ExternalIdentity.AddClaim(new Claim(OpenIdAttributeExchangeType.CardAdressFull, identity.customer_address_id_card.id_card_address_full ?? ""));
                //loginInfo.ExternalIdentity.AddClaim(new Claim(OpenIdAttributeExchangeType.CardAddressStreet1, identity.customer_address_id_card.id_card_street_address1 ?? ""));
                //loginInfo.ExternalIdentity.AddClaim(new Claim(OpenIdAttributeExchangeType.CardAddressStreet2, identity.customer_address_id_card.id_card_street_address2 ?? ""));
                //loginInfo.ExternalIdentity.AddClaim(new Claim(OpenIdAttributeExchangeType.CardAddressSubdistrict, identity.customer_address_id_card.id_card_address_subdistrict ?? ""));
                //loginInfo.ExternalIdentity.AddClaim(new Claim(OpenIdAttributeExchangeType.CardAddressDistrict, identity.customer_address_id_card.id_card_address_district ?? ""));
                //loginInfo.ExternalIdentity.AddClaim(new Claim(OpenIdAttributeExchangeType.CardAddressProvince, identity.customer_address_id_card.id_card_address_province ?? ""));
                //loginInfo.ExternalIdentity.AddClaim(new Claim(OpenIdAttributeExchangeType.CardAddressCountry, identity.customer_address_id_card.id_card_address_country ?? ""));
                //loginInfo.ExternalIdentity.AddClaim(new Claim(OpenIdAttributeExchangeType.CardAddressZipcode, identity.customer_address_id_card.id_card_address_zipcode ?? ""));

                loginInfo.ExternalIdentity.AddClaim(new Claim(OpenIdAttributeExchangeType.LoginProvider, UserProviderEnum.NDID.GetEnumStringValue() ?? ""));

                return loginInfo;
            }

            return null;
        }

        private async Task<NDIDCheckRequest> NDIDCheckRequestIdentityStatus(string requestId)
        {
            try
            {
                var response = await (NDIDUrl + "/utility/requests/" + requestId).GetJsonAsync<NDIDCheckRequest>();
                NDIDLoginTransaction.Add(requestId, NDIDTransactionType.RPCheckRequestStatus, "rp_check_request_succes", "Successfuly", JsonConvert.SerializeObject(response));
                return response;
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync();
                NDIDLoginTransaction.Add(requestId, NDIDTransactionType.RPCheckRequestStatus, "rp_check_request_error", ex.ToString(), JsonConvert.SerializeObject(error));
            }
            catch (Exception ex)
            {
                NDIDLoginTransaction.Add(requestId, NDIDTransactionType.RPCheckRequestStatus, "rp_check_request_error", ex.Message, ex.ToString());
            }

            return null;
        }

        private void NDIDCleanIdentity(string requestId, string nodeId = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(requestId))
                {
                    var postData = new { node_id = nodeId };
                    var response = (NDIDUrl + "/rp/requests/housekeeping/data/" + requestId).PostJsonAsync(postData);
                }
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync();
            }
            catch (Exception ex)
            {

            }
        }

        private void NDIDCloseRequestIdentity(string requestId, string referenceId, string callbackUrl, string nodeId = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(requestId))
                {
                    var postData = new
                    {
                        node_id = nodeId,
                        reference_id = referenceId,
                        request_id = requestId,
                        callback_url = callbackUrl
                    };

                    var response = (NDIDUrl + "/rp/requests/close").PostJsonAsync(postData);
                }
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync();
            }
            catch (Exception ex)
            {

            }
        }

        private object NDIDGetRequestStatus(string requestId, string status, string returnUrl, bool? closed, bool? timeout, out string requestStatus, out string message)
        {
            requestStatus = status;

            if (status == NDIDRequestStatus.Completed.GetEnumStringValue())
            {
                // complete
                message = "Successfully";
                return new { LoginUrl = Url.Action("ExternalLoginCallback", "Account", new { token = requestId }) };
            }
            else if (timeout == true || closed == true || status == NDIDRequestStatus.Rejected.GetEnumStringValue() || status == NDIDRequestStatus.Complicated.GetEnumStringValue())
            {
                // reject or time out or closed
                message = string.Format("Failed with Status: {0}, Closed: {1}, Timeout: {2}", status, closed, timeout);
                requestStatus = NDIDRequestStatus.Rejected.GetEnumStringValue();
            }
            else
            {
                // other
                message = string.Format("Status: {0}, Closed: {1}, Timeout: {2}", status, closed, timeout);
            }

            return null;
        }

        private NDIDCallBackData NDIDGetCallbackData(HttpRequestBase request, out string postData)
        {
            var req = Request.InputStream;
            req.Seek(0, SeekOrigin.Begin);
            postData = new StreamReader(req).ReadToEnd();

            if (!string.IsNullOrEmpty(postData))
            {
                return JsonConvert.DeserializeObject<NDIDCallBackData>(postData);
            }

            return null;
        }

        #endregion
    }
}