using BizPortal.AppsHook;
using BizPortal.Areas.Apps.Controllers;
using BizPortal.Areas.WebApi.Controllers;
using BizPortal.DAL;
using BizPortal.DAL.MongoDB;
using BizPortal.Extensions;
using BizPortal.Models;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using BizPortal.ViewModels.Apps;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using EGA.EGA_Development.Util.MailV2.Data;
using Mapster;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using static BizPortal.Utils.Helpers.EmailHelper;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class ApplicationsController : ApiControllerBase, IProgress<int>
    {
        ApplicationStatusV2Enum[] _singleRequestStatusID = new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.COMPLETED, ApplicationStatusV2Enum.PENDING, ApplicationStatusV2Enum.WAITING, ApplicationStatusV2Enum.CHECK };
        ApplicationStatusV2Enum[] _apiStatusAllowed = new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.COMPLETED, ApplicationStatusV2Enum.REJECTED, ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE, ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE, ApplicationStatusV2Enum.PENDING, ApplicationStatusV2Enum.CHECK };
        string[] _apiStatusOtherAllowed = new string[] { ApplicationStatusOtherValueConst.DONE, ApplicationStatusOtherValueConst.REJECT, ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING, ApplicationStatusOtherValueConst.WAITING_USER_WORKING, ApplicationStatusOtherValueConst.WAITING_AGENT_PROCESS };
        string[] _paymentMethodAllowed = new string[] { PaymentMethodValueConst.AT_OWNER_ORG, PaymentMethodValueConst.BILL_PAYMENT };
        string[] _permitDeliveryMethodAllowed = new string[] { PermitDeliveryTypeValueConst.AT_OWNER_ORG, PermitDeliveryTypeValueConst.BY_MAIL };
        private readonly int[] _utilityApps = new int[] { 5, 6, 7, 8, 10 };

        string serviceTokenPath = ConfigurationManager.AppSettings["FileServiceUploadTokenPath"];
        string serviceUploadPath = ConfigurationManager.AppSettings["FileServicePath"];
        string consumerKey = ConfigurationManager.AppSettings["FileConsumerKey"];
        string secret = ConfigurationManager.AppSettings["FileConsumerSecret"];

        public object ApplicationRequestID { get; private set; }

        public void Report(int value)
        {

        }

        [HttpPost]
        [Route("Api/V2/Applications/Requests")]
        public async Task<ResponseData<object>> Requests(ApplicationRequestViewModel model)
        {
            // เก็บ Log
            var log = new ActivityLog { Type = "ApplicationRequest", Path = "Api/V2/Applications/Requests", Data = model };
            log.Create();

            // Validate data model
            if (!ModelState.IsValid)
            {
                CaptureError(new CustomValidationException(ModelState), true, null, model);
            }

            var isTestMode = bool.Parse(ConfigurationManager.AppSettings["TestMode"].ToString());
            var currentDateTime = DateTime.Now;
            var hookStage = AppsStage.None;
            var response = new ResponseData<object>()
            {
                ApplicationRequestData = new Dictionary<string, object>(),
                Type = ResultDataType.Error,
                Message = Resources.ApplicationStatusRequests.MSG_ADD_FAILED
            };
            var identity = User.Identity;
            var user = (ApplicationUser)null;
            var request = (ApplicationRequestEntity)null;
            var db = new ApplicationDbContext();
            var mailMerges = new Dictionary<string, string>();
            var mailMergeHookTranslates = new Dictionary<string, string>();
            var sendToEmail = string.Empty;
            var ccToEmails = (List<string>)null;
            var isAgentConfirmPayment = false;

            // ดึงข้อมูล Application
            var app = db.Applications.Where(w => w.ApplicationID == model.ApplicationID).SingleOrDefault();
            if (app == null)
            {
                CaptureError(new BizPortalException(Resources.Application.MSG_NOT_FOUND, System.Net.HttpStatusCode.NotFound), true, null, model);
            }

            // ดึงข้อมูลใบคำร้อง กับ ข้อมูล user ที่มาจาก API ภายนอกกับ login ภายใน
            if (Request.Headers.Contains("Consumer-Key"))
            {
                if (!isTestMode)
                {
                    string requestedConsumer = Request.Headers.GetValues("Consumer-Key").FirstOrDefault();
                    if (app.ConsumerKey == null || Guid.Parse(requestedConsumer) != app.ConsumerKey)
                    {
                        CaptureError(new BizPortalException(Resources.Application.MSG_SERVICE_IS_NOT_ALLOWED), true, null, model);
                    }
                }

                if (model.ApplicationRequestID == null)
                {
                    CaptureError(new BizPortalException(Resources.Application.MSG_REQUEST_NOT_FOUND, System.Net.HttpStatusCode.NotFound), true, null, model);
                }

                if (!ApplicationRequestEntity.HasRequest(model.ApplicationRequestID.Value, _apiStatusAllowed))
                {
                    CaptureError(new BizPortalException(Resources.Application.MSG_REQUEST_NOT_FOUND, System.Net.HttpStatusCode.NotFound), true, null, model);
                }

                request = ApplicationRequestEntity.Get(model.ApplicationRequestID.Value, model.ApplicationID, model.IdentityID);

                if (request == null)
                {
                    CaptureError(new BizPortalException(Resources.Application.MSG_REQUEST_NOT_FOUND, System.Net.HttpStatusCode.NotFound), true, null, model);
                }

                if (model.Status == ApplicationStatusV2Enum.DRAFT || ((request.Status != ApplicationStatusV2Enum.INCOMPLETE && request.Status != ApplicationStatusV2Enum.WAITING) && model.Status == ApplicationStatusV2Enum.WAITING))
                {
                    CaptureError(new BizPortalException(Resources.Application.MSG_INVALID_STATUS), true, null, model);
                }

                hookStage = AppsStage.ApiUpdate;
                model.ReplyFromOrg = true;
                model.ReplyFromApiUpdate = true;
                user = db.Users.Where(w => w.JuristicID == request.IdentityID || w.CitizenID == request.IdentityID).FirstOrDefault();
            }
            else if (identity.IsAuthenticated)
            {
                string uid = User.Identity.GetUserId();
                user = db.Users.Where(o => o.Id == uid).Single();

                if (!UserManager.IsInRole(uid, ConfigurationValues.ROLES_ORG_AGENT_NAME) && !UserManager.IsInRole(uid, ConfigurationValues.ROLES_ADMIN_NAME))
                {
                    // User ผู้ประกอบการ
                    model.IdentityID = IdentityID;
                    if (string.IsNullOrEmpty(model.IdentityName))
                    {
                        model.IdentityName = IdentityFullName;
                    }
                    model.IdentityType = IdentityType;
                    model.EmailMessage = null;
                    model.SmsMessage = null;
                    model.RequestedFiles = null;
                    model.ReplyFromOrg = false;

                    if (model.ApplicationRequestID != null)
                    {
                        var entity = ApplicationRequestEntity.Get(model.ApplicationRequestID.Value);
                        if (entity == null)
                        {
                            CaptureError(new BizPortalException(Resources.Application.MSG_REQUEST_NOT_FOUND, System.Net.HttpStatusCode.NotFound), true, null, model);
                        }
                        if (entity.Status == ApplicationStatusV2Enum.FAILED && entity.StatusOther == ApplicationStatusOtherValueConst.RESENDABLE)
                        {
                            hookStage = AppsStage.UserCreate;

                        }
                        else
                        {
                            hookStage = AppsStage.UserUpdate;
                            request = entity;
                        }
                    }
                    else if (model.Status == ApplicationStatusV2Enum.WAITING || model.Status == ApplicationStatusV2Enum.CHECK)
                    {
                        hookStage = AppsStage.UserCreate;

                        if (!app.MultipleRequestSupport && ApplicationRequestEntity.HasRequest(app.ApplicationID, model.IdentityID, _singleRequestStatusID))
                        {
                            CaptureError(new BizPortalException(Resources.Application.MSG_MUTI_REQUEST_NOT_ALLOWED), true, null, model);
                        }
                    }
                }
                else
                {
                    // User เจ้าหน้าที่
                    var entity = ApplicationRequestEntity.Get(model.ApplicationRequestID.Value);
                    if (entity == null)
                    {
                        CaptureError(new BizPortalException(Resources.Application.MSG_REQUEST_NOT_FOUND, System.Net.HttpStatusCode.NotFound), true, null, model);
                    }

                    request = entity;
                    hookStage = AppsStage.AgentUpdate;
                    model.IdentityID = user.CitizenID;
                    model.IdentityName = user.UserName;
                    model.IdentityType = IdentityType;
                    model.ReplyFromOrg = true;
                }
            }
            else
            {
                CaptureError(new BizPortalException(Resources.Application.MSG_INVALID_REQUEST), true, null, model);
            }

            ApplicationStatusV2Enum? nextStatus = null;
            var applicationStatusSequence = !string.IsNullOrEmpty(app.StatusSequence) ? app.StatusSequence.Split(',') : null;
            if (applicationStatusSequence != null && applicationStatusSequence.Length > 0)
            {
                if (applicationStatusSequence.Contains(model.Status.ToString()))
                {
                    for (int i = 0; i < applicationStatusSequence.Length; i++)
                    {
                        if (model.Status.ToString() == applicationStatusSequence[i])
                        {
                            bool isLastStatus = false;
                            if (i == applicationStatusSequence.Length - 1)
                            {
                                isLastStatus = true;
                            }

                            if (!isLastStatus)
                            {
                                nextStatus = applicationStatusSequence[i + 1].ToEnumValue();
                            }
                        }
                    }
                }
            }

            // Validate ข้อมูลตามประเภทของ user และสถานะทีส่งมา
            if (hookStage == AppsStage.AgentUpdate || hookStage == AppsStage.ApiUpdate)
            {
                #region [สำหรับใบคำร้องไฟฟ้า ประปา โทรศัพท์]
                if (_utilityApps.Contains(model.ApplicationID))
                {
                    model.StatusRemark = model.StatusOther;

                    if (model.Status == ApplicationStatusV2Enum.COMPLETED)
                    {
                        model.StatusOther = ApplicationStatusOtherValueConst.DONE;
                    }
                    else if (model.Status == ApplicationStatusV2Enum.REJECTED)
                    {
                        model.StatusOther = ApplicationStatusOtherValueConst.REJECT;
                    }
                    else
                    {
                        model.StatusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
                    }
                }
                #endregion

                if (model.Status != ApplicationStatusV2Enum.COMPLETED && model.Status != ApplicationStatusV2Enum.REJECTED)
                {
                    if (string.IsNullOrEmpty(model.StatusOther))
                    {
                        CaptureError(new BizPortalException("ไม่พบ StatusOther"), true, null, model);
                    }
                    else if (!_apiStatusOtherAllowed.Contains(model.StatusOther))
                    {
                        CaptureError(new BizPortalException("StatusOther ไม่ถูกต้อง"), true, null, model);
                    }
                }

                if (model.Status == ApplicationStatusV2Enum.CHECK)
                {

                }
                else if (model.Status == ApplicationStatusV2Enum.WAITING)
                {

                }
                else if (model.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE)
                {
                    if (model.StatusOther == ApplicationStatusOtherValueConst.WAITING_USER_WORKING)
                    {
                        if (model.IsFee || (model.CGDPayment != null && model.CGDPayment.Catalogs.Count > 0))
                        {
                            if (model.Fee == null)
                            {
                                CaptureError(new BizPortalException("ไม่พบ Fee"), true, null, model);
                            }
                            else if (model.Fee <= decimal.Zero)
                            {
                                CaptureError(new BizPortalException("Fee ไม่ถูกต้อง"), true, null, model);
                            }

                            if (string.IsNullOrEmpty(model.DueDateForPayFee))
                            {
                                CaptureError(new BizPortalException("ไม่พบ DueDateForPayFee"), true, null, model);
                            }

                            if (model.PaymentMethodEnabledChoice == null)
                            {
                                CaptureError(new BizPortalException("ไม่พบ PaymentMethodEnabledChoice"), true, null, model);
                            }
                            else if (model.PaymentMethodEnabledChoice.Where(o => !_paymentMethodAllowed.Contains(o)).Count() > 0)
                            {
                                CaptureError(new BizPortalException("PaymentMethodEnabledChoice ไม่ถูกต้อง"), true, null, model);
                            }

                            if (model.PaymentMethodEnabledChoice.Contains(PaymentMethodValueConst.BILL_PAYMENT))
                            {
                                if (model.AttachBillPayment == null && model.BillPaymentFiles == null && model.CGDPayment == null)
                                {
                                    CaptureError(new BizPortalException("ไม่พบข้อมูล Bill Payment"), true, null, model);
                                }
                            }
                        }

                        if (nextStatus == ApplicationStatusV2Enum.CHECK)
                        {
                            if (model.PermitDeliveryTypeEnabledChoice == null)
                            {
                                CaptureError(new BizPortalException("ไม่พบ PermitDeliveryTypeEnabledChoice"), true, null, model);
                            }
                            else if (model.PermitDeliveryTypeEnabledChoice.Where(o => !_permitDeliveryMethodAllowed.Contains(o)).Count() > 0)
                            {
                                CaptureError(new BizPortalException("PermitDeliveryTypeEnabledChoice ไม่ถูกต้อง"), true, null, model);
                            }
                        }
                    }
                    else if (model.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_PROCESS)
                    {
                        // do nothing
                    }
                    else
                    {
                        CaptureError(new BizPortalException("StatusOther ไม่ถูกต้อง"), true, null, model);
                    }
                }
                else if (model.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE)
                {

                }
                else if (model.Status == ApplicationStatusV2Enum.COMPLETED)
                {
                    if (request.PermitDeliveryType == PermitDeliveryTypeValueConst.BY_MAIL && string.IsNullOrEmpty(model.EMSTrackingNumber))
                    {
                        CaptureError(new BizPortalException("ไม่พบ EMSTrackingNumber"), true, null, model);
                    }
                }

                if (model.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE || (model.RequestedFiles != null && model.RequestedFiles.Count() > 0) || (model.GovFiles != null && model.GovFiles.Count() > 0) || !string.IsNullOrEmpty(model.Remark))
                {
                    model.LastUpdatedFrom = "AGENT";
                }
            }
            else if (hookStage == AppsStage.UserUpdate)
            {
                if (model.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE)
                {
                    if (model.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING)
                    {
                        if (string.IsNullOrEmpty(model.PaymentMethod))
                        {
                            CaptureError(new BizPortalException("ไม่พบ PaymentMethod"), true, null, model);
                        }

                        if (!model.NoDocument)
                        {
                            if (string.IsNullOrEmpty(model.PermitDeliveryType))
                            {
                                CaptureError(new BizPortalException("ไม่พบ PermitDeliveryType"), true, null, model);
                            }
                            else if (model.PermitDeliveryType == PermitDeliveryTypeValueConst.BY_MAIL && string.IsNullOrEmpty(model.PermitDeliveryAddress))
                            {
                                CaptureError(new BizPortalException("ไม่พบ PermitDeliveryAddress"), true, null, model);
                            }
                        }
                    }
                    else
                    {
                        CaptureError(new BizPortalException("StatusOther ไม่ถูกต้อง"), true, null, model);
                    }
                }

                model.LastUpdatedFrom = "USER";
            }

            // Agent only want to update ApplicationRequestNumberAgent, no need to record a transaction.
            if (hookStage == AppsStage.AgentUpdate && model.IsRequestNumberAgent && model.ReplyFromOrg)
            {
                request = ApplicationRequestEntity.Get(model.ApplicationRequestID.Value);
                Service.ApplicationStatusService.UpdateApplicationRequestNumberAgent(request, model, ref response);
                return response;
            }

            // จัดการ status, file upload (ไฟล์แนบ + bill payment), e-license
            if (!model.DisableUpdateStatus.GetValueOrDefault())
            {
                // status
                if (model.ApplicationRequestID != null)
                {
                    var req = ApplicationRequestEntity.Get(model.ApplicationRequestID.Value);
                    if (req.Status == ApplicationStatusV2Enum.COMPLETED || req.Status == ApplicationStatusV2Enum.REJECTED)
                    {
                        if (model.Status != ApplicationStatusV2Enum.COMPLETED && model.Status != ApplicationStatusV2Enum.REJECTED)
                        {
                            CaptureError(new BizPortalException(Resources.Application.MSG_REQUEST_HAS_BEEN_CLOSED), true, null, model);
                        }
                    }
                }

                if (model.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && model.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING)
                {
                    isAgentConfirmPayment = true;
                }

                var trans = model.Adapt<ApplicationRequestTransactionEntity>();

                // Upload attachments to file service
                if (model.Attachments != null && model.Attachments.Length > 0)
                {
                    trans.GovFiles = new List<FileMetadataEntity>();

                    foreach (var file in model.Attachments)
                    {
                        var stream = new System.IO.MemoryStream(Convert.FromBase64String(file.Base64String));
                        var fileInfo = new
                        {
                            Name = file.FileName,
                            ContentType = file.ContentType,
                            Size = stream.Length,
                            IsPublic = false
                        };
                        string fileInfoJson = JsonConvert.SerializeObject(fileInfo);
                        string token = FileHelper.RequestAccessToken(serviceTokenPath, consumerKey, secret, fileInfoJson);

                        var uploaded = await FileHelper.UploadFile(serviceUploadPath, token, consumerKey, secret, fileInfoJson, stream, this);
                        var fileItem = new FileMetadataEntity();
                        TypeAdapter.Adapt<FileItem, FileMetadataEntity>(uploaded, fileItem);

                        if (fileItem.Extras == null)
                        {
                            fileItem.Extras = new Dictionary<string, object>();
                        }

                        if (fileItem.Extras.ContainsKey("OWNER_IDENT_ID"))
                        {
                            fileItem.Extras["OWNER_IDENT_ID"] = model.IdentityID;
                        }
                        else
                        {
                            fileItem.Extras.Add("OWNER_IDENT_ID", model.IdentityID);
                        }

                        trans.GovFiles.Add(fileItem);
                    }
                }

                // Payment
                if (model.PaymentMethodEnabledChoice != null)
                {
                    // validate model before create bill;
                    if (model.BillPaymentTypeForPaymentMethod == "CGD" && model.CGDPayment != null)
                    {
                        trans.BillPaymentFiles = new List<FileMetadataEntity>();

                        var pctrl = new PaymentController();
                        var personalType = (int)CGDPersonType.Citizen;
                        var businessNo = "";
                        var businessName = "";
                        var citizenNo = "";
                        var titleName = "";
                        var firstName = "";
                        var middleName = "";
                        var lastName = "";
                        var mobileNo = "";
                        var email = "";
                        var invoiceEndDate = DateTime.Now.AddDays(30);
                        DateTime.TryParseExact(model.DueDateForPayFee, "dd/MM/yyyy", new CultureInfo(CurrentCulture), DateTimeStyles.None, out invoiceEndDate);

                        if (invoiceEndDate.Year < DateTime.Now.Year)
                        {
                            invoiceEndDate = invoiceEndDate.AddYears(543);
                        }

                        if (request.IdentityType == UserTypeEnum.Juristic)
                        {
                            var requestor = db.Users.Where(e => e.JuristicID == request.IdentityID).FirstOrDefault();
                            personalType = (int)CGDPersonType.Juristic;
                            businessNo = requestor.JuristicID;
                            businessName = requestor.FullName;
                        }
                        else
                        {
                            var requestor = db.Users.Where(e => e.CitizenID == request.IdentityID).FirstOrDefault();
                            personalType = (int)CGDPersonType.Citizen;
                            citizenNo = requestor.CitizenID;
                            titleName = requestor.Prefix == "-" ? "" : requestor.Prefix;
                            firstName = requestor.Firstname;
                            lastName = requestor.Lastname;
                            mobileNo = requestor.PhoneNumber;
                            email = requestor.Email;
                        }

                        var address = GetCGDIdentityAddress(request.IdentityType, request.Adapt<ApplicationRequestViewModel>());
                        var orgInfo = GetOrganizationInfomation(request.OrgCode);
                        var uploaded = await pctrl.GenerateCGDBill(new BillpaymentParamViewModel
                        {
                            ApplicationRequestID = request.ApplicationRequestID,
                            ApplicationRequestNumber = request.ApplicationRequestNumber,
                            PersonType = personalType,
                            CitizenNo = citizenNo,
                            TitleName = titleName,
                            FirstName = firstName,
                            MiddleName = middleName,
                            LastName = lastName,
                            BusinessNo = businessNo,
                            BusinessName = businessName,
                            MobileNo = mobileNo,
                            Email = email,
                            HouseNo = address.Address,
                            BuildingName = address.Building,
                            Moo = address.Moo,
                            Soi = address.Soi,
                            Road = address.Road,
                            Tambon = address.Tumbol,
                            Amphur = address.Amphur,
                            Province = address.Province,
                            TambonCode = address.TumbolID,
                            AmphurCode = address.AmphurID,
                            ProvinceCode = address.ProvinceID,
                            PostCode = address.PostCode,
                            CanSendToEmail = false,
                            RefNo1 = request.ApplicationRequestNumber,
                            InvoiceCreateDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffzzz", new CultureInfo("en")),
                            InvoiceStartDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffzzz", new CultureInfo("en")),
                            InvoiceEndDate = invoiceEndDate.ToString("yyyy-MM-ddTHH:mm:ss.ffzzz", new CultureInfo("en")),
                            OrgNameTH = !string.IsNullOrEmpty(orgInfo.OrgNameTH) ? orgInfo.OrgNameTH : "",
                            OrgNameEN = !string.IsNullOrEmpty(orgInfo.OrgNameEN) ? orgInfo.OrgNameEN : "",
                            OrgPhoneNumber = "-",
                            UserName = "admin", //model.CGDPayment.UserName,
                            Password = "1234", //model.CGDPayment.Password,
                            PaymentCatalog = model.CGDPayment.Catalogs
                        });
                        var fileItem = uploaded.fileMetadata.Adapt<FileMetadataEntity>();

                        if (fileItem.Extras == null)
                        {
                            fileItem.Extras = new Dictionary<string, object>();
                        }

                        if (fileItem.Extras.ContainsKey("OWNER_IDENT_ID"))
                        {
                            fileItem.Extras["OWNER_IDENT_ID"] = request.IdentityID;
                        }
                        else
                        {
                            fileItem.Extras.Add("OWNER_IDENT_ID", request.IdentityID);
                        }

                        if (fileItem.Extras.ContainsKey("PAYMENT_GATEWAY_TYPE"))
                        {
                            fileItem.Extras["PAYMENT_GATEWAY_TYPE"] = model.BillPaymentTypeForPaymentMethod;
                        }
                        else
                        {
                            fileItem.Extras.Add("PAYMENT_GATEWAY_TYPE", model.BillPaymentTypeForPaymentMethod);
                        }

                        if (fileItem.Extras.ContainsKey("PAYMENT_GATEWAY_REF"))
                        {
                            fileItem.Extras["PAYMENT_GATEWAY_REF"] = uploaded.paymentGatewayRef;
                        }
                        else
                        {
                            fileItem.Extras.Add("PAYMENT_GATEWAY_REF", uploaded.paymentGatewayRef);
                        }

                        trans.BillPaymentFiles.Add(fileItem);
                    }
                    else if (model.ReplyFromApiUpdate && model.AttachBillPayment != null && model.AttachBillPayment.Length > 0)
                    {
                        trans.BillPaymentFiles = new List<FileMetadataEntity>();

                        // Upload attachments to file service
                        foreach (var bill in model.AttachBillPayment)
                        {
                            var stream = new System.IO.MemoryStream(System.Convert.FromBase64String(bill.Base64String));
                            var fileInfo = new
                            {
                                Name = bill.FileName,
                                ContentType = bill.ContentType,
                                Size = stream.Length,
                                IsPublic = false
                            };
                            string fileInfoJson = JsonConvert.SerializeObject(fileInfo);
                            string token = FileHelper.RequestAccessToken(serviceTokenPath, consumerKey, secret, fileInfoJson);

                            var uploaded = await FileHelper.UploadFile(serviceUploadPath, token, consumerKey, secret, fileInfoJson, stream, this);
                            var fileItem = uploaded.Adapt<FileMetadataEntity>();

                            if (fileItem.Extras == null)
                            {
                                fileItem.Extras = new Dictionary<string, object>();
                            }

                            if (fileItem.Extras.ContainsKey("OWNER_IDENT_ID"))
                            {
                                fileItem.Extras["OWNER_IDENT_ID"] = request.IdentityID;
                            }
                            else
                            {
                                fileItem.Extras.Add("OWNER_IDENT_ID", request.IdentityID);
                            }

                            trans.BillPaymentFiles.Add(fileItem);
                        }
                    }
                    else if (!model.ReplyFromApiUpdate && model.BillPaymentFiles != null && model.BillPaymentFiles.Length > 0)
                    {
                        foreach (var file in model.BillPaymentFiles)
                        {
                            if (file.Extras == null)
                            {
                                file.Extras = new Dictionary<string, string>();
                            }

                            if (file.Extras.ContainsKey("OWNER_IDENT_ID"))
                            {
                                file.Extras["OWNER_IDENT_ID"] = request.IdentityID;
                            }
                            else
                            {
                                file.Extras.Add("OWNER_IDENT_ID", request.IdentityID);
                            }

                            trans.BillPaymentFiles.Add(file.Adapt<FileMetadataEntity>());
                        }
                    }
                    else
                    {
                        if (model.PaymentMethodEnabledChoice.Contains(PaymentMethodValueConst.BILL_PAYMENT))
                        {
                            throw new Exception("ไม่พบข้อมูล Bill payment");
                        }
                    }
                }

                // E-License
                if (!string.IsNullOrEmpty(app.AppsHookClassName))
                {
                    var hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", app.AppsHookClassName).Unwrap();
                    var currentSignings = model.ApplicationRequestID.HasValue ? EDocumentEntity.GetAll(model.ApplicationRequestID.Value) : null;

                    if (model.ELicenses != null && hook != null)
                    {
                        // ลบ singing เดิม
                        if (model.ELicenses.Any(e => e.SigningStatus != EDocumentStatus.COMPLETED.ToString()) && currentSignings != null)
                        {
                            foreach (var currentSigning in currentSignings)
                            {
                                currentSigning.SigningStatus = EDocumentStatus.DELETED;
                                currentSigning.UpdatedDate = DateTime.Now;
                                currentSigning.Update();
                            }
                        }

                        foreach (var eLicense in model.ELicenses)
                        {
                            // update ข้อมูลที่เจ้าหน้าที่กรอกก่อนการ render
                            if (eLicense.SigningPersons != null && eLicense.SigningPersons.Count > 0)
                            {
                                var i = 0;
                                foreach (var data in eLicense.SigningPersons)
                                {
                                    request = request.AddExtraData("ELICENSE_INFORMATION", "Signer_" + i, data.FirstName + " " + data.LastName);
                                    request = request.AddExtraData("ELICENSE_INFORMATION", "SignerDecription_" + i, data.Position);
                                    i++;
                                }

                                request.Update();
                            }

                            if (eLicense.SigningExtendedDatas != null && eLicense.SigningExtendedDatas.Count > 0)
                            {
                                foreach (var data in eLicense.SigningExtendedDatas)
                                {
                                    request = request.AddExtraData("ELICENSE_INFORMATION", data.Name, data.Value);
                                }

                                request.Update();
                            }

                            // manage e-license
                            if (eLicense.SigningStatus == EDocumentStatus.COMPLETED.ToString())
                            {
                                // ถ้าเป็น sign แบบบุคคลแต่ใช้ cert องค์กรณ์ต้องทำการ ส่ง sign ด้วย
                                if (eLicense.SigningType == EDocumentType.OrgByPerson.ToString())
                                {
                                    var edCtrl = new BizPortal.Utils.EDocument(request.ApplicationID);
                                    var signingDocument = EDocumentEntity.Get(request.ApplicationRequestID);
                                    var signatureId = edCtrl.OrganizationSigning(signingDocument.DocumentID);

                                    if (!string.IsNullOrEmpty(signatureId))
                                    {
                                        signingDocument.SigningStatus = EDocumentStatus.COMPLETED;
                                    }
                                    else
                                    {
                                        throw new Exception("ไม่สามารถลงนามเอกสารแบบหน่วยงานได้ กรุณาลองใหม่อีกครั้ง");
                                    }
                                }
                            }
                            else
                            {
                                // เพิ่ม signing ใหม่
                                if (!string.IsNullOrEmpty(eLicense.TemplateID))
                                {
                                    var edCtrl = new BizPortal.Utils.EDocument(request.ApplicationID);

                                    // กรณีที่หน่วยงานใช้เอกสารตัวเองไม่ต้องดึงข้อมูลมาใส่ ไม่ต้องส่ง sign รองรับจาก API ด้วย
                                    if (!string.IsNullOrEmpty(eLicense.Url) || eLicense.Attachment != null)
                                    {
                                        var documentTemplateId = string.IsNullOrEmpty(eLicense.TemplateID) ? null : eLicense.TemplateID;
                                        var documentId = string.IsNullOrEmpty(eLicense.DocumentID) ? null : eLicense.DocumentID;
                                        var documentName = string.IsNullOrEmpty(eLicense.Name) ? null : eLicense.Name;
                                        var documentUrl = string.IsNullOrEmpty(eLicense.Url) ? null : eLicense.Url;

                                        if (eLicense.Attachment != null)
                                        {
                                            documentId = edCtrl.RegisterDocument(eLicense.TemplateID, eLicense.Attachment.FileName, eLicense.Attachment.ContentType, eLicense.Attachment.Base64String);
                                        }
                                        else if (!string.IsNullOrEmpty(eLicense.Url))
                                        {
                                            documentId = Guid.NewGuid().ToString();
                                        }

                                        var signingDocument = new EDocumentEntity
                                        {
                                            ApplicationID = request.ApplicationID,
                                            ApplicationRequestID = model.ApplicationRequestID.Value,
                                            TemplateID = documentTemplateId,
                                            DocumentID = documentId,
                                            DocumentName = documentName,
                                            DocumentUrl = documentUrl,
                                            SigningType = EDocumentType.NotSign,
                                            PersonalSigners = new List<EDocumentPersonalSigner>(),
                                            SigningStatus = EDocumentStatus.COMPLETED,
                                            CreatedDate = DateTime.Now,
                                            UpdatedDate = DateTime.Now
                                        };

                                        signingDocument.Create();
                                    }
                                    else
                                    {
                                        var eLicenseData = hook.GenerateELicenseData(request.ApplicationRequestID);
                                        var signers = eLicense.SigningPersons.Adapt<List<SigningPerson>>();

                                        if (eLicenseData != null)
                                        {
                                            var documentTemplateId = !string.IsNullOrEmpty(eLicense.TemplateID) ? eLicense.TemplateID : null;
                                            var documentId = !string.IsNullOrEmpty(eLicense.DocumentID) ? eLicense.DocumentID : edCtrl.RenderDocument(eLicense.TemplateID, eLicenseData, signers);
                                            var documentName = !string.IsNullOrEmpty(eLicense.Name) ? eLicense.Name : null;

                                            Enum.TryParse(eLicense.SigningType, out EDocumentType signingType);

                                            var signingDocument = new EDocumentEntity
                                            {
                                                ApplicationID = request.ApplicationID,
                                                ApplicationRequestID = model.ApplicationRequestID.Value,
                                                TemplateID = documentTemplateId,
                                                DocumentID = documentId,
                                                DocumentName = documentName,
                                                SigningType = signingType,
                                                PersonalSigners = new List<EDocumentPersonalSigner>(),
                                                SigningStatus = EDocumentStatus.NEW,
                                                CreatedDate = DateTime.Now,
                                                UpdatedDate = DateTime.Now
                                            };

                                            if (eLicense.SigningType == EDocumentType.Personal.ToString() || eLicense.SigningType == EDocumentType.OrgByPerson.ToString())
                                            {
                                                foreach (var item in eLicense.SigningPersons)
                                                {
                                                    signingDocument.PersonalSigners.Add(new EDocumentPersonalSigner
                                                    {
                                                        SigningOrder = item.Order,
                                                        IdentityID = item.CitizenID,
                                                        IdentityName = item.FirstName + "" + item.LastName,
                                                        IdentityFirstName = item.FirstName,
                                                        IdentityLastName = item.LastName,
                                                        IdentityPosition = item.Position,
                                                        SignatureLeft = item.Left,
                                                        SignatureBottom = item.Bottom,
                                                        SignatureHeight = item.Height,
                                                        SignatureWidth = item.Width,
                                                        SignatureBase64 = item.SignatureBase64,
                                                        PersonalSigningStatus = PersonalSigningStatus.NEW
                                                    });
                                                }
                                            }
                                            else
                                            {
                                                var signatureId = edCtrl.OrganizationSigning(documentId);

                                                if (!string.IsNullOrEmpty(signatureId))
                                                {
                                                    signingDocument.SigningStatus = EDocumentStatus.COMPLETED;
                                                }
                                                else
                                                {
                                                    throw new Exception("ไม่สามารถลงนามเอกสารแบบหน่วยงานได้ กรุณาลองใหม่อีกครั้ง");
                                                }
                                            }

                                            signingDocument.Create();
                                        }
                                        else
                                        {
                                            throw new Exception("ไม่สามารถลงนามเอกสารแบบหน่วยงานได้ เนื่องจากไม่พบข้อมูล e-license");
                                        }
                                    }
                                }
                            }
                        }

                        isAgentConfirmPayment = false;
                    }
                    else
                    {
                        if (currentSignings != null && currentSignings.Count > 0)
                        {
                            isAgentConfirmPayment = false;
                        }
                    }
                }

                if (model.EmailMessage != null && model.EmailMessage.Attachments != null && model.EmailMessage.Attachments.Length > 0)
                {
                    trans.EmailMessage.Attachments = new List<FileMetadataEntity>();
                    if (trans.GovFiles == null)
                    {
                        trans.GovFiles = new List<FileMetadataEntity>();
                    }

                    // Upload attachments to file service
                    foreach (var file in model.EmailMessage.Attachments)
                    {
                        var stream = new System.IO.MemoryStream(System.Convert.FromBase64String(file.Base64String));
                        var fileInfo = new
                        {
                            Name = file.FileName,
                            ContentType = file.ContentType,
                            Size = stream.Length,
                            IsPublic = false
                        };
                        string fileInfoJson = JsonConvert.SerializeObject(fileInfo);
                        string token = FileHelper.RequestAccessToken(serviceTokenPath, consumerKey, secret, fileInfoJson);

                        var uploaded = await FileHelper.UploadFile(serviceUploadPath, token, consumerKey, secret, fileInfoJson, stream, this);
                        var fileItem = new FileMetadataEntity();
                        TypeAdapter.Adapt<FileItem, FileMetadataEntity>(uploaded, fileItem);
                        trans.EmailMessage.Attachments.Add(fileItem);
                        trans.GovFiles.Add(fileItem);
                    }
                }

                if (hookStage == AppsStage.UserCreate)
                {
                    var vm = DB.Applications.Where(o => !o.IsDeleted && o.ApplicationID == trans.ApplicationID)
                                .GroupJoin(DB.ApplicationTranslations.Where(o => o.LanguageID == DB.CurrentLanguageID), l => l.ApplicationID, r => r.ApplicationID, (l, r) => new { Application = l, Translation = r.FirstOrDefault() })
                                .GroupJoin(DB.OrganizationTranslations, l => l.Application.OrgCode, r => r.OrgCode, (l, r) => new { Application = l.Application, Translation = l.Translation, OrgTranslation = r.FirstOrDefault() })
                                .Select(o => new
                                {
                                    AppType = o.Application.ApplicationType,
                                    AppSysName = o.Application.ApplicationSysName,
                                    AppName = o.Translation != null ? o.Translation.ApplicationName : o.Application.ApplicationSysName,
                                    OrgCode = o.Application.OrgCode,
                                    OrganizationName = o.OrgTranslation != null ? o.OrgTranslation.OrgName : o.Application.Organization.OrgSysName,
                                    OrgAddress = o.OrgTranslation != null ? o.OrgTranslation.Address : "",
                                    NoDocument = o.Application.NoDocument
                                }).FirstOrDefault();

                    trans.PermitName = vm.AppName;
                    trans.OrgNameTH = vm.OrganizationName;
                    trans.OrgAddress = vm.OrgAddress;
                    trans.OrgCode = vm.OrgCode;
                    trans.AppSysName = vm.AppSysName;
                    trans.ApplicationType = vm.AppType;
                    trans.NoDocument = vm.NoDocument;

                    var business = DB.BusinessGroups.Where(x => x.BusinessSysName == model.Business).FirstOrDefault();
                    if (business != null)
                    {
                        trans.BusinessId = business.BusinessSysName;
                        trans.BusinessName = business.BusinessNameTH;
                    }
                }

                #region Generate Receipt
                //if (trans.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE)
                //{
                //    if (!string.IsNullOrEmpty(app.AppsHookClassName))
                //    {
                //        var hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", app.AppsHookClassName).Unwrap();
                //        trans = hook.GenerateBizReceipt(request, trans, user);
                //    }
                //}
                #endregion

                request = trans.Save();
            }
            else
            {
                request = ApplicationRequestEntity.Get(model.ApplicationRequestID.Value);
            }

            // Generate new request number
            if (hookStage == AppsStage.UserCreate && model.Status != ApplicationStatusV2Enum.DRAFT)
            {
                request.GenerateRequestNumber();
            }

            model.DisabledSendingSystemEmail = false;

            // App hook
            if (!string.IsNullOrEmpty(app.AppsHookClassName))
            {
                var applicationRequest = request.Adapt<ApplicationRequestEntity, ApplicationRequestViewModel>();
                var hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", app.AppsHookClassName).Unwrap();

                if (!model.DisableUpdateStatus.GetValueOrDefault())
                {
                    var invokeResult = hook.Invoke(hookStage, applicationRequest, request.AppHookInfo, ref request);
                    if (invokeResult.Success)
                    {
                        // Update AppHookInfo
                        if (invokeResult.Data != null)
                        {
                            request.AppHookInfo = invokeResult.Data;
                        }

                        // Update FileUploaded
                        if (applicationRequest.UploadedFiles != null)
                        {
                            foreach (var group in applicationRequest.UploadedFiles)
                            {
                                if (group == null || group.Files == null)
                                {
                                    continue;
                                }

                                var groupDB = request.UploadedFiles.Where(o => o.FileGroupID == group.FileGroupID).SingleOrDefault();
                                if (groupDB != null)
                                {
                                    TypeAdapter.Adapt<FileGroup, FileGroupEntity>(group, groupDB);
                                    groupDB.Update();
                                }
                            }
                        }

                        request.Update();
                        response.Type = ResultDataType.Success;
                        response.Message = Resources.Global.RESPONSE_MSG_SUCCESS;
                    }
                    else if (!invokeResult.Success)
                    {
                        CaptureError(new BizPortalException(invokeResult.Message), false, new Dictionary<string, object>()
                        {
                            { "HookResultType", response.Type.ToString() },
                            { "HookResultData", response.Type.ToString() }
                        }, model);

                        if (invokeResult.Data != null)
                        {
                            request.AppHookInfo = invokeResult.Data;
                        }

                        if (hookStage == AppsStage.UserCreate)
                        {
                            request.Status = ApplicationStatusV2Enum.FAILED;
                            request.StatusOther = ApplicationStatusOtherValueConst.RESENDABLE;
                        }
                        request.Update();
                        response.Type = ResultDataType.Error;
                        response.Message = invokeResult.Message;
                    }

                    model.DisabledSendingSystemEmail = invokeResult.DisabledSendingSystemEmail;
                    sendToEmail = invokeResult.SendToEmail;
                    ccToEmails = invokeResult.CcToEmails;

                    if (string.IsNullOrEmpty(sendToEmail))
                    {
                        sendToEmail = db.Users.Where(x => x.Id == CurrentUserID).Select(x => x.Email).SingleOrDefault();
                    }

                    if (isTestMode)
                    {
                        response.Data = request;
                    }
                    else
                    {
                        response.ApplicationRequestData.Add("ApplicationRequestID", request.ApplicationRequestID);
                        response.ApplicationRequestData.Add("IdentityID", request.IdentityID);
                    }
                }

                mailMergeHookTranslates = hook.TranslateKeyValue(applicationRequest);
            }
            else
            {
                // ถ้าไม่มี AppsHookClassName จะต้อง update request ด้วย
                request.Update();
                response.Type = ResultDataType.Success;
                response.Message = Resources.Global.RESPONSE_MSG_SUCCESS;
                if (model.ApplicationID == 1 || model.ApplicationID == 2)
                {
                    response.ApplicationRequestData.Add("Status", request.Status);
                }
            }

            #region [Send Email]

            // Send Email
            mailMerges.Add("{{BIZ_NUMBER}}", request.ApplicationRequestNumber);
            mailMerges.Add("{{IDENTITY_ID}}", request.IdentityID);

            if (request.IdentityType == UserTypeEnum.Juristic)
            {
                mailMerges.Add("{{IDENTITY_NAME}}", string.Format(Resources.ApplicationStatusRequests.JURISTIC_PERSON, request.IdentityName));
            }

            if (request.IdentityType == UserTypeEnum.Citizen)
            {
                mailMerges.Add("{{IDENTITY_NAME}}", string.Format(Resources.ApplicationStatusRequests.KHUN, request.IdentityName));
                mailMerges.Add("{{DETAIL_URL}}", ConfigurationManager.AppSettings["ServicesDomain"] + "/th/Track/Detail/" + request.ApplicationRequestID.ToString() + "?Citizen=true");
                mailMerges.Add("{{DETAIL_URL_EN}}", ConfigurationManager.AppSettings["ServicesDomain"] + "/en/Track/Detail/" + request.ApplicationRequestID.ToString() + "?Citizen=true");
            }
            else
            {
                mailMerges.Add("{{DETAIL_URL}}", ConfigurationManager.AppSettings["ServicesDomain"] + "/th/Track/Detail/" + request.ApplicationRequestID.ToString());
                mailMerges.Add("{{DETAIL_URL_EN}}", ConfigurationManager.AppSettings["ServicesDomain"] + "/en/Track/Detail/" + request.ApplicationRequestID.ToString());
            }

            if (mailMergeHookTranslates != null)
            {
                foreach (var trn in mailMergeHookTranslates)
                {
                    mailMerges["{{" + trn.Key + "}}"] = trn.Value;
                }
            }

            if (string.IsNullOrEmpty(user.Email) && mailMerges.ContainsKey("{{CONTACT_EMAIL}}"))
            {
                user.Email = mailMerges["{{CONTACT_EMAIL}}"];
            }

            if (ccToEmails == null)
            {
                ccToEmails = new List<string>();
            }

            //if (mailMerges.ContainsKey("{{CONTACT_EMAIL}}") && !ccToEmails.Contains(mailMerges["{{CONTACT_EMAIL}}"]))
            //{
            //    ccToEmails.Add(mailMerges["{{CONTACT_EMAIL}}"]);
            //} 

            if (!model.DisabledSendingSystemEmail && model.EmailMessage == null && user != null && !string.IsNullOrEmpty(user.Email))
            {
                // System sending email
                if (hookStage == AppsStage.UserCreate)
                {
                    // ส่งอีเมลหน้าเจ้าหน้าที่ กรณียื่นคำร้อง
                    EmailHelper.SendCreateRequestEmailToAgent(getAgentEmails(request), new StatusUpdateEmailToAgentModel
                    {
                        OrgName = request.OrgNameTH,
                        PermitName = request.PermitName,
                        RequestNumber = request.ApplicationRequestNumber,
                        Status = ResourceHelper.GetResourceWord("STATUS_" + request.Status, "ApplicationStatusRequests", CurrentCulture),
                        StatusOther = request.GetStatusEmailOtherText(),
                        ViewUrl = Url.ServiceAction("Detail", "BackOffice/ApplicationStatus", "", request.ApplicationRequestID.ToString())
                    });
                }
                else if (hookStage == AppsStage.UserUpdate)
                {
                    // ส่งอีเมลแจ้งเจ้าหน้าที่ กรณีผู้ประกอบการอัพเดทเอกสารคำร้อง
                    EmailHelper.SendStatusUpdateEmailToAgent(getAgentEmails(request), new StatusUpdateEmailToAgentModel
                    {
                        OrgName = request.OrgNameTH,
                        PermitName = request.PermitName,
                        RequestNumber = request.ApplicationRequestNumber,
                        Status = ResourceHelper.GetResourceWord("STATUS_" + request.Status, "ApplicationStatusRequests", CurrentCulture),
                        StatusOther = request.GetStatusEmailOtherText(),
                        ViewUrl = Url.ServiceAction("Detail", "BackOffice/ApplicationStatus", "", request.ApplicationRequestID.ToString())
                    });
                }
                else if (hookStage == AppsStage.AgentUpdate || hookStage == AppsStage.ApiUpdate)
                {
                    // ช่วง e-doc ไม่ต้องส่ง e-mail
                    if (!(model.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && model.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING && !isAgentConfirmPayment))
                    {


                        // ส่งอีเมลแจ้งผู้ประกอบการ กรณีเจ้าหน้าที่มีการอัพเดทสถานะคำร้อง
                        EmailHelper.SendStatusUpdateEmailToRequestor(sendToEmail, new StatusUpdateEmailModel
                        {
                            RequestorName = request.IdentityName,
                            RequestNumber = request.ApplicationRequestNumber,
                            OrgName = request.OrgNameTH,
                            PermitName = request.PermitName,
                            Status = ResourceHelper.GetResourceWord("STATUS_" + request.Status, "ApplicationStatusRequests", CurrentCulture),
                            StatusOther = request.GetStatusEmailOtherText(),
                            Remark = request.Remark,
                            ViewUrl = Url.ServiceAction("Detail", "Track", "", request.ApplicationRequestID.ToString())
                        });

                    }
                    else
                    {
                        if (model.ELicenses.Any(e => e.SigningPersons != null))
                        {
                            // ส่งอีเมลแจ้งผู้ลงนาม กรณีเจ้าหน้าที่ยื่นคำร้องให้
                            EmailHelper.SendNoticeSigningEmailToSigner(getSignerEmails(request), new StatusUpdateEmailToAgentModel
                            {
                                OrgName = request.OrgNameTH,
                                PermitName = request.PermitName,
                                RequestNumber = request.ApplicationRequestNumber,

                                ViewUrl = Url.ServiceAction("Signing", "apps", "", "")
                            });
                        }
                    }
                }
            }

            // Send Completed Email
            //if (model.Status == ApplicationStatusV2Enum.COMPLETED)
            //{
            //    if (app.IsEnableELicense)
            //    {
            //        model.EmailMessage = new ApplicationStatusEmailMessage
            //        {
            //            Subject = "[Biz Portal] ใบอนุญาตของท่านได้รับการอนุมัติเรียบร้อยแล้ว",
            //            Body = "ใบอนุญาตของท่านได้รับการอนุมัติเรียบร้อยแล้ว ท่านสามารถดูรายละเอียดได้ที่ <a href='" + Url.ServiceAction("Detail", "Track", "", model.ApplicationRequestID.HasValue ? model.ApplicationRequestID.ToString() : "") + "'> คลิก </a>",
            //            IsHtmlBody = true
            //        };
            //    }
            //}

            // Email Message from API and Apphook
            if (model.EmailMessage != null && user != null)
            {
                var msg = model.EmailMessage;

                foreach (var mm in mailMerges)
                {
                    msg.Body = msg.Body.Replace(mm.Key, mm.Value);
                }

                bool sent = false;

                List<MailAddress> ccMailAddrs = new List<MailAddress>();
                foreach (var cmail in ccToEmails)
                {
                    ccMailAddrs.Add(new MailAddress() { Address = cmail });
                }

                if (!string.IsNullOrEmpty(model.EmailMessage.SendToEmail))
                {
                    // ส่งถึงอีเมล์ที่ระบุมาใน model
                    sent = EmailHelper.SendEmailV2(
                         new MailAddress[] { new MailAddress { Name = model.EmailMessage.SendToEmail, Address = model.EmailMessage.SendToEmail } },
                         ccMailAddrs.ToArray(),
                         msg.Subject, msg.Body, msg.IsHtmlBody,
                         null, null, msg.Attachments);
                }
                else if (!string.IsNullOrEmpty(sendToEmail))
                {
                    // ส่งถึงอีเมล์ที่ระบุมาใน apphook
                    sent = EmailHelper.SendEmailV2(
                         new MailAddress[] { new MailAddress { Name = sendToEmail, Address = sendToEmail } },
                         ccMailAddrs.ToArray(),
                         msg.Subject, msg.Body, msg.IsHtmlBody,
                         null, null, msg.Attachments);
                }
                else
                {
                    // ส่งถึง user (ประชาชน/ผู้ประกอบการ) และ cc
                    sent = EmailHelper.SendEmailV2(
                        new MailAddress[] { new MailAddress { Name = user.FullName, Address = user.Email } },
                        ccMailAddrs.ToArray(),
                        msg.Subject, msg.Body, msg.IsHtmlBody,
                        null, null, msg.Attachments);
                }

                if (!sent)
                {
                    response.Type = ResultDataType.SuccessWithErrors;
                    response.ValidationErrors.Add("EmailResult", "Sending email failed.");
                }
            }

            #endregion

            #region [Send SMS]
            // Send SMS
            if (model.SmsMessage != null)
            {
                string trackShortUrlKey = "{{TRACK_SHORT_URL}}";
                string shortUrl = string.Empty;

                if (model.SmsMessage.Message.Contains(trackShortUrlKey))
                {
                    var gClient = new RestClient("https://www.googleapis.com");
                    var gRequest = new RestRequest("urlshortener/v1/url", Method.POST);
                    gRequest.AddHeader("Accept", "application/json");
                    gRequest.AddHeader("Content-Type", "application/json");
                    gRequest.AddParameter("key", ConfigurationManager.AppSettings["googleapi:UrlShortener"], ParameterType.QueryString);
                    if (request.IdentityType == UserTypeEnum.Citizen)
                    {
                        gRequest.AddParameter("application/json", JsonConvert.SerializeObject(new { longUrl = ServerHelper.ResolveServerUrl("~/th/Track/Detail/" + request.ApplicationRequestID.ToString() + "?Citizen=true") }), ParameterType.RequestBody);
                    }
                    else
                    {
                        gRequest.AddParameter("application/json", JsonConvert.SerializeObject(new { longUrl = ServerHelper.ResolveServerUrl("~/th/Track/Detail/" + request.ApplicationRequestID.ToString()) }), ParameterType.RequestBody);
                    }
                    gRequest.Method = Method.POST;

                    // execute the request
                    IRestResponse gResponse = gClient.Execute(gRequest);
                    if (gResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        shortUrl = JObject.Parse(gResponse.Content)["id"].ToString();
                    }

                    model.SmsMessage.Message = model.SmsMessage.Message.Replace(trackShortUrlKey, shortUrl);
                }

                SmsMessageSendingStatus smsResult = model.SmsMessage.SendSms();
                if (smsResult.Status != SendingStatus.Success)
                {
                    response.Type = ResultDataType.SuccessWithErrors;
                    response.ValidationErrors.Add("SmsResult", smsResult);
                }
            }
            #endregion

            return response;
        }

        [HttpPost]
        [Route("Api/V2/Applications/RequestsResend")]
        public async Task<ResponseData<object>> RequestsResend([FromBody] string id)
        {
            Guid ApplicationRequestID = Guid.Parse(id);
            var request = ApplicationRequestEntity.Get(ApplicationRequestID);

            if (request.Status == ApplicationStatusV2Enum.FAILED && request.StatusOther == ApplicationStatusOtherValueConst.RESENDABLE)
            {
                if (request.Transactions != null && request.Transactions.Count > 0)
                {
                    var tran = request.Transactions.First();
                    request.UploadedFiles = tran.UploadedFiles;
                }
                var viewModel = request.Adapt<ApplicationRequestViewModel>();

                viewModel.Status = ApplicationStatusV2Enum.CHECK;
                viewModel.StatusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST;

                return await Requests(viewModel);
            }

            return new ResponseData<object>()
            {
                ApplicationRequestData = new Dictionary<string, object>(),
                Type = ResultDataType.Error,
                Message = Resources.ApplicationStatusRequests.MSG_ADD_FAILED
            };
        }

        /// <summary>
        /// ลบไฟล์
        /// </summary>
        /// <param name="id">Application Request ID</param>
        /// <param name="fileType">File Types: uploaded, gov, request</param>
        /// <param name="fileID"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Api/V2/Applications/Requests/{id}/files/{fileType}/{fileID}/{fileGroupID}")]
        public ResponseData<object> DeleteFile(Guid id, string fileType, string fileID, Guid? fileGroupID)
        {
            ResponseData<object> response = new ResponseData<object>()
            {
                Type = ResultDataType.Error,
                Message = Resources.ApplicationStatusRequests.MSG_REMOVE_FILE_FAILED
            };

            ApplicationRequestEntity request = ApplicationRequestEntity.Get(id);
            if (request == null)
            {
                throw new BizPortalException(Resources.Application.MSG_REQUEST_NOT_FOUND, System.Net.HttpStatusCode.NotFound);
            }

            var db = new ApplicationDbContext();
            // Get Application
            var app = db.Applications.Where(w => w.ApplicationID == request.ApplicationID).SingleOrDefault();
            if (app == null)
            {
                throw new BizPortalException(Resources.Application.MSG_NOT_FOUND, System.Net.HttpStatusCode.NotFound);
            }
            //else if (!app.ConsumerKey.HasValue)
            //{
            //    throw new BizPortalException(Resources.Application.MSG_CONSUMER_KEY_NOT_FOUND);
            //}

            // Get User
            var identity = User.Identity;

            if (Request.Headers.Contains("Consumer-Key"))
            {
                string requestedConsumer = Request.Headers.GetValues("Consumer-Key").FirstOrDefault();
                if (app.ConsumerKey == null || Guid.Parse(requestedConsumer) != app.ConsumerKey)
                {
                    throw new BizPortalException(Resources.Application.MSG_SERVICE_IS_NOT_ALLOWED);
                }
            }
            else if (identity.IsAuthenticated)
            {
                string uid = User.Identity.GetUserId();
                ApplicationUser user = db.Users.Where(o => o.Id == uid).Single();

                if (!UserManager.IsInRole(uid, ConfigurationValues.ROLES_ORG_AGENT_NAME) && !UserManager.IsInRole(uid, ConfigurationValues.ROLES_ADMIN_NAME))
                {
                    if (request.IdentityID != user.CitizenID && request.IdentityID != user.JuristicID)
                    {
                        throw new BizPortalException(Resources.Application.MSG_INVALID_REQUEST);
                    }
                }
            }
            else
            {
                throw new BizPortalException(Resources.Application.MSG_INVALID_REQUEST);
            }

            switch (fileType.ToLower())
            {
                case "uploaded":
                    if (fileGroupID == null)
                    {
                        throw new BizPortalException(Resources.Application.MSG_INVALID_REQUEST);
                    }
                    request.RemoveUploadedFiles(fileGroupID.Value, new string[] { fileID });
                    break;
                case "gov":
                    request.RemoveGovFiles(new string[] { fileID });
                    break;
                case "request":
                    request.RemoveRequestFiles(new string[] { fileID });
                    break;
                case "epermit":
                    request.RemoveEPermitFile(new string[] { fileID });
                    break;
                case "billpayment":
                    request.RemoveBillPaymentFile(new string[] { fileID });
                    break;
            }

            response.Message = Resources.ApplicationStatusRequests.MSG_REMOVE_FILE_SUCCESS;
            response.Type = ResultDataType.Success;

            return response;
        }

        [HttpGet]
        [Route("Api/V2/Applications/Transactions")]
        public ResponseData<object> Transactions(Guid applicationRequestId)
        {
            try
            {
                var request = ApplicationRequestEntity.Get(applicationRequestId);
                var transactions = request.Transactions
                                          .Select(e => new ApplicationTransactionViewModel
                                          {
                                              IsReplyFromOrg = e.ReplyFromOrg || e.ReplyFromApiUpdate,
                                              Remark = e.Remark,
                                              Status = e.Status,
                                              StatusOther = e.StatusOther,
                                              TranctionDate = e.UpdatedDate
                                          })
                                          .OrderBy(e => e.TranctionDate)
                                          .ToList();

                return new ResponseData<object>()
                {
                    Data = transactions,
                    Type = ResultDataType.Success,
                    Message = "Successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<object>()
                {
                    Data = null,
                    Type = ResultDataType.Error,
                    Message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("Api/V2/Applications/ExportRequestData")]
        [Authorize]
        public HttpResponseMessage RequestData(int applicationId, Guid applicationRequestId)
        {
            using (var stream = new MemoryStream())
            {
                try
                {
                    var app = DB.Applications.Where(w => w.ApplicationID == applicationId).SingleOrDefault();
                    var hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", app.AppsHookClassName).Unwrap();
                    var request = hook.GenerateRequestData(applicationRequestId);
                    var requestForm = ApplicationRequestEntity.Get(applicationRequestId);
                    var byteData = Encoding.UTF8.GetBytes(request);

                    stream.Write(byteData, 0, byteData.Length);
                    stream.Position = 0;

                    var result = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(stream.ToArray())
                    };
                    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = $"{app.ApplicationID}-{requestForm.ApplicationRequestNumber}-{requestForm.IdentityID}.txt"
                    };

                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    return result;

                }
                catch (Exception ex)
                {
                    var byteData = Encoding.UTF8.GetBytes($"{{\"Message\":\"ไม่สามารถ Export ข้อมูลคำร้องได้ {ex.ToString()}\"}}");

                    stream.Write(byteData, 0, byteData.Length);
                    stream.Position = 0;

                    var result = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new ByteArrayContent(stream.ToArray())
                    };

                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return result;
                }
            }
        }

        [HttpPost]
        [Route("Api/V2/Applications/Chat")]
        public ResponseData<object> Chat(ChatViewModel model)
        {
            // Validate data model
            if (!ModelState.IsValid)
            {
                CaptureError(new CustomValidationException(ModelState), true);
            }

            var isTestMode = bool.Parse(ConfigurationManager.AppSettings["TestMode"].ToString());
            var identity = User.Identity;
            var user = (ApplicationUser)null;
            var request = (ApplicationRequestEntity)null;
            var db = new ApplicationDbContext();
            var response = new ResponseData<object>()
            {
                Type = ResultDataType.Error,
                Message = Resources.ApplicationStatusRequests.MSG_CHAT_FAILD
            };

            try
            {

                // ดึงข้อมูล Application
                var app = db.Applications.Where(w => w.ApplicationID == model.ApplicationID).SingleOrDefault();
                if (app == null)
                {
                    CaptureError(new BizPortalException(Resources.Application.MSG_NOT_FOUND, System.Net.HttpStatusCode.NotFound), true, null);
                }

                // ดึงข้อมูลใบคำร้อง กับ ข้อมูล user ที่มาจาก API ภายนอกกับ login ภายใน
                if (Request.Headers.Contains("Consumer-Key"))
                {
                    if (!isTestMode)
                    {
                        string requestedConsumer = Request.Headers.GetValues("Consumer-Key").FirstOrDefault();
                        if (app.ConsumerKey == null || Guid.Parse(requestedConsumer) != app.ConsumerKey)
                        {
                            CaptureError(new BizPortalException(Resources.Application.MSG_SERVICE_IS_NOT_ALLOWED), true);
                        }
                    }

                    if (model.ApplicationRequestID == null)
                    {
                        CaptureError(new BizPortalException(Resources.Application.MSG_REQUEST_NOT_FOUND, System.Net.HttpStatusCode.NotFound), true);
                    }

                    if (!ApplicationRequestEntity.HasRequest(model.ApplicationRequestID, _apiStatusAllowed))
                    {
                        CaptureError(new BizPortalException(Resources.Application.MSG_REQUEST_NOT_FOUND, System.Net.HttpStatusCode.NotFound), true);
                    }

                    request = ApplicationRequestEntity.Get(model.ApplicationRequestID, model.ApplicationID, model.ChatUserID);

                    if (request == null)
                    {
                        CaptureError(new BizPortalException(Resources.Application.MSG_REQUEST_NOT_FOUND, System.Net.HttpStatusCode.NotFound), true);
                    }

                    user = db.Users.Where(w => w.JuristicID == request.IdentityID || w.CitizenID == request.IdentityID).FirstOrDefault();
                }
                else if (identity.IsAuthenticated)
                {
                    string uid = User.Identity.GetUserId();
                    user = db.Users.Where(o => o.Id == uid).Single();

                    if (!UserManager.IsInRole(uid, ConfigurationValues.ROLES_ORG_AGENT_NAME) && !UserManager.IsInRole(uid, ConfigurationValues.ROLES_ADMIN_NAME))
                    {
                        // User ผู้ประกอบการ
                        model.ChatUserID = IdentityID;
                        if (string.IsNullOrEmpty(model.ChatUserFullName))
                        {
                            model.ChatUserFullName = IdentityFullName;
                        }
                        model.ChatUserType = IdentityType.GetEnumStringValue();


                        if (model.ApplicationRequestID != null)
                        {
                            var entity = ApplicationRequestEntity.Get(model.ApplicationRequestID);
                            if (entity == null)
                            {
                                CaptureError(new BizPortalException(Resources.Application.MSG_REQUEST_NOT_FOUND, System.Net.HttpStatusCode.NotFound), true);
                            }
                            request = entity;
                        }
                    }
                    else
                    {
                        // User เจ้าหน้าที่
                        var entity = ApplicationRequestEntity.Get(model.ApplicationRequestID);
                        if (entity == null)
                        {
                            CaptureError(new BizPortalException(Resources.Application.MSG_REQUEST_NOT_FOUND, System.Net.HttpStatusCode.NotFound), true);
                        }

                        request = entity;
                        model.ChatUserID = user.CitizenID;
                        model.ChatUserFullName = user.FullName;
                        model.ChatUserType = IdentityType.GetEnumStringValue();
                    }
                }
                else
                {
                    CaptureError(new BizPortalException(Resources.Application.MSG_INVALID_REQUEST), true);
                }

                // Update chat
                if (request.Chats == null)
                {
                    request.Chats = new List<ApplicationRequestChatEntity>();
                }

                request.Chats.Add(new ApplicationRequestChatEntity
                {
                    ChatID = Guid.NewGuid(),
                    ChatUserFullName = model.ChatUserFullName,
                    ChatUserID = model.ChatUserID,
                    ChatUserType = model.ChatUserType,
                    ChatText = model.ChatText,
                    CreateDate = DateTime.Now
                });

                request.Update();
                response.Type = ResultDataType.Success;
                response.Message = Resources.ApplicationStatusRequests.MSG_CHAT_SUCCESS;
                response.Data = request.Chats;

            }
            catch (Exception ex)
            {
                response.Data = ex.ToString();
            }

            return response;
        }

        /// <summary>
        /// SingleForm Request
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/V2/SingleForm/Requests")]
        [Authorize]
        public async Task<ResponseData<object>> SingleFormRequests([FromBody] SingleFormRequestViewModel model)
        {
            var tran = (SingleFormTransaction)null;
            var response = new ResponseData<object>()
            {
                ApplicationRequestData = new Dictionary<string, object>(),
                Type = ResultDataType.Error,
                Message = Resources.ApplicationStatusRequests.MSG_ADD_FAILED
            };
            var db = new ApplicationDbContext();

            //Get User
            var identity = User.Identity;
            if (identity.IsAuthenticated)
            {
                string uid = User.Identity.GetUserId();
                ApplicationUser user = db.Users.Where(o => o.Id == uid).Single();

                if (!UserManager.IsInRole(uid, ConfigurationValues.ROLES_ORG_AGENT_NAME) && !UserManager.IsInRole(uid, ConfigurationValues.ROLES_ADMIN_NAME))
                {
                    model.IdentityID = IdentityID;
                    model.IdentityType = IdentityType;
                }
            }
            else
            {
                throw new Exception(Resources.Global.MSG_WARNING_UNAUTHORIZED);
            }

            var request = SingleFormRequestEntity.Get(model.IdentityID);
            var tmpData = new List<SingleFormSectionDataEntity>();

            if (request == null)
            {
                request = new SingleFormRequestEntity();
                request.Create();
            }
            else
            {
                var sectionList = new List<string>();
                if (model.SectionData != null && model.SectionData.Count > 0)
                {
                    foreach (var modelData in model.SectionData)
                    {
                        sectionList.Add(modelData.SectionName);
                        var oldSectionData = request.SectionData.Where(x => x.SectionName == modelData.SectionName).FirstOrDefault();

                        // Preserve old SectionData if the new form change section type
                        if (modelData.FormData == null)
                        {
                            modelData.FormData = new Dictionary<string, object>();
                        }

                        if (oldSectionData != null)
                        {
                            foreach (var kv in oldSectionData.FormData)
                            {
                                // Preserve old SectionData if the new form doesn't use it
                                if (!modelData.FormData.ContainsKey(kv.Key))
                                {
                                    modelData.FormData[kv.Key] = kv.Value;
                                }
                            }
                        }
                    }
                    tmpData = request.SectionData.Where(o => !sectionList.Contains(o.SectionName)).ToList();
                }
            }
            bool applyDomainRoute = bool.Parse(ConfigurationManager.AppSettings["ApplyDomainRoute"]);
            TypeAdapter.Adapt<SingleFormRequestViewModel, SingleFormRequestEntity>(model, request);

            if (tmpData != null && tmpData.Count > 0)
            {
                request.SectionData.AddRange(tmpData);
            }

            foreach (var section in request.SectionData)
            {
                if (section.Type == SectionType.Form)
                {
                    Dictionary<string, object> temp = new Dictionary<string, object>();
                    foreach (var data in section.FormData)
                    {
                        if (data.Value != null && data.Value.GetType() == typeof(JArray))
                        {
                            JArray arr = (JArray)data.Value;
                            if (data.Key.Contains("_REPEATER"))
                            {
                                temp.Add(data.Key, JsonConvert.SerializeObject(arr.ToObject<object[]>()));
                            }
                            else if (data.Key.Contains("CHECKBOXES"))
                            {
                                temp.Add(data.Key, JsonConvert.SerializeObject(arr.ToObject<string[]>()));
                            }
                        }
                    }

                    if (temp.Count > 0)
                    {
                        foreach (var item in temp)
                        {
                            section.FormData[item.Key] = item.Value;
                        }
                    }
                }
                else if (section.Type == SectionType.ArrayOfForms)
                {
                    foreach (var list in section.ArrayData)
                    {
                        Dictionary<string, object> temp = new Dictionary<string, object>();
                        foreach (var data in list)
                        {
                            if (data.Value != null && data.Value.GetType() == typeof(JArray))
                            {
                                JArray arr = (JArray)data.Value;
                                if (data.Key.Contains("_REPEATER"))
                                {
                                    temp.Add(data.Key, JsonConvert.SerializeObject(arr.ToObject<object[]>()));
                                }
                                else if (data.Key.Contains("CHECKBOXES"))
                                {
                                    temp.Add(data.Key, JsonConvert.SerializeObject(arr.ToObject<string[]>()));
                                }
                            }
                        }

                        if (temp.Count > 0)
                        {
                            foreach (var item in temp)
                            {
                                list[item.Key] = item.Value;
                            }
                        }
                    }
                }

            }

            request.Update();
            response.Type = ResultDataType.Success;

            if (response.Type == ResultDataType.Success)
            {
                response.Data = model;
                if (request.Status == ApplicationStatusV2Enum.DRAFT)
                {
                    response.Message = Resources.ApplicationStatusRequests.MSG_SAVE_DARFT_SUCCESS;
                }
                else if (request.Status == ApplicationStatusV2Enum.WAITING)
                {
                    request.Status = ApplicationStatusV2Enum.DRAFT;
                    request.Update();

                    var dbTrans = MongoFactory.GetSingleFormTransactionCollection();
                    tran = dbTrans.AsQueryable().Where(o => o.IdentityID == IdentityID).SingleOrDefault();
                    if (tran != null)
                    {
                        string[] apps = tran.Apps.ToArray();
                        int[] appIDs = DB.Applications.Where(o => !o.IsDeleted && o.SingleFormEnabled && apps.Contains(o.ApplicationSysName)).Select(o => o.ApplicationID).ToArray();
                        Dictionary<string, ApplicationRequestDataGroupViewModel> data = null;
                        FileGroup[] files = tran.UploadedFiles.Adapt<List<FileGroup>>().ToArray();

                        if (appIDs.Length > 0)
                        {
                            data = new Dictionary<string, ApplicationRequestDataGroupViewModel>();
                            foreach (var sec in request.SectionData)
                            {
                                ApplicationRequestDataGroupViewModel group = new ApplicationRequestDataGroupViewModel()
                                {
                                    GroupName = sec.SectionName,
                                    Data = new Dictionary<string, object>(),
                                    Visible = true
                                };
                                data.Add(group.GroupName, group);

                                if (sec.Type == SectionType.Form)
                                {
                                    foreach (var item in sec.FormData)
                                    {
                                        group.Data.Add(item.Key, item.Value.DefaultString());
                                    }
                                }
                                else if (sec.Type == SectionType.ArrayOfForms)
                                {
                                    group.Data.Add(group.GroupName + "_TOTAL", sec.ArrayData.Count.ToString());
                                    for (int i = 0; i < sec.ArrayData.Count; i++)
                                    {
                                        var form = sec.ArrayData[i];
                                        foreach (var item in form)
                                        {
                                            group.Data.Add(string.Format("{0}_{1}", item.Key, i), item.Value.DefaultString());
                                        }
                                    }
                                }
                            }

                            string uid = User.Identity.GetUserId();
                            var user = db.Users.Where(o => o.Id == uid).Single();
                            string identityName = user.FullName;
                            if (IdentityType == UserTypeEnum.Juristic)
                            {
                                var profile = IdentityHelper.GetJuristicProfile(IdentityID);
                                if (profile != null && profile.HasValues)
                                {
                                    JuristicProfile juristic = profile.ToObject<JuristicProfile>();
                                    identityName = juristic.JuristicName_TH;

                                    Dictionary<string, object> dbd = new Dictionary<string, object>()
                                    {
                                        { "JURISTIC_ID", juristic.JuristicID },
                                        { "JURISTIC_NAME_TH", juristic.JuristicName_TH },
                                        { "JURISTIC_NAME_EN", juristic.JuristicName_EN },
                                        { "JURISTIC_ADDR", juristic.Address },
                                        { "REGIS_CAPTAL", juristic.RegisterCapital.ToString() },
                                        { "REGIS_DATE", juristic.RegisterDate },
                                        { "PAID_REGIS_CAPITAL", juristic.PaidRegisterCapital.ToString() },
                                        { "TYPE", juristic.JuristicType },
                                    };
                                    request.IdentityName = juristic.JuristicName_TH;

                                    if (juristic.CommitteeInformations != null)
                                    {
                                        var total = juristic.CommitteeInformations.Length;

                                        dbd.Add("COMMITTEE_TOTAL", total.ToString());
                                        for (int i = 0; i < total; i++)
                                        {
                                            string idx = i.ToString();
                                            var com = juristic.CommitteeInformations[i];
                                            dbd.Add("COMITTEE_" + i + "_FIRST_NAME", com.FirstName);
                                            dbd.Add("COMITTEE_" + i + "_LAST_NAME", com.LastName);
                                            dbd.Add("COMITTEE_" + i + "_TITLE", com.Title);
                                            dbd.Add("COMITTEE_" + i + "_CITIZEN_ID", com.CitizenID);
                                        }
                                    }

                                    data.Add("DBD", new ApplicationRequestDataGroupViewModel()
                                    {
                                        GroupName = "DBD",
                                        Data = dbd
                                    });
                                }
                            }
                            else if (IdentityType == UserTypeEnum.Citizen)
                            {
                                data.Add("OPENID", new ApplicationRequestDataGroupViewModel()
                                {
                                    GroupName = "OPENID",
                                    Data = new Dictionary<string, object>()
                                    {
                                        { "FULLNAME", IdentityFullName },
                                        { "PREFIX_TH", IdentityPrefix }, // prefix ที่ดึงจาก openid แต่การใช้จริงจะใช้ข้อมูลจากหน้า singleform
                                        { "FIRSTNAME_TH", IdentityFirstname },
                                        { "LASTNAME_TH", IdentityLastname },
                                    }
                                });
                            }

                            Guid batchId = Guid.NewGuid();
                            model.BatchID = batchId;
                            int errorCount = 0;

                            var dbAppFile = MongoFactory.GetSingleFormAppFileCollection().AsQueryable();
                            var dbFileList = MongoFactory.GetSingleFormFileListCollection().AsQueryable();
                            var appSysNames = DB.Applications.Where(o => appIDs.Contains(o.ApplicationID))
                                .Select(a => a.ApplicationSysName)
                                .ToArray();

                            foreach (var appID in appIDs)
                            {
                                var app = DB.Applications.Where(o => o.ApplicationID == appID).Single();
                                var appName = app.ApplicationSysName;
                                var duration = request.IdentityType == UserTypeEnum.Citizen ? app.CitizenOperatingDays : app.OperatingDays;
                                decimal? fee = null;
                                decimal emsFee = 0;
                                bool permitCanBeDeliveredOnPayment = false;
                                if (!string.IsNullOrEmpty(app.AppsHookClassName))
                                {
                                    IAppsHook hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", app.AppsHookClassName).Unwrap();
                                    fee = hook.CalculateFee(request.SectionData.Cast<ISectionData>().ToList());
                                    emsFee = hook.CalculateEMSFee(request.SectionData.Cast<ISectionData>().ToList());
                                    permitCanBeDeliveredOnPayment = hook.PermitCanBeDeliveredOnPayment;
                                }
                                else
                                {
                                    fee = null;
                                }

                                var appFiles = dbAppFile.Where(f => f.AppSysName == appName).SelectMany(f => f.Files).Distinct().ToList();
                                var fileList = dbFileList.Where(f => appFiles.Contains(f.FileName)).ToList();
                                fileList.Add(dbFileList.Where(o => o.FileName == "FREE_DOC").SingleOrDefault());
                                var fileGroups = fileList.Select(f => f.FileGroup).Distinct().ToList();
                                var appFilteredGroups = new List<FileGroup>();
                                foreach (var g in fileGroups)
                                {
                                    var targetGroups = files.Where(o => o.Description == g).ToList();
                                    foreach (var targetGroup in targetGroups)
                                    {
                                        if (targetGroup != null)
                                        {
                                            var addedGroup = new FileGroup();
                                            addedGroup.Description = targetGroup.Description;
                                            addedGroup.Files = new List<FileMetadata>();
                                            addedGroup.Extras = targetGroup.Extras;

                                            addedGroup.CreatedDate = targetGroup.CreatedDate;
                                            addedGroup.UpdatedDate = targetGroup.UpdatedDate;

                                            foreach (var f in fileList.Where(f => f.FileGroup == g))
                                            {
                                                Regex reg = new Regex("^" + f.FileName + "([\\-_]([0-9]+))*$");

                                                var targetFiles = targetGroup.Files.Where(o => reg.IsMatch(o.FileTypeCode)).ToList();

                                                foreach (var tf in targetFiles)
                                                {
                                                    addedGroup.Files.Add(tf);
                                                }
                                            }
                                            if (addedGroup.Files.Count > 0)
                                            {
                                                appFilteredGroups.Add(addedGroup);
                                            }
                                        }
                                    }
                                }

                                ApplicationRequestViewModel appRequest = new ApplicationRequestViewModel()
                                {
                                    IdentityName = request.IdentityName,
                                    ApplicationID = appID,
                                    Status = ApplicationStatusV2Enum.CHECK,
                                    StatusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST,
                                    Data = data,
                                    UploadedFiles = appFilteredGroups.ToArray(),
                                    RequestBatchID = batchId,
                                    PermitCanBeDeliveredOnPayment = permitCanBeDeliveredOnPayment,
                                    Fee = fee,
                                    EMSFee = emsFee,
                                    Duration = duration,
                                    SourceIPAddress = System.Web.HttpContext.Current.Request.UserHostAddress,
                                    Business = tran.BusinessId,
                                    ConsentDateTimeStamp = tran.ConsentTimeStamp,
                                };

                                var result = await Requests(appRequest);
                                if (result.Type == ResultDataType.Success)
                                {
                                    if (IdentityType == UserTypeEnum.Citizen && appID == 7)
                                    {
                                        var totRequest = MongoFactory.GetApplicationRequestCollection().AsQueryable().Where(o => o.IdentityID == IdentityID && o.ApplicationID == 7).OrderByDescending(o => o.CreatedDate).FirstOrDefault();
                                        if (totRequest != null && totRequest.Data.ContainsKey("TOT_RESPONSE_DATA"))
                                        {
                                            var totUrl = totRequest.Data["TOT_RESPONSE_DATA"].Data["RESULT_URL"];
                                            response.ApplicationRequestData.Add(appName, new string[] { ResultDataType.Success.ToString(), totUrl });
                                        }
                                    }
                                    else if (IdentityType == UserTypeEnum.Juristic && appID == 9)
                                    {
                                        var vatUrl = string.Format("{0}{1}", ConfigurationManager.AppSettings["VAT_PRINT_PP01"], IdentityID);
                                        response.ApplicationRequestData.Add(appName, new string[] { ResultDataType.Success.ToString(), "", vatUrl });
                                    }
                                    else
                                    {
                                        response.ApplicationRequestData.Add(appName, new string[] { ResultDataType.Success.ToString() });
                                    }

                                    // Send mail here (If request is success)

                                }
                                else
                                {
                                    response.ApplicationRequestData.Add(appName, new string[] { ResultDataType.Error.ToString() });
                                    errorCount++;
                                }
                            }
                            if (!string.IsNullOrEmpty(user.Email))
                            {
                                var confModel = SingleFormController.GetConfirmationFormModel(batchId, IdentityType, DB);
                                if (IdentityType == UserTypeEnum.Juristic)
                                {
                                    confModel.RequestorName = confModel.CompanyName;
                                }
                                string url = Url.ServiceAction("Dashboard", "Track", "","");
                                EmailHelper.SendRequestSubmitEmail(user.Email, confModel, url);
                               
                            }
                        }

                    }
                    response.Message = Resources.ApplicationStatusRequests.MSG_ADD_SUCCESS;
                }
            }
            if (response.Type == ResultDataType.Success)
            {
                if (model.Status == ApplicationStatusV2Enum.WAITING)
                {
                    var dbTrans = MongoFactory.GetSingleFormTransactionCollection();
                    tran = dbTrans.AsQueryable().Where(o => o.IdentityID == IdentityID).SingleOrDefault();
                    // SingleFormTransaction is completed
                    if (tran != null)
                    {
                        tran.Apps = new List<string>();
                        tran.AppStep = 0;
                        tran.AppStepTotal = 0;
                        tran.FileCnt = -1;
                        tran.FileTotal = 0;
                        MongoFactory.GetSingleFormTransactionCollection().Update(tran);
                    }
                }
                else
                {
                    var trandb = MongoFactory.GetSingleFormTransactionCollection().AsQueryable();
                    tran = trandb.Where(o => o.TransactionID == model.TransactionID).Single();
                    tran.AppStep = Math.Max(tran.AppStep, model.appStep);
                    tran.LastUpdateTime = DateTime.Now;
                    MongoFactory.GetSingleFormTransactionCollection().Update(tran);
                }
            }
            return response;
        }

        [HttpPost]
        [Route("Api/V2/SingleForm/RegisterFileMetaData")]
        [Authorize]
        public ResponseData<object> SingleFormRegisterFileMetaData(FileMetadataEntity model)
        {
            var fdb = MongoFactory.GetFileMetadataCollection();
            model.Extras = new Dictionary<string, object>
            {
                { "OWNER_IDENT_ID", IdentityID }
            };
            fdb.InsertOne(model);
            ResponseData<object> response = new ResponseData<object>()
            {
                Type = ResultDataType.Success,
                Message = Resources.ApplicationStatusRequests.MSG_ADD_SUCCESS,
            };
            return response;
        }

        [HttpPost]
        [Route("Api/V2/SingleForm/Attachments")]
        [Authorize]
        public ResponseData<object> SingleFormAttachments(SingleFormFileUploadViewModel model)
        {
            ResponseData<object> response = new ResponseData<object>()
            {
                Type = ResultDataType.Error,
                Message = Resources.ApplicationStatusRequests.MSG_ADD_FAILED
            };

            SingleFormTransaction tran = new SingleFormTransaction()
            {
                TransactionID = Guid.Parse(model.transaction_id),
                IdentityID = IdentityID
            };

            if (model != null && model.UploadedFiles != null && model.UploadedFiles.Count > 0)
            {
                tran.UploadedFiles = model.UploadedFiles.Adapt<List<FileGroupEntity>>();
                var IdentityName = string.Empty;

                if (IdentityType == UserTypeEnum.Juristic)
                {
                    try
                    {
                        var profile = IdentityHelper.GetJuristicProfile(IdentityID);
                        if (profile != null && profile.HasValues)
                        {
                            JuristicProfile juristic = profile.ToObject<JuristicProfile>();
                            IdentityName = juristic.JuristicName_TH;
                        }
                    }
                    catch { }
                }
                else if (IdentityType == UserTypeEnum.Citizen)
                {
                    IdentityName = string.Format("{0} {1}", IdentityFirstname, IdentityLastname);
                    //try
                    //{
                    //    var profile = IdentityHelper.GetCitizenProfile(IdentityID);
                    //    if (profile != null && profile.HasValues)
                    //    {
                    //        IdentityName = string.Format("{0} {1}", profile["NameTH_FirstName"].DefaultString(), profile["NameTH_SurName"].DefaultString());
                    //    }
                    //}
                    //catch { }
                }

                if (tran.UploadedFiles != null && tran.UploadedFiles.Count > 0)
                {
                    foreach (var group in tran.UploadedFiles)
                    {
                        group.IdentityID = IdentityID;
                        group.IdentityName = !string.IsNullOrEmpty(IdentityName) ? IdentityName : null;
                        group.IdentityType = IdentityType;
                        group.CreatedDate = group.UpdatedDate = DateTime.Now;

                        foreach (var file in group.Files)
                        {
                            file.IdentityID = IdentityID;
                            file.IdentityName = !string.IsNullOrEmpty(IdentityName) ? IdentityName : null;
                            file.IdentityType = IdentityType;
                            file.FileGroupID = group.FileGroupID;
                            file.CreatedDate = file.UpdatedDate = DateTime.Now;

                            if (!file.Extras.ContainsKey("FILETYPECODE"))
                            {
                                file.Extras.Add("FILETYPECODE", file.FileTypeCode);
                            }
                        }
                    }
                }
            }

            // Send mail here
            tran.FileCnt = model.fileCnt;
            tran.FileTotal = model.fileTotal;
            tran.Save();
            response.Type = ResultDataType.Success;

            if (response.Type == ResultDataType.Success)
            {
                response.Data = model;
                response.Message = Resources.ApplicationStatusRequests.MSG_SAVE_DARFT_SUCCESS;
            }

            return response;
        }

        [HttpDelete]
        [Route("Api/V2/SingleForm/Attachments/{transactionID}/files/{fileID}/{fileGroupID}")]
        public ResponseData<object> SingleFormDeleteFile(Guid transactionID, string fileID, Guid? fileGroupID)
        {
            ResponseData<object> response = new ResponseData<object>()
            {
                Type = ResultDataType.Error,
                Message = Resources.ApplicationStatusRequests.MSG_REMOVE_FILE_FAILED
            };

            SingleFormTransaction tran = SingleFormTransaction.Get(transactionID);
            if (tran == null)
            {
                throw new Exception("SingleForm Transaction is not founded.");
            }

            var db = new ApplicationDbContext();
            //Get User
            var identity = User.Identity;
            if (identity.IsAuthenticated)
            {
                string uid = User.Identity.GetUserId();
                ApplicationUser user = db.Users.Where(o => o.Id == uid).Single();

                if (!UserManager.IsInRole(uid, ConfigurationValues.ROLES_ORG_AGENT_NAME) && !UserManager.IsInRole(uid, ConfigurationValues.ROLES_ADMIN_NAME))
                {
                    if (tran.IdentityID != user.CitizenID && tran.IdentityID != user.JuristicID)
                    {
                        throw new Exception();
                    }
                }
            }
            else
            {
                throw new Exception();
            }

            if (fileGroupID == null)
            {
                throw new Exception();
            }
            tran.RemoveUploadedFiles(fileGroupID.Value, new string[] { fileID });

            response.Message = Resources.ApplicationStatusRequests.MSG_REMOVE_FILE_SUCCESS;
            response.Type = ResultDataType.Success;

            return response;
        }

        [HttpPost]
        [Route("Api/V2/Applications/Sendmailalert")]
        [Authorize]
        public ResponseData<object> Sendalertmail(SendAlertEmailViewModel data)
        {
            bool sent = false;


            List<MailAddress> ccMailAddrs = new List<MailAddress>();
            ResponseData<object> response = new ResponseData<object>()
            {
                Type = ResultDataType.Error,
                Message = Resources.ApplicationStatusRequests.MSG_REMOVE_FILE_FAILED
            };
            if (!string.IsNullOrEmpty(data.msgbody))
            {
                string strbody = string.Empty;
                string strsubject = string.Empty;
                strsubject = "แจ้งผู้ประกอบการจากระบบ Biz Portal";
                strbody = string.Format("เรียนผู้ประกอบการ  {0}<br>เจ้าหน้าที่มีข้อความถึงท่านผู้ประกอบการดังนี้ <pre>{1}</pre> <br>หากพบปัญหาในการใช้งานหรือมีข้อสงสัย กรุณาติดต่อที่ contact @dga.or.th หรือโทร 02 - 612 - 6060<br>ขอขอบพระคุณที่ใช้บริการผ่านระบบ Biz Portal<br>ทีมงาน Biz Portal<br>* **อีเมลนี้เป็นการแจ้งจากระบบอัตโนมัติ กรุณาอย่าตอบกลับ * **", data.fullname, data.msgbody);
                SendAlertEmailViewModel msg = new SendAlertEmailViewModel();

                try
                {
                    sent = EmailHelper.SendEmailV2(
                            new MailAddress[] { new MailAddress { Name = data.fullname, Address = data.email } },
                            ccMailAddrs.ToArray(),
                            strsubject, strbody, true,
                            null, null, null);

                    response.Message = "ส่งอีเมลไปยังผู้ประกอบการเรียบร้อย"; ;
                    response.Type = ResultDataType.Success;
                }
                catch (Exception e)
                {
                    response.Message = "ไม่สามารถส่งอีเมลไปยังผู้ประกอบการได้กรุณาลองอีกครั้ง"; ;
                    response.Type = ResultDataType.Error;
                }
            }
            return response;
        }

        [HttpPost]
        [Route("Api/V2/Applications/SendConfirmSMS")]
        [Authorize]
        public string SendConfirmSMS()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var generator = new Random();
            var otp = generator.Next(133456, 999999).ToString("D6");
            var otpref = new string(Enumerable.Repeat(chars, 4).Select(s => s[generator.Next(s.Length)]).ToArray());
            var userId = User.Identity.GetUserId();
            var phoneNo = ConfigurationManager.AppSettings["TestMode"] == "true" && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["SMSTestNumber"]) ? ConfigurationManager.AppSettings["SMSTestNumber"] : UserManager.GetPhoneNumber(userId);

            if (!string.IsNullOrEmpty(phoneNo))
            {
                var SmsMessage = new ApplicationStatusSmsMessage
                {
                    MobileNumbers = new string[] { phoneNo },
                    Message = string.Format("รหัส OTP สำหรับยืนยันการทำรายการของท่านคือ : {0} รหัสอ้างอิง({1})", otp, otpref),
                };

                SmsMessage.SendSms();

                return phoneNo + "," + otp + "," + otpref;
            }
            else
            {
                throw new Exception("ไม่สามารถส่ง OTP สำหรับการยืนยันตัวตนได้ กรุณาลองใหม่อีกครั้ง");
            }
        }

        [HttpPost]
        [Route("Api/V2/Applications/RenderELicense")]
        [Authorize]
        public List<ElicenseRenderResponseModel> RenderELicense(ElicenseRenderViewModel model)
        {
            try
            {
                var documentIds = new List<ElicenseRenderResponseModel>();

                foreach (var eLicense in model.ELicenses)
                {
                    var request = ApplicationRequestEntity.Get(eLicense.ApplicationRequestID.Value);
                    var application = DB.Applications.Where(e => e.ApplicationID == request.ApplicationID).FirstOrDefault();
                    var hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", application.AppsHookClassName).Unwrap();
                    var edCtrl = new BizPortal.Utils.EDocument(application.ApplicationID);

                    // update ข้อมูลที่เจ้าหน้าที่กรอกก่อนการ render
                    if (eLicense.SigningPersons != null && eLicense.SigningPersons.Count > 0)
                    {
                        var i = 0;
                        foreach (var data in eLicense.SigningPersons)
                        {
                            request = request.AddExtraData("ELICENSE_INFORMATION", "Signer_" + i, data.FirstName + " " + data.LastName);
                            request = request.AddExtraData("ELICENSE_INFORMATION", "SignerDecription_" + i, data.Position);
                            i++;
                        }

                        request.Update();
                    }

                    if (eLicense.SigningExtendedDatas != null)
                    {
                        foreach (var data in eLicense.SigningExtendedDatas)
                        {
                            request = request.AddExtraData("ELICENSE_INFORMATION", data.Name, data.Value);
                        }

                        request.Update();
                    }

                    // render template
                    var eLicenseData = hook.GenerateELicenseData(eLicense.ApplicationRequestID.Value);
                    var signers = eLicense.SigningPersons.Adapt<List<SigningPerson>>();

                    if (eLicenseData != null)
                    {
                        var docId = edCtrl.RenderDocument(eLicense.TemplateID, eLicenseData, signers);

                        documentIds.Add(
                            new ElicenseRenderResponseModel
                            {
                                TemplateID = eLicense.TemplateID,
                                DocumentID = docId
                            }
                        );
                    }
                    else
                    {
                        throw new Exception("ไม่สามารถสร้างเอกสารได้ เนื่องจากไม่พบข้อมูล e-license");
                    }
                }

                return documentIds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<MailAddress> getAgentEmails(ApplicationRequestEntity request)
        {
            var sendEmails = new List<MailAddress>();
            var emailRegex = new Regex(RegExPattern.EMAIL, RegexOptions.IgnoreCase);
            var agentEmails = new List<MailAddress>();
            agentEmails = DB.MemberServices.Include(e => e.ApplicationUser)
                                     .Include(e => e.MemberServiceAreas)
                                     .Where(e => (e.ApplicationID == request.ApplicationID && e.MemberServiceAreas.Count < 1) ||
                                        (e.ApplicationID == request.ApplicationID && e.MemberServiceAreas.Any(s => (s.ProvinceID == -1 || ((s.ProvinceID == request.ProvinceID) && (s.DistrictID == -1 || ((s.DistrictID == request.AmphurID) && (s.SectionID == -1 || (s.SectionID == request.TumbolID)))))) && !s.IsDeleted)
                                        && !e.IsDeleted))
                                     .Select(e => new MailAddress { Name = e.ApplicationUser.Email ?? "", Address = e.ApplicationUser.Email ?? "" })
                                     .ToList();

            //Get OrgAgent email who are the officer of department and not in MemberServices/Areas
            var orgAgents = (from tbApp in DB.Applications
                             join tbUser in DB.Users on tbApp.OrgCode equals tbUser.OrgCode
                             where tbApp.ApplicationID == request.ApplicationID && !(from tbMemberservices in DB.MemberServices where tbMemberservices.IsDeleted == false select tbMemberservices.UserID).Contains(tbUser.Id)
                             select tbUser.Email).ToList();

            foreach (var email in orgAgents)
            {
                agentEmails.Add(new MailAddress { Name = email ?? "", Address = email ?? "" });
            }

            //join tbMemberservices in DB.MemberServices  on tbUser.Id equals tbMemberservices.UserID
            //where tbApp.ApplicationID == applicationID

            foreach (var email in agentEmails)
            {
                if (emailRegex.IsMatch(email.Address))
                {
                    sendEmails.Add(email);
                }
            }
         
            return sendEmails;
        }

        private List<MailAddress> getSignerEmails(ApplicationRequestEntity request)
        {
            var sendEmails = new List<MailAddress>();
            var emailRegex = new Regex(RegExPattern.EMAIL, RegexOptions.IgnoreCase);
            var signerEmails = new List<MailAddress>();
            var currentSigning = EDocumentEntity.GetAll(request.ApplicationRequestID).OrderByDescending(e => e.CreatedDate).FirstOrDefault();

            if (currentSigning != null)
            {
                var citizenIds = currentSigning.PersonalSigners.Select(e => e.IdentityID).ToList();

                signerEmails = DB.Users.Where(e => citizenIds.Contains(e.CitizenID))
                        .Select(e => new MailAddress { Name = e.Email ?? "", Address = e.Email ?? "" })
                        .ToList();

                foreach (var email in signerEmails)
                {
                    if (emailRegex.IsMatch(email.Address))
                    {
                        sendEmails.Add(email);
                    }
                }
              
            }

            return sendEmails;
        }
    }
}
