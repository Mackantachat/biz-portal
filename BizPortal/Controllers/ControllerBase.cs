using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using BizPortal.Utils.Annotations;
using BizPortal.Utils.Extensions;
using BizPortal.Models;
using BizPortal.DAL;
using Newtonsoft.Json;
using BizPortal.ViewModels;
using System.Security.Claims;
using EGA.Owin.Security.EGAOpenID;
using EGA.Owin.Security.Utils.Extensions;
using Newtonsoft.Json.Serialization;
using BizPortal.Utils.Helpers;
using System.Configuration;
using BizPortal.Integrated;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using EGA.WS;
using log4net;
using System.Globalization;
using System.Data.Entity;
using BizPortal.DAL.MongoDB;

namespace BizPortal.Controllers
{
    [Internationalization]
    public abstract class ControllerBase : Controller
    {
        private ApplicationDbContext _db;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private EGAWSAPI _api;
        private EGAWSAPI _apiDBD;


        protected readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
        public EGAWSAPI ApiDBD
        {
            get
            {
                if (_apiDBD == null)
                {
                    _apiDBD = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKeyDBDSecured"], ConfigurationManager.AppSettings["ConsumerSecretDBDSecured"]);
                }

                return _apiDBD;
            }
        }

        public ApplicationDbContext DB
        {
            get
            {
                if (_db == null)
                    _db = new ApplicationDbContext();
                return _db;// ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
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
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            protected set
            {
                _userManager = value;
            }
        }

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
                string userType = User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.UserType);
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

        public string CitizenBirthDate
        {
            get
            {
                DateTime date = DateTime.MinValue;
                return DateTime.TryParseExact(User.Identity.GetBirthDate(), "yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en"), DateTimeStyles.None, out date) ? date.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th")) : "";
            }
        }

        public string IdentityProvider
        {
            get
            {
                return User.Identity.GetClaimValueOrDefault(OpenIdAttributeExchangeType.LoginProvider);
            }
        }

        public string IdentityEmail => User.Identity.GetEmail();

#if DEBUG
        public static Dictionary<string, string> testUserOrgCode
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "5450700062022", "999000" },
                    { "1909800862188", "13011000" },
                    { "1103700961739", "999001" },
                    { "1429900214280", "999001" }, //OSS
                    { "3320700050053", "999005" },
                    { "3610600216046", "999006" },
                };
            }
        }
#endif

        public string OrganizationID
        {
            get
            {
                //#if DEBUG
                //                string orgCode = ConfigurationManager.AppSettings["TestOrgranizationID"].ToString();
                //                if (Convert.ToBoolean(ConfigurationManager.AppSettings["EnableTestOrgranizationID"]))
                //                {
                //                    if (testUserOrgCode.ContainsKey(User.Identity.Name))
                //                    {
                //                        orgCode = testUserOrgCode[User.Identity.Name];
                //                    }
                //                }
                //                else
                //                {
                //                    orgCode = User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.OrganizationID);
                //                }
                //                return orgCode;
                //#else
                return User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.OrganizationID);
                //#endif
            }
        }

        public string CurrentUserID
        {
            get
            {
                return User.Identity.GetUserId();
            }
        }

        public string CurrentCulture { get { return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName; } }

        public string GetBaseUrl()
        {
            var request = Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/") appUrl += "/";

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.CurrentLang = CurrentCulture;
            if (ViewBag.ActiveMenu == null)
                ViewBag.ActiveMenu = PageNameEnum.DASHBOARD.GetEnumStringValue();

            ViewBag.ApplyDomainRoute = bool.Parse(ConfigurationManager.AppSettings["ApplyDomainRoute"]);

            if (ViewBag.ApplyDomainRoute)
            {
                ViewBag.BizDomain = ConfigurationManager.AppSettings["BizDomain"];
                ViewBag.ServicesDomain = ConfigurationManager.AppSettings["ServicesDomain"];
            }

            if (User.Identity.IsAuthenticated && !User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME))
            {
                ViewBag.IdentityType = IdentityType;

                if (!filterContext.HttpContext.Request.IsAjaxRequest() && filterContext.HttpContext.Request.HttpMethod.ToLower() == "get")
                {
                    if (!filterContext.HttpContext.Request.Url.AbsolutePath.ToLower().Contains("/account/"))
                    {

                    }
                }
            }

            base.OnActionExecuted(filterContext);
        }

        public void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }

        public void notifyMsg(NotifyMsgType type, string msg, string returnUrl = null)
        {
            var cname = BizPortalSetting.ALERT_COOKIE;
            var cvalue = new
            {
                type = type.GetEnumStringValue(),
                msg = Uri.EscapeUriString(msg),
                returnUrl = returnUrl
            };

            Response.Cookies.Add(new HttpCookie(cname, JsonConvert.SerializeObject(cvalue)));
        }

        public void modalMsg(string title, string htmlBody, string htmlFooter, bool isStatic)
        {
            var cname = BizPortalSetting.MODAL_COOKIE;
            var cvalue = new
            {
                title = Uri.EscapeUriString(title),
                htmlBody = Uri.EscapeUriString(htmlBody),
                htmlFooter = Uri.EscapeUriString(htmlFooter),
                isStatic = isStatic
            };

            Response.Cookies.Add(new HttpCookie(cname, JsonConvert.SerializeObject(cvalue)));
        }

        public string GenerateTableToolTemplate<T>(string controllerName, string area, T id, DatatableDisplay param, string returnUrl = "")
        {
            string template = "";
            string view = string.Format("<a href='{0}' class='btn btn-xs btn-info m-b-xs m-r-xs'>{1}</a>", Url.Action("Detail", controllerName, new { id = id, area = area }), Resources.Global.BTN_DETAIL);
            string approve = string.Format("<a href='{0}' class='btn btn-xs btn-info m-b-xs m-r-xs'>{1}</a>", Url.Action("Approve", controllerName, new { id = id, area = area }), Resources.Global.BTN_ACTIVE);

            string editDisable = param.canEdit ? "" : "disabled";
            string edit = string.Format("<a href='{0}' {2} class='btn btn-xs btn-warning m-b-xs m-r-xs'>{1}</a>", Url.Action("Edit", controllerName, new { id = id, area = area }), Resources.Global.BTN_EDIT, editDisable);
            string deleteDisable = param.canDelete ? "" : "disabled";
            string delete = string.Format("<a href='#' onclick='delete{0}(\"{1}\")' class='btn btn-xs btn-danger m-b-xs m-r-xs'> {2}</a>", controllerName, id, Resources.Global.BTN_DELETE, deleteDisable);
            string suspendDisable = param.canSuspend ? "" : "disabled";
            string suspend = string.Format("<a href='#' onclick='suspend{0}({1})' class='btn btn-xs btn-danger m-b-xs m-r-xs'>{2}</a>", controllerName, id, Resources.Global.BTN_INACTIVE, suspendDisable);

            string enableDisable = param.canSuspend ? "" : "disabled";
            string enable = string.Format("<a href='#' onclick='enable{0}({1})' class='btn btn-xs btn-success m-b-xs m-r-xs'>{2}</a>", controllerName, id, Resources.Global.BTN_ACTIVE, enableDisable);
            string duplicateDisable = param.canSuspend ? "" : "disabled";
            string duplicate = string.Format("<a href='#' onclick='duplicate{0}({1})' class='btn btn-xs btn-warning dker m-b-xs m-r-xs'>{2}</a>", controllerName, id, Resources.Global.BTN_COPY, duplicateDisable);
            string printDisable = param.canSuspend ? "" : "disabled";
            string print = string.Format("<a href='{0}' target='_blank' class='btn btn-xs btn-default m-b-xs m-r-xs'>{1}</a>", Url.Action("GenerateCitizenGuide", "Guide", new { area = "", id = id, returnUrl = returnUrl }), Resources.Global.BTN_PRINT, printDisable);

            if (param.canView)
            {
                if (param.canApprove)
                {
                    template = template + approve;
                }
                else
                {
                    template = template + view;
                }
            }
            if (param.canEditVisible)
            {
                template = template + edit;
            }

            if (param.canDuplicateVisible)
            {
                template = template + duplicate;
            }

            if (param.canSuspendVisible)
            {
                template = template + suspend;
            }

            if (param.canEnableVisible)
            {
                template = template + enable;
            }

            if (param.canDeleteVisible)
            {
                template = template + delete;
            }


            if (param.canPrintVisible)
            {
                template = template + print;
            }

            return template;
        }

        public List<SelectListItem> GetApplicationStatusList(DropdownlistType type = DropdownlistType.SELECT)
        {
            List<SelectListItem> lst = DB.ApplicationStatusTranslations.Where(w => w.ApplicationStatus.ApplicationSysStatusName != "DRAFT" && w.ApplicationStatus.ApplicationSysStatusName != "CANCEL" && w.ApplicationStatus.ApplicationSysStatusName != "OTHER").OrderBy(o => o.ApplicationStatusID).Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => new SelectListItem() { Value = o.ApplicationStatusID.ToString(), Text = o.ApplicationStatusName }).ToList();

            return lst.AddCriteria(type);
        }

        public List<SelectListItem> GetApplicationStatusListV2(bool draftStatusIncluded, DropdownlistType type = DropdownlistType.SELECT)
        {
            string resourceFile = "ApplicationStatusRequests";

            List<SelectListItem> lst = new List<SelectListItem>();
            if (draftStatusIncluded) //*** v3 ตัดออกโดยการส่ง ค่า draftStatusIncluded = false มา
            {
                lst.Add(new SelectListItem()
                {
                    Value = ApplicationStatusV2Enum.DRAFT.ToString(),
                    Text = ResourceHelper.GetResourceWord("STATUS_" + ApplicationStatusV2Enum.DRAFT.ToString(), resourceFile, CurrentCulture)
                });
            }

            lst.Add(new SelectListItem()
            {
                Value = ApplicationStatusV2Enum.CHECK.ToString(),
                Text = ResourceHelper.GetResourceWord("STATUS_" + ApplicationStatusV2Enum.CHECK.ToString(), resourceFile, CurrentCulture)
            });
            lst.Add(new SelectListItem()
            {
                Value = ApplicationStatusV2Enum.PENDING.ToString(),
                Text = ResourceHelper.GetResourceWord("STATUS_" + ApplicationStatusV2Enum.PENDING.ToString(), resourceFile, CurrentCulture)
            });
            lst.Add(new SelectListItem()
            {
                Value = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE.ToString(),
                Text = ResourceHelper.GetResourceWord("STATUS_" + ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE.ToString(), resourceFile, CurrentCulture)
            });

            lst.Add(new SelectListItem()
            {
                Value = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE.ToString(),
                Text = ResourceHelper.GetResourceWord("STATUS_" + ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE.ToString(), resourceFile, CurrentCulture)
            });

            lst.Add(new SelectListItem()
            {
                Value = ApplicationStatusV2Enum.COMPLETED.ToString(),
                Text = ResourceHelper.GetResourceWord("STATUS_" + ApplicationStatusV2Enum.COMPLETED.ToString(), resourceFile, CurrentCulture)
            });

            lst.Add(new SelectListItem()
            {
                Value = ApplicationStatusV2Enum.REJECTED.ToString(),
                Text = ResourceHelper.GetResourceWord("STATUS_" + ApplicationStatusV2Enum.REJECTED.ToString(), resourceFile, CurrentCulture)
            });

            return lst.AddCriteria(type);
        }

        public List<SelectListItem> GetApplicationStatusListV2NewFlow(ApplicationRequestEntity model, out ApplicationStatusV2Enum nextStatus)
        {
            var statusList = new List<SelectListItem>();
            var application = DB.Applications.Where(e => e.ApplicationID == model.ApplicationID).FirstOrDefault();
            var previousStatus = ApplicationStatusV2Enum.CHECK;
            var currentStatus = model.Status;
            var nextNextStatus = ApplicationStatusV2Enum.COMPLETED;
            var nextStatusText = "";
            var currentStatusText = "";

            nextStatus = model.Status;

            if (application != null && !string.IsNullOrEmpty(application.StatusSequence))
            {
                var status = application.StatusSequence.Split(',');

                // previous status
                foreach (var s in status)
                {
                    if (s == currentStatus.ToString())
                    {
                        break;
                    }
                    else
                    {
                        Enum.TryParse(s, out previousStatus);
                    }
                }

                // next status
                var isBreak = false;
                if (nextStatus == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && model.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_PROCESS)
                {
                    nextStatus = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
                }
                else 
                {
                    foreach (var s in status)
                    {
                        if (isBreak)
                        {
                            Enum.TryParse(s, out nextStatus);
                            break;
                        }

                        if (s == currentStatus.ToString())
                        {
                            isBreak = true;
                        }
                    }
                }

                // status after next
                isBreak = false;
                foreach (var s in status)
                {
                    if (isBreak)
                    {
                        Enum.TryParse(s, out nextNextStatus);
                        break;
                    }

                    if (s == nextStatus.ToString())
                    {
                        isBreak = true;
                    }
                }
            }
            else
            {
                switch (model.Status)
                {
                    case ApplicationStatusV2Enum.CHECK:
                        nextStatus = ApplicationStatusV2Enum.PENDING;
                        break;
                    case ApplicationStatusV2Enum.PENDING:
                        nextStatus = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
                        break;
                    case ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE:
                        if (model.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_PROCESS)
                        {
                            nextStatus = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
                        }
                        else 
                        {
                            nextStatus = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
                        }
                        break;
                    case ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE:
                        nextStatus = ApplicationStatusV2Enum.COMPLETED;
                        break;
                    default:
                        break;
                }
            }

            // set default
            statusList.Add(new SelectListItem { Value = "", Text = Resources.Global.DDL_SELECT });

            // set next status
            switch (nextStatus)
            {
                case ApplicationStatusV2Enum.PENDING:
                    if (currentStatus == ApplicationStatusV2Enum.CHECK)
                    {
                        nextStatusText = "แบบฟอร์มเอกสารครบถ้วน";
                    }
                    else if (currentStatus == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE)
                    {
                        nextStatusText = "ชำระค่าธรรมเนียมแล้ว";
                    }
                    else if (currentStatus == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE)
                    {
                        nextStatusText = "ออกใบอนุญาตแล้ว";
                    }

                    break;
                case ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE:
                    if (currentStatus == ApplicationStatusV2Enum.CHECK)
                    {
                        nextStatusText = "แบบฟอร์มเอกสารครบถ้วน";
                    }
                    else if (currentStatus == ApplicationStatusV2Enum.PENDING)
                    {
                        nextStatusText = "อนุมัติ";
                    }
                    else if (currentStatus == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE)
                    {
                        nextStatusText = "จัดการช่องทางรับใบอนุญาตและช่องทางการชำระเงิน";
                    }
                    else if (currentStatus == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE)
                    {
                        nextStatusText = "ออกใบอนุญาตแล้ว";
                    }

                    break;
                case ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE:
                    if (currentStatus == ApplicationStatusV2Enum.CHECK)
                    {
                        nextStatusText = "แบบฟอร์มเอกสารครบถ้วน";
                    }
                    else if (currentStatus == ApplicationStatusV2Enum.PENDING)
                    {
                        nextStatusText = "อนุมัติ";
                    }
                    else if (currentStatus == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE)
                    {
                        nextStatusText = "ชำระค่าธรรมเนียมแล้ว";
                    }

                    break;
                case ApplicationStatusV2Enum.COMPLETED:
                    if (currentStatus == ApplicationStatusV2Enum.CHECK)
                    {
                        nextStatusText = "อนุมัติ";
                    }
                    else if (currentStatus == ApplicationStatusV2Enum.PENDING)
                    {
                        if (model.PermitDeliveryType == "AT_OWNER_ORG")
                        {
                            nextStatusText = "ผู้ประกอบการมารับใบอนุญาตแล้ว";
                        }
                        else if (model.PermitDeliveryType == "BY_MAIL")
                        {
                            nextStatusText = "ส่งใบอนุญาตทางไปรษณีย์แล้ว";
                        }
                        else
                        {
                            nextStatusText = "อนุมัติ";
                        }
                    }
                    else if (currentStatus == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE)
                    {
                        if (model.PermitDeliveryType == "AT_OWNER_ORG")
                        {
                            nextStatusText = "ผู้ประกอบการมารับใบอนุญาตแล้ว";
                        }
                        else if (model.PermitDeliveryType == "BY_MAIL")
                        {
                            nextStatusText = "ส่งใบอนุญาตทางไปรษณีย์แล้ว";
                        }
                        else
                        {
                            nextStatusText = "ชำระค่าธรรมเนียมแล้ว";
                        }
                    }
                    else if (currentStatus == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE)
                    {
                        if (model.StatusOther == "WAITING_AGENT_WORKING")
                        {
                            if (application.IsEnableELicense && (application.SigningType == EDocumentType.Personal.ToString() || application.SigningType == EDocumentType.OrgByPerson.ToString()) && !model.IsPendingSigning)
                            {
                                nextStatusText = "";
                            }
                            else
                            {
                                nextStatusText = "ออกใบอนุญาต";
                            }
                        }
                        else
                        {
                            if (model.PermitDeliveryType == "AT_OWNER_ORG")
                            {
                                nextStatusText = "ผู้ประกอบการมารับใบอนุญาตแล้ว";
                            }
                            else if (model.PermitDeliveryType == "BY_MAIL")
                            {
                                nextStatusText = "ส่งใบอนุญาตทางไปรษณีย์แล้ว";
                            }
                            else
                            {
                                nextStatusText = "ดำเนินการเสร็จสิ้น";
                            }
                        }
                    }

                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(nextStatusText))
            {
                statusList.Add(new SelectListItem { Value = nextStatus.ToString(), Text = nextStatusText });
            }

            // set current status
            switch (model.Status)
            {
                case ApplicationStatusV2Enum.CHECK:
                    currentStatusText = Resources.ApplicationStatusRequests.ACTION_ADJUST_PENDING;
                    break;
                case ApplicationStatusV2Enum.PENDING:
                    currentStatusText = Resources.ApplicationStatusRequests.ACTION_ADJUST_PENDING;
                    break;
                case ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE:
                    if (application.IsEnableELicense && (application.SigningType == EDocumentType.Personal.ToString() || application.SigningType == EDocumentType.OrgByPerson.ToString()))
                    {
                        if (model.IsPendingSigning)
                        {
                            currentStatusText = "แก้ไขใบอนุญาต";
                        }
                        else
                        {
                            currentStatusText = Resources.ApplicationStatusRequests.ACTION_CREATING_LICENSE;
                        }
                    }
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(currentStatusText))
            {
                statusList.Add(new SelectListItem { Value = currentStatus.ToString(), Text = currentStatusText });
            }

            // set common status
            statusList.Add(new SelectListItem { Value = ApplicationStatusV2Enum.REJECTED.ToString(), Text = Resources.ApplicationStatusRequests.ACTION_DECLINE });

            return statusList;
        }

        public ApplicationStatusV2Enum GetApplicationStatusNextState(ApplicationRequestEntity model)
        {
            var application = DB.Applications.Where(e => e.ApplicationID == model.ApplicationID).FirstOrDefault();
            var nextStatus = model.Status;

            if (application != null && !string.IsNullOrEmpty(application.StatusSequence))
            {
                var status = application.StatusSequence.Split(',');
                var currentStatus = model.Status.ToString();
                var isBreak = false;

                foreach (var s in status)
                {
                    if (isBreak)
                    {
                        Enum.TryParse(s, out nextStatus);
                        break;
                    }

                    if (s == currentStatus)
                    {
                        isBreak = true;
                    }
                }
            }

            return nextStatus;
        }

        public List<SelectListItem> GetApplicationStatusOtherList(DropdownlistType type = DropdownlistType.SELECT)
        {

            string resourceFile = "ApplicationStatusRequests";

            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem()
            {
                Value = "WAITING_AGENT_READ_REQUEST",
                Text = ResourceHelper.GetResourceWord("STATUS_OTHER_WAITING_AGENT_READ_REQUEST", resourceFile, CurrentCulture)
            });

            lst.Add(new SelectListItem()
            {
                Value = "WAITING_AGENT_WORKING",
                Text = ResourceHelper.GetResourceWord("STATUS_OTHER_WAITING_AGENT_WORKING", resourceFile, CurrentCulture)
            });
            lst.Add(new SelectListItem()
            {
                Value = "WAITING_AGENT_CHECK_CORRECTION",
                Text = ResourceHelper.GetResourceWord("STATUS_OTHER_WAITING_AGENT_WORKING::WAITING_USER_WORKING", resourceFile, CurrentCulture)
            });
            lst.Add(new SelectListItem()
            {
                Value = "WAITING_USER_WORKING",
                Text = ResourceHelper.GetResourceWord("STATUS_OTHER_WAITING_USER_WORKING", resourceFile, CurrentCulture)
            });
            lst.Add(new SelectListItem()
            {
                Value = "DONE",
                Text = ResourceHelper.GetResourceWord("STATUS_OTHER_DONE", resourceFile, CurrentCulture)
            });

            return lst.AddCriteria(type);
        }

        public List<SelectListItem> GetRolesList(bool emptyItemIncluded = false, string role = "")
        {
            var roles = new List<SelectListItem>();

            if (role == ConfigurationValues.ROLES_ORG_ADMIN_NAME)
            {
                roles = DB.Roles
                          .Where(e => e.Name == ConfigurationValues.ROLES_ORG_ADMIN_NAME)
                          .OrderBy(e => e.Order)
                          .Select(o => new SelectListItem { Value = o.Id.ToString(), Text = o.Description })
                          .ToList();
            }
            else
            {
                roles = DB.Roles
                          .OrderBy(e => e.Order)
                          .Select(o => new SelectListItem { Value = o.Id.ToString(), Text = o.Description })
                         .ToList();
            }


            if (emptyItemIncluded)
            {
                roles.Insert(0, new SelectListItem()
                {
                    Value = string.Empty,
                    Text = string.Empty
                });
            }

            return roles;
        }

        public List<SelectListItem> GetUserTypesList(bool emptyItemIncluded = false, string role = "")
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            if (emptyItemIncluded)
            {
                lst.Add(new SelectListItem()
                {
                    Value = string.Empty,
                    Text = string.Empty
                });
            }

            if (role == ConfigurationValues.ROLES_ORG_ADMIN_NAME)
            {
                lst.Add(new SelectListItem()
                {
                    Value = "Citizen",
                    Text = "ประชาชน"
                });

                lst.Add(new SelectListItem()
                {
                    Value = "GovernmentAgent",
                    Text = "เจ้าหน้าที่รัฐ"
                });
            }
            else
            {
                lst.Add(new SelectListItem()
                {
                    Value = "JuristicPerson",
                    Text = "นิติบุคคล"
                });

                lst.Add(new SelectListItem()
                {
                    Value = "Citizen",
                    Text = "ประชาชน"
                });
                lst.Add(new SelectListItem()
                {
                    Value = "Foreigner",
                    Text = "ชาวต่างชาติ"
                });

                lst.Add(new SelectListItem()
                {
                    Value = "GovernmentAgent",
                    Text = "เจ้าหน้าที่รัฐ"
                });

            }


            return lst;
        }

        public List<SelectListItem> GetJuristicList(DropdownlistType type = DropdownlistType.SELECT)
        {
            List<SelectListItem> lst = DB.Users.Where(w => w.UserType == "JuristicPerson").Select(o => new SelectListItem() { Value = o.JuristicID, Text = !string.IsNullOrEmpty(o.FullName) ? o.FullName : o.JuristicID }).OrderBy(o => o.Text).ToList();
            return lst.AddCriteria(type);
        }

        public List<SelectListItem> GetApplicationlist(DropdownlistType type = DropdownlistType.SELECT)
        {
            List<SelectListItem> lst = DB.ApplicationTranslations
                .Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture)
                .Select(o => new SelectListItem() { Value = o.ApplicationID.ToString(), Text = o.ApplicationName })
                .OrderBy(o => o.Text).ToList();
            return lst.AddCriteria(type);
        }

        public List<SelectListItem> GetApplicationList(string orgCode, DropdownlistType type = DropdownlistType.SELECT)
        {
            List<SelectListItem> lst = DB.ApplicationTranslations
                .Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture && w.Application.OrgCode == orgCode)
                .Select(o => new SelectListItem() { Value = o.ApplicationID.ToString(), Text = o.ApplicationName })
                .OrderBy(o => o.Text).ToList();
            return lst.AddCriteria(type);
        }

        public List<SelectListItem> GetMemberServiceList(DropdownlistType type = DropdownlistType.SELECT, string id = "", bool isOrgAdmin = false, string orgCode = "")
        {
            var lst = DB.ApplicationTranslations
                        .Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture && !w.Application.IsDeleted)
                        .Select(o => new SelectListItem() { Value = o.ApplicationID.ToString(), Text = o.ApplicationName })
                        .OrderBy(o => o.Text)
                        .ToList();

            if (!string.IsNullOrEmpty(id))
            {
                var serviceIds = new List<string>();

                if (isOrgAdmin)
                {
                    serviceIds = DB.MemberManageServices
                                   .Where(e => e.UserID == id && !e.IsDeleted)
                                   .Select(e => e.ApplicationID.ToString())
                                   .ToList();

                    if (!string.IsNullOrEmpty(orgCode) && (serviceIds == null || serviceIds.Count == 0))
                    {
                        serviceIds = DB.Applications
                                       .Where(e => e.OrgCode == orgCode && !e.IsDeleted)
                                       .Select(e => e.ApplicationID.ToString())
                                       .ToList();
                    }
                }
                else
                {
                    serviceIds = DB.MemberServices
                                   .Where(e => e.UserID == id && !e.IsDeleted)
                                   .Select(e => e.ApplicationID.ToString())
                                   .ToList();

                    if (!string.IsNullOrEmpty(orgCode) && (serviceIds == null || serviceIds.Count == 0))
                    {
                        serviceIds = DB.Applications
                                       .Where(e => e.OrgCode == orgCode && !e.IsDeleted)
                                       .Select(e => e.ApplicationID.ToString())
                                       .ToList();
                    }
                }

                lst = lst.Where(e => serviceIds.Contains(e.Value))
                         .ToList();
            }

            return lst.AddCriteria(type);
        }

        public List<SelectListItem> GetSectionList(DropdownlistType type = DropdownlistType.SELECT)
        {
            List<SelectListItem> lst = DB.SectionTranslations.OrderBy(o => o.SectionName).Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => new SelectListItem() { Value = o.SectionID.ToString(), Text = o.SectionName }).ToList();
            return lst.AddCriteria(type);
        }

        public List<SelectListItem> GetCategoryList(DropdownlistType type = DropdownlistType.SELECT)
        {
            List<SelectListItem> lst = DB.CategoryTranslations.OrderBy(o => o.CategoryName).Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => new SelectListItem() { Value = o.CategoryID.ToString(), Text = o.CategoryName }).ToList();
            return lst.AddCriteria(type);
        }

        public List<SelectListItem> GetOrganizationList(DropdownlistType type = DropdownlistType.SELECT, bool emptyItemIncluded = false)
        {
#if DEBUG
            List<SelectListItem> lst = DB.OrganizationTranslations.Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => new SelectListItem() { Value = o.OrgCode.ToString(), Text = o.OrgName }).OrderBy(o => o.Text).ToList();
#else
            EGovServices svc = new EGovServices();
            var orgs = svc.GetOrganizations("th-TH");
            List<SelectListItem> lst = orgs.Select(o => new SelectListItem() { Value = o.Code, Text = o.Name }).OrderBy(o => o.Text).ToList();
#endif


            if (emptyItemIncluded)
            {
                lst.Insert(0, new SelectListItem()
                {
                    Value = string.Empty,
                    Text = string.Empty
                });
            }


            return lst.AddCriteria(type);
        }

        public string GetOrganizationName(string orgCode)
        {
            return DB.OrganizationTranslations.Where(w => w.OrgCode == orgCode && w.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.OrgName).SingleOrDefault();
        }

        public List<SelectListItem> GetServiceList(string orgCode = "", bool includeOrgName = false)
        {
            var currentLaguageId = CurrentCulture == "th" ? 1 : 2;
            var applications = DB.ApplicationTranslations
                                 .Include(e => e.Application)
                                 .Include(e => e.Application.Organization.OrganizationTranslations)
                                 .Where(w => w.LanguageID == currentLaguageId && !w.Application.IsDeleted && !w.ApplicationName.Contains("(ไม่ใช้)"))
                                 .ToList();

            if (!string.IsNullOrEmpty(orgCode))
            {
                if (includeOrgName)
                {
                    return applications.Where(e => e.Application.OrgCode == orgCode)
                                       .Select(e => new SelectListItem() { Value = e.ApplicationID.ToString(), Text = e.ApplicationName + " - " + e.Application.Organization.OrganizationTranslations.Where(f => f.LanguageID == currentLaguageId).Select(f => f.OrgName).FirstOrDefault(), Group = new SelectListGroup { Name = e.Application.OrgCode } })
                                       .OrderBy(e => e.Value == orgCode)
                                       .ThenBy(e => e.Text)
                                       .ToList();
                }
                else
                {
                    return applications.Select(e => new SelectListItem() { Value = e.ApplicationID.ToString(), Text = e.ApplicationName, Group = new SelectListGroup { Name = e.Application.OrgCode } })
                                       .OrderBy(e => e.Value == orgCode)
                                       .ThenBy(e => e.Text)
                                       .ToList();
                }
            }
            else
            {
                if (includeOrgName)
                {
                    return applications.Select(e => new SelectListItem() { Value = e.ApplicationID.ToString(), Text = e.ApplicationName + " - " + e.Application.Organization.OrganizationTranslations.Where(f => f.LanguageID == currentLaguageId).Select(f => f.OrgName).FirstOrDefault(), Group = new SelectListGroup { Name = e.Application.OrgCode } })
                                       .OrderBy(e => e.Text)
                                       .ToList();
                }
                else
                {
                    return applications.Select(e => new SelectListItem() { Value = e.ApplicationID.ToString(), Text = e.ApplicationName, Group = new SelectListGroup { Name = e.Application.OrgCode } })
                                       .OrderBy(e => e.Text)
                                       .ToList();
                }
            }
        }

        public List<SelectListItem> GetManageServiceList(string id, string orgCode)
        {
            var currentLanguageId = CurrentCulture == "th" ? 1 : 2;
            var manageServiceList = new List<SelectListItem>();

            if (User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME) || User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME))
            {
                var serviceList = DB.ApplicationTranslations
                                    .Include(e => e.Application)
                                    .Include(e => e.Application.Organization)
                                    .Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture && !w.Application.IsDeleted)
                                    .ToList();

                manageServiceList = serviceList.Select(e => new SelectListItem() { Value = e.ApplicationID.ToString(), Text = e.ApplicationName + " - " + e.Application.Organization.OrgSysName, Group = new SelectListGroup { Name = e.Application.OrgCode } })
                                               .OrderBy(e => e.Value == orgCode)
                                               .ThenBy(e => e.Text)
                                               .ToList();
            }
            else
            {
                var memberManageList = DB.MemberManageServices
                                         .Include(e => e.Application.Organization)
                                         .Include(e => e.Application.ApplicationTranslations)
                                         .Where(e => e.UserID == id && !e.IsDeleted)
                                         .ToList();

                if (memberManageList.Count > 0)
                {
                    manageServiceList = memberManageList.Select(e => new SelectListItem() { Value = e.ApplicationID.ToString(), Text = e.Application.ApplicationTranslations.Where(t => t.ApplicationID == e.ApplicationID && t.LanguageID == currentLanguageId).Select(t => t.ApplicationName).FirstOrDefault() + " - " + e.Application.Organization.OrgSysName, Group = new SelectListGroup { Name = e.Application.OrgCode } })
                                                        .OrderBy(e => e.Value == orgCode)
                                                        .ThenBy(e => e.Text)
                                                        .ToList();
                }
                else
                {
                    var serviceList = DB.ApplicationTranslations
                                    .Include(e => e.Application)
                                    .Include(e => e.Application.Organization)
                                    .Include(e => e.Application.ApplicationTranslations)
                                    .Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture && w.Application.OrgCode == orgCode && !w.Application.IsDeleted)
                                    .ToList();

                    manageServiceList = serviceList.Select(e => new SelectListItem() { Value = e.ApplicationID.ToString(), Text = e.ApplicationName + " - " + e.Application.Organization.OrgSysName, Group = new SelectListGroup { Name = e.Application.OrgCode } })
                                                   .OrderBy(e => e.Value == orgCode)
                                                   .ThenBy(e => e.Text)
                                                   .ToList();
                }
            }

            return manageServiceList;
        }

        public List<SelectListItem> getContactQuestion([Bind(Include = "type")] DropdownlistType type = DropdownlistType.NONE)
        {
            List<SelectListItem> statusList = default(QuestionEnumType).ToSelectList().AddCriteria(type);
            foreach (var item in statusList)
            {
                item.Text = ResourceHelper.GetResourceWord(item.Text, "ContactUs", CurrentCulture);
            }
            return statusList;
        }

        public JuristicRequestFileUploadType[] getJuristicRequestFileUploadType(string applicationSysName)
        {
            if (applicationSysName == "SSO Register Employer") // ปรับให้ดึงจาก Database
                return new JuristicRequestFileUploadType[] { new JuristicRequestFileUploadType { Name = "ใบแจ้งการประเมินเงินสมทบกองทุนเงินทดแทนประจำปี", Code = "C01" }, new JuristicRequestFileUploadType { Name = "ใบรับรองการขึ้นทะเบียนนายจ้าง", Code = "C02" } };
            else
                return new JuristicRequestFileUploadType[] { };
        }

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
    }
}