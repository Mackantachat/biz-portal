using BizPortal.AppsHook;
using BizPortal.Areas.WebApiV2.Controllers;
using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using BizPortal.ViewModels.V2;
using iTextSharp.text.pdf.qrcode;
using Mapster;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BizPortal.Areas.Manage.Controllers
{
    [Authorize(Roles = ConfigurationValues.ROLES_ADMIN_NAME + "," +
        ConfigurationValues.ROLES_OPDC_ADMIN_NAME + "," +
        ConfigurationValues.ROLES_ORG_ADMIN_NAME + "," +
        ConfigurationValues.ROLES_ORG_AGENT_NAME + "," +
        ConfigurationValues.ROLES_OPDC_AGENT_NAME)]
    public class ApplicationStatusController : ManageControllerBase
    {
        // GET: Manage/ApplicationStatus
        public ActionResult Index()
        {
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);
            var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);
            var isOrgAgent = User.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME);
            var isOPDCAgent = User.IsInRole(ConfigurationValues.ROLES_OPDC_AGENT_NAME);

            var memberServices = DB.MemberServices
                                  .Include(e => e.MemberServiceAreas)
                                  .Where(e => e.UserID == CurrentUserID && !e.IsDeleted)
                                  .SelectMany(e => e.MemberServiceAreas)
                                  .Select(e => new
                                  {
                                      ApplicationID = e.MemberService.ApplicationID,
                                      ProvinceID = e.ProvinceID,
                                      DistrictID = e.DistrictID,
                                      SectionID = e.SectionID
                                  })
                                  .ToList();

            ViewBag.ActiveMenu = PageNameBackendEnum.APP_STATUS.GetEnumStringValue();
            ViewBag.IsAdmin = isAdmin;
            ViewBag.IsOPDCAdmin = isOPDCAdmin;
            ViewBag.IsOrgAdmin = isOrgAdmin;
            ViewBag.IsOrgAgent = isOrgAgent;
            ViewBag.IsOPDCAgent = isOPDCAgent;
            ViewBag.ApplicationStatusList = new SelectList(GetApplicationStatusListV2(false), "Value", "Text", ""); ;
            ViewBag.ApplicationStatusOtherList = new SelectList(GetApplicationStatusOtherList(), "Value", "Text");
            ViewBag.OrganizationList = new SelectList(GetOrganizationList(), "Value", "Text");
            ViewBag.ApplicationList = new SelectList(GetMemberServiceList(DropdownlistType.ALL, ((isAdmin || isOPDCAdmin || isOPDCAgent) ? "" : CurrentUserID), isOrgAdmin, OrganizationID), "Value", "Text");
            ViewBag.MemberServiceList = memberServices;

            return View();
        }

        public ActionResult Detail(Guid? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var model = getApplicationRequestByRole(id.Value);
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);
            var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);
            var isOrgAgent = User.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME);
            var isOPDCAgent = User.IsInRole(ConfigurationValues.ROLES_OPDC_AGENT_NAME);

            ViewBag.IsAdmin = isAdmin;
            ViewBag.IsOPDCAdmin = isOPDCAdmin;
            ViewBag.IsOrgAdmin = isOrgAdmin;
            ViewBag.IsOrgAgent = isOrgAgent;
            ViewBag.IsOPDCAgent = isOPDCAgent;
            ViewBag.Holidays = new List<Holiday>();

            if (model == null)
            {
                notifyMsg(NotifyMsgType.Error, Resources.ApplicationStatusRequests.MSG_NOT_FOUND);
                return RedirectToAction("Index", "ApplicationStatus", new { Area = "Manage" });
            }

            if (model.Status == ApplicationStatusV2Enum.CHECK && model.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST && User.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME))
            {
                ConfirmView(id.Value);
                model = getApplicationRequestByRole(id.Value);
            }

            if (model.Status == ApplicationStatusV2Enum.CHECK && (model.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST || model.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING))
            {
                int holidayQueryLen = 300;
                var today = DateTime.Now.Date;
                var untilDate = today + TimeSpan.FromDays(holidayQueryLen);
                ViewBag.Holidays = MongoFactory.GetHolidayCollection()
                                               .AsQueryable()
                                               .Where(x => x.Date > today && x.Date <= untilDate)
                                               .OrderBy(x => x.Date)
                                               .ToList();
            }

            if (string.IsNullOrEmpty(model.IdentityName))
            {
                string name = DB.Users.Where(o => o.JuristicID == model.IdentityID || o.CitizenID == model.IdentityID).Select(o => o.FullName).FirstOrDefault();

                if (!string.IsNullOrEmpty(name))
                {
                    model.IdentityName = name;
                }
            }
            string mails = DB.Users.Where(o => o.JuristicID == model.IdentityID || o.CitizenID == model.IdentityID).Select(o => o.Email).FirstOrDefault();
            ViewBag.GeneralEmail = mails;// model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL");
            #region Try to get Contact Number from UserManager
            var user = UserManager.FindByName(model.IdentityID);
            if (user != null) ViewBag.RequestorPhone = user.PhoneNumber;
            #endregion

            foreach (var tran in model.Transactions)
            {
                if (string.IsNullOrEmpty(tran.IdentityName) && !string.IsNullOrEmpty(tran.IdentityID))
                {
                    string name = DB.Users.Where(o => o.JuristicID == tran.IdentityID || o.CitizenID == tran.IdentityID).Select(o => o.FullName).SingleOrDefault();

                    if (!string.IsNullOrEmpty(name))
                    {
                        tran.IdentityName = name;
                        model.Update();
                    }
                }
            }

            ApplicationRequestViewModel viewModel = model.Adapt<ApplicationRequestViewModel>();

            ViewBag.Fee = model.Fee;
            var userModel = DB.Users.Where(w => w.Id == model.UpdatedByAgent).FirstOrDefault();
            string fullName = "-";
            string agentPhone = "-";

            if (userModel != null)
            {
                fullName = userModel.FullName;
                agentPhone = string.IsNullOrEmpty(userModel.PhoneNumber) ? "-" : userModel.PhoneNumber;
            }

            ViewBag.UpdatedByAgentName = fullName;
            ViewBag.UpdatedByAgentPhone = agentPhone;

            #region Add Default Data
            Dictionary<string, object> defaults = new Dictionary<string, object>();
            if (viewModel.Data != null && viewModel.Data.Count > 0)
            {
                foreach (var sec in viewModel.Data)
                {
                    string key = sec.Key + "::";
                    var value = sec.Value;
                    foreach (var secDataKey in value.Data)
                    {
                        defaults.Add(key + secDataKey.Key, secDataKey.Value);
                    }
                }
            }
            ViewBag.Defaults = defaults;
            #endregion

            if (viewModel.Data.TryGetData("LEGACY").ThenGetStringData("LEGACY_VERSION") == "1.0.0")
            {
                // Legacy Version 1.0.0
                var oldID = DB.JuristicApplicationStatusRequests.Where(o => o.MigratedRequestID == id).Select(o => o.JuristicApplicationStatusRequestID).Single();
                var query = DB.JuristicRequestViews.Where(w => w.JuristicApplicationStatusRequestID == oldID && w.TwoLetterISOLanguageName == CurrentCulture).AsQueryable();

                var oldModel = query.Select(s => new JuristicApplicationStatusRequestViewModel
                {
                    ApplicationName = s.ApplicationName,
                    JuristicApplicationStatusRequestID = s.JuristicApplicationStatusRequestID,
                    OrganizationName = s.OrgName,
                    Remark = s.Remark,
                    ApplicationID = s.ApplicationID,
                    CreatedDate = s.CreatedDate,
                    JuristicID = s.JuristicID,
                    ApplicationStatusID = s.ApplicationStatusID,
                    ApplicationStatusOther = s.ApplicationStatusOther,
                    ApplicationStatusName = s.ApplicationStatusName,
                    CreatedUserID = s.CreatedUserID,
                    OrgCode = s.OrgCode,
                    CreatedUserName = s.UserName,
                    CreatedFullName = s.FullName,
                    ApplicationSysName = s.ApplicationSysName
                }).SingleOrDefault();

                if (oldModel == null)
                {
                    notifyMsg(NotifyMsgType.Error, Resources.ApplicationStatusRequests.MSG_NOT_FOUND);
                    return RedirectToAction("Index", "ApplicationStatus", new { Area = "Manage" });
                }

                ViewBag.FileList = getJuristicRequestFileUploadType(oldModel.ApplicationSysName);
                ViewBag.Title = string.Format("{0} {1}", Resources.ApplicationStatusRequests.APPLICATION_REQUEST, model.ApplicationName);
                ViewBag.ActiveMenu = PageNameBackendEnum.APP_STATUS.GetEnumStringValue();
                ViewBag.ApplicationStatusList = new SelectList(GetApplicationStatusList(DropdownlistType.NONE), "Value", "Text");
                ViewBag.AppID = model.ApplicationID;

                return View("Detail_1_0_0", oldModel);
            }
            else
            {
                #region VAT
                if (model.ApplicationID == 9)
                {
                    ApplicationStatusV2Enum[] pendingStatus = new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.WAITING, ApplicationStatusV2Enum.CHECK };
                    if (pendingStatus.Contains(model.Status))
                    {
                        model.Status = ApplicationStatusV2Enum.COMPLETED;
                        model.StatusName = Resources.ApplicationStatusRequests.STATUS_PENDING_VAT;
                    }
                    else if (model.Status == ApplicationStatusV2Enum.COMPLETED)
                    {
                        model.StatusName = "เป็นผู้ประกอบการจดทะเบียน เว้นแต่เข้าลักษณะที่ไม่ออกใบทะเบียน";
                    }
                    else
                    {
                        model.StatusName = ResourceHelper.GetResourceWord("STATUS_" + model.Status, "ApplicationStatusRequests", CurrentCulture);
                    }
                }
                else
                {
                    model.StatusName = ResourceHelper.GetResourceWord("STATUS_" + model.Status, "ApplicationStatusRequests", CurrentCulture);
                }
                #endregion

                // Current Version
                var application = DB.Applications.Where(o => o.ApplicationID == model.ApplicationID)
                                                 .Include(e => e.SigningExtendedDatas)
                                                 .Include(e => e.SigningPersons)
                                                 .Include(e => e.SigningPositions)
                                                 .FirstOrDefault();

                var applicationSysName = application.ApplicationSysName;

                model.ApplicationName = DB.ApplicationTranslations.Where(o => o.ApplicationID == model.ApplicationID && o.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.ApplicationName).FirstOrDefault();

                ViewBag.IsOCPB = (applicationSysName == AppSystemNameTextConst.APP_DIRECT_MARKETING || applicationSysName == AppSystemNameTextConst.APP_DIRECT_SELL);
                ViewBag.ShowSignForm = (applicationSysName == AppSystemNameTextConst.APP_BUILDING || applicationSysName == AppSystemNameTextConst.APP_BUILDING_RENEW);
                ViewBag.ActiveMenu = PageNameBackendEnum.APP_STATUS.GetEnumStringValue();
                ViewBag.OrgName = GetOrganizationName(OrganizationID);
                ViewBag.IsEnableCGDPayment = application.EnableCGDPayment;

                // elicense
                var elicenses = new List<ELicenseViewModel>();

                if (application.IsEnableELicense) 
                {
                    var signings = EDocumentEntity.GetAll(model.ApplicationRequestID);
                    var signingTemplateId = viewModel.IdentityType == UserTypeEnum.Juristic ? application.SigningDocumentJuristicTemplateID : application.SigningDocumentCitizenTemplateID;
                    var signingExtendedDatas = new List<SigningExtendedDataViewData>();
                    var signingPositions = new List<SigningPositionViewModel>();
                    var signingPersons = new List<SigningPersonViewModel>();

                    if (application.SigningType != EDocumentType.NotSign.ToString())
                    {
                        signingExtendedDatas = application.SigningExtendedDatas.Where(e => e.UserType == viewModel.IdentityType).Select(e => new SigningExtendedDataViewData
                        {
                            Type = e.Type,
                            Label = e.Label,
                            Name = e.Name,
                            Value = e.Value
                        }).ToList();
                    }

                    if (application.SigningType == EDocumentType.Personal.ToString() || application.SigningType == EDocumentType.OrgByPerson.ToString())
                    {
                        foreach (var item in application.SigningPositions.OrderBy(m => m.Order))
                        {
                            signingPositions.Add(new SigningPositionViewModel
                            {
                                Bottom = item.Bottom,
                                Height = item.Height,
                                Left = item.Left,
                                Width = item.Width
                            });
                        }

                        if (signings != null && signings.Count > 0)
                        {
                            foreach (var item in signings.FirstOrDefault().PersonalSigners)
                            {
                                signingPersons.Add(new SigningPersonViewModel
                                {
                                    Order = item.SigningOrder,
                                    CitizenID = item.IdentityID,
                                    FirstName = item.IdentityFirstName,
                                    LastName = item.IdentityLastName,
                                    Position = item.IdentityPosition,
                                    Bottom = item.SignatureBottom,
                                    Left = item.SignatureLeft,
                                    Height = item.SignatureHeight,
                                    Width = item.SignatureWidth,
                                    SignatureBase64 = item.SignatureBase64,
                                    Status = item.PersonalSigningStatus,
                                    Remark = item.PersonalSigningRemark
                                });
                            }
                        }
                        else
                        {
                            foreach (var item in application.SigningPersons)
                            {
                                var signer = item.Adapt<SigningPersonViewModel>();

                                signingPersons.Add(signer);
                            }
                        }
                    }

                    if (signings != null && signings.Count > 0)
                    {
                        model.IsPendingSigning = true;

                        foreach (var signing in signings)
                        {
                            elicenses.Add(new ELicenseViewModel
                            {
                                ApplicationRequestID = signing.ApplicationRequestID,
                                Name = signing.DocumentName,
                                TemplateID = signingTemplateId,
                                DocumentID = signing.DocumentID,
                                Url = signing.DocumentUrl,
                                SigningDocumentType = application.SigningDocumentType,
                                SigningType = signing.SigningType.ToString(),
                                SigningStatus = signing.SigningStatus.ToString(),
                                SigningExtendedDatas = signingExtendedDatas,
                                SigningPositions = signingPositions,
                                SigningPersons = signingPersons,
                            });
                        }
                    }
                    else 
                    {
                        elicenses.Add(new ELicenseViewModel
                        {
                            ApplicationRequestID = viewModel.ApplicationRequestID,
                            Name = null,
                            TemplateID = signingTemplateId,
                            DocumentID = null,
                            Url = null,
                            SigningDocumentType = application.SigningDocumentType,
                            SigningType = application.SigningType,
                            SigningStatus = null,
                            SigningExtendedDatas = signingExtendedDatas,
                            SigningPositions = signingPositions,
                            SigningPersons = signingPersons,
                        });
                    }
                }

                ViewBag.IsEnableELicense = application.IsEnableELicense;
                ViewBag.ELicenses = elicenses;


                // Get App Hook
                ViewBag.AppsHookClassName = DB.Applications.Where(o => o.ApplicationID == model.ApplicationID).Select(o => o.AppsHookClassName).SingleOrDefault();
                string detailViewName = ViewNameForAgentConst.ORIGINAL;
                if (!string.IsNullOrEmpty(ViewBag.AppsHookClassName))
                {
                    var hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", ViewBag.AppsHookClassName).Unwrap();
                    if (!string.IsNullOrEmpty(hook.DetailViewName))
                    {
                        detailViewName = hook.DetailViewName;
                    }
                }

                if (detailViewName == ViewNameForAgentConst.NEW_FLOW)
                {
                    var statusList = GetApplicationStatusListV2NewFlow(model, out ApplicationStatusV2Enum nextStatus);
                    ViewBag.NextSataus = nextStatus;
                    ViewBag.ApplicationStatusList = new SelectList(statusList, "Value", "Text");
                }
                else
                {
                    ViewBag.ApplicationStatusList = new SelectList(GetApplicationStatusListV2(false, DropdownlistType.NONE), "Value", "Text");
                }

                return View(detailViewName, model);
            }
        }

        public ActionResult Print(Guid id)
        {
            var model = getApplicationRequestByRole(id);

            if (model == null)
            {
                notifyMsg(NotifyMsgType.Error, Resources.ApplicationStatusRequests.MSG_NOT_FOUND);
                return RedirectToAction("Index", "ApplicationStatus", new { Area = "Manage" });
            }

            if (string.IsNullOrEmpty(model.IdentityName))
            {
                string name = DB.Users.Where(o => o.JuristicID == model.IdentityID || o.CitizenID == model.IdentityID).Select(o => o.FullName).FirstOrDefault();

                if (!string.IsNullOrEmpty(name))
                {
                    model.IdentityName = name;
                    model.Update();
                }
            }

            foreach (var tran in model.Transactions)
            {
                if (string.IsNullOrEmpty(tran.IdentityName) && !string.IsNullOrEmpty(tran.IdentityID))
                {
                    string name = DB.Users.Where(o => o.JuristicID == tran.IdentityID || o.CitizenID == tran.IdentityID).Select(o => o.FullName).SingleOrDefault();

                    if (!string.IsNullOrEmpty(name))
                    {
                        tran.IdentityName = name;
                        model.Update();
                    }
                }
            }

            ApplicationRequestViewModel viewModel = new ApplicationRequestViewModel();
            TypeAdapter.Adapt<ApplicationRequestEntity, ApplicationRequestViewModel>(model, viewModel);

            ViewBag.Fee = model.Fee;

            #region Add Default Data
            Dictionary<string, object> defaults = new Dictionary<string, object>();
            if (viewModel.Data != null && viewModel.Data.Count > 0)
            {
                foreach (var sec in viewModel.Data)
                {
                    string key = sec.Key + "::";
                    var value = sec.Value;
                    foreach (var secDataKey in value.Data)
                    {
                        defaults.Add(key + secDataKey.Key, secDataKey.Value);
                    }
                }
            }
            ViewBag.Defaults = defaults;
            #endregion

            if (viewModel.Data.TryGetData("LEGACY").ThenGetStringData("LEGACY_VERSION") == "1.0.0")
            {
                // Legacy Version 1.0.0
                var oldID = DB.JuristicApplicationStatusRequests.Where(o => o.MigratedRequestID == id).Select(o => o.JuristicApplicationStatusRequestID).Single();
                var query = DB.JuristicRequestViews.Where(w => w.JuristicApplicationStatusRequestID == oldID && w.TwoLetterISOLanguageName == CurrentCulture).AsQueryable();

                var oldModel = query.Select(s => new JuristicApplicationStatusRequestViewModel
                {
                    ApplicationName = s.ApplicationName,
                    JuristicApplicationStatusRequestID = s.JuristicApplicationStatusRequestID,
                    OrganizationName = s.OrgName,
                    Remark = s.Remark,
                    ApplicationID = s.ApplicationID,
                    CreatedDate = s.CreatedDate,
                    JuristicID = s.JuristicID,
                    ApplicationStatusID = s.ApplicationStatusID,
                    ApplicationStatusOther = s.ApplicationStatusOther,
                    ApplicationStatusName = s.ApplicationStatusName,
                    CreatedUserID = s.CreatedUserID,
                    OrgCode = s.OrgCode,
                    CreatedUserName = s.UserName,
                    CreatedFullName = s.FullName,
                    ApplicationSysName = s.ApplicationSysName
                }).SingleOrDefault();

                if (oldModel == null)
                {
                    notifyMsg(NotifyMsgType.Error, Resources.ApplicationStatusRequests.MSG_NOT_FOUND);
                    return RedirectToAction("Index", "ApplicationStatus", new { Area = "Manage" });
                }

                ViewBag.FileList = getJuristicRequestFileUploadType(oldModel.ApplicationSysName);
                ViewBag.Title = string.Format("{0} {1}", Resources.ApplicationStatusRequests.APPLICATION_REQUEST, model.ApplicationName);
                ViewBag.ActiveMenu = PageNameBackendEnum.APP_STATUS.GetEnumStringValue();
                ViewBag.ApplicationStatusList = new SelectList(GetApplicationStatusList(DropdownlistType.NONE), "Value", "Text");
                ViewBag.AppID = model.ApplicationID;

            }
            else
            {
                // Current Version
                model.ApplicationName = DB.ApplicationTranslations.Where(o => o.ApplicationID == model.ApplicationID && o.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.ApplicationName).FirstOrDefault();
                var appSysName = DB.Applications.Where(o => o.ApplicationID == model.ApplicationID).Select(o => o.ApplicationSysName).FirstOrDefault();
                ViewBag.IsOCPB = (appSysName == AppSystemNameTextConst.APP_DIRECT_MARKETING || appSysName == AppSystemNameTextConst.APP_DIRECT_SELL);
                ViewBag.ShowSignForm = (appSysName == AppSystemNameTextConst.APP_BUILDING || appSysName == AppSystemNameTextConst.APP_BUILDING_RENEW);
                model.StatusName = ResourceHelper.GetResourceWord("STATUS_" + model.Status, "ApplicationStatusRequests", CurrentCulture);
                ViewBag.ActiveMenu = PageNameBackendEnum.APP_STATUS.GetEnumStringValue();

                // Get App Hook
                ViewBag.AppsHookClassName = DB.Applications.Where(o => o.ApplicationID == model.ApplicationID).Select(o => o.AppsHookClassName).SingleOrDefault();
                var hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", ViewBag.AppsHookClassName).Unwrap();
                if (hook.HasOrgPdfForm && !AppHookFilter.IsDisableOrgPdfForm(appSysName))
                {

                    var b = hook.GetOrgPdfFormContent(model, Server.MapPath);
                    return File(b, "application/pdf");
                }
                string detailViewName = ViewNameForAgentConst.ORIGINAL;
                if (!string.IsNullOrEmpty(ViewBag.AppsHookClassName))
                {
                    if (!string.IsNullOrEmpty(hook.DetailViewName))
                    {
                        detailViewName = hook.DetailViewName;
                    }
                }

                ViewBag.ApplicationStatusList = new SelectList(GetApplicationStatusListV2(false, DropdownlistType.NONE), "Value", "Text");
                ViewBag.OrgName = GetOrganizationName(OrganizationID);
            }
            return View(model);
        }

        public ActionResult PrintMap(Guid id)
        {
            var model = getApplicationRequestByRole(id);

            if (model == null)
            {
                notifyMsg(NotifyMsgType.Error, Resources.ApplicationStatusRequests.MSG_NOT_FOUND);
                return RedirectToAction("Index", "ApplicationStatus", new { Area = "Manage" });
            }

            if (string.IsNullOrEmpty(model.IdentityName))
            {
                string name = DB.Users.Where(o => o.JuristicID == model.IdentityID || o.CitizenID == model.IdentityID).Select(o => o.FullName).FirstOrDefault();

                if (!string.IsNullOrEmpty(name))
                {
                    model.IdentityName = name;
                    model.Update();
                }
            }

            foreach (var tran in model.Transactions)
            {
                if (string.IsNullOrEmpty(tran.IdentityName) && !string.IsNullOrEmpty(tran.IdentityID))
                {
                    string name = DB.Users.Where(o => o.JuristicID == tran.IdentityID || o.CitizenID == tran.IdentityID).Select(o => o.FullName).SingleOrDefault();

                    if (!string.IsNullOrEmpty(name))
                    {
                        tran.IdentityName = name;
                        model.Update();
                    }
                }
            }

            ApplicationRequestViewModel viewModel = new ApplicationRequestViewModel();
            TypeAdapter.Adapt<ApplicationRequestEntity, ApplicationRequestViewModel>(model, viewModel);

            ViewBag.Fee = model.Fee;

            if (viewModel.Data.TryGetData("LEGACY").ThenGetStringData("LEGACY_VERSION") == "1.0.0")
            {
                // Legacy Version 1.0.0
                var oldID = DB.JuristicApplicationStatusRequests.Where(o => o.MigratedRequestID == id).Select(o => o.JuristicApplicationStatusRequestID).Single();
                var query = DB.JuristicRequestViews.Where(w => w.JuristicApplicationStatusRequestID == oldID && w.TwoLetterISOLanguageName == CurrentCulture).AsQueryable();

                var oldModel = query.Select(s => new JuristicApplicationStatusRequestViewModel
                {
                    ApplicationName = s.ApplicationName,
                    JuristicApplicationStatusRequestID = s.JuristicApplicationStatusRequestID,
                    OrganizationName = s.OrgName,
                    Remark = s.Remark,
                    ApplicationID = s.ApplicationID,
                    CreatedDate = s.CreatedDate,
                    JuristicID = s.JuristicID,
                    ApplicationStatusID = s.ApplicationStatusID,
                    ApplicationStatusOther = s.ApplicationStatusOther,
                    ApplicationStatusName = s.ApplicationStatusName,
                    CreatedUserID = s.CreatedUserID,
                    OrgCode = s.OrgCode,
                    CreatedUserName = s.UserName,
                    CreatedFullName = s.FullName,
                    ApplicationSysName = s.ApplicationSysName
                }).SingleOrDefault();

                if (oldModel == null)
                {
                    notifyMsg(NotifyMsgType.Error, Resources.ApplicationStatusRequests.MSG_NOT_FOUND);
                    return RedirectToAction("Index", "ApplicationStatus", new { Area = "Manage" });
                }

                ViewBag.FileList = getJuristicRequestFileUploadType(oldModel.ApplicationSysName);
                ViewBag.Title = string.Format("{0} {1}", Resources.ApplicationStatusRequests.APPLICATION_REQUEST, model.ApplicationName);
                ViewBag.ActiveMenu = PageNameBackendEnum.APP_STATUS.GetEnumStringValue();
                ViewBag.ApplicationStatusList = new SelectList(GetApplicationStatusList(DropdownlistType.NONE), "Value", "Text");
                ViewBag.AppID = model.ApplicationID;

            }
            else
            {
                // Current Version
                model.ApplicationName = DB.ApplicationTranslations.Where(o => o.ApplicationID == model.ApplicationID && o.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.ApplicationName).FirstOrDefault();
                var appSysName = DB.Applications.Where(o => o.ApplicationID == model.ApplicationID).Select(o => o.ApplicationSysName).FirstOrDefault();
                ViewBag.IsOCPB = (appSysName == AppSystemNameTextConst.APP_DIRECT_MARKETING || appSysName == AppSystemNameTextConst.APP_DIRECT_SELL);
                ViewBag.ShowSignForm = (appSysName == AppSystemNameTextConst.APP_BUILDING || appSysName == AppSystemNameTextConst.APP_BUILDING_RENEW);
                model.StatusName = ResourceHelper.GetResourceWord("STATUS_" + model.Status, "ApplicationStatusRequests", CurrentCulture);
                ViewBag.ActiveMenu = PageNameBackendEnum.APP_STATUS.GetEnumStringValue();

                // Get App Hook
                ViewBag.AppsHookClassName = DB.Applications.Where(o => o.ApplicationID == model.ApplicationID).Select(o => o.AppsHookClassName).SingleOrDefault();
                var hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", ViewBag.AppsHookClassName).Unwrap();
                #region พิมพ์แผนที่ไม่ต้องการ print แบบ PDF
                //if (hook.HasOrgPdfForm)
                //{

                //    var b = hook.GetOrgPdfFormContent(model, Server.MapPath);
                //    return File(b, "application/pdf");
                //}
                #endregion

                string detailViewName = ViewNameForAgentConst.ORIGINAL;
                if (!string.IsNullOrEmpty(ViewBag.AppsHookClassName))
                {
                    if (!string.IsNullOrEmpty(hook.DetailViewName))
                    {
                        detailViewName = hook.DetailViewName;
                    }
                }

                ViewBag.ApplicationStatusList = new SelectList(GetApplicationStatusListV2(false, DropdownlistType.NONE), "Value", "Text");
                ViewBag.OrgName = GetOrganizationName(OrganizationID);
            }
            return View(model);
        }

        [HttpPost]
        [Route("ApplicationStatus/ConfirmView")]
        public bool ConfirmView(Guid id)
        {
            try
            {
                ApplicationRequestEntity model = null;
                model = ApplicationRequestEntity.Get(id);
                model.Status = ApplicationStatusV2Enum.CHECK;
                model.StatusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
                model.WaitingApproveDateTime = DateTime.Now;
                model.UpdatedByAgent = User.Identity.GetUserId();

                var repo = MongoFactory.GetApplicationRequestCollection();
                repo.Update(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private ApplicationRequestEntity getApplicationRequestByRole(Guid applicationRequestId)
        {
            ApplicationRequestEntity model = null;
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);
            var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);
            var isOrgAgent = User.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME);
            var isOPDCAgent = User.IsInRole(ConfigurationValues.ROLES_OPDC_AGENT_NAME);

            if (isAdmin || isOPDCAdmin || isOPDCAgent)
            {
                model = ApplicationRequestEntity.Get(applicationRequestId);
            }
            else if (isOrgAdmin)
            {
                var serviceIds = DB.MemberManageServices
                                   .Where(e => e.UserID == CurrentUserID && !e.IsDeleted)
                                   .Select(e => e.ApplicationID)
                                   .ToList();

                if (serviceIds.Count > 0)
                {
                    model = ApplicationRequestEntity.GetByMemberService(applicationRequestId, serviceIds);
                }
                else
                {
                    model = ApplicationRequestEntity.GetByOrgCode(applicationRequestId, OrganizationID);
                }
            }
            else if (isOrgAgent)
            {
                var serviceIds = DB.MemberServices
                                   .Where(e => e.UserID == CurrentUserID && !e.IsDeleted)
                                   .Select(e => e.ApplicationID)
                                   .ToList();

                if (serviceIds.Count > 0)
                {
                    model = ApplicationRequestEntity.GetByMemberService(applicationRequestId, serviceIds);
                }
                else
                {
                    model = ApplicationRequestEntity.GetByOrgCode(applicationRequestId, OrganizationID);
                }
            }

            return model;
        }

    }
}