using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Extensions;
using System.ComponentModel.DataAnnotations;
using BizPortal.Utils;
using BizPortal.DAL;
using System.Configuration;
using BizPortal.Models;
using BizPortal.Utils.Helpers;
using System.Security.Claims;
using EGA.Owin.Security.EGAOpenID;
using EGA.Owin.Security.Utils.Extensions;
using Mapster;
using System.Web;

namespace BizPortal.ViewModels
{
    public class JuristicApplicationStatusRequestViewModel
    {
        public int JuristicApplicationStatusRequestID { get; set; }
        [CustomRequired]
        [Display(Name = "APPLICATION", ResourceType = typeof(Resources.Application))]
        public int ApplicationID { get; set; }

        [CustomRequired]
        [Display(Name = "JURISTIC", ResourceType = typeof(Resources.Application))]
        public string JuristicID { get; set; }

        [Display(Name = "REMARK", ResourceType = typeof(Resources.Global))]
        public string Remark { get; set; }

        public string ApplicationStatusName { get; set; }
        public string ApplicationStatusTxt
        {
            get
            {
                switch (ApplicationStatusID)
                {
                    case (int)ApplicationStatusEnum.OTHER:
                        return string.Format("{0} {1}", ApplicationStatusName, ApplicationStatusOther);
                        break;
                    default:
                        return ApplicationStatusName;
                        break;
                }
            }
        }

        public string ApplicationName { get; set; }
        public string OrganizationName { get; set; }
        public string OrgCode { get; set; }

        public DateTime CreatedDate { get; set; }
        string _CreatedDateTxt = "";
        public string CreatedDateTxt { get { return CreatedDate.ToStringFormat(); } set { _CreatedDateTxt = value; } }

        public DateTime UpdatedDate { get; set; }
        string _UpdatedDateTxt = "";
        public string UpdatedDateTxt { get { return UpdatedDate.ToStringFormat(); } set { _UpdatedDateTxt = value; } }

        public string CreatedUserID { get; set; }
        public string CreatedUserName { get; set; }
        public string CreatedFullName { get; set; }

        [CustomRequiredIf("ApplicationStatusID", 2, ErrorMessageResourceName = "PLEASE_ENTER_APPLICATION_STATUS_OTHER", ErrorMessageResourceType = typeof(Resources.ApplicationStatusRequests))]
        [Display(Name = "REMARK", ResourceType = typeof(Resources.Global))]
        public string ApproveRemark { get; set; }
        [Display(Name = "APPLICATION_STATUS_REQUEST", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public int? ApplicationStatusID { get; set; }

        [CustomRequiredIf("ApplicationStatusID", 5, ErrorMessageResourceName = "PLEASE_ENTER_APPLICATION_STATUS_OTHER", ErrorMessageResourceType = typeof(Resources.ApplicationStatusRequests))]
        [Display(Name = "APPLICATION_STATUS_OTHER", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public string ApplicationStatusOther { get; set; }

        public string ApplicationSysName { get; set; }

        public string ApplicationUrl { get; set; }
    }

    public class JuristicApplicationDocumentRequestViewModel
    {
        public string DocumentID { get; set; }
        public string DocumentType { get; set; }
        public string OwnerReferCode { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
    }

    public class JuristicApplicationStatusRequestSubmitViewModel
    {
        [CustomRequired]
        public string JuristicID { get; set; }
        public List<FileMetadata> FileIDs { get; set; }

        [CustomRequired]
        public int ApplicationID { get; set; }
        public string Remark { get; set; }
        public int? JuristicApplicationStatusRequestID { get; set; }
        public bool IsSubmit { get; set; }

        #region [Optional Members]

        public bool DisableSendingEmail { get; set; }

        public int? CustomAppStatusID { get; set; }

        [StringLength(450)]
        public string CustomTransactionCode { get; set; }

        public DateTime? CustomTransactionDate { get; set; }

        #endregion

        public ResponseData<object> Save()
        {
            var db = new ApplicationDbContext();
            ResponseData<object> response = new ResponseData<object>();
            var user = db.Users.Where(w => w.JuristicID == JuristicID).SingleOrDefault();
            var application = db.Applications.Where(w => w.ApplicationID == ApplicationID).SingleOrDefault();
            int applicationStatusID = IsSubmit ? (int)ApplicationStatusEnum.PENDING : (int)ApplicationStatusEnum.DRAFT;
            if (CustomAppStatusID.HasValue)
                applicationStatusID = CustomAppStatusID.Value;
            int?[] singleRequestStatusID = new int?[] { (int)ApplicationStatusEnum.COMPLETED, (int)ApplicationStatusEnum.PENDING, (int)ApplicationStatusEnum.OTHER };
            DateTime currentDate = CustomTransactionDate.HasValue ? CustomTransactionDate.Value : DateTime.Now;
            bool notLinkedToJuristicUser = user == null;

            if (user == null)
            {
                DisableSendingEmail = true; // Override
                user = db.Users.Where(o => o.UserName.ToLower() == "system").Single();
            }
            else if (!DisableSendingEmail && user != null && string.IsNullOrEmpty(user.Email))
            {
                var context = HttpContext.Current;
                // Try to get Email from Open ID
                var service = context.User.Identity.CreateOpenIDServiceInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"], bool.Parse(ConfigurationManager.AppSettings["TestMode"]));
                var info = service.GetUser(context.User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.AuthToken));
                if (info != null)
                {
                    if (info.Email != null)
                    {
                        user.Email = info.Email.StringValue;
                        user.EmailConfirmed = info.Email.VerifiedLevel.StartsWith("VerifiedLevel");
                        context.User.Identity.UpdateClaimValue(ClaimTypes.Email, user.Email);
                        db.SaveChanges();
                    }
                }
            }

            //if (user == null)
            //{
            //    response.Type = ResultDataType.Error;
            //    response.Data = null;
            //    response.Message = Resources.Membership.MSG_NOT_FOUND;
            //}
            //else if (string.IsNullOrEmpty(user.Email) && IsSubmit)
            //{
            //    response.Type = ResultDataType.Warning;
            //    response.Data = null;
            //    response.Message = Resources.ApplicationStatusRequests.MSG_USER_EMAIL_REQUIRED;
            //}
            if (application == null)
            {
                response.Type = ResultDataType.Error;
                response.Data = null;
                response.Message = Resources.Application.MSG_NOT_FOUND;
            }
            else if (!application.ConsumerKey.HasValue)
            {
                response.Type = ResultDataType.Error;
                response.Data = null;
                response.Message = Resources.Application.MSG_CONSUMER_KEY_NOT_FOUND;
            }
            else if (!application.MultipleRequestSupport
                && JuristicApplicationStatusRequestID == null
                && db.JuristicApplicationStatusRequests
                        .Where(o =>
                            o.ApplicationID == application.ApplicationID
                            && o.JuristicID == JuristicID
                            && singleRequestStatusID.Contains(o.ApplicationStatusID)).Any())
            {
                // สามารถ Request ได้เพียงครั้งเดียวเท่านั้น
                response.Type = ResultDataType.Error;
                response.Data = null;
                response.Message = Resources.Application.MSG_MUTI_REQUEST_NOT_ALLOWED;
            }
            else
            {
                using (var ts = db.Database.BeginTransaction())
                {
                    try
                    {
                        JuristicApplicationStatusRequest juristicAppStatusRequest = null;
                        if (JuristicApplicationStatusRequestID.HasValue && JuristicApplicationStatusRequestID > 0)
                        {
                            juristicAppStatusRequest = db.JuristicApplicationStatusRequests.Where(w => w.JuristicID == JuristicID && w.JuristicApplicationStatusRequestID == JuristicApplicationStatusRequestID).SingleOrDefault();
                        }
                        else
                        {
                            juristicAppStatusRequest = new JuristicApplicationStatusRequest()
                            {
                                ApplicationID = ApplicationID,
                                JuristicID = JuristicID,
                                Remark = Remark,
                                CreatedDate = currentDate,
                                CreatedUserID = user.Id,
                                NotLinkedToJuristicUser = notLinkedToJuristicUser,
                                IsDeleted = false,
                                TransactionCode = CustomTransactionCode
                            };
                            db.JuristicApplicationStatusRequests.Add(juristicAppStatusRequest);
                        }

                        if (juristicAppStatusRequest == null)
                        {
                            response.Type = ResultDataType.Error;
                            response.Data = null;
                            response.Message = Resources.ApplicationStatusRequests.MSG_NOT_FOUND;
                        }
                        else
                        {
                            juristicAppStatusRequest.ApplicationStatusID = applicationStatusID;
                            juristicAppStatusRequest.UpdatedDate = currentDate;
                            juristicAppStatusRequest.UpdatedUserID = user.Id;
                            //db.Entry(juristicAppStatusRequest).State = System.Data.Entity.EntityState.Modified;

                            JuristicApplicationStatusRequestLog juristicAppStatusRequestLog = new JuristicApplicationStatusRequestLog()
                            {
                                JuristicApplicationStatusRequestID = juristicAppStatusRequest.JuristicApplicationStatusRequestID,
                                Remark = Remark,
                                ApplicationStatusID = applicationStatusID,
                                ApplicationStatusOther = null,
                                CreatedDate = currentDate,
                                CreatedUserID = user.Id,
                                IsDeleted = false,
                                TransactionCode = CustomTransactionCode
                            };
                            db.JuristicApplicationStatusRequestLogs.Add(juristicAppStatusRequestLog);
                            db.SaveChanges(false);

                            //[Obsolete]
                            //string[] fileIds = model.FileIDs.Select(s => s.FileID).ToArray();
                            //string[] fileDrestions = model.FileIDs.Select(s => s.FileDescription).ToArray();
                            //Dictionary<string, string>[] extras = model.FileIDs.Select(s => s.Extras).ToArray();
                            //BizPortal.Utils.EgaContentStore.UpdateStatusList(application.ConsumerKey.Value.ToString(), fileIds, extras, juristicAppStatusRequest.JuristicApplicationStatusRequestID, fileDrestions, FileStatus.InUse);


                            List<FileMetadata> metadatas = TypeAdapter.Adapt<List<FileMetadata>, List<FileMetadata>>(FileIDs);
                            if (metadatas.Count > 0)
                            {
                                var result = EgaContentStore.UpdateStatuses(application.ConsumerKey.Value.ToString(), juristicAppStatusRequest.JuristicApplicationStatusRequestID, metadatas, FileStatus.InUse);
                            }

                            ts.Commit();
                            if (IsSubmit)
                            {
                                if (!DisableSendingEmail)
                                {
                                    var applicationTrans = db.ApplicationTranslations.Where(w => w.ApplicationID == juristicAppStatusRequest.ApplicationID && w.Language.TwoLetterISOLanguageName == System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName).SingleOrDefault();
                                    if (applicationTrans != null)
                                    {
                                        string mailMsg = applicationTrans.SubmitMailMessage;
                                        if (string.IsNullOrEmpty(mailMsg))
                                            mailMsg = "";//TODO: DEFAULT MESSAGE

                                        //var url = new UrlHelper(System.Web.HttpContext.Current.Request.RequestContext);
                                        //string trackUrl = url.AbsoluteAction("Detail", "Track", new { Id = juristicAppStatusRequest.JuristicApplicationStatusRequestID });
                                        if (!string.IsNullOrEmpty(juristicAppStatusRequest.CreatedUser.Email))
                                        {
                                            EmailHelper.SendJuristicRequestSubmitEmail(juristicAppStatusRequest.CreatedUser.Email, mailMsg, juristicAppStatusRequest.CreatedDate, null, null, UserTypeEnum.Juristic);
                                        }
                                    }
                                }

                                response.Message = Resources.ApplicationStatusRequests.MSG_ADD_SUCCESS;
                            }
                            else
                            {
                                response.Message = Resources.ApplicationStatusRequests.MSG_SAVE_DARFT_SUCCESS;
                            }

                            response.Type = ResultDataType.Success;
                            response.Data = new { JuristicApplicationStatusRequestID = juristicAppStatusRequest.JuristicApplicationStatusRequestID };
                        }
                    }
                    catch (Exception ex)
                    {
                        ts.Rollback();
                        response.Type = ResultDataType.Error;
                        response.Data = new { JuristicApplicationStatusRequestID = JuristicApplicationStatusRequestID };
                        response.Message = Resources.ApplicationStatusRequests.MSG_ADD_FAILED;
                    }
                }
            }

            return response;
        }
    }

    public class JuristicApplicationStatusRequestChangeViewModel
    {
        public int? JuristicApplicationStatusRequestID { get; set; }
        public int ApplicationStatusID { get; set; }
        public string ApproveRemark { get; set; }
        public string ApplicationStatusOther { get; set; }
        public string FileIDs { get; set; }
    }
}
