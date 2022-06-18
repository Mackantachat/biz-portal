using BizPortal.AppsHook;
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using BizPortal.ViewModels.V2;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Configuration;
using BizPortal.Integrated;
using BizPortal.ViewModels.ControlData;
using BizPortal.Models;
using BizPortal.ViewModels.Select2;
using BizPortal.Utils.Annotations;

namespace BizPortal.Controllers
{
    [Authorize]
    [FilterUser]
    [ForceServiceDomain]
    public class TrackController : ControllerBase
    {
        // GET: Track
        public ActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        private DashboardRequestModel GetDashboardStatus(ApplicationRequestEntity g)
        {
            DashboardRequestModel buff = new DashboardRequestModel();
            buff.RequestBatchID = g.RequestBatchID.ToString();
            buff.LastUpdateTime = g.UpdatedDateByAgent.ToLocalTime().ToString("dd-MM-yyyy, HH:mm");
            buff.CreatedDate = g.CreatedDate.ToLocalTime().ToString("dd-MM-yyyy, HH:mm");
            buff.Requests = new List<RequestItem>();

            var ar = g;
            RequestItem b = new RequestItem();
            b.ApplicationID = ar.ApplicationID; // Somjet 
            buff.SortGroup = 1;
            b.LastUpate = ar.UpdatedDateByAgent;

            #region Status
            if (ar.ApplicationID == 9)
            {
                ApplicationStatusV2Enum[] invalidStatus = new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.COMPLETED, ApplicationStatusV2Enum.REJECTED, ApplicationStatusV2Enum.FAILED };

                if (!invalidStatus.Contains(ar.Status))
                {
                    // Set VAT Status manually
                    Dictionary<string, string> vatArgs = new Dictionary<string, string>();
                    vatArgs.Add("TaxID", ar.IdentityID);
                    var vatStatus = Api.Get(ConfigurationManager.AppSettings["VAT_STATUS_WS_URL"], vatArgs);

                    if (vatStatus != null && vatStatus.HasValues)
                    {
                        if (vatStatus["responseData"]["VatApr"].ToString() == "1" || vatStatus["responseData"]["VatStatus"].ToString() == "Y")
                        {
                            ar.Status = ApplicationStatusV2Enum.COMPLETED;
                            ar.StatusOther = string.Empty;
                            ar.Update();
                        }
                        else if (vatStatus["responseData"]["VatApr"].ToString() == "2")
                        {
                            ar.Status = ApplicationStatusV2Enum.REJECTED;
                            ar.StatusOther = string.Empty;
                            ar.Update();
                        }
                        else if (vatStatus["responseData"]["VatApr"].ToString() == "0")
                        {
                            ar.Status = ApplicationStatusV2Enum.CHECK;
                            ar.StatusOther = string.Empty;
                            ar.Update();
                        }
                        else
                        {
                            ar.Status = ApplicationStatusV2Enum.FAILED;
                            ar.StatusOther = ApplicationStatusOtherValueConst.RESENDABLE;
                            ar.Update();
                        }
                    }
                }

                ApplicationStatusV2Enum[] pendingStatus = new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.WAITING, ApplicationStatusV2Enum.CHECK };
                if (pendingStatus.Contains(ar.Status))
                {
                    ar.Status = ApplicationStatusV2Enum.COMPLETED;
                    b.CustomStatusName = ar.StatusName = "เป็นผู้ประกอบการจดทะเบียน เว้นแต่เข้าลักษณะที่ไม่ออกใบทะเบียน";
                }
                else if (ar.Status == ApplicationStatusV2Enum.COMPLETED)
                {
                    b.CustomStatusName = ar.StatusName = "อนุมัติ";
                }
                else
                {
                    b.CustomStatusName = ar.StatusName = ResourceHelper.GetResourceWord("STATUS_" + ar.Status, "ApplicationStatusRequests", CurrentCulture);
                }
            }
            else if (ar.ApplicationID == 7)
            {
                // TOT ประชาชน
                if (ar.Status == ApplicationStatusV2Enum.INCOMPLETE && ar.StatusOther == "WAITING_AGENT_READ_REQUEST")
                {
                    b.NextStepUrl = ar.Data["TOT_RESPONSE_DATA"].Data["RESULT_URL"];
                }
            }
            else if (ar.ApplicationID == 1 || ar.ApplicationID == 2)
            {
                ar.NoDocument = true;
                ar.Update();
            }
            #endregion

            #region [เช็คสถานะการชำระเงินของกรมบัญชีกลาง]
            if (ar.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE &&
                ar.PaymentMethodEnabledChoice != null &&
                ar.PaymentMethodEnabledChoice.Contains("BILL_PAYMENT"))
            {
                // TODO: API เช็คสถานะการชำระเงินของกรมบัญชีกลาง

            }
            #endregion

            #region [กำหนดวันที่ของแต่ละสถานะ]
            if (ar.Status == ApplicationStatusV2Enum.REJECTED)
            {
                b.StatusBeforeRejected = ar.Transactions[ar.Transactions.Count - 2].Status;
                b.RejectRemarkText = Resources.Global.REQUEST_DENIED;
                buff.SortGroup = 2;
            }
            #endregion

            #region [กำหนดตำแหน่งของวงกลมและคำที่แสดงในหน้า Dashboard]
            //if (status == ApplicationStatusV2Enum.WAITING)
            //{
            //    b.Status = 1;
            //}
            //else if (status == ApplicationStatusV2Enum.CHECK)
            //{
            //    b.Status = 2;
            //}
            //else if (status == ApplicationStatusV2Enum.PENDING)
            //{
            //    b.Status = 3;
            //}
            //else if (status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE)
            //{
            //    b.Status = 4;
            //    if (ar.PaymentMethodEnabledChoice != null && ar.PaymentMethodEnabledChoice.Contains("BILL_PAYMENT") && 
            //        ar.BillPaymentFiles != null && ar.BillPaymentFiles.Count > 0)
            //    {
            //        FileMetadata billToView = new FileMetadata();
            //        var billFromModel = ar.BillPaymentFiles.OrderByDescending(o => o.CreatedDate).FirstOrDefault();
            //        TypeAdapter.Adapt<FileMetadataEntity, FileMetadata>(billFromModel, billToView);
            //        b.BillPaymentFile = billToView;
            //    }
            //}
            //else if (status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE)
            //{
            //    b.Status = 5;
            //}
            //else if (status == ApplicationStatusV2Enum.COMPLETED)
            //{
            //    b.Status = 6;
            //    buff.SortGroup = 3;
            //}
            //else if (status == ApplicationStatusV2Enum.INCOMPLETE || status == ApplicationStatusV2Enum.FAILED)
            //{
            //    b.Status = 1;
            //}

            //if (statusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST)
            //{
            //    b.StatusOther = 1;
            //}
            //else if (statusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING)
            //{
            //    b.StatusOther = 2;
            //}
            //else if (statusOther == ApplicationStatusOtherValueConst.WAITING_USER_WORKING)
            //{
            //    b.StatusOther = 3;

            //    if (ar.Status != ApplicationStatusV2Enum.COMPLETED && ar.Status != ApplicationStatusV2Enum.REJECTED)
            //    {
            //        buff.SortGroup = 0;
            //    }
            //}
            //else if (statusOther == ApplicationStatusOtherValueConst.DONE)
            //{
            //    b.StatusOther = 4;
            //}
            //else if (statusOther == ApplicationStatusOtherValueConst.REJECT || statusOther == ApplicationStatusOtherValueConst.CANCEL_COMPLETED)
            //{
            //    b.StatusOther = 5;
            //}

            //if (status == ApplicationStatusV2Enum.INCOMPLETE)
            //{
            //    b.StatusOther = 3;
            //}
            //else if (status == ApplicationStatusV2Enum.FAILED)
            //{
            //    b.StatusOther = 10; // Send failed
            //}
            #endregion

            b.Status = ar.Status;
            b.StatusOther = ar.StatusOther;

            var statusSeq = DB.Applications.Where(o => o.ApplicationID == ar.ApplicationID).Select(o => o.StatusSequence).FirstOrDefault();
            if (!string.IsNullOrEmpty(statusSeq))
            {
                b.StatusSequence = statusSeq.Split(',').ToList();
            }
            else
            {
                b.StatusSequence = new List<string>()
                {
                    ApplicationStatusV2Enum.CHECK.ToString(),
                    ApplicationStatusV2Enum.PENDING.ToString(),
                    ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE.ToString(),
                    ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE.ToString(),
                    ApplicationStatusV2Enum.COMPLETED.ToString()
                };
            }

            if (ar.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE)
            {
                if (ar.PaymentMethodEnabledChoice != null && ar.BillPaymentFiles != null && ar.BillPaymentFiles.Count > 0)
                {
                    FileMetadata billToView = new FileMetadata();
                    var billFromModel = ar.BillPaymentFiles.OrderByDescending(o => o.CreatedDate).FirstOrDefault();
                    TypeAdapter.Adapt<FileMetadataEntity, FileMetadata>(billFromModel, billToView);
                    b.BillPaymentFile = billToView;
                }
            }

            if (ar.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE)
            {
                ReceiptRunningTransaction receiptData = ReceiptRunningTransaction.GetByApplicationRequestNumber(ar.ApplicationRequestNumber);
                if (receiptData != null)
                {
                    var receiptFromModel = ar.GovFiles.Where(x => x.FileID == receiptData.FileId).FirstOrDefault();
                    if (receiptFromModel != null)
                    {
                        FileMetadata receiptToView = new FileMetadata();
                        TypeAdapter.Adapt<FileMetadataEntity, FileMetadata>(receiptFromModel, receiptToView);
                        b.ReceiptFile = receiptToView;
                    }
                }
            }

            b.ApplicationName = DB.ApplicationTranslations.Where(o => o.ApplicationID == ar.ApplicationID && o.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.ApplicationName).FirstOrDefault();
            b.RequestNo = ar.ApplicationRequestNumber;
            b.SLDDate = ar.CreatedDate.AddDays(ar.Duration ?? 0).ToLocalTime().ToString("d MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH"));
            b.OrgName = DB.OrganizationTranslations.Where(o => o.OrgCode == ar.OrgCode && o.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.OrgName).FirstOrDefault();
            b.NoFee = (ar.Fee.GetValueOrDefault(0) == 0 && ar.NoDocument);
            b.NoDoc = ar.NoDocument;
            b.CheckApproveDateTime = ar.CheckApproveDateTime.HasValue ? ar.CheckApproveDateTime.Value.ToLocalTime().ToString("dd/MM/yyyy") : "";
            b.PendingApproveDateTime = ar.PendingApproveDateTime.HasValue ? ar.PendingApproveDateTime.Value.ToLocalTime().ToString("dd/MM/yyyy") : "";
            b.PaidFeeApproveDateTime = ar.PaidFeeApproveDateTime.HasValue ? ar.PaidFeeApproveDateTime.Value.ToLocalTime().ToString("dd/MM/yyyy") : "";
            b.CreateLicenseApproveDateTime = ar.CreateLicenseApproveDateTime.HasValue ? ar.CreateLicenseApproveDateTime.Value.ToLocalTime().ToString("dd/MM/yyyy") : "";
            b.ApplicationRequestID = ar.ApplicationRequestID.ToString();
            b.ExpectedFinishDate = ar.ExpectedFinishDate;
            b.CreatedDate = ar.CreatedDate;
            b.DueDateForPayFee = ar.DueDateForPayFee;
            b.EMSTrackingNumber = ar.EMSTrackingNumber;
            b.UserCanGetAppDate = ar.UserCanGetAppDate;
            b.UserCanGetAppDateEnd = ar.UserCanGetAppDateEnd;
            b.PermitDeliveryType = ar.PermitDeliveryType;
            b.PaymentMethod = ar.PaymentMethod;
            b.LastUpdatedFrom = ar.LastUpdatedFrom;
            b.PaymentMethodOrgDetail = ar.PaymentMethodOrgDetail;
            b.PaymentMethodOrgAddress = ar.PaymentMethodOrgAddress;
            b.PaymentMethodOrgTel = ar.PaymentMethodOrgTel;
            b.PermitDeliveryOrgDetail = ar.PermitDeliveryOrgDetail;
            b.PermitDeliveryOrgAddress = ar.PermitDeliveryOrgAddress;
            b.PermitDeliveryOrgTel = ar.PermitDeliveryOrgTel;

            buff.Requests.Add(b);
            return buff;
        }

        private List<string> GetApplicationNameFromSqlServer(List<string> appsFromMongo)
        {
            var appIdsFromSqlServer = DB.Applications
                            .Where(w => appsFromMongo.Contains(w.ApplicationSysName))
                            .Select(r => r.ApplicationID);

            var appsFromSqlServer = DB.ApplicationTranslations
                .Where(o => appIdsFromSqlServer.Contains(o.ApplicationID)
                    && o.Language.TwoLetterISOLanguageName == CurrentCulture)
                .OrderBy(o => o.ApplicationID)
                .Select(o => o.ApplicationName).ToList();

            return appsFromSqlServer;
        }

        private DashboardViewModel GetCurrentUserDashBoard()
        {
            DashboardViewModel result = new DashboardViewModel();
            result.RequestList = new List<DashboardRequestModel>();
            result.RequestApproveList = new List<DashboardRequestModel>();
            result.RequestOwnerList = new List<DashboardRequestModel>();

            result.IdentityName = IdentityFullName;
            result.IdentityType = IdentityType;
            result.IdentityID = IdentityID;

            //คำขอที่ร่างค้างไว้
            var sftquery = MongoFactory.GetSingleFormTransactionCollection().AsQueryable();
            var sft = sftquery.FirstOrDefault(o => o.IdentityID == IdentityID && o.Apps != null && o.Apps.Count > 0);
            if (sft != null)
            {
                result.DraftRequest = "01";
                result.LastUpdateTime = string.Format("วันที่ปรับปรุงข้อมูลล่าสุด: {0} : ร่างคำร้อง/คำขอ #1", sft.LastUpdateTime.ToLocalTime().ToString("dd-MM-yyyy, HH:mm"));
                result.AppList = new List<string>();

                if (sft.Apps != null && sft.Apps.Count() > 0)
                {
                    var apps = GetApplicationNameFromSqlServer(sft.Apps);
                    if (apps != null && apps.Count > 0)
                    {
                        foreach (var a in apps)
                        {
                            result.AppList.Add(string.Format("- {0}", a));
                        }
                    }
                    else
                    {
                        foreach (var a in sft.Apps)
                        {
                            result.AppList.Add(string.Format("- {0}", a));
                        }
                    }
                }
                result.AppStep = string.Format("{0} / {1}", sft.AppStep, sft.AppStepTotal);
                result.NumApp = string.Format("ใบอนุญาต ({0})", result.AppList.Count());
                result.FileCnt = sft.FileCnt;
                result.FileTotal = sft.FileTotal;

                result.DraftRequsetTransID = sft.TransactionID;
                result.DraftRequestAppStep = sft.AppStep;
                result.DraftRequestStep = sft.Step;
            }
            else
            {
                result.AppList = new List<string>();
                result.DraftRequest = "00";
            }

            var appquery = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            var requests = appquery.Where(o => o.IdentityID == IdentityID && o.Status != ApplicationStatusV2Enum.DRAFT)
                .OrderByDescending(o => o.UpdatedDateByAgent)
                .ToList();
            foreach (var g in requests)
            {
                if (g.Status == ApplicationStatusV2Enum.COMPLETED)
                {
                    result.RequestOwnerList.Add(GetDashboardStatus(g));
                }

                if (g.Status == ApplicationStatusV2Enum.COMPLETED || g.Status == ApplicationStatusV2Enum.REJECTED)
                {
                    result.RequestApproveList.Add(GetDashboardStatus(g));
                }
                else
                {
                    result.RequestList.Add(GetDashboardStatus(g));
                }
            }

            //คำขอที่รออนุมัติ
            var app = appquery.Where(o => o.IdentityID == IdentityID && (o.Status != ApplicationStatusV2Enum.COMPLETED && o.Status != ApplicationStatusV2Enum.REJECTED && o.Status != ApplicationStatusV2Enum.DRAFT));
            if (app != null && app.Any())
            {
                result.WaitingApproveRequest = app.Count().ToString("#0#");
            }
            else
            {
                result.WaitingApproveRequest = "00";
                result.LastUpdateTime = "";
            }

            //คำขอที่อนุมัติแล้ว
            var appApprove = appquery.Where(o => o.IdentityID == IdentityID && (o.Status == ApplicationStatusV2Enum.COMPLETED || o.Status == ApplicationStatusV2Enum.REJECTED));
            if (appApprove != null && appApprove.Any())
            {
                result.ApproveRequest = appApprove.Count().ToString("#0#");
            }
            else
            {
                result.ApproveRequest = "00";
                result.LastUpdateTime = "";
            }

            // Order by CreatedDate again!!
            result.RequestList = result.RequestList.OrderByDescending(o => o.Requests.First().CreatedDate).ToList();

            return result;
        }

        public ActionResult Dashboard()
        {
            DashboardViewModel model = GetCurrentUserDashBoard();
            return View(model);
        }

        public ActionResult DisplayQRCode(Guid appReqId)
        {
            ApplicationRequestEntity model = ApplicationRequestEntity.GetByIdentity(appReqId, IdentityID, IdentityType);
            if (model != null)
            {
                return View("_QRCodeForm", model);
            }
            else
            {
                return View("Dashboard", GetCurrentUserDashBoard());
            }
        }

        public ActionResult Detail(Guid id)
        {
            ApplicationRequestEntity model = ApplicationRequestEntity.GetByIdentity(id, IdentityID, IdentityType);
            Dictionary<string, object> defaults = new Dictionary<string, object>();

            if (model == null)
            {
                notifyMsg(NotifyMsgType.Error, Resources.ApplicationStatusRequests.MSG_NOT_FOUND);
                return RedirectToAction("Index", "Track", new { Area = "" });
            }

            var user = DB.Users.Where(o => o.JuristicID == model.IdentityID || o.CitizenID == model.IdentityID).FirstOrDefault();
            if (string.IsNullOrEmpty(model.IdentityName))
            {

                if (!string.IsNullOrEmpty(user.FullName))
                {
                    model.IdentityName = user.FullName;
                    model.Update();
                }
            }
            ViewBag.requestorTel = user.PhoneNumber;
            bool fromApiUpdate = false;
            if (model.Transactions != null && model.Transactions.Count > 0)
            {
                foreach (var tran in model.Transactions)
                {
                    if (string.IsNullOrEmpty(tran.IdentityName) && !tran.ReplyFromOrg)
                    {
                        string name = DB.Users.Where(o => o.JuristicID == tran.IdentityID || o.CitizenID == tran.IdentityID).Select(o => o.FullName).FirstOrDefault();

                        if (!string.IsNullOrEmpty(name))
                        {
                            tran.IdentityName = name;
                            model.Update();
                        }
                    }
                }
                var lastestTran = model.Transactions.OrderByDescending(o => o.CreatedDate).FirstOrDefault();
                if (lastestTran.ReplyFromOrg && lastestTran.ReplyFromApiUpdate)
                {
                    fromApiUpdate = true;
                }
            }

            ViewBag.FromApiUpdate = fromApiUpdate;

            ApplicationRequestViewModel viewModel = model.Adapt<ApplicationRequestViewModel>();


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
                    return RedirectToAction("Index", "Track", new { Area = "" });
                }

                ViewBag.ApplicationArticle = new ApplicationArticleViewModel() { Articles = new ArticleViewModel[] { new ArticleViewModel { ArticleID = 7, ArticleName = "การดำเนินการด้านแรงงานในการเป็นนายจ้าง" } }, Categories = new string[] { "เริ่มต้นธุรกิจ" }, OrganizationNames = new string[] { "สำนักงานประกันสังคม" } };
                ViewBag.Title = model.ApplicationName;

                return View("Detail_1_0_0", oldModel);
            }
            else
            {
                #region Set Status
                if (model.ApplicationID == 9)
                {
                    ApplicationStatusV2Enum[] invalidStatus = new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.COMPLETED, ApplicationStatusV2Enum.REJECTED, ApplicationStatusV2Enum.FAILED };

                    if (!invalidStatus.Contains(model.Status))
                    {
                        // Set VAT Status manually
                        Dictionary<string, string> vatArgs = new Dictionary<string, string>();
                        vatArgs.Add("TaxID", model.IdentityID);
                        var vatStatus = Api.Get(ConfigurationManager.AppSettings["VAT_STATUS_WS_URL"], vatArgs);

                        if (vatStatus != null && vatStatus.HasValues)
                        {
                            if (vatStatus["responseData"]["VatApr"].ToString() == "1" || vatStatus["responseData"]["VatStatus"].ToString() == "Y")
                            {
                                model.Status = ApplicationStatusV2Enum.COMPLETED;
                                model.StatusOther = string.Empty;
                                model.Update();
                            }
                            else if (vatStatus["responseData"]["VatApr"].ToString() == "2")
                            {
                                model.Status = ApplicationStatusV2Enum.REJECTED;
                                model.StatusOther = string.Empty;
                                model.Update();
                            }
                            else if (vatStatus["responseData"]["VatApr"].ToString() == "0")
                            {
                                model.Status = ApplicationStatusV2Enum.CHECK;
                                model.StatusOther = string.Empty;
                                model.Update();
                            }
                            else
                            {
                                model.Status = ApplicationStatusV2Enum.FAILED;
                                model.StatusOther = ApplicationStatusOtherValueConst.RESENDABLE;
                                model.Update();
                            }
                        }
                    }

                    ApplicationStatusV2Enum[] pendingStatus = new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.WAITING, ApplicationStatusV2Enum.CHECK };
                    if (pendingStatus.Contains(model.Status))
                    {
                        model.Status = ApplicationStatusV2Enum.COMPLETED;
                        model.StatusName = "เป็นผู้ประกอบการจดทะเบียน เว้นแต่เข้าลักษณะที่ไม่ออกใบทะเบียน";
                    }
                    else if (model.Status == ApplicationStatusV2Enum.COMPLETED)
                    {
                        model.StatusName = "อนุมัติ";
                    }
                    else
                    {
                        model.StatusName = ResourceHelper.GetResourceWord("STATUS_" + model.Status, "ApplicationStatusRequests", CurrentCulture);
                    }
                    var vatRegDate = viewModel.Data.TryGetData("VAT_RESPONSE_DATA").ThenGetStringData("VAT_REGIS_DATE");
                    if (!string.IsNullOrEmpty(vatRegDate))
                    {
                        defaults.Add("VAT_REGIS_DATE", vatRegDate);
                    }
                }
                else
                {
                    model.StatusName = ResourceHelper.GetResourceWord("STATUS_" + model.Status.ToString(), "ApplicationStatusRequests", CurrentCulture);
                }
                #endregion

                model.ApplicationName = DB.ApplicationTranslations.Where(o => o.ApplicationID == model.ApplicationID && o.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.ApplicationName).FirstOrDefault();
                var userModel = DB.Users.Where(w => w.Id == model.UpdatedByAgent).FirstOrDefault();
                string fullName = "-";
                string agentPhone = "-";
                if (userModel != null)
                {
                    fullName = userModel.FullName;
                    agentPhone = string.IsNullOrEmpty(userModel.PhoneNumber) ? "-" : userModel.PhoneNumber;
                }

                #region [Set Default Ajax Data]
                defaults.Add("MWA_BRANCH", viewModel.Data.TryGetData("MWA_INFORMATION").ThenGetStringData("MWA_BRANCH_TEXT"));
                defaults.Add("MEA_BRANCH", viewModel.Data.TryGetData("MEA_INFORMATION").ThenGetStringData("MEA_BRANCH_TH"));
                defaults.Add("TOT_BRANCH", new List<string>()
                {
                    viewModel.Data.TryGetData("TOT_INFORMATION").ThenGetStringData("TOT_1ST_BRANCH_TEXT"),
                    viewModel.Data.TryGetData("TOT_INFORMATION").ThenGetStringData("TOT_2ND_BRANCH_TEXT")
                });
                defaults.Add("PWA_BRANCH", new Select2Opt[]
                {
                    new Select2Opt() { Text = viewModel.Data.TryGetData("PWA_INFORMATION").ThenGetStringData("PWA_BRANCH_TEXT") }
                });

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

                if (model.IdentityType == UserTypeEnum.Juristic)
                {
                    var provinces = GeoService.Provinces(string.Empty);
                    var provinceID = IdentityID.Substring(1, 2);
                    var regisProvince = provinces.Where(o => o.ID == provinceID).SingleOrDefault();
                    if (regisProvince != null)
                    {
                        defaults.Add("REGISTER_PROVINCE", new AddressControlData() { Province = regisProvince });
                    }
                    if (viewModel.Data != null && viewModel.Data.Count > 0)
                    {
                        var capital = viewModel.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("REGISTER_CAPITAL");
                        if (!string.IsNullOrEmpty(capital))
                        {
                            defaults.Add("REGISTER_CAPITAL", capital);
                        }
                        var capitalPaid = viewModel.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("REGISTER_CAPITAL_PAID");
                        if (!string.IsNullOrEmpty(capitalPaid))
                        {
                            defaults.Add("REGISTER_CAPITAL_PAID", capitalPaid);
                        }
                    }
                }
                #endregion

                var dbRequest = MongoFactory.GetSingleFormRequestCollection().AsQueryable();
                var sgRequest = dbRequest.Where(o => o.IdentityID == IdentityID).SingleOrDefault();
                var application = DB.Applications.Where(o => o.ApplicationID == model.ApplicationID).Select(o => new { AppsHookClassName = o.AppsHookClassName, FileOwner = o.FileOwner, IsEnableELicense = o.IsEnableELicense }).SingleOrDefault();

                if (sgRequest != null)
                {
                    var mapApps = new List<string>() { "MEA_INFORMATION", "MWA_INFORMATION", "TOT_INFORMATION", "PWA_INFORMATION" };
                    var mapData = sgRequest.SectionData.Where(o => mapApps.Contains(o.SectionName)).ToList();
                    if (mapData != null && mapData.Count > 0)
                    {
                        foreach (var sec in mapData)
                        {
                            if (sec.SectionName == "MEA_INFORMATION" && sec.FormData.ContainsKey("MEA_BRANCH_TH"))
                            {
                                if (!defaults.ContainsKey("MEA_BRANCH"))
                                {
                                    defaults.Add("MEA_BRANCH", sec.FormData["MEA_BRANCH_TH"]);
                                }
                                else
                                {
                                    defaults["MEA_BRANCH"] = sec.FormData["MEA_BRANCH_TH"];
                                }
                            }
                            else if (sec.SectionName == "MWA_INFORMATION" && sec.FormData.ContainsKey("MWA_BRANCH_TEXT"))
                            {
                                if (!defaults.ContainsKey("MWA_BRANCH"))
                                {
                                    defaults.Add("MWA_BRANCH", sec.FormData["MWA_BRANCH_TEXT"]);
                                }
                                else
                                {
                                    defaults["MWA_BRANCH"] = sec.FormData["MWA_BRANCH_TEXT"];
                                }
                            }
                            else if (sec.SectionName == "TOT_INFORMATION" && sec.FormData.ContainsKey("TOT_1ST_BRANCH_TEXT") && sec.FormData.ContainsKey("TOT_2ND_BRANCH_TEXT"))
                            {
                                if (!defaults.ContainsKey("TOT_BRANCH"))
                                {
                                    defaults.Add("TOT_BRANCH", new List<string>() { sec.FormData["TOT_1ST_BRANCH_TEXT"].ToString(), sec.FormData["TOT_2ND_BRANCH_TEXT"].ToString() });
                                }
                                else
                                {
                                    defaults["TOT_BRANCH"] = new List<string>() { sec.FormData["TOT_1ST_BRANCH_TEXT"].ToString(), sec.FormData["TOT_2ND_BRANCH_TEXT"].ToString() };
                                }
                            }
                            else if (sec.SectionName == "PWA_INFORMATION" && sec.FormData.ContainsKey("PWA_BRANCH_TEXT"))
                            {
                                if (!defaults.ContainsKey("PWA_BRANCH"))
                                {
                                    defaults.Add("PWA_BRANCH", sec.FormData["PWA_BRANCH_TEXT"]);
                                }
                                else
                                {
                                    defaults["PWA_BRANCH"] = sec.FormData["PWA_BRANCH_TEXT"];
                                }
                            }
                        }
                    }
                }

                ViewBag.Defaults = defaults;
                ViewBag.UpdatedByAgentName = fullName;
                ViewBag.UpdatedByAgentPhone = agentPhone;
                ViewBag.AppsHookClassName = application.AppsHookClassName;
                ViewBag.FileOwners = application.FileOwner;

                // elicense
                var elicenses = new List<ELicenseViewModel>();

                if (application.IsEnableELicense) 
                {
                    var signings = EDocumentEntity.GetAll(model.ApplicationRequestID);

                    if (signings != null && signings.Count > 0)
                    {
                        foreach (var signing in signings)
                        {
                            elicenses.Add(new ELicenseViewModel
                            {
                                ApplicationRequestID = signing.ApplicationRequestID,
                                DocumentID = signing.DocumentID,
                                Name = signing.DocumentName,
                                Url = signing.DocumentUrl,
                                SigningType = signing.SigningType.ToString(),
                                SigningStatus = signing.SigningStatus.ToString(),
                            });
                        }
                    }
                }

                ViewBag.IsEnableELicense = application.IsEnableELicense;
                ViewBag.ELicenses = elicenses;

                string detailViewName = ViewNameForUserConst.ORIGINAL;
                if (!string.IsNullOrEmpty(ViewBag.AppsHookClassName))
                {
                    var hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", ViewBag.AppsHookClassName).Unwrap();
                    if (!string.IsNullOrEmpty(hook.TrackDetailViewName))
                    {
                        detailViewName = hook.TrackDetailViewName;
                    }
                }

                if (model.RequestedFiles != null && model.RequestedFiles.Count > 0)
                {
                    var files = MongoFactory.GetSingleFormFileListCollection().AsQueryable();

                    foreach (var requestedFile in model.RequestedFiles)
                    {
                        var file = files.Where(e => e.FileName == requestedFile.FileTypeCode).FirstOrDefault();

                        if (file != null)
                        {
                            requestedFile.FileIsEnableUrl = file.FileIsEnableUrl;
                        }
                        else
                        {
                            requestedFile.FileIsEnableUrl = false;
                        }
                    }
                }

                return View(detailViewName, model);
            }
        }

        [EncryptedActionParameter]
        public ActionResult Receipt(string applicationRequestNumber)
        {
            var receiptData = ReceiptRunningTransaction.GetByApplicationRequestNumber(applicationRequestNumber);
            var request = MongoFactory.GetApplicationRequestCollection().Find(x => x.ApplicationRequestNumber == applicationRequestNumber).FirstOrDefault();
            if (receiptData != null && request != null)
            {
                return RedirectToAction("GetV2", "File", new { id = receiptData.FileId, rid = request.ApplicationRequestID, area = "" });
            }
            return HttpNotFound();
        }
    }
}