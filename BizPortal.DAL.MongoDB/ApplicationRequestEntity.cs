using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels.V2;

namespace BizPortal.DAL.MongoDB
{
    public class ApplicationRequestEntity : ApplicationRequestBasedEntity
    {

        public string GetStatusOtherText()
        {
            //string text = ResourceHelper.GetResourceWordWithDefault(
            //        "STATUS_OTHER_" + this.StatusOther,
            //        "ApplicationStatusRequests",
            //        "STATUS_OTHER_" + this.StatusOther,
            //        System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            string text = ResourceHelper.GetResourceWordWithDefault(
                    "STATUS_OTHER_" + this.StatusOther,
                    "ApplicationStatusRequests",
                    this.StatusOther,
                    System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            if ((this.Status == ApplicationStatusV2Enum.CHECK || this.Status == ApplicationStatusV2Enum.PENDING)
                &&
                this.Transactions != null && this.Transactions.Count > 0)
            {
                return ResourceHelper.GetResourceWordWithDefault(
                    "STATUS_OTHER_" + this.StatusOther + "::" + this.Transactions[this.Transactions.Count - 1].StatusOther,
                    "ApplicationStatusRequests",
                    text,
                    System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            }
            else if (this.Status == ApplicationStatusV2Enum.REJECTED)
            {
                return ResourceHelper.GetResourceWordWithDefault(
                       "STATUS_OTHER_REJECT",
                       "ApplicationStatusRequests",
                       text,
                       System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            }
            //else if (this.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && this.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING && this.PaymentMethod != null)
            //{
            //    return ResourceHelper.GetResourceWordWithDefault(
            //           "STATUS_OTHER_PAID_FEE",
            //           "ApplicationStatusRequests",
            //           text,
            //           System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            //}      
            //else if (this.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && this.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_PROCESS)
            //{
            //    return ResourceHelper.GetResourceWordWithDefault(
            //         "STATUS_OTHER_AGENT_APPROVE",
            //         "ApplicationStatusRequests",
            //         text,
            //         System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            //}
            else
            {
                return text;
            }
        }
        public string GetStatusEmailOtherText()
        {
            //string text = ResourceHelper.GetResourceWordWithDefault(
            //        "STATUS_OTHER_" + this.StatusOther,
            //        "ApplicationStatusRequests",
            //        "STATUS_OTHER_" + this.StatusOther,
            //        System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            string text = ResourceHelper.GetResourceWordWithDefault(
                    "STATUS_OTHER_EMAIL_" + this.StatusOther,
                    "ApplicationStatusRequests",
                    this.StatusOther,
                    System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            if ((this.Status == ApplicationStatusV2Enum.CHECK || this.Status == ApplicationStatusV2Enum.PENDING)
                &&
                this.Transactions != null && this.Transactions.Count > 1)
            {
                return ResourceHelper.GetResourceWordWithDefault(
                    "STATUS_OTHER_EMAIL_" + this.StatusOther + "::" + this.Transactions[this.Transactions.Count - 2].StatusOther,
                    "ApplicationStatusRequests",
                    text,
                    System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            }
            else if (this.Status == ApplicationStatusV2Enum.REJECTED)
            {
                return ResourceHelper.GetResourceWordWithDefault(
                       "STATUS_OTHER_EMAIL_REJECT",
                       "ApplicationStatusRequests",
                       text,
                       System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            }
            else if (this.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && this.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING)
            {
                if (this.PaymentMethod != null)
                {
                    return ResourceHelper.GetResourceWordWithDefault(
                      "STATUS_OTHER_PAID_FEE",
                      "ApplicationStatusRequests",
                      text,
                      System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
                }
                else
                {
                    return ResourceHelper.GetResourceWordWithDefault(
                     "STATUS_OTHER_EMAIL_WAITING_AGENT_PAID_FEE",
                     "ApplicationStatusRequests",
                     text,
                     System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);

                }
            }
            else if (this.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && this.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_PROCESS)
            {
                return ResourceHelper.GetResourceWordWithDefault(
                     "STATUS_OTHER_AGENT_APPROVE",
                     "ApplicationStatusRequests",
                     text,
                     System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            }
            else if (this.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && this.StatusOther == ApplicationStatusOtherValueConst.WAITING_USER_WORKING)
            {
                return ResourceHelper.GetResourceWordWithDefault(
                     "STATUS_OTHER_EMAIL_WAITING_USER_PAID_FEE",
                     "ApplicationStatusRequests",
                     text,
                     System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            }
            else if (this.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && this.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING)
            {
                return ResourceHelper.GetResourceWordWithDefault(
                     "STATUS_OTHER_EMAIL_WAITING_AGENT_PAID_FEE",
                     "ApplicationStatusRequests",
                     text,
                     System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            }
            else if (this.Status == ApplicationStatusV2Enum.COMPLETED)
            {
                return ResourceHelper.GetResourceWordWithDefault(
                     "STATUS_OTHER_EMAIL_COMPLETED",
                     "ApplicationStatusRequests",
                     text,
                     System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            }
            else
            {
                return text;
            }
        }
        public string GetPaymentChannel()
        {
            if (Fee == 0 && ((EMSFee.HasValue && EMSFee.Value == 0) || PermitDeliveryType != PermitDeliveryTypeValueConst.BY_MAIL))
            {
                return "-"; // Total fee = 0, No payment needed
            }
            var paymentMethod = string.IsNullOrEmpty(this.PaymentMethod) ? "PAYMENT_Null" : "PAYMENT_" + this.PaymentMethod;

            if (this.PaymentMethod == PaymentMethodValueConst.AT_OWNER_ORG)
            {
                paymentMethod = !string.IsNullOrEmpty(this.PaymentMethodOrgDetail) ? this.PaymentMethodOrgDetail : this.OrgNameTH.Trim();
            }
            return ResourceHelper.GetResourceWordWithDefault(paymentMethod, "ApplicationStatusRequests", paymentMethod, System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
        }

        public string GetPaymentAddress()
        {
            if (Fee == 0 && ((EMSFee.HasValue && EMSFee.Value == 0) || PermitDeliveryType != PermitDeliveryTypeValueConst.BY_MAIL))
            {
                return "-"; // Total fee = 0, No payment needed
            }
            var deliveryAddress = "-";
            switch (this.PaymentMethod)
            {
                case PaymentMethodValueConst.AT_OSS: deliveryAddress = System.Configuration.ConfigurationManager.AppSettings["OSSAddress"]; break;
                case PaymentMethodValueConst.AT_OWNER_ORG: deliveryAddress = !string.IsNullOrEmpty(this.PaymentMethodOrgAddress) ? this.PaymentMethodOrgAddress : this.OrgAddress; break;
                case PaymentMethodValueConst.BILL_PAYMENT: deliveryAddress = ResourceHelper.GetResourceWordWithDefault("BILL_PAYMENT_LOCATION_PAY_FEE", "ApplicationStatusRequests", "BILL_PAYMENT_LOCATION_PAY_FEE", System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName); break;
                case PaymentMethodValueConst.QR_CODE: deliveryAddress = ResourceHelper.GetResourceWordWithDefault("QR_CODE_LOCATION_PAY_FEE", "ApplicationStatusRequests", "QR_CODE_LOCATION_PAY_FEE", System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName); break;
                default: break;
            }
            return deliveryAddress;
        }

        public class RequestorInfo
        {
            public string Name { get; set; }
            public string Tel { get; set; }
        }

        public RequestorInfo GetRequestorInfo()
        {
            RequestorInfo info = new RequestorInfo();
            if (this.IdentityType == UserTypeEnum.Juristic)
            {
                if (this.Data.ContainsKey("REQUESTOR_INFORMATION"))
                {
                    var requestorInfo = this.Data["REQUESTOR_INFORMATION"];
                    if (requestorInfo.Data.ContainsKey("DROPDOWN_REQUESTOR_INFORMATION_TITLE_TEXT_0") &&
                        requestorInfo.Data.ContainsKey("REQUESTOR_INFORMATION_NAME_0") &&
                        requestorInfo.Data.ContainsKey("REQUESTOR_INFORMATION_LASTNAME_0"))
                    {
                        info.Name = string.Format("{0} {1} {2}", requestorInfo.Data["DROPDOWN_REQUESTOR_INFORMATION_TITLE_TEXT_0"].DefaultString()
                         , requestorInfo.Data["REQUESTOR_INFORMATION_NAME_0"].DefaultString()
                         , requestorInfo.Data["REQUESTOR_INFORMATION_LASTNAME_0"].DefaultString());
                    }
                    else
                    {
                        info.Name = "-";
                    }
                    info.Tel = requestorInfo.Data.TryGetString("ADDRESS_TELEPHONE_REQUESTOR_INFORMATION_ADDRESS_0", "-");
                }
                else
                {
                    info.Name = "-";
                    info.Tel = "-";
                }
            }
            else
            {
                var requestorInfo = this.Data["GENERAL_INFORMATION"];
                if (requestorInfo.Data.ContainsKey("CITIZEN_FULLNAME_TH"))
                {
                    info.Name = requestorInfo.Data["CITIZEN_FULLNAME_TH"];
                }
                else if (requestorInfo.Data.ContainsKey("DROPDOWN_CITIZEN_TITLE_TEXT") &&
                    requestorInfo.Data.ContainsKey("CITIZEN_NAME") &&
                    requestorInfo.Data.ContainsKey("CITIZEN_LASTNAME"))
                {
                    info.Name = string.Format("{0} {1} {2}",
                        requestorInfo.Data["DROPDOWN_CITIZEN_TITLE_TEXT"],
                        requestorInfo.Data["CITIZEN_NAME"],
                        requestorInfo.Data["CITIZEN_LASTNAME"]);
                }
                info.Tel = "-";
            }
            return info;
        }

        public ApplicationRequestEntity()
        {
            ApplicationRequestID = Guid.NewGuid();
            Data = new Dictionary<string, ApplicationRequestDataGroupEntity>();
            UploadedFiles = new List<FileGroupEntity>();
            GovFiles = new List<FileMetadataEntity>();
            RequestedFiles = new List<FileMetadataEntity>();
            BillPaymentFiles = new List<FileMetadataEntity>();
        }

        public static ApplicationRequestEntity Get(Guid id)
        {
            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            var request = repo.Where(o => o.ApplicationRequestID == id).SingleOrDefault();
            if (request != null)
            {
                request.UploadedFiles = request.GetUploadedFiles();
            }
            return request;
        }

        public static ApplicationRequestEntity GetRelateLicense(string licenseId, string identityID)
        {
            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            var request = repo.Where(o => o.Data["ELICENSE_INFORMATION"].Data["Identifier"] == licenseId && o.IdentityID == identityID).SingleOrDefault();
            if (request != null)
            {
                request.UploadedFiles = request.GetUploadedFiles();
            }
            return request;
        }

        public static ApplicationRequestEntity Get(Guid id, int appID, string identityID)
        {
            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            var request = repo.Where(o => o.ApplicationRequestID == id && o.ApplicationID == appID && o.IdentityID == identityID).SingleOrDefault();
            if (request != null)
            {
                request.UploadedFiles = request.GetUploadedFiles();
            }
            return request;
        }

        public static ApplicationRequestEntity GetByOrgCode(Guid id, string orgCode)
        {
            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            var request = repo.Where(o => o.ApplicationRequestID == id && o.OrgCode == orgCode).SingleOrDefault();
            if (request != null)
            {
                request.UploadedFiles = request.GetUploadedFiles();
            }
            return request;
        }

        public static ApplicationRequestEntity GetByMemberService(Guid id, List<int> serviceIds)
        {
            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            var request = repo.Where(o => o.ApplicationRequestID == id && serviceIds.Contains(o.ApplicationID)).SingleOrDefault();
            if (request != null)
            {
                request.UploadedFiles = request.GetUploadedFiles();
            }
            return request;
        }

        public static ApplicationRequestEntity GetByIdentity(Guid id, string identityID, UserTypeEnum identityType)
        {
            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            var request = repo.Where(o => o.ApplicationRequestID == id && o.IdentityID == identityID && o.IdentityType == identityType).SingleOrDefault();
            if (request != null)
            {
                request.UploadedFiles = request.GetUploadedFiles();
            }
            return request;
        }

        // Get Draft Request
        public static ApplicationRequestEntity Get(int appID, string identityID, UserTypeEnum identityType, ApplicationStatusV2Enum[] statusIDs = null)
        {
            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            var requestQuery = repo.Where(o => o.ApplicationID == appID && o.IdentityID == identityID && o.IdentityType == identityType);

            if (statusIDs != null)
            {
                requestQuery = requestQuery.Where(o => statusIDs.Contains(o.Status));
            }

            var request = requestQuery.OrderByDescending(o => o.CreatedDate).FirstOrDefault();

            if (request != null)
            {
                request.UploadedFiles = request.GetUploadedFiles();
            }
            return request;
        }

        public static bool HasRequest(Guid applicationRequestID, ApplicationStatusV2Enum[] statusIDs)
        {
            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            var hasRequest = repo.Where(o => o.ApplicationRequestID == applicationRequestID
                && statusIDs.Contains(o.Status)).Any();
            return hasRequest;
        }

        public static bool HasRequest(int appID, string identityID, ApplicationStatusV2Enum[] statusIDs)
        {
            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            var hasRequest = repo.Where(o => o.ApplicationID == appID
                && o.IdentityID == identityID
                && statusIDs.Contains(o.Status)).Any();
            return hasRequest;
        }

        public void Create()
        {
            CreatedDate = UpdatedDate = UpdatedDateByRequestor = UpdatedDateByAgent = DateTime.Now;
            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            MongoFactory.GetApplicationRequestCollection().InsertOne(this);
        }

        public void Update()
        {
            UpdatedDate = DateTime.Now;
            var coll = MongoFactory.GetApplicationRequestCollection();
            coll.Update(this);
        }

        public void UpdateFromRequestor()
        {
            UpdatedDateByRequestor = DateTime.Now;
            Update();
        }

        public void UpdateFromAgent(string agentUserID)
        {
            UpdatedDateByAgent = DateTime.Now;
            UpdatedByAgent = agentUserID;
            Update();
        }

        public void Delete()
        {
            var repo = MongoFactory.GetApplicationRequestCollection();
            var fgRepo = MongoFactory.GetFileGroupCollection();
            var fRepo = MongoFactory.GetFileMetadataCollection();

            var files = fRepo.AsQueryable().Where(o => o.ApplicationRequestID == ApplicationRequestID).Select(o => o.Id).ToArray();
            foreach (var file in files)
            {
                fRepo.Delete(file);
            }

            var groups = fgRepo.AsQueryable().Where(o => o.ApplicationRequestID == ApplicationRequestID).Select(o => o.Id).ToArray();
            foreach (var group in groups)
            {
                fgRepo.Delete(group);
            }

            repo.Delete(this);
        }

        public void RemoveUploadedFiles(Guid fileGroupID, string[] fileIDs)
        {
            var coll = MongoFactory.GetFileGroupCollection();
            var repo = coll.AsQueryable();

            //foreach (var group in UploadedFiles)
            //{
            //    group.Files = group.Files.Where(o => !fileIDs.Contains(o.FileID)).ToList();

            //    if (group.Files.Count == 0)
            //    {
            //        repo.Delete(group);
            //    }
            //    else
            //    {
            //        repo.Update(group);
            //    }
            //}

            var group = UploadedFiles.Where(o => o.FileGroupID == fileGroupID).SingleOrDefault();
            if (group != null)
            {
                group.Files = group.Files.Where(o => !fileIDs.Contains(o.FileID)).ToList();

                if (group.Files.Count == 0)
                {
                    coll.Delete(group);
                }
                else
                {
                    coll.Update(group);
                }
            }

            UploadedFiles = GetUploadedFiles();
        }

        public void RemoveGovFiles(string[] fileIDs)
        {
            var coll = MongoFactory.GetApplicationRequestCollection();
            GovFiles = GovFiles.Where(o => !fileIDs.Contains(o.FileID)).ToList();
            coll.Update(this);
        }

        public void RemoveRequestFiles(string[] fileIDs)
        {
            var coll = MongoFactory.GetApplicationRequestCollection();
            var repo = coll.AsQueryable();
            RequestedFiles = RequestedFiles.Where(o => o.Extras != null
                && (!o.Extras.ContainsKey(ExtraKeyEnum.REQUEST_FILE_ID.ToString()) || !fileIDs.Contains(o.Extras[ExtraKeyEnum.REQUEST_FILE_ID.ToString()])
                )).ToList();
            coll.Update(this);
        }

        public void RemoveEPermitFile(string[] fileIDs)
        {
            var coll = MongoFactory.GetApplicationRequestCollection();
            EPermitFiles = EPermitFiles.Where(o => !fileIDs.Contains(o.FileID)).ToList();
            coll.Update(this);
        }

        public void RemoveBillPaymentFile(string[] fileIDs)
        {
            var coll = MongoFactory.GetApplicationRequestCollection();
            BillPaymentFiles = BillPaymentFiles.Where(o => !fileIDs.Contains(o.FileID)).ToList();
            coll.Update(this);
        }

        private List<FileGroupEntity> GetUploadedFiles()
        {
            var repo = MongoFactory.GetFileGroupCollection().AsQueryable();
            return repo.Where(o => o.ApplicationRequestID == ApplicationRequestID).ToList();
        }

        public void GenerateRequestNumber()
        {
            if (string.IsNullOrEmpty(this.ApplicationRequestNumber))
            {
                var coll = MongoFactory.GetApplicationRequestCollection();
                var repo = coll.AsQueryable();

                string prefix = "N"; // Uncategorize prefix
                if (IdentityType == UserTypeEnum.Juristic)
                {
                    prefix = "J";
                }
                else if (IdentityType == UserTypeEnum.Citizen)
                {
                    prefix = "C";
                }
                ApplicationRequestRunningNumber = 1;

                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);
                var query = repo.Where(o => o.IdentityType == IdentityType && o.CreatedDate >= today && o.CreatedDate < tomorrow && Status != ApplicationStatusV2Enum.DRAFT);
                if (query.Any())
                {
                    ApplicationRequestRunningNumber = query.Max(o => o.ApplicationRequestRunningNumber) + 1;
                }
                ApplicationRequestNumber = string.Format("{0}{1:yy}{1:MM}{1:dd}{2:000}", prefix, today, ApplicationRequestRunningNumber);
                if (string.IsNullOrEmpty(this.Id))
                {
                    coll.InsertOne(this);
                }
                else
                {
                    coll.Update(this);
                }
            }
        }

        [BsonRepresentation(BsonType.String)]
        public Guid ApplicationRequestID { get; set; }

        /// <summary>
        /// เลขรับเรื่อง หรือ Request ID (RequestID)
        /// TYYMMDDRRR(10หลัก)
        /// T คือ ประเภทผู้ใช้บริการ คือ นิติบุคคล = J , บุคคลธรรมดา = C
        /// YY คือ ปี พ.ศ. 2 หลักท้าย เช่น 2559 YY = 59
        /// MM คือ เดือนเป็นตัวเลข เช่น สิงหาคม MM = 08
        /// DD คือ วันที่ เช่น วันที่ 29 DD = 29
        /// RRR คือ Running Number การส่งข้อมูลบริการในแต่ละวัน นับแยกตามประเภทผู้ใช้บริการ(เริ่มนับ 001 ใหม่ทุกวัน)
        /// เช่น C590829001, J590829001
        /// </summary>
        public string ApplicationRequestNumber { get; set; }

        public int ApplicationRequestRunningNumber { get; set; }

        [BsonIgnoreIfNull]
        public AppHookInfo AppHookInfo { get; set; }

        public List<ApplicationRequestTransactionEntity> Transactions { get; set; }

        /// <summary>
        /// ไฟล์เอกสารแนบ
        /// </summary>
        [BsonIgnore]
        public List<FileGroupEntity> UploadedFiles { get; set; }

        public List<ApplicationRequestChatEntity> Chats { get; set; }

        /// <summary>
        /// ข้อมูลเพิ่มเติมที่รับเพิ่มจากหน่วยงาน
        /// </summary>
        [BsonIgnore]
        public Dictionary<string, object> Extras { get; set; }

        public static void UpdateApplicationRequsetArea()
        {
            var requests = MongoFactory.GetDatabase().GetCollection<BsonDocument>("ApplicationRequest");
            var result = requests.Find("{$or:[{Province:{$exists:false}},{Province:null},{ProvinceID:{$type:'string'}}]}").ToList();

            foreach (var item in result)
            {
                BsonValue data = null;
                BsonValue address = null;
                BsonValue id = null;
                BsonValue applicationID = null;
                BsonValue provinceId = null;
                BsonValue districtId = null;
                BsonValue sectionId = null;
                BsonValue province = null;
                BsonValue district = null;
                BsonValue section = null;

                item.TryGetValue("ApplicationID", out applicationID);

                if (applicationID != null)
                {
                    if (applicationID == 1 || applicationID == 2)
                    {
                        // **** แบบเก่าจะไม่มีข้อมูลที่อยู่เก็บเลยต้องกำหนดให้เป็นกรุงเทพไปก่อน ****

                        // การขอขึ้นทะเบียนนายจ้างและผู้ประกันตน
                        // SSO Register Employee

                        item.TryGetValue("_id", out id);

                        provinceId = 10;
                        districtId = 0;
                        sectionId = 0;

                        province = "กรุงเทพมหานคร";
                        district = "";
                        section = "";
                    }
                    else if (applicationID == 9)
                    {
                        // แบบฟอร์มขอจดทะเบียนภาษีมูลค่าเพิ่ม

                        item.TryGetValue("_id", out id);

                        if (item.TryGetValue("Data", out data))
                        {
                            if (data.AsBsonDocument.TryGetValue("JURISTIC_ADDRESS_INFORMATION", out address))
                            {
                                // ของใหม่
                                var area = address["Data"].AsBsonDocument;

                                area.TryGetValue("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS", out provinceId);
                                area.TryGetValue("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS", out districtId);
                                area.TryGetValue("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS", out sectionId);

                                area.TryGetValue("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT", out province);
                                area.TryGetValue("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT", out district);
                                area.TryGetValue("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT", out section);
                            }
                            else
                            {
                                // **** แบบเก่าจะไม่มีข้อมูลที่อยู่เก็บเลยต้องกำหนดให้เป็นกรุงเทพไปก่อน ****

                                provinceId = 10;
                                districtId = 0;
                                sectionId = 0;

                                province = "กรุงเทพมหานคร";
                                district = "";
                                section = "";
                            }
                        }
                    }
                    else if (applicationID == 5 || applicationID == 6 || applicationID == 7 || applicationID == 8 || applicationID == 10)
                    {
                        // ขอใช้บริการโทรศัพท์พื้นฐาน และอินเทอร์เน็ต 7
                        // แบบฟอร์มขอใช้ไฟฟ้า 5
                        // แบบฟอร์มขอใช้น้ำประปา 6
                        // แบบฟอร์มขอใช้ไฟฟ้า (ภูมิภาค) 8
                        // แบบฟอร์มขอใช้น้ำประปา (ภูมิภาค) 10

                        item.TryGetValue("_id", out id);

                        if (item.TryGetValue("Data", out data))
                        {
                            if (data.AsBsonDocument.TryGetValue("UTILITY_ADDRESS_INFORMATION", out address))
                            {
                                var area = address["Data"].AsBsonDocument;

                                area.TryGetValue("ADDRESS_PROVINCE_UTILITY_ADDRESS", out provinceId);
                                area.TryGetValue("ADDRESS_AMPHUR_UTILITY_ADDRESS", out districtId);
                                area.TryGetValue("ADDRESS_TUMBOL_UTILITY_ADDRESS", out sectionId);

                                area.TryGetValue("ADDRESS_PROVINCE_UTILITY_ADDRESS_TEXT", out province);
                                area.TryGetValue("ADDRESS_AMPHUR_UTILITY_ADDRESS_TEXT", out district);
                                area.TryGetValue("ADDRESS_TUMBOL_UTILITY_ADDRESS_TEXT", out section);
                            }
                            else
                            {
                                // **** แบบเก่าข้อมูลจะเก็บที่ ADDRESS_INFORMATION แต่ถ้าไม่มีข้อมูลเลยต้องกำหนดให้เป็นกรุงเทพไปก่อน ****

                                if (data.AsBsonDocument.TryGetValue("ADDRESS_INFORMATION", out address))
                                {
                                    var area = address["Data"].AsBsonDocument;

                                    area.TryGetValue("ADDRESS_PROVINCE_ID", out provinceId);
                                    area.TryGetValue("ADDRESS_AMPHUR_ID", out districtId);
                                    area.TryGetValue("ADDRESS_TAMBOL_ID", out sectionId);

                                    area.TryGetValue("ADDRESS_PROVINCE", out province);
                                    area.TryGetValue("ADDRESS_AMPHUR", out district);
                                    area.TryGetValue("ADDRESS_TAMBOL", out section);
                                }
                                else
                                {
                                    provinceId = 10;
                                    districtId = 0;
                                    sectionId = 0;

                                    province = "กรุงเทพมหานคร";
                                    district = "";
                                    section = "";
                                }
                            }
                        }
                    }
                }

                if (id != null)
                {
                    requests.FindOneAndUpdate
                    (
                        Builders<BsonDocument>.Filter.Eq("_id", id),
                        Builders<BsonDocument>.Update.Set("ProvinceID", BsonStringToInt(provinceId))
                                                        .Set("AmphurID", BsonStringToInt(districtId))
                                                        .Set("TumbolID", BsonStringToInt(sectionId))
                                                        .Set("Province", province)
                                                        .Set("Amphur", district)
                                                        .Set("Tumbol", section)
                    );
                }
            }
        }

        public static void FixWrongStatus(string oldstatus, string newstatus)
        {
            var requests = MongoFactory.GetDatabase().GetCollection<BsonDocument>("ApplicationRequest");
            var result = requests.Find("{'Status' : '" + oldstatus + "'}").Project("{'Status' : 1}").ToList();

            // request level
            foreach (var item in result)
            {
                var id = null as BsonValue;
                var status = item["Status"].ToString();

                if (status == oldstatus)
                {
                    if (item.TryGetValue("_id", out id))
                    {
                        requests.FindOneAndUpdate
                        (
                            Builders<BsonDocument>.Filter.Eq("_id", id),
                            Builders<BsonDocument>.Update.Set("Status", newstatus)
                        );
                    }
                }
            }

            // transaction level
            result = requests.Find("{'Transactions.Status' : '" + oldstatus + "'}").Project("{'Transactions.Status' : 1}").ToList();

            foreach (var item in result)
            {
                var transactionIndex = 0;
                var id = null as BsonValue;
                var transactions = item["Transactions"] as BsonArray;

                foreach (var transaction in transactions)
                {
                    var status = transaction["Status"].ToString();

                    if (status == oldstatus)
                    {
                        if (item.TryGetValue("_id", out id))
                        {
                            requests.FindOneAndUpdate
                            (
                                Builders<BsonDocument>.Filter.Eq("_id", id),
                                Builders<BsonDocument>.Update.Set("Transactions." + transactionIndex + ".Status", newstatus)
                            );
                        }
                    }

                    transactionIndex++;
                }
            }
        }

        public static void FixWrongApplicationID(int oldId, int newId)
        {
            var requests = MongoFactory.GetDatabase().GetCollection<BsonDocument>("ApplicationRequest");
            var result = requests.Find("{'ApplicationID' : " + oldId + "}").Project("{'ApplicationID' : 1}").ToList();

            // request level
            foreach (var item in result)
            {
                var id = null as BsonValue;
                var applicationId = item["ApplicationID"].ToInt32();

                if (applicationId == oldId)
                {
                    if (item.TryGetValue("_id", out id))
                    {
                        requests.FindOneAndUpdate
                        (
                            Builders<BsonDocument>.Filter.Eq("_id", id),
                            Builders<BsonDocument>.Update.Set("ApplicationID", newId)
                        );
                    }
                }
            }

            // transaction level
            result = requests.Find("{'Transactions.ApplicationID' : " + oldId + "}").Project("{'Transactions.ApplicationID' : 1}").ToList();

            foreach (var item in result)
            {
                var transactionIndex = 0;
                var id = null as BsonValue;
                var transactions = item["Transactions"] as BsonArray;

                foreach (var transaction in transactions)
                {
                    var applicationId = transaction["ApplicationID"].ToInt32();

                    if (applicationId == oldId)
                    {
                        if (item.TryGetValue("_id", out id))
                        {
                            requests.FindOneAndUpdate
                            (
                                Builders<BsonDocument>.Filter.Eq("_id", id),
                                Builders<BsonDocument>.Update.Set("Transactions." + transactionIndex + ".ApplicationID", newId)
                            );
                        }
                    }

                    transactionIndex++;
                }
            }
        }

        public static void FixWrongUpdateSectionControlID(string section, string control, string oldValue, string newValue)
        {
            var requests = MongoFactory.GetDatabase().GetCollection<BsonDocument>("ApplicationRequest");
            var result = requests.Find("{ 'Data." + section + ".Data." + control + "': { $ne: null }}").Project("{'Data." + section + ".Data." + control + "' : 1}").ToList();

            // request level
            foreach (var item in result)
            {
                var id = null as BsonValue;
                BsonValue data = null;
                BsonValue dSection = null;
                BsonValue dValue = null;
                if (item.TryGetValue("Data", out data))
                {
                    if (data.AsBsonDocument.TryGetValue(section, out dSection))
                    {
                        var dControl = dSection["Data"].AsBsonDocument;

                        dControl.TryGetValue(control, out dValue);
                        if (dValue == oldValue)
                        {
                            if (item.TryGetValue("_id", out id))
                            {
                                requests.FindOneAndUpdate
                                (
                                    Builders<BsonDocument>.Filter.Eq("_id", id),
                                    Builders<BsonDocument>.Update.Set("Data." + section + ".Data." + control, newValue)
                                );

                            }
                        }
                    }

                }

            }
            // transaction level
            result = requests.Find("{'Transactions': { $ne: null }}").Project("{'Transactions' : 1}").ToList();

            foreach (var item in result)
            {
                var transactionIndex = 0;
                var id = null as BsonValue;
                BsonValue data = null;
                BsonValue tSection = null;
                BsonValue tValue = null;
                var transactions = item["Transactions"] as BsonArray;

                foreach (var transaction in transactions)
                {


                    if (transaction["Data"].BsonType != BsonType.Null)
                    {
                        data = transaction["Data"] as BsonValue;
                        if (data.AsBsonDocument.TryGetValue(section, out tSection))
                        {
                            var tControl = tSection["Data"].AsBsonDocument;

                            tControl.TryGetValue(control, out tValue);
                            if (tValue == oldValue)
                            {
                                if (item.TryGetValue("_id", out id))
                                {
                                    requests.FindOneAndUpdate
                                    (
                                        Builders<BsonDocument>.Filter.Eq("_id", id),
                                        Builders<BsonDocument>.Update.Set("Transactions." + transactionIndex + ".Data." + section + ".Data." + control, newValue)
                                    );

                                }
                            }
                        }

                    }
                    transactionIndex++;
                }
            }
        }

        public ApplicationRequestEntity AddExtraData(string sectionName, string fieldName, string value)
        {
            ApplicationRequestEntity request = this;
            if (!request.Data.ContainsKey(sectionName))
            {
                request.Data.Add(sectionName, new ApplicationRequestDataGroupEntity()
                {
                    GroupName = sectionName,
                    GroupDescription = string.Empty
                });
            }
            var secData = request.Data[sectionName].Data;
            if (secData.ContainsKey(fieldName))
            {
                secData.Remove(fieldName);
            }
            secData.Add(fieldName, value);

            return request;
        }

        [BsonIgnore]
        public bool IsPendingSigning { get; set; }

        private static int? BsonStringToInt(BsonValue data)
        {
            try
            {
                return data.ToInt32();
            }
            catch (Exception)
            {
            }

            return null;
        }
    }

    public class AppHookInfo
    {
        public string AppsStage { get; set; }

        public string Message { get; set; }

        public string Data { get; set; }

        public string SentData { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? ExceuteDate { get; set; }

        public string Result { get; set; }

        public bool Schedule { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? ScheduleDate { get; set; }

        public int ScheduleCount { get; set; }
    }
}
