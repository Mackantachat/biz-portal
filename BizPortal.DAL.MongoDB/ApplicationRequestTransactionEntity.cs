using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using Mapster;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace BizPortal.DAL.MongoDB
{
    public class ApplicationRequestTransactionEntity : ApplicationRequestBasedEntity
    {
        public ApplicationRequestTransactionEntity()
        {
            ApplicationRequestID = Guid.NewGuid();
            ApplicationRequestTransactionID = Guid.NewGuid();
            Data = new Dictionary<string, ApplicationRequestDataGroupEntity>();
            UploadedFiles = new List<FileGroupEntity>();
            GovFiles = new List<FileMetadataEntity>();
            RequestedFiles = new List<FileMetadataEntity>();
            EPermitFiles = new List<FileMetadataEntity>();
            PaymentMethodEnabledChoice = new List<string>();
            PermitDeliveryTypeEnabledChoice = new List<string>();
            BillPaymentFiles = new List<FileMetadataEntity>();
        }

        public ApplicationRequestEntity Save()
        {
            try
            {
                var repo = MongoFactory.GetApplicationRequestCollection();
                ApplicationRequestEntity request = null;
                DateTime dtNow = DateTime.Now;

                if (ApplicationRequestID != null)
                {
                    request = ApplicationRequestEntity.Get(ApplicationRequestID.Value);
                }
                else
                {
                    request = ApplicationRequestEntity.Get(ApplicationID, IdentityID, IdentityType, new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.DRAFT });
                }

                if (Data != null)
                {
                    Data = Data.ToUpperKeyDictionary(); // Uppercase Dictionary's key
                    foreach (var group in Data)
                    {
                        group.Value.Data = group.Value.Data.ToUpperKeyDictionary();
                    }
                }

                if (request == null)
                {
                    request = new ApplicationRequestEntity();
                    TypeAdapter.Adapt(this, request);
                    request.ApplicationRequestID = Guid.NewGuid();

                    // Frontis: Stamp RevisionCode and RevisionName in the ApplicationRequestEntity.
                    var secGroup = MongoFactory.GetFormSectionGroupCollection().AsQueryable().First();
                    request.FormRevisionCode = secGroup.RevisionCode;
                    request.FormRevisionName = secGroup.RevisionName;

                    request.Create();
                }
                else
                {
                    if (request.Status == ApplicationStatusV2Enum.DRAFT && Status != ApplicationStatusV2Enum.DRAFT)
                    {
                        // เปลี่ยนจากสถานะแบบร่าง เป็นอย่างอื่นแล้ว ถือว่าพึ่งสร้าง ให้ปรับวันที่สร้างเอกสารใหม่
                        request.CreatedDate = dtNow;
                        request.OrgCode = this.OrgCode;
                        request.OrgNameTH = this.OrgNameTH;
                        request.OrgAddress = this.OrgAddress;
                        request.PermitName = this.PermitName;
                        request.ExpectSLADate = this.ExpectSLADate;
                    }

                    request.Status = Status;
                    request.StatusOther = StatusOther;
                    request.StatusRemark = StatusRemark;
                    request.Remark = Remark;
                    request.LastUpdatedFrom = LastUpdatedFrom;

                    if (Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE)
                    {
                        if (Fee.HasValue)
                        {
                            request.Fee = Fee;
                        }

                        if (EMSFee.HasValue)
                        {
                            request.EMSFee = EMSFee;
                        }

                        if (!string.IsNullOrEmpty(EMSFeePaymentType))
                        {
                            request.EMSFeePaymentType = EMSFeePaymentType;
                        }

                        if (DueDateForPayFee.HasValue)
                        {
                            if (DueDateForPayFee.Value.Year < 1800) 
                            {
                                DueDateForPayFee = DueDateForPayFee.Value.AddYears(543);
                            }

                            request.DueDateForPayFee = DueDateForPayFee;
                        }

                        if (PaymentMethodEnabledChoice != null && PaymentMethodEnabledChoice.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(request.PaymentMethod))
                            {
                                request.PaymentMethod = null;
                            }
                            request.PaymentMethodEnabledChoice = PaymentMethodEnabledChoice;
                        }

                        if (PermitDeliveryTypeEnabledChoice != null && PermitDeliveryTypeEnabledChoice.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(request.PermitDeliveryType))
                            {
                                request.PermitDeliveryType = null;
                                request.PermitDeliveryAddress = null;
                            }
                            request.PermitDeliveryTypeEnabledChoice = PermitDeliveryTypeEnabledChoice;
                        }

                        if (!string.IsNullOrEmpty(PaymentMethod))
                        {
                            request.PaymentMethod = PaymentMethod;
                        }

                        if (!string.IsNullOrEmpty(PermitDeliveryType))
                        {
                            request.PermitDeliveryType = PermitDeliveryType;
                        }

                        if (!string.IsNullOrEmpty(PermitDeliveryAddress))
                        {
                            request.PermitDeliveryAddress = PermitDeliveryAddress;
                        }

                        if (!string.IsNullOrEmpty(OrgAddress))
                        {
                            request.OrgAddress = OrgAddress;
                        }

                        if (!string.IsNullOrEmpty(PaymentMethodOrgDetail)) 
                        {
                            request.PaymentMethodOrgDetail = PaymentMethodOrgDetail;
                        }

                        if (!string.IsNullOrEmpty(PaymentMethodOrgAddress))
                        {
                            request.PaymentMethodOrgAddress = PaymentMethodOrgAddress;
                        }

                        if (!string.IsNullOrEmpty(PaymentMethodOrgTel))
                        {
                            request.PaymentMethodOrgTel = PaymentMethodOrgTel;
                        }

                        if (!string.IsNullOrEmpty(PermitDeliveryOrgDetail))
                        {
                            request.PermitDeliveryOrgDetail = PermitDeliveryOrgDetail;
                        }

                        if (!string.IsNullOrEmpty(PermitDeliveryOrgAddress))
                        {
                            request.PermitDeliveryOrgAddress = PermitDeliveryOrgAddress;
                        }

                        if (!string.IsNullOrEmpty(PermitDeliveryOrgTel))
                        {
                            request.PermitDeliveryOrgTel = PermitDeliveryOrgTel;
                        }

                    }
                    else if (Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE)
                    {
                        if (!string.IsNullOrEmpty(PermitDeliveryType))
                        {
                            request.PermitDeliveryType = PermitDeliveryType;
                        }

                        if (!string.IsNullOrEmpty(PermitDeliveryAddress))
                        {
                            request.PermitDeliveryAddress = PermitDeliveryAddress;
                        }

                        if (!string.IsNullOrEmpty(OrgAddress))
                        {
                            request.OrgAddress = OrgAddress;
                        }
                    }
                    else if (Status == ApplicationStatusV2Enum.COMPLETED)
                    {
                        request.EMSTrackingNumber = EMSTrackingNumber;
                    }

                    if (ExpectedFinishDate.HasValue && ExpectedFinishDate > DateTime.MinValue)
                    {
                        request.ExpectedFinishDate = CheckFormatDateTH(ExpectedFinishDate, false);
                    }

                    if (UserCanGetAppDate.HasValue && UserCanGetAppDate > DateTime.MinValue)
                    {
                        request.UserCanGetAppDate = UserCanGetAppDate;
                    }

                    if (UserCanGetAppDateEnd.HasValue && UserCanGetAppDateEnd > DateTime.MinValue)
                    {
                        request.UserCanGetAppDateEnd = UserCanGetAppDateEnd;
                    }

                    #region Update ค่าเหล่านี้ที่ Object Transaction
                    this.ApplicationID = request.ApplicationID;
                    this.ApplicationRequestID = request.ApplicationRequestID;
                    this.PermitDeliveryAddress = request.PermitDeliveryAddress;
                    this.PermitDeliveryType = request.PermitDeliveryType;
                    this.PaymentMethod = request.PaymentMethod;
                    this.EMSTrackingNumber = request.EMSTrackingNumber;
                    this.Fee = request.Fee;
                    this.EMSFee = request.EMSFee;
                    this.EMSFeePaymentType = request.EMSFeePaymentType;
                    this.DueDateForPayFee = request.DueDateForPayFee;
                    this.ExpectedFinishDate = request.ExpectedFinishDate;
                    this.UserCanGetAppDate = request.UserCanGetAppDate;
                    this.UserCanGetAppDateEnd = request.UserCanGetAppDateEnd;
                    this.LastUpdatedFrom = request.LastUpdatedFrom;
                    this.PaymentMethodOrgDetail = request.PaymentMethodOrgDetail;
                    this.PaymentMethodOrgAddress = request.PaymentMethodOrgAddress;
                    this.PaymentMethodOrgTel = request.PaymentMethodOrgTel;
                    this.PermitDeliveryOrgDetail = request.PermitDeliveryOrgDetail;
                    this.PermitDeliveryOrgAddress = request.PermitDeliveryOrgAddress;
                    this.PermitDeliveryOrgTel = request.PermitDeliveryOrgTel;

                    #endregion

                    if (Data != null)
                    {
                        if (request.Data == null)
                        {
                            request.Data = new Dictionary<string, ApplicationRequestDataGroupEntity>();
                        }

                        foreach (var group in Data)
                        {
                            if (!request.Data.ContainsKey(group.Key))
                            {
                                request.Data.Add(group.Key, new ApplicationRequestDataGroupEntity()
                                {
                                    GroupName = group.Key,
                                    GroupDescription = string.Empty
                                });
                            }

                            var oldGroup = request.Data[group.Key];
                            if (oldGroup != null)
                            {
                                if (oldGroup.Data == null)
                                    oldGroup.Data = new Dictionary<string, string>();

                                foreach (var data in group.Value.Data)
                                {
                                    oldGroup.Data[data.Key] = data.Value;
                                }
                            }
                            else
                            {
                                request.Data[group.Key] = group.Value;
                            }
                        }
                    }

                    if (Extras != null)
                    {
                        request.Extras = Extras;
                    }

                    if (UploadedFiles != null)
                    {
                        foreach (var group in UploadedFiles)
                        {
                            var oldGroup = request.UploadedFiles.Where(o => o.FileGroupID == group.FileGroupID).SingleOrDefault();
                            if (oldGroup != null)
                            {
                                var existingFileIDs = oldGroup.Files.Select(o => o.FileID);
                                var newFiles = group.Files.Where(o => !existingFileIDs.Contains(o.FileID));
                                oldGroup.Files.AddRange(newFiles);
                                TypeAdapter.Adapt(oldGroup, group);
                            }
                            else
                            {
                                request.UploadedFiles.Add(group);
                            }
                        }
                    }

                    if (GovFiles != null)
                    {
                        if (request.GovFiles == null)
                            request.GovFiles = new List<FileMetadataEntity>();

                        foreach (var file in GovFiles)
                        {
                            file.Extras = file.Extras.ToUpperKeyDictionary(); // Uppercase Dictionary's key
                            file.ApplicationRequestID = (Guid)this.ApplicationRequestID;
                            file.TransactionID = this.ApplicationRequestTransactionID;
                            file.CreatedDate = DateTime.Now;
                            file.UpdatedDate = DateTime.Now;
                            request.GovFiles.Add(file);
                        }
                    }

                    if (RequestedFiles != null)
                    {
                        if (request.RequestedFiles == null)
                            request.RequestedFiles = new List<FileMetadataEntity>();

                        foreach (var file in RequestedFiles)
                        {
                            if (file.Extras == null)
                            {
                                file.Extras = new Dictionary<string, object>();
                            }

                            file.Extras = file.Extras.ToUpperKeyDictionary(); // Uppercase Dictionary's key
                            file.Extras.Add(ExtraKeyEnum.REQUEST_FILE_ID.ToString(), Guid.NewGuid().ToString());
                            file.Extras.Add(ExtraKeyEnum.REQUEST_FILE_NAME.ToString(), file.FileName);
                            file.Extras.Add(ExtraKeyEnum.REQUEST_FILE_REASON.ToString(), file.FileReason);

                            request.RequestedFiles.Add(file);
                        }
                    }

                    if (EPermitFiles != null)
                    {
                        if (request.EPermitFiles == null)
                            request.EPermitFiles = new List<FileMetadataEntity>();

                        foreach (var file in EPermitFiles)
                        {
                            file.Extras = file.Extras.ToUpperKeyDictionary(); // Uppercase Dictionary's key
                            file.ApplicationRequestID = (Guid)this.ApplicationRequestID;
                            file.TransactionID = this.ApplicationRequestTransactionID;
                            file.CreatedDate = DateTime.Now;
                            file.UpdatedDate = DateTime.Now;
                            request.EPermitFiles.Add(file);
                        }
                    }

                    if (PaymentMethodEnabledChoice != null)
                    {
                        if (BillPaymentFiles != null)
                        {
                            if (request.BillPaymentFiles == null)
                            {
                                request.BillPaymentFiles = new List<FileMetadataEntity>();
                            }

                            foreach (var file in BillPaymentFiles)
                            {
                                file.Extras = file.Extras.ToUpperKeyDictionary(); // Uppercase Dictionary's key
                                file.ApplicationRequestID = (Guid)this.ApplicationRequestID;
                                file.TransactionID = this.ApplicationRequestTransactionID;
                                file.CreatedDate = DateTime.Now;
                                file.UpdatedDate = DateTime.Now;
                                request.BillPaymentFiles.Add(file);
                            }
                        }
                    }
                }

                CreatedDate = UpdatedDate = request.UpdatedDate = dtNow;

                if (request.Transactions == null)
                {
                    request.Transactions = new List<ApplicationRequestTransactionEntity>();
                }

                request.Transactions.Add(this);

                if (UploadedFiles != null && UploadedFiles.Count > 0)
                {
                    var fgRepo = MongoFactory.GetFileGroupCollection();
                    var fRepo = MongoFactory.GetFileMetadataCollection();

                    foreach (var group in UploadedFiles)
                    {
                        group.Extras = group.Extras.ToUpperKeyDictionary(); // Uppercase Dictionary's key

                        foreach (var file in group.Files)
                        {
                            file.Extras = file.Extras.ToUpperKeyDictionary(); // Uppercase Dictionary's key

                            if (string.IsNullOrEmpty(file.Id))
                            {
                                file.IdentityID = IdentityID;
                                file.IdentityName = IdentityName;
                                file.IdentityType = IdentityType;
                                file.ApplicationID = ApplicationID;
                                file.ApplicationRequestID = request.ApplicationRequestID;
                                file.FileGroupID = group.FileGroupID;
                                file.CreatedDate = file.UpdatedDate = DateTime.Now;
                                fRepo.InsertOne(file);
                            }
                            else
                            {
                                file.UpdatedDate = DateTime.Now;
                                fRepo.Update(file);
                            }

                            if (file.Extras != null && file.Extras.ContainsKey(ExtraKeyEnum.REQUEST_FILE_ID.ToString()) && request.RequestedFiles != null && request.RequestedFiles.Count > 0)
                            {
                                // Remove matched request file
                                var rfile = request.RequestedFiles.Where(o => o.Extras[ExtraKeyEnum.REQUEST_FILE_ID.ToString()].Equals(file.Extras[ExtraKeyEnum.REQUEST_FILE_ID.ToString()])).SingleOrDefault();
                                request.RequestedFiles.Remove(rfile);
                            }
                        }

                        if (!string.IsNullOrEmpty(group.Id))
                        {
                            if (group.Files.Count > 0)
                            {
                                group.UpdatedDate = DateTime.Now;
                                fgRepo.Update(group);
                            }
                            else
                            {
                                fgRepo.Delete(group);
                            }
                        }
                        else
                        {
                            if (group.Files.Count > 0)
                            {
                                group.IdentityID = IdentityID;
                                group.IdentityName = IdentityName;
                                group.IdentityType = IdentityType;
                                group.ApplicationID = ApplicationID;
                                group.ApplicationRequestID = request.ApplicationRequestID;
                                group.CreatedDate = group.UpdatedDate = DateTime.Now;
                                fgRepo.InsertOne(group);
                            }
                        }
                    }

                    if (request.RequestedFiles != null && request.RequestedFiles.Count == 0)
                    {
                        request.RequestedFiles = null;
                    }
                }

                return request;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetStatusOtherText()
        {
            string text = ResourceHelper.GetResourceWordWithDefault(
                    "STATUS_OTHER_" + this.StatusOther,
                    "ApplicationStatusRequests",
                    this.StatusOther,
                    System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);

            if (this.Status == ApplicationStatusV2Enum.COMPLETED)
            {
                return ResourceHelper.GetResourceWordWithDefault(
                       "STATUS_OTHER_DONE",
                       "ApplicationStatusRequests",
                       text,
                       System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            }
            if (this.Status == ApplicationStatusV2Enum.REJECTED)
            {
                return ResourceHelper.GetResourceWordWithDefault(
                       "STATUS_OTHER_REJECT",
                       "ApplicationStatusRequests",
                       text,
                       System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            }
            else if (string.IsNullOrEmpty(this.StatusOther))
            {
                return ResourceHelper.GetResourceWordWithDefault(
                       "STATUS_OTHER_WAITING_USER_WORKING",
                       "ApplicationStatusRequests",
                       text,
                       System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            }
            else
            {
                return text;
            }
        }

        [BsonRepresentation(BsonType.String)]
        public virtual Guid ApplicationRequestTransactionID { get; set; }
 
        private static DateTime? CheckFormatDateUS(DateTime? date)
        {
            DateTime? resultDate = null;
            try
            {
                DateTime tryDate;
                var culture = CultureInfo.CreateSpecificCulture("en-US");
                var styles = DateTimeStyles.None;
                if (DateTime.TryParse(date.Value.ToString("yyyy-MM-dd HH:mm:ss"), culture, styles, out tryDate))
                {


                    string dateNow = DateTime.Now.ToString("yyyy-MM-dd", culture);
                    string tryValueDate = tryDate.ToString("yyyy-MM-dd", culture);
                    if (Convert.ToDateTime(tryValueDate) < Convert.ToDateTime(dateNow).Date)
                    {
                        throw new Exception(string.Format("Please select today or future date."));
                    }
                    else
                    {
                        tryDate = DateTime.Parse(tryValueDate, culture);
                        resultDate = tryDate;
                    }
                }
                else
                {
                    resultDate = DateTime.Now.AddDays(7);
                }
            }
            catch (Exception ex)
            {
            }

            return resultDate;
        }

        private static DateTime? CheckFormatDateTH(DateTime? date, bool isCheckFutureDate)
        {
            DateTime? resultDate = null;
            try
            {
                DateTime tryDate;
                if (DateTime.TryParse(date.ToString(), out tryDate))
                {
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");

                    string dateNow = DateTime.Now.ToString("yyyy-MM-dd", culture);
                    string tryValueDate = tryDate.ToString("yyyy-MM-dd", culture);

                    if (isCheckFutureDate)
                    {
                        if (Convert.ToDateTime(tryValueDate) < Convert.ToDateTime(dateNow).Date)
                        {
                            throw new Exception("Please select today or future date.");
                        }
                        else
                        {
                            tryDate = DateTime.Parse(tryValueDate, culture);
                            resultDate = tryDate;
                        }
                    }
                    else
                    {
                        tryDate = DateTime.Parse(tryValueDate, culture);
                        resultDate = tryDate;
                    }
                }
                else
                {
                    throw new Exception("Incorrect format date.");
                }
                return resultDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region [Optional Properties]
        [BsonIgnore]
        public Guid? ApplicationRequestID { get; set; }

        /// <summary>
        /// ไม่ส่งอีเมล์แจ้งสถานะของระบบ
        /// </summary>
        public bool DisabledSendingSystemEmail { get; set; }

        /// <summary>
        /// ส่งข้อมูลมากรณีต้องการส่งอีเมล์ (ไม่ใช้ Default Email)
        /// </summary>
        public EmailMessageEntity EmailMessage { get; set; }

        /// <summary>
        /// ส่งข้อมูลกรณีต้องการส่ง sms
        /// </summary>
        public ApplicationStatusSmsMessage SmsMessage { get; set; }

        /// <summary>
        /// ไฟล์เอกสารแนบ
        /// </summary>
        public List<FileGroupEntity> UploadedFiles { get; set; }

        /// <summary>
        /// เป็นรายการตอบกลับจากหน่วยงาน
        /// </summary>
        public bool ReplyFromOrg { get; set; }

        public bool ReplyFromApiUpdate { get; set; }

        /// <summary>
        /// ข้อมูลเพิ่มเติมที่รับเพิ่มจากหน่วยงาน
        /// </summary>
        public Dictionary<string, object> Extras { get; set; }
        #endregion
    }
}
