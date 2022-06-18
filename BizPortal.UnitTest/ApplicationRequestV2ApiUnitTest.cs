using BizPortal.Areas.WebApiV2.Controllers;
using BizPortal.DAL;
using BizPortal.DAL.MongoDB;
using BizPortal.Models;
using BizPortal.ViewModels.V2;
using EGA.EGA_Development.Util.MailV2.Data;
using EGA.Owin.Security.EGAOpenID;
using Mapster;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace BizPortal.UnitTest
{

    [TestClass]
    public class ApplicationRequestV2ApiUnitTest
    {
        #region[Initial]

        string citizenId = "1709900381574";
        string juristicId = "0503560002791";
        int applicationId = 59;
        string applicationRequestModel = "{\"ApplicationID\":59,\"Status\":3,\"StatusOther\":\"WAITING_AGENT_READ_REQUEST\",\"StatusOtherText\":null,\"StatusBeforeCancel\":null,\"ActionReply\":0,\"PermitDeliveryAddress\":null,\"PermitDeliveryType\":null,\"PermitDeliverOnPayment_OK\":false,\"PaymentMethod\":null,\"PaymentMethodEnabledChoice\":null,\"BillPaymentTypeForPaymentMethod\":null,\"PermitDeliveryTypeEnabledChoice\":null,\"EMSTrackingNumber\":null,\"DisableUpdateStatus\":null,\"ApplicationRequestNumber\":null,\"ApplicationRequestRunningNumber\":0,\"IsRequestNumberAgent\":false,\"ApplicationRequestNumberAgent\":null,\"IdentityID\":\"5633071108732\",\"IdentityName\":\"นาย test14 test\",\"IdentityType\":0,\"ApplicationRequestID\":null,\"RequestBatchID\":\"dcc9787a-4b0f-46ab-94fe-0eefb972d382\",\"Fee\":null,\"EMSFee\":50.0,\"DueDateForPayFee\":null,\"Duration\":null,\"PermitCanBeDeliveredOnPayment\":false,\"UserCanGetAppDate\":null,\"ExpectedFinishDate\":null,\"ProvinceID\":null,\"AmphurID\":null,\"TumbolID\":null,\"Province\":null,\"Amphur\":null,\"Tumbol\":null,\"SourceIPAddress\":\"::1\",\"ReplyFromOrg\":false,\"ReplyFromApiUpdate\":false,\"TransactionCode\":null,\"TransactionDate\":null,\"Data\":{\"SELL_FOOD_INFORMATION\":{\"GroupName\":\"SELL_FOOD_INFORMATION\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"SELL_FOOD_INFORMATION__APP_TYPE_OPTION\":\"หนังสือรับรองการแจ้ง\",\"SELL_FOOD_INFORMATION__LICENSE_TYPE_OPTION\":\"หนังสือรับรองการแจ้ง\",\"SELL_FOOD_INFORMATION__BUSINESS_TYPE_OPTION\":\"APP_SELL_FOOD_LOCATION_TYPE_OPTION__SELL\",\"SELL_FOOD_INFORMATION__PURPOSE_OPTION\":\"APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__SELL\",\"SELL_FOOD_INFORMATION__FOOD_TYPE\":\"test\"}},\"SELL_FOOD_FOOD_WORKER_INFO\":{\"GroupName\":\"SELL_FOOD_FOOD_WORKER_INFO\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"SELL_FOOD_FOOD_WORKER_INFO_TOTAL\":\"0\"}},\"SELL_FOOD_FOOD_MANAGER_INFO\":{\"GroupName\":\"SELL_FOOD_FOOD_MANAGER_INFO\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__TITLE\":\"01\",\"SELL_FOOD_FOOD_MANAGER_INFO__NAME\":\"test\",\"SELL_FOOD_FOOD_MANAGER_INFO__LASTNAME\":\"test\",\"SELL_FOOD_FOOD_MANAGER_INFO__AGE\":\"31\",\"DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__NATIONALITY\":\"70\",\"ADDRESS_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"123\",\"ADDRESS_MOO_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"ADDRESS_SOI_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"ADDRESS_ROAD_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"ADDRESS_PROVINCE_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"10\",\"ADDRESS_AMPHUR_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"05\",\"ADDRESS_TUMBOL_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"08\",\"ADDRESS_TELEPHONE_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"0123456789\",\"ADDRESS_TELEPHONE_EXT_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"ADDRESS_FAX_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"SELL_FOOD_CONFIRM_SELL_FOOD_CONFIRM__TRUE\":\"true\",\"DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__TITLE_TEXT\":\"นาย\",\"DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__NATIONALITY_TEXT\":\"ไทย\",\"ADDRESS_PROVINCE_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS_TEXT\":\"กรุงเทพมหานคร\",\"ADDRESS_AMPHUR_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS_TEXT\":\"เขตบางเขน\",\"ADDRESS_TUMBOL_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS_TEXT\":\"ท่าแร้ง\"}},\"CITIZEN_GENERAL_INFORMATION_HEADER\":{\"GroupName\":\"CITIZEN_GENERAL_INFORMATION_HEADER\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{}},\"GENERAL_INFORMATION\":{\"GroupName\":\"GENERAL_INFORMATION\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"INFORMATION_HEADER__REQUEST_DATE\":\"1/3/2562 16:21:43\",\"INFORMATION_HEADER__REQUEST_AT\":\"Biz Portal\",\"INFORMATION__REQUEST_AS_OPTION\":\"บุคคลธรรมดา\",\"DROPDOWN_CITIZEN_TITLE\":\"01\",\"CITIZEN_NAME\":\"test14\",\"CITIZEN_LASTNAME\":\"test\",\"GENERAL_INFORMATION__CITIZEN_AGE\":\"31\",\"DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY\":\"70\",\"IDENTITY_ID\":\"5633071108732\",\"DROPDOWN_CITIZEN_TITLE_TEXT\":\"นาย\",\"DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT\":\"ไทย\"}},\"CITIZEN_ADDRESS_INFORMATION\":{\"GroupName\":\"CITIZEN_ADDRESS_INFORMATION\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"ADDRESS_CITIZEN_ADDRESS\":\"123\",\"ADDRESS_MOO_CITIZEN_ADDRESS\":\"\",\"ADDRESS_SOI_CITIZEN_ADDRESS\":\"\",\"ADDRESS_BUILDING_CITIZEN_ADDRESS\":\"\",\"ADDRESS_ROOMNO_CITIZEN_ADDRESS\":\"\",\"ADDRESS_FLOOR_CITIZEN_ADDRESS\":\"\",\"ADDRESS_ROAD_CITIZEN_ADDRESS\":\"\",\"ADDRESS_PROVINCE_CITIZEN_ADDRESS\":\"10\",\"ADDRESS_AMPHUR_CITIZEN_ADDRESS\":\"15\",\"ADDRESS_TUMBOL_CITIZEN_ADDRESS\":\"04\",\"ADDRESS_POSTCODE_CITIZEN_ADDRESS\":\"10600\",\"ADDRESS_TELEPHONE_CITIZEN_ADDRESS\":\"0123456789\",\"ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS\":\"\",\"ADDRESS_FAX_CITIZEN_ADDRESS\":\"\",\"ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT\":\"กรุงเทพมหานคร\",\"ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT\":\"เขตธนบุรี\",\"ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT\":\"บุคคโล\"}},\"REQUESTOR_INFORMATION__HEADER\":{\"GroupName\":\"REQUESTOR_INFORMATION__HEADER\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION\":\"REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER\"}},\"INFORMATION_STORE\":{\"GroupName\":\"INFORMATION_STORE\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"INFORMATION_STORE__USE_CITIZEN_ADDRESS_INFORMATION_STORE__USE_CITIZEN_ADDRESS__TRUE\":\"true\",\"INFORMATION_STORE_NAME_TH\":\"test\",\"INFORMATION_STORE_NAME_EN\":\"\",\"ADDRESS_INFORMATION_STORE__ADDRESS\":\"123\",\"ADDRESS_MOO_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_SOI_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_ROAD_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS\":\"10\",\"ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS\":\"15\",\"ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS\":\"04\",\"ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS\":\"10600\",\"ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS\":\"0123456789\",\"ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_FAX_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_LAT_INFORMATION_STORE__ADDRESS\":\"13.7553323\",\"ADDRESS_LNG_INFORMATION_STORE__ADDRESS\":\"100.5226751\",\"SEARCH_TEXT_GOOGLE_MAP\":\"บุคคโล เขตธนบุรี กรุงเทพมหานคร\",\"INFORMATION_STORE_AREA\":\"50\",\"CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION\":\"INFORMATION_STORE_BUILDING_TYPE_OPTION__OWNED\",\"INFORMATION_STORE_HEALTH_OTHER\":\"\",\"ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT\":\"กรุงเทพมหานคร\",\"ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT\":\"เขตธนบุรี\",\"ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT\":\"บุคคโล\"}},\"OPENID\":{\"GroupName\":\"OPENID\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"FULLNAME\":\"นาย test14 test\",\"PREFIX_TH\":\"นาย\",\"FIRSTNAME_TH\":\"test14\",\"LASTNAME_TH\":\"test\",\"GENDER\":\"ชาย\"}}},\"Remark\":null,\"OrgNameTH\":null,\"OrgAddress\":null,\"DisabledSendingSystemEmail\":false,\"EmailMessage\":null,\"SmsMessage\":null,\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"UploadedFiles\":[{\"FileGroupID\":\"a51f62b2-1cc6-48a0-9ac2-da3c923f8b4a\",\"Description\":\"CITIZEN_INFORMATION\",\"Files\":[{\"FileID\":\"5c78fd6a70b8b312889bf296\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"ID_CARD_COPY\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"ID_CARD_COPY\",\"DISPLAYNAME\":\"เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: บุคคลผู้ขออนุญาต *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7070b8b312889bf299\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"CITIZEN_RENAME_MARRIAGE_DOC\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"CITIZEN_RENAME_MARRIAGE_DOC\",\"DISPLAYNAME\":\"ใบสำคัญการเปลี่ยนชื่อ/ทะเบียนสมรส\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7370b8b312889bf29c\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"CITIZEN_BKK_FOOD_HEALTH_CERT\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"CITIZEN_BKK_FOOD_HEALTH_CERT\",\"DISPLAYNAME\":\"หนังสือรับรองผ่านการอบรมหลักสูตรสุขาภิบาลอาหารของกรุงเทพมหานคร: บุคคลผู้ขออนุญาต\",\"PREDOC\":\"false\"},\"TYPE\":null}],\"Extras\":null},{\"FileGroupID\":\"791addfd-7f9f-472c-bec6-202e236b67aa\",\"Description\":\"INFORMATION_STORE_FILE_SECTION\",\"Files\":[{\"FileID\":\"5c78fd7670b8b312889bf29f\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT\",\"DISPLAYNAME\":\"หนังสือแจ้งการใช้หรือเปลี่ยนแปลงการใช้ประโยชน์ที่ดินตามกฎหมายว่าด้วยการผังเมือง *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7870b8b312889bf2a2\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_BUILDING_OWNER_DOC\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_BUILDING_OWNER_DOC\",\"DISPLAYNAME\":\"เอกสารแสดงความเป็นเจ้าของอาคารสถานที่ที่ใช้เป็นร้าน/สถานประกอบการ เช่น ใบอนุญาตก่อสร้าง หรือสัญญาซื้อขาย *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7b70b8b312889bf2a5\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_MAP\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_MAP\",\"DISPLAYNAME\":\"แผนที่สังเขป แสดงสถานที่ตั้งของร้าน/สถานประกอบการ *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7e70b8b312889bf2a8\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD\",\"DISPLAYNAME\":\"ทะเบียนบ้าน:อาคารที่ตั้งร้าน / สถานประกอบการ *\",\"PREDOC\":\"false\"},\"TYPE\":null}],\"Extras\":null},{\"FileGroupID\":\"a910792d-7a4e-41a1-af51-49668e13a859\",\"Description\":\"BUILDING_FILE_SECTION\",\"Files\":[{\"FileID\":\"5c78fd8170b8b312889bf2ab\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_BUILDING_CONTROL_DOC\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_BUILDING_CONTROL_DOC\",\"DISPLAYNAME\":\"หลักฐานเกี่ยวกับการใช้อาคารตามกฏหมายว่าด้วยการควบคุมอาคาร เช่น ใบอนุญาตก่อสร้าง (อ.1) หรือใบรับรองการเปิดใช้อาคาร *\",\"PREDOC\":\"false\"},\"TYPE\":null}],\"Extras\":null},{\"FileGroupID\":\"77c76377-97f9-4d48-9de5-e67e8209652d\",\"Description\":\"SELL_FOOD_FILE_SECTION\",\"Files\":[{\"FileID\":\"5c78fd8370b8b312889bf2ae\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"SELL_FOOD_POLUTION_CONTROL_CHART\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"SELL_FOOD_POLUTION_CONTROL_CHART\",\"DISPLAYNAME\":\"แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงการป้องกันมลพิษ *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd8770b8b312889bf2b1\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"SELL_FOOD_HEALTH_CONTROL_CHART\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"SELL_FOOD_HEALTH_CONTROL_CHART\",\"DISPLAYNAME\":\"แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงสุขลักษณะภายในสถานประกอบการ *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd8a70b8b312889bf2b4\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"SELL_FOOD_SEFTY_CONTROL_CHART\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"SELL_FOOD_SEFTY_CONTROL_CHART\",\"DISPLAYNAME\":\"แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงระบบความปลอดภัยในการทำงาน *\",\"PREDOC\":\"false\"},\"TYPE\":null}],\"Extras\":null}],\"GovFiles\":null,\"RequestedFiles\":null,\"EPermitFiles\":null,\"EPermitFilesForDisplay\":null,\"BillPaymentFiles\":null,\"AttachBillPayment\":null,\"Attachments\":null,\"CreatedDate\":\"0001-01-01T00:00:00\",\"UpdatedDate\":\"0001-01-01T00:00:00\",\"UpdatedDateByRequestor\":\"2019-03-03T12:46:49.0155433+07:00\",\"UpdatedDateByAgent\":\"0001-01-01T00:00:00\",\"UpdatedByAgent\":null,\"LastRequestorUpdateEmail\":null,\"isPassStepWaiting\":null,\"Chats\":null,\"CgdPayments\":null}";

        ApplicationsController ctrl = new ApplicationsController();
        ApplicationDbContext db = new ApplicationDbContext();

        private async Task<Guid> init(UserTypeEnum userType, bool isLicensing = true, ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK, string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, string deliveryType = PermitDeliveryTypeValueConst.BY_MAIL)
        {
            // init user
            var user = userType == UserTypeEnum.Juristic ? db.Users.Where(e => e.JuristicID != null && e.Email != null).FirstOrDefault() : db.Users.Where(e => e.CitizenID == citizenId).FirstOrDefault();

            // init identity
            var identityId = userType == UserTypeEnum.Juristic ? user.JuristicID : user.CitizenID;
            var identity = new GenericIdentity(user.UserName);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.FullName ?? ""));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email ?? ""));
            identity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.UserType, userType == UserTypeEnum.Juristic ? UserTypeEnum.Juristic.GetEnumStringValue() : UserTypeEnum.Citizen.GetEnumStringValue()));
            identity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.JuristicID, user.JuristicID ?? ""));
            identity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.CitizenID, user.CitizenID ?? ""));
            identity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.PassportID, user.PassportID ?? ""));
            identity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.FullName, user.FullName ?? ""));
            Thread.CurrentPrincipal = new GenericPrincipal(identity, null);

            // init application request
            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            var appRequest = repo.Where(o => o.IdentityID == identityId && o.ApplicationID == applicationId).FirstOrDefault();

            if (appRequest == null)
            {
                var appRequestModel = JsonConvert.DeserializeObject<ApplicationRequestViewModel>(applicationRequestModel);
                ctrl.Request = new HttpRequestMessage();
                var response = await ctrl.Requests(appRequestModel);
                appRequest = response.Data.Adapt<ApplicationRequestEntity>();
                appRequest.PermitDeliveryType = deliveryType;
                appRequest.Update();
            }
            else
            {
                appRequest.Status = status;
                appRequest.StatusOther = statusOther;
                appRequest.NoDocument = !isLicensing;
                appRequest.PermitDeliveryType = deliveryType;
                appRequest.Update();
            }

            return appRequest.ApplicationRequestID;
        }

        private async Task<ApplicationRequestEntity> createRequest(UserTypeEnum userType, int applicationId, Guid applicationRequestId, ApplicationStatusV2Enum status, string statusOther = "", FileMetadata[] requestedFiles = null, int fee = 0, string emsTrackingNumber = null, string userCanGetAppDate = null)
        {
            var model = new ApplicationRequestViewModel
            {
                ApplicationRequestID = applicationRequestId,
                ApplicationID = applicationId,
                Status = status,
                StatusOther = statusOther,
                RequestedFiles = requestedFiles,
                Fee = fee,
                DueDateForPayFee = fee > 0 ? DateTime.Now.AddDays(30).ToString("dd/MM/yyyy", new CultureInfo("en")) : null
            };

            if (userType == UserTypeEnum.Citizen)
            {
                model.IdentityType = UserTypeEnum.Citizen;
                model.IdentityID = citizenId;
            }

            if (userType == UserTypeEnum.Juristic)
            {
                model.IdentityType = UserTypeEnum.Juristic;
                model.IdentityID = juristicId;
            }

            if (model.Fee != null && model.Fee > 0)
            {
                model.PaymentMethodEnabledChoice = new string[] { "BILL_PAYMENT" };
                model.BillPaymentTypeForPaymentMethod = "OWNER_ORG";
                model.PermitDeliveryTypeEnabledChoice = new string[] { "BY_MAIL" };
                model.AttachBillPayment = new Base64Attachment[] {
                    new Base64Attachment
                    {
                        Base64String = "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ICAgICAgICA8eG1wOk1ldGFkYXRhRGF0ZT4yMDE4LTEyLTI0VDE1OjQyOjE1KzA3OjAwPC94bXA6TWV0YWRhdGFEYXRlPgogICAgICA8L3JkZjpEZXNjcmlwdGlvbj4KICAgICAgPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIKICAgICAgICAgICAgeG1sbnM6cGRmPSJodHRwOi8vbnMuYWRvYmUuY29tL3BkZi8xLjMvIj4KICAgICAgICAgPHBkZjpQcm9kdWNlcj5BY3JvYmF0IERpc3RpbGxlciAxMC4xLjEgKFdpbmRvd3MpPC9wZGY6UHJvZHVjZXI+CiAgICAgIDwvcmRmOkRlc2NyaXB0aW9uPgogICAgICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgICAgICAgICB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iPgogICAgICAgICA8ZGM6Zm9ybWF0PmFwcGxpY2F0aW9uL3BkZjwvZGM6Zm9ybWF0PgogICAgICAgICA8ZGM6Y3JlYXRvcj4KICAgICAgICAgICAgPHJkZjpTZXE+CiAgICAgICAgICAgICAgIDxyZGY6bGkvPgogICAgICAgICAgICA8L3JkZjpTZXE+CiAgICAgICAgIDwvZGM6Y3JlYXRvcj4KICAgICAgICAgPGRjOnRpdGxlPgogICAgICAgICAgICA8cmRmOkFsdD4KICAgICAgICAgICAgICAgPHJkZjpsaSB4bWw6bGFuZz0ieC1kZWZhdWx0Ii8+CiAgICAgICAgICAgIDwvcmRmOkFsdD4KICAgICAgICAgPC9kYzp0aXRsZT4KICAgICAgPC9yZGY6RGVzY3JpcHRpb24+CiAgICAgIDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiCiAgICAgICAgICAgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIj4KICAgICAgICAgPHhtcE1NOkRvY3VtZW50SUQ+dXVpZDplODU0YjJjZi1hMzQ3LTQ4ZmUtYWRiNy1lMmYxMDgyYTBiMDE8L3htcE1NOkRvY3VtZW50SUQ+CiAgICAgICAgIDx4bXBNTTpJbnN0YW5jZUlEPnV1aWQ6MTM0YWVmZTAtOWE4MC00NzY1LTg4NjItMzA3MDllOGQ4MTI5PC94bXBNTTpJbnN0YW5jZUlEPgogICAgICA8L3JkZjpEZXNjcmlwdGlvbj4KICAgPC9yZGY6UkRGPgo8L3g6eG1wbWV0YT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgIAo8P3hwYWNrZXQgZW5kPSJ3Ij8+DQplbmRzdHJlYW0NZW5kb2JqDTMgMCBvYmoNPDwvRmlsdGVyL0ZsYXRlRGVjb2RlL0ZpcnN0IDQvTGVuZ3RoIDQ5L04gMS9UeXBlL09ialN0bT4+c3RyZWFtDQpo3rJQMFCwsdF3zi/NK1Ew1PfOTCmONjQCCgbF6odUFqTqBySmpxbb2QEEGADf+gutDQplbmRzdHJlYW0NZW5kb2JqDTQgMCBvYmoNPDwvRmlsdGVyL0ZsYXRlRGVjb2RlL0ZpcnN0IDQvTGVuZ3RoIDE1MC9OIDEvVHlwZS9PYmpTdG0+PnN0cmVhbQ0KaN5sjLEKwjAUAH/lbU0QkrzQoJZSKHYVCoouXWoTMBAaeX3F37eDOLndcHdHMFDXul35mUlIfaIwcsxzN3IQXWUNHtDaEl254c7sC2OKr7X5/WWi+GKnfEpwC7RsJThllZX6nP2fCbrfpKfs1ymQaCfKj5GhiwvHlAIBGoUKYRD3OPv8XgYp9TVyCkI2zUeAAQDmXjPVDQplbmRzdHJlYW0NZW5kb2JqDTUgMCBvYmoNPDwvRGVjb2RlUGFybXM8PC9Db2x1bW5zIDQvUHJlZGljdG9yIDEyPj4vRmlsdGVyL0ZsYXRlRGVjb2RlL0lEWzwyMDhCMzIwNjNBREJDMjg4QzQ1RTcxNzEwQjA5Mzk4RD48MUE5MkE1MDQ5ODU1M0Q0QTlFMjRGMjZCMEUwQjg2MDk+XS9JbmZvIDkgMCBSL0xlbmd0aCA0OC9Sb290IDExIDAgUi9TaXplIDEwL1R5cGUvWFJlZi9XWzEgMiAxXT4+c3RyZWFtDQpo3mJiAAImxrSlDEwMjG1Agi8fxOoFEZ+BEp3PgSwGBkYgwfQfSDAyAAQYAJT/BhsNCmVuZHN0cmVhbQ1lbmRvYmoNc3RhcnR4cmVmDQoxMTYNCiUlRU9GDQo=",
                        ContentType= "application/pdf",
                        FileName = "test.pdf"
                    }
                };
            }

            if (!string.IsNullOrEmpty(emsTrackingNumber))
            {
                model.EMSTrackingNumber = emsTrackingNumber;
            }

            if (!string.IsNullOrEmpty(userCanGetAppDate))
            {
                model.UserCanGetAppDate = userCanGetAppDate;
            }

            var request = new HttpRequestMessage();
            request.Headers.Add("Consumer-Key", "59a92baa-4418-4b69-8ea9-67eecc042aa2");
            ctrl.Request = request;

            HttpContext.Current = GetMockedHttpContext();

            var response = await ctrl.Requests(model);

            return response.Data.Adapt<ApplicationRequestEntity>();
        }

        private async Task clean()
        {
            var userManager = new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>(db));
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(db));
        }

        private HttpContext GetMockedHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://localhost:45598/", "");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                    new HttpStaticObjectsCollection(), 10, true,
                                                    HttpCookieMode.AutoDetect,
                                                    SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                                    null, CallingConventions.Standard,
                                                    new[] { typeof(HttpSessionStateContainer) },
                                                    null)
                                              .Invoke(new object[] { sessionContainer });

            return httpContext;
        }

        #endregion

        #region[Step 1 ตรวจสอบเอกสาร]

        /// <summary>
        /// ตรวจสอบเอกสาร กำลังรอเจ้าหน้าที่ทำงาน
        /// </summary>
        [TestMethod]
        public async Task CheckWithAgentWorking()
        {
            // arrange 
            var userType = UserTypeEnum.Citizen;
            var applicationRequestId = await init(userType);
            var status = ApplicationStatusV2Enum.CHECK;
            var statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
            var requestedFiles = null as FileMetadata[];

            // act
            var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

            // assert
            Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.CHECK && appRequest.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING);
        }

        ///// <summary>
        ///// ตรวจสอบเอกสาร มีการแก้ไขรอ ผปก. ทำการแก้ไข
        ///// </summary>
        //[TestMethod]
        //public async Task CheckWithUserWorking()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.CHECK;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.CHECK && appRequest.ActionReply == "ADJUST");
        //}

        ///// <summary>
        ///// ตรวจสอบเอกสาร ต้องการเอกสารเพิ่มรอ ผปก. ยื่นเอกสารเพิ่มเติม
        ///// </summary>
        //[TestMethod]
        //public async Task CheckWithRequestedFiles()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.CHECK;
        //    var statusOther = "";
        //    var requestedFiles = new FileMetadata[] { new FileMetadata { FileName = "test.pdf" } };

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.CHECK && appRequest.ActionReply == "ADJUST");
        //}

        ///// <summary>
        ///// ตรวจสอบเอกสาร ไม่ขอเอกสารเพิ่ม
        ///// </summary>
        //[TestMethod]
        //public async Task CheckWithOutRequestedFiles()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.CHECK;
        //    var statusOther = "";
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.CHECK && appRequest.ActionReply == "EMPTY");
        //}

        ///// <summary>
        ///// ตรวจสอบเอกสาร สถานะย่อยเป็นข้อความ
        ///// </summary>
        //[TestMethod]
        //public async Task CheckWithOtherStatus()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.CHECK;
        //    var statusOther = "กำลังทดสอบ";
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.CHECK && appRequest.ActionReply == "EMPTY");
        //}

        ///// <summary>
        ///// ตรวจสอบเอกสาร ไม่มีสถานะย่อย
        ///// </summary>
        //[TestMethod]
        //public async Task CheckWithOutOtherStatus()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.CHECK;
        //    var statusOther = "กำลังทดสอบ";
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.CHECK && appRequest.ActionReply == "EMPTY");
        //}

        #endregion

        //#region[Step 2 รอเจ้าหน้าที่ดำเนินการ]

        ///// <summary>
        ///// กำลังดำเนินการ รอเจ้าหน้าที่ดำเนินการ
        ///// </summary>
        //[TestMethod]
        //public async Task PendingWithAgentWorking()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.PENDING;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    // แปลงได้เป็น CHECK : APPROVE จากนั้นผ่านกระบวนการของ frontis ได้มาเป็น PENDING, WAITING_AGENT_WORKING, APPROVE
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PENDING && appRequest.ActionReply == "APPROVE");
        //}

        ///// <summary>
        ///// ดำเนินการ รอ ผปก.ดำเนินการ
        ///// </summary>
        //[TestMethod]
        //public async Task PendingWithUserWorking()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.PENDING;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PENDING && appRequest.ActionReply == "ADJUST");
        //}

        ///// <summary>
        ///// ดำเนินการ รอ ผปก.ยื่นเอกสารเพิ่ม
        ///// </summary>
        //[TestMethod]
        //public async Task PendingWithRequestedFile()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.PENDING;
        //    var statusOther = "";
        //    var requestedFiles = new FileMetadata[] { new FileMetadata { FileName = "test.pdf" } };

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PENDING && appRequest.ActionReply == "ADJUST");
        //}

        ///// <summary>
        ///// ดำเนินการ ไม่ขอเอกสารเพิ่ม
        ///// </summary>
        //[TestMethod]
        //public async Task PendingWithoutRequestedFile()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.PENDING;
        //    var statusOther = "";
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PENDING && appRequest.ActionReply == "EMPTY");
        //}

        ///// <summary>
        ///// ดำเนินการ สถานะย่อยเป็นข้อความ
        ///// </summary>
        //[TestMethod]
        //public async Task PendingWithOtherStatus()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.PENDING;
        //    var statusOther = "กำลังทดสอบ";
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PENDING && appRequest.ActionReply == "EMPTY");
        //}

        ///// <summary>
        ///// ดำเนินการ ไม่มีสถานะย่อย
        ///// </summary>
        //[TestMethod]
        //public async Task PendingWithOutOtherStatus()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.PENDING;
        //    var statusOther = "";
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PENDING && appRequest.ActionReply == "EMPTY");
        //}

        //#endregion

        //#region[Step 3 ทำเรื่องจ่ายเงิน]

        ///// <summary>
        ///// ทำเรื่องจ่ายเงิน รอเจ้าหน้าที่ดำเนินการ
        ///// </summary>
        //[TestMethod]
        //public async Task PayingWithAgentWorking()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
        //    var requestedFiles = null as FileMetadata[];
        //    var fee = 500;

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && appRequest.ActionReply == "APPROVE");
        //}

        ///// <summary>
        ///// ทำเรื่องจ่ายเงิน รอ ผปก.ดำเนินการ
        ///// </summary>
        //[TestMethod]
        //public async Task PayingWithUserWorking()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
        //    var requestedFiles = null as FileMetadata[];
        //    var fee = 500;

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && appRequest.ActionReply == "APPROVE");
        //}

        ///// <summary>
        ///// ทำเรื่องจ่ายเงิน สถานะย่อยเป็นข้อความ
        ///// </summary>
        //[TestMethod]
        //public async Task PayingWithOtherStatus()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
        //    var statusOther = "กำลังทดสอบ";
        //    var requestedFiles = null as FileMetadata[];
        //    var fee = 500;

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && appRequest.ActionReply == "APPROVE");
        //}

        ///// <summary>
        ///// ทำเรื่องจ่ายเงิน ไม่มีสถานะย่อย
        ///// </summary>
        //[TestMethod]
        //public async Task PayingWithOutOtherStatus()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
        //    var statusOther = "";
        //    var requestedFiles = null as FileMetadata[];
        //    var fee = 500;

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && appRequest.ActionReply == "APPROVE");
        //}

        ///// <summary>
        ///// ทำเรื่องจ่ายเงิน แต่ไม่มีค่าบริการ
        ///// </summary>
        //[TestMethod]
        //public async Task PayingNoFee()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
        //    var statusOther = "";
        //    var requestedFiles = null as FileMetadata[];
        //    var fee = 0;

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && appRequest.ActionReply == "APPROVE");
        //}

        ///// <summary>
        ///// ทำเรื่องจ่ายเงิน แต่ไม่มีค่าบริการ และมีใบอนุญาต
        ///// </summary>
        //[TestMethod]
        //public async Task PayingNoFeeWithLicense()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
        //    var statusOther = "";
        //    var requestedFiles = null as FileMetadata[];
        //    var fee = 0;

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && appRequest.ActionReply == "APPROVE");
        //}

        ///// <summary>
        ///// ทำเรื่องจ่ายเงิน มีค่าบริการ แต่ไม่มีใบอนุญาต
        ///// </summary>
        //[TestMethod]
        //public async Task PayingWithOutLicense()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType, false);
        //    var status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
        //    var statusOther = "";
        //    var requestedFiles = null as FileMetadata[];
        //    var fee = 500;

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && appRequest.ActionReply == "APPROVE");
        //}

        //#endregion

        //#region[Step 4 ทำเรื่องออกใบอนุญาต]

        ///// <summary>
        ///// ออกใบอนุญาต รอเจ้าหน้าที่ดำเนินการ
        ///// </summary>
        //[TestMethod]
        //public async Task CreateLicenseWithAgentWorking()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && appRequest.ActionReply == "PAID_FEE");
        //}


        ///// <summary>
        ///// ออกใบอนุญาต สถานะย่อยเป็นข้อความ
        ///// </summary>
        //[TestMethod]
        //public async Task CreateLicenseWithOtherStatus()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
        //    var statusOther = "กำลังทดสอบ";
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && appRequest.ActionReply == "PAID_FEE");
        //}

        ///// <summary>
        ///// ออกใบอนุญาต สถานะย่อยเป็นข้อความ
        ///// </summary>
        //[TestMethod]
        //public async Task CreateLicenseWithOutOtherStatus()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
        //    var statusOther = "";
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && appRequest.ActionReply == "PAID_FEE");
        //}

        //#endregion

        //#region[Step 5 รอผู้ประกอบการมารับใบอนุญาต]

        ///// <summary>
        ///// ออกใบอนุญาต ผปก.รับทาง email
        ///// </summary>
        //[TestMethod]
        //public async Task CreateLicenseWithUserWorkingByEmail()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, PermitDeliveryTypeValueConst.BY_MAIL);
        //    var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
        //    var requestedFiles = null as FileMetadata[];
        //    var fee = 500;
        //    var trackingNumber = Guid.NewGuid().ToString();

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee, trackingNumber);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.COMPLETED && appRequest.ActionReply == ApplicationActionReplyRequestEnum.SENT_APPLICATION_BY_EMAIL.ToString() && appRequest.EMSTrackingNumber == trackingNumber);
        //}


        ///// <summary>
        ///// ออกใบอนุญาต ผปก.รับทาง email แต่ไม่มี tracking number
        ///// </summary>
        //[TestMethod]
        //[ExpectedException(typeof(Exception), "EMSTrackingNumber not found")]
        //public async Task CreateLicenseWithUserWorkingByEmailNoTrackingNumber()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, PermitDeliveryTypeValueConst.BY_MAIL);
        //    var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
        //    var requestedFiles = null as FileMetadata[];
        //    var fee = 500;
        //    var trackingNumber = "";

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee, trackingNumber);
        //}

        ///// <summary>
        ///// ออกใบอนุญาต
        ///// </summary>
        //[TestMethod]
        //public async Task CompletedWithEMSTrackingNumber()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, null);
        //    var status = ApplicationStatusV2Enum.COMPLETED;
        //    var statusOther = "";
        //    var requestedFiles = null as FileMetadata[];
        //    var emsTrackingNumber = "กำลังทดสอบ";

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, 0, emsTrackingNumber);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.COMPLETED && appRequest.ActionReply == "EMPTY" && appRequest.EMSTrackingNumber == "กำลังทดสอบ");
        //}

        ///// <summary>
        ///// ออกใบอนุญาต ผปก.รับทาง ORG
        ///// </summary>
        //[TestMethod]
        //public async Task CreateLicenseWithUserWorkingByOrg()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, PermitDeliveryTypeValueConst.AT_OWNER_ORG);
        //    var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
        //    var requestedFiles = null as FileMetadata[];
        //    var fee = 500;
        //    var userCanGetAppDate = DateTime.Now.ToString("dd/MM/yyyy");

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee, "", userCanGetAppDate);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && appRequest.ActionReply == "TELL_USER_TO_GET_APPLICATION");
        //}

        ///// <summary>
        ///// ออกใบอนุญาต ผปก.รับทาง ORG แต่ไม่มีวันที่นัดรับ
        ///// </summary>
        //[TestMethod]
        //[ExpectedException(typeof(Exception), "UserCanGetAppDate not found")]
        //public async Task CreateLicenseWithUserWorkingByOrgNoGetDate()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, PermitDeliveryTypeValueConst.AT_OWNER_ORG);
        //    var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
        //    var requestedFiles = null as FileMetadata[];
        //    var fee = 500;
        //    var userCanGetAppDate = null as string;

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee, "", userCanGetAppDate);
        //}

        ///// <summary>
        ///// ออกใบอนุญาต ผปก.รับทาง OSS
        ///// </summary>
        //[TestMethod]
        //public async Task CreateLicenseWithUserWorkingByOSS()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, PermitDeliveryTypeValueConst.AT_OSS);
        //    var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
        //    var requestedFiles = null as FileMetadata[];
        //    var fee = 500;
        //    var userCanGetAppDate = DateTime.Now.ToString("dd/MM/yyyy");

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee, "", userCanGetAppDate);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && appRequest.ActionReply == "TELL_USER_TO_GET_APPLICATION");
        //}

        ///// <summary>
        ///// ออกใบอนุญาต ผปก.รับทาง ORG แต่ไม่มีวันที่นัดรับ
        ///// </summary>
        //[TestMethod]
        //[ExpectedException(typeof(Exception), "UserCanGetAppDate not found")]
        //public async Task CreateLicenseWithUserWorkingByOssNoGetDate()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, PermitDeliveryTypeValueConst.AT_OSS);
        //    var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
        //    var requestedFiles = null as FileMetadata[];
        //    var fee = 500;
        //    var userCanGetAppDate = null as string;

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee, "", userCanGetAppDate);
        //}

        //#endregion

        //#region[Step 6 ผปก. รับใบอนุญาตแล้วหรือไม่ต้องรอการรับ]

        ///// <summary>
        ///// ผปก.รับใบอนุญาตแล้ว หรือไม่ต้องรอการรับ หรือไม่มีใบอนุญาต
        ///// </summary>
        //[TestMethod]
        //public async Task CompletedWithAgentWorking()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, null);
        //    var status = ApplicationStatusV2Enum.COMPLETED;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.COMPLETED && appRequest.ActionReply == "EMPTY");
        //}

        ///// <summary>
        ///// ผปก.รับใบอนุญาตแล้ว หรือไม่ต้องรอการรับ หรือไม่มีใบอนุญาต
        ///// </summary>
        //[TestMethod]
        //public async Task CompletedWithUserWorking()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, null);
        //    var status = ApplicationStatusV2Enum.COMPLETED;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.COMPLETED && appRequest.ActionReply == "EMPTY");
        //}

        ///// <summary>
        ///// ผปก.รับใบอนุญาตแล้ว หรือไม่ต้องรอการรับ หรือไม่มีใบอนุญาต
        ///// </summary>
        //[TestMethod]
        //public async Task CompletedWithOtherStatus()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, null);
        //    var status = ApplicationStatusV2Enum.COMPLETED;
        //    var statusOther = "กำลังทดสอบ";
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.COMPLETED && appRequest.ActionReply == "EMPTY");
        //}

        ///// <summary>
        ///// ผปก.รับใบอนุญาตแล้ว หรือไม่ต้องรอการรับ หรือไม่มีใบอนุญาต
        ///// </summary>
        //[TestMethod]
        //public async Task CompletedWithOutOtherStatus()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, null);
        //    var status = ApplicationStatusV2Enum.COMPLETED;
        //    var statusOther = "";
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.COMPLETED && appRequest.ActionReply == "EMPTY");
        //}


        //#endregion

        //#region[Step 7 ปฏิเสธการออกใบอนุญาต]

        ///// <summary>
        ///// ปฏิเสธการออกใบอนุญาต
        ///// </summary>
        //[TestMethod]
        //public async Task RejectedWithAgentWorking()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.REJECTED;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.REJECTED && appRequest.ActionReply == "EMPTY");
        //}

        ///// <summary>
        ///// ปฏิเสธการออกใบอนุญาต
        ///// </summary>
        //[TestMethod]
        //public async Task RejectedWithUserWorking()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.REJECTED;
        //    var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.REJECTED && appRequest.ActionReply == "EMPTY");
        //}

        ///// <summary>
        ///// ปฏิเสธการออกใบอนุญาต
        ///// </summary>
        //[TestMethod]
        //public async Task RejectedWithOtherStatus()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.REJECTED;
        //    var statusOther = "กำลังทดสอบ";
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.REJECTED && appRequest.ActionReply == "EMPTY");
        //}

        ///// <summary>
        ///// ปฏิเสธการออกใบอนุญาต
        ///// </summary>
        //[TestMethod]
        //public async Task RejectedWithOutOtherStatus()
        //{
        //    // arrange 
        //    var userType = UserTypeEnum.Citizen;
        //    var applicationRequestId = await init(userType);
        //    var status = ApplicationStatusV2Enum.REJECTED;
        //    var statusOther = "";
        //    var requestedFiles = null as FileMetadata[];

        //    // act
        //    var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

        //    // assert
        //    Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.REJECTED && appRequest.ActionReply == "EMPTY");
        //}

        //#endregion
    }


    //[TestClass]
    //public class ApplicationRequestV2ApiActionReplyUnitTest
    //{
    //    #region[Initial]

    //    string citizenId = "1709900381574";
    //    string juristicId = "0503560002791";
    //    int applicationId = 59;
    //    string applicationRequestModel = "{\"ApplicationID\":59,\"Status\":3,\"StatusOther\":\"WAITING_AGENT_READ_REQUEST\",\"StatusOtherText\":null,\"StatusBeforeCancel\":null,\"ActionReply\":0,\"PermitDeliveryAddress\":null,\"PermitDeliveryType\":null,\"PermitDeliverOnPayment_OK\":false,\"PaymentMethod\":null,\"PaymentMethodEnabledChoice\":null,\"BillPaymentTypeForPaymentMethod\":null,\"PermitDeliveryTypeEnabledChoice\":null,\"EMSTrackingNumber\":null,\"DisableUpdateStatus\":null,\"ApplicationRequestNumber\":null,\"ApplicationRequestRunningNumber\":0,\"IsRequestNumberAgent\":false,\"ApplicationRequestNumberAgent\":null,\"IdentityID\":\"5633071108732\",\"IdentityName\":\"นาย test14 test\",\"IdentityType\":0,\"ApplicationRequestID\":null,\"RequestBatchID\":\"dcc9787a-4b0f-46ab-94fe-0eefb972d382\",\"Fee\":null,\"EMSFee\":50.0,\"DueDateForPayFee\":null,\"Duration\":null,\"PermitCanBeDeliveredOnPayment\":false,\"UserCanGetAppDate\":null,\"ExpectedFinishDate\":null,\"ProvinceID\":null,\"AmphurID\":null,\"TumbolID\":null,\"Province\":null,\"Amphur\":null,\"Tumbol\":null,\"SourceIPAddress\":\"::1\",\"ReplyFromOrg\":false,\"ReplyFromApiUpdate\":false,\"TransactionCode\":null,\"TransactionDate\":null,\"Data\":{\"SELL_FOOD_INFORMATION\":{\"GroupName\":\"SELL_FOOD_INFORMATION\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"SELL_FOOD_INFORMATION__APP_TYPE_OPTION\":\"หนังสือรับรองการแจ้ง\",\"SELL_FOOD_INFORMATION__LICENSE_TYPE_OPTION\":\"หนังสือรับรองการแจ้ง\",\"SELL_FOOD_INFORMATION__BUSINESS_TYPE_OPTION\":\"APP_SELL_FOOD_LOCATION_TYPE_OPTION__SELL\",\"SELL_FOOD_INFORMATION__PURPOSE_OPTION\":\"APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__SELL\",\"SELL_FOOD_INFORMATION__FOOD_TYPE\":\"test\"}},\"SELL_FOOD_FOOD_WORKER_INFO\":{\"GroupName\":\"SELL_FOOD_FOOD_WORKER_INFO\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"SELL_FOOD_FOOD_WORKER_INFO_TOTAL\":\"0\"}},\"SELL_FOOD_FOOD_MANAGER_INFO\":{\"GroupName\":\"SELL_FOOD_FOOD_MANAGER_INFO\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__TITLE\":\"01\",\"SELL_FOOD_FOOD_MANAGER_INFO__NAME\":\"test\",\"SELL_FOOD_FOOD_MANAGER_INFO__LASTNAME\":\"test\",\"SELL_FOOD_FOOD_MANAGER_INFO__AGE\":\"31\",\"DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__NATIONALITY\":\"70\",\"ADDRESS_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"123\",\"ADDRESS_MOO_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"ADDRESS_SOI_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"ADDRESS_ROAD_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"ADDRESS_PROVINCE_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"10\",\"ADDRESS_AMPHUR_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"05\",\"ADDRESS_TUMBOL_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"08\",\"ADDRESS_TELEPHONE_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"0123456789\",\"ADDRESS_TELEPHONE_EXT_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"ADDRESS_FAX_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"SELL_FOOD_CONFIRM_SELL_FOOD_CONFIRM__TRUE\":\"true\",\"DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__TITLE_TEXT\":\"นาย\",\"DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__NATIONALITY_TEXT\":\"ไทย\",\"ADDRESS_PROVINCE_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS_TEXT\":\"กรุงเทพมหานคร\",\"ADDRESS_AMPHUR_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS_TEXT\":\"เขตบางเขน\",\"ADDRESS_TUMBOL_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS_TEXT\":\"ท่าแร้ง\"}},\"CITIZEN_GENERAL_INFORMATION_HEADER\":{\"GroupName\":\"CITIZEN_GENERAL_INFORMATION_HEADER\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{}},\"GENERAL_INFORMATION\":{\"GroupName\":\"GENERAL_INFORMATION\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"INFORMATION_HEADER__REQUEST_DATE\":\"1/3/2562 16:21:43\",\"INFORMATION_HEADER__REQUEST_AT\":\"Biz Portal\",\"INFORMATION__REQUEST_AS_OPTION\":\"บุคคลธรรมดา\",\"DROPDOWN_CITIZEN_TITLE\":\"01\",\"CITIZEN_NAME\":\"test14\",\"CITIZEN_LASTNAME\":\"test\",\"GENERAL_INFORMATION__CITIZEN_AGE\":\"31\",\"DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY\":\"70\",\"IDENTITY_ID\":\"5633071108732\",\"DROPDOWN_CITIZEN_TITLE_TEXT\":\"นาย\",\"DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT\":\"ไทย\"}},\"CITIZEN_ADDRESS_INFORMATION\":{\"GroupName\":\"CITIZEN_ADDRESS_INFORMATION\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"ADDRESS_CITIZEN_ADDRESS\":\"123\",\"ADDRESS_MOO_CITIZEN_ADDRESS\":\"\",\"ADDRESS_SOI_CITIZEN_ADDRESS\":\"\",\"ADDRESS_BUILDING_CITIZEN_ADDRESS\":\"\",\"ADDRESS_ROOMNO_CITIZEN_ADDRESS\":\"\",\"ADDRESS_FLOOR_CITIZEN_ADDRESS\":\"\",\"ADDRESS_ROAD_CITIZEN_ADDRESS\":\"\",\"ADDRESS_PROVINCE_CITIZEN_ADDRESS\":\"10\",\"ADDRESS_AMPHUR_CITIZEN_ADDRESS\":\"15\",\"ADDRESS_TUMBOL_CITIZEN_ADDRESS\":\"04\",\"ADDRESS_POSTCODE_CITIZEN_ADDRESS\":\"10600\",\"ADDRESS_TELEPHONE_CITIZEN_ADDRESS\":\"0123456789\",\"ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS\":\"\",\"ADDRESS_FAX_CITIZEN_ADDRESS\":\"\",\"ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT\":\"กรุงเทพมหานคร\",\"ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT\":\"เขตธนบุรี\",\"ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT\":\"บุคคโล\"}},\"REQUESTOR_INFORMATION__HEADER\":{\"GroupName\":\"REQUESTOR_INFORMATION__HEADER\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION\":\"REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER\"}},\"INFORMATION_STORE\":{\"GroupName\":\"INFORMATION_STORE\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"INFORMATION_STORE__USE_CITIZEN_ADDRESS_INFORMATION_STORE__USE_CITIZEN_ADDRESS__TRUE\":\"true\",\"INFORMATION_STORE_NAME_TH\":\"test\",\"INFORMATION_STORE_NAME_EN\":\"\",\"ADDRESS_INFORMATION_STORE__ADDRESS\":\"123\",\"ADDRESS_MOO_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_SOI_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_ROAD_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS\":\"10\",\"ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS\":\"15\",\"ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS\":\"04\",\"ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS\":\"10600\",\"ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS\":\"0123456789\",\"ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_FAX_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_LAT_INFORMATION_STORE__ADDRESS\":\"13.7553323\",\"ADDRESS_LNG_INFORMATION_STORE__ADDRESS\":\"100.5226751\",\"SEARCH_TEXT_GOOGLE_MAP\":\"บุคคโล เขตธนบุรี กรุงเทพมหานคร\",\"INFORMATION_STORE_AREA\":\"50\",\"CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION\":\"INFORMATION_STORE_BUILDING_TYPE_OPTION__OWNED\",\"INFORMATION_STORE_HEALTH_OTHER\":\"\",\"ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT\":\"กรุงเทพมหานคร\",\"ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT\":\"เขตธนบุรี\",\"ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT\":\"บุคคโล\"}},\"OPENID\":{\"GroupName\":\"OPENID\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"FULLNAME\":\"นาย test14 test\",\"PREFIX_TH\":\"นาย\",\"FIRSTNAME_TH\":\"test14\",\"LASTNAME_TH\":\"test\",\"GENDER\":\"ชาย\"}}},\"Remark\":null,\"OrgNameTH\":null,\"OrgAddress\":null,\"DisabledSendingSystemEmail\":false,\"EmailMessage\":null,\"SmsMessage\":null,\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"UploadedFiles\":[{\"FileGroupID\":\"a51f62b2-1cc6-48a0-9ac2-da3c923f8b4a\",\"Description\":\"CITIZEN_INFORMATION\",\"Files\":[{\"FileID\":\"5c78fd6a70b8b312889bf296\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"ID_CARD_COPY\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"ID_CARD_COPY\",\"DISPLAYNAME\":\"เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: บุคคลผู้ขออนุญาต *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7070b8b312889bf299\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"CITIZEN_RENAME_MARRIAGE_DOC\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"CITIZEN_RENAME_MARRIAGE_DOC\",\"DISPLAYNAME\":\"ใบสำคัญการเปลี่ยนชื่อ/ทะเบียนสมรส\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7370b8b312889bf29c\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"CITIZEN_BKK_FOOD_HEALTH_CERT\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"CITIZEN_BKK_FOOD_HEALTH_CERT\",\"DISPLAYNAME\":\"หนังสือรับรองผ่านการอบรมหลักสูตรสุขาภิบาลอาหารของกรุงเทพมหานคร: บุคคลผู้ขออนุญาต\",\"PREDOC\":\"false\"},\"TYPE\":null}],\"Extras\":null},{\"FileGroupID\":\"791addfd-7f9f-472c-bec6-202e236b67aa\",\"Description\":\"INFORMATION_STORE_FILE_SECTION\",\"Files\":[{\"FileID\":\"5c78fd7670b8b312889bf29f\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT\",\"DISPLAYNAME\":\"หนังสือแจ้งการใช้หรือเปลี่ยนแปลงการใช้ประโยชน์ที่ดินตามกฎหมายว่าด้วยการผังเมือง *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7870b8b312889bf2a2\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_BUILDING_OWNER_DOC\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_BUILDING_OWNER_DOC\",\"DISPLAYNAME\":\"เอกสารแสดงความเป็นเจ้าของอาคารสถานที่ที่ใช้เป็นร้าน/สถานประกอบการ เช่น ใบอนุญาตก่อสร้าง หรือสัญญาซื้อขาย *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7b70b8b312889bf2a5\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_MAP\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_MAP\",\"DISPLAYNAME\":\"แผนที่สังเขป แสดงสถานที่ตั้งของร้าน/สถานประกอบการ *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7e70b8b312889bf2a8\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD\",\"DISPLAYNAME\":\"ทะเบียนบ้าน:อาคารที่ตั้งร้าน / สถานประกอบการ *\",\"PREDOC\":\"false\"},\"TYPE\":null}],\"Extras\":null},{\"FileGroupID\":\"a910792d-7a4e-41a1-af51-49668e13a859\",\"Description\":\"BUILDING_FILE_SECTION\",\"Files\":[{\"FileID\":\"5c78fd8170b8b312889bf2ab\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_BUILDING_CONTROL_DOC\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_BUILDING_CONTROL_DOC\",\"DISPLAYNAME\":\"หลักฐานเกี่ยวกับการใช้อาคารตามกฏหมายว่าด้วยการควบคุมอาคาร เช่น ใบอนุญาตก่อสร้าง (อ.1) หรือใบรับรองการเปิดใช้อาคาร *\",\"PREDOC\":\"false\"},\"TYPE\":null}],\"Extras\":null},{\"FileGroupID\":\"77c76377-97f9-4d48-9de5-e67e8209652d\",\"Description\":\"SELL_FOOD_FILE_SECTION\",\"Files\":[{\"FileID\":\"5c78fd8370b8b312889bf2ae\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"SELL_FOOD_POLUTION_CONTROL_CHART\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"SELL_FOOD_POLUTION_CONTROL_CHART\",\"DISPLAYNAME\":\"แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงการป้องกันมลพิษ *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd8770b8b312889bf2b1\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"SELL_FOOD_HEALTH_CONTROL_CHART\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"SELL_FOOD_HEALTH_CONTROL_CHART\",\"DISPLAYNAME\":\"แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงสุขลักษณะภายในสถานประกอบการ *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd8a70b8b312889bf2b4\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"SELL_FOOD_SEFTY_CONTROL_CHART\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"SELL_FOOD_SEFTY_CONTROL_CHART\",\"DISPLAYNAME\":\"แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงระบบความปลอดภัยในการทำงาน *\",\"PREDOC\":\"false\"},\"TYPE\":null}],\"Extras\":null}],\"GovFiles\":null,\"RequestedFiles\":null,\"EPermitFiles\":null,\"EPermitFilesForDisplay\":null,\"BillPaymentFiles\":null,\"AttachBillPayment\":null,\"Attachments\":null,\"CreatedDate\":\"0001-01-01T00:00:00\",\"UpdatedDate\":\"0001-01-01T00:00:00\",\"UpdatedDateByRequestor\":\"2019-03-03T12:46:49.0155433+07:00\",\"UpdatedDateByAgent\":\"0001-01-01T00:00:00\",\"UpdatedByAgent\":null,\"LastRequestorUpdateEmail\":null,\"isPassStepWaiting\":null,\"Chats\":null,\"CgdPayments\":null}";

    //    ApplicationsController ctrl = new ApplicationsController();
    //    ApplicationDbContext db = new ApplicationDbContext();

    //    private async Task<Guid> init(UserTypeEnum userType, bool isLicensing = true, ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK, string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, string deliveryType = PermitDeliveryTypeValueConst.BY_MAIL)
    //    {
    //        // init user
    //        var user = userType == UserTypeEnum.Juristic ? db.Users.Where(e => e.JuristicID != null && e.Email != null).FirstOrDefault() : db.Users.Where(e => e.CitizenID == citizenId).FirstOrDefault();

    //        // init identity
    //        var identityId = userType == UserTypeEnum.Juristic ? user.JuristicID : user.CitizenID;
    //        var identity = new GenericIdentity(user.UserName);
    //        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
    //        identity.AddClaim(new Claim(ClaimTypes.Name, user.FullName ?? ""));
    //        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email ?? ""));
    //        identity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.UserType, userType == UserTypeEnum.Juristic ? UserTypeEnum.Juristic.GetEnumStringValue() : UserTypeEnum.Citizen.GetEnumStringValue()));
    //        identity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.JuristicID, user.JuristicID ?? ""));
    //        identity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.CitizenID, user.CitizenID ?? ""));
    //        identity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.PassportID, user.PassportID ?? ""));
    //        identity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.FullName, user.FullName ?? ""));
    //        Thread.CurrentPrincipal = new GenericPrincipal(identity, null);

    //        // init application request
    //        var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
    //        var appRequest = repo.Where(o => o.IdentityID == identityId && o.ApplicationID == applicationId).FirstOrDefault();

    //        if (appRequest == null)
    //        {
    //            var appRequestModel = JsonConvert.DeserializeObject<ApplicationRequestViewModel>(applicationRequestModel);
    //            ctrl.Request = new HttpRequestMessage();
    //            var response = await ctrl.Requests(appRequestModel);
    //            appRequest = response.Data.Adapt<ApplicationRequestEntity>();
    //            appRequest.PermitDeliveryType = deliveryType;
    //            appRequest.Update();
    //        }
    //        else
    //        {
    //            appRequest.Status = status;
    //            appRequest.StatusOther = statusOther;
    //            appRequest.NoDocument = !isLicensing;
    //            appRequest.PermitDeliveryType = deliveryType;
    //            appRequest.Update();
    //        }

    //        return appRequest.ApplicationRequestID;
    //    }

    //    private async Task<ApplicationRequestEntity> createRequest(UserTypeEnum userType, int applicationId, Guid applicationRequestId, ApplicationStatusV2Enum status, string statusOther = "", FileMetadata[] requestedFiles = null, int fee = 0, string emsTrackingNumber = null, string userCanGetAppDate = null)
    //    {
    //        var model = new ApplicationRequestViewModel
    //        {
    //            ApplicationRequestID = applicationRequestId,
    //            ApplicationID = applicationId,
    //            Status = status,
    //            StatusOther = statusOther,
    //            RequestedFiles = requestedFiles,
    //            Fee = fee,
    //            DueDateForPayFee = fee > 0 ? DateTime.Now.AddDays(30).ToString("dd/MM/yyyy", new CultureInfo("en")) : null
    //        };

    //        if (userType == UserTypeEnum.Citizen)
    //        {
    //            model.IdentityType = UserTypeEnum.Citizen;
    //            model.IdentityID = citizenId;
    //        }

    //        if (userType == UserTypeEnum.Juristic)
    //        {
    //            model.IdentityType = UserTypeEnum.Juristic;
    //            model.IdentityID = juristicId;
    //        }

    //        if (model.Fee != null && model.Fee > 0)
    //        {
    //            model.PaymentMethodEnabledChoice = new string[] { "BILL_PAYMENT" };
    //            model.BillPaymentTypeForPaymentMethod = "OWNER_ORG";
    //            model.PermitDeliveryTypeEnabledChoice = new string[] { "BY_MAIL" };
    //            model.AttachBillPayment = new Base64Attachment[] {
    //                new Base64Attachment
    //                {
    //                    Base64String = "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ICAgICAgICA8eG1wOk1ldGFkYXRhRGF0ZT4yMDE4LTEyLTI0VDE1OjQyOjE1KzA3OjAwPC94bXA6TWV0YWRhdGFEYXRlPgogICAgICA8L3JkZjpEZXNjcmlwdGlvbj4KICAgICAgPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIKICAgICAgICAgICAgeG1sbnM6cGRmPSJodHRwOi8vbnMuYWRvYmUuY29tL3BkZi8xLjMvIj4KICAgICAgICAgPHBkZjpQcm9kdWNlcj5BY3JvYmF0IERpc3RpbGxlciAxMC4xLjEgKFdpbmRvd3MpPC9wZGY6UHJvZHVjZXI+CiAgICAgIDwvcmRmOkRlc2NyaXB0aW9uPgogICAgICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgICAgICAgICB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iPgogICAgICAgICA8ZGM6Zm9ybWF0PmFwcGxpY2F0aW9uL3BkZjwvZGM6Zm9ybWF0PgogICAgICAgICA8ZGM6Y3JlYXRvcj4KICAgICAgICAgICAgPHJkZjpTZXE+CiAgICAgICAgICAgICAgIDxyZGY6bGkvPgogICAgICAgICAgICA8L3JkZjpTZXE+CiAgICAgICAgIDwvZGM6Y3JlYXRvcj4KICAgICAgICAgPGRjOnRpdGxlPgogICAgICAgICAgICA8cmRmOkFsdD4KICAgICAgICAgICAgICAgPHJkZjpsaSB4bWw6bGFuZz0ieC1kZWZhdWx0Ii8+CiAgICAgICAgICAgIDwvcmRmOkFsdD4KICAgICAgICAgPC9kYzp0aXRsZT4KICAgICAgPC9yZGY6RGVzY3JpcHRpb24+CiAgICAgIDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiCiAgICAgICAgICAgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIj4KICAgICAgICAgPHhtcE1NOkRvY3VtZW50SUQ+dXVpZDplODU0YjJjZi1hMzQ3LTQ4ZmUtYWRiNy1lMmYxMDgyYTBiMDE8L3htcE1NOkRvY3VtZW50SUQ+CiAgICAgICAgIDx4bXBNTTpJbnN0YW5jZUlEPnV1aWQ6MTM0YWVmZTAtOWE4MC00NzY1LTg4NjItMzA3MDllOGQ4MTI5PC94bXBNTTpJbnN0YW5jZUlEPgogICAgICA8L3JkZjpEZXNjcmlwdGlvbj4KICAgPC9yZGY6UkRGPgo8L3g6eG1wbWV0YT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgIAo8P3hwYWNrZXQgZW5kPSJ3Ij8+DQplbmRzdHJlYW0NZW5kb2JqDTMgMCBvYmoNPDwvRmlsdGVyL0ZsYXRlRGVjb2RlL0ZpcnN0IDQvTGVuZ3RoIDQ5L04gMS9UeXBlL09ialN0bT4+c3RyZWFtDQpo3rJQMFCwsdF3zi/NK1Ew1PfOTCmONjQCCgbF6odUFqTqBySmpxbb2QEEGADf+gutDQplbmRzdHJlYW0NZW5kb2JqDTQgMCBvYmoNPDwvRmlsdGVyL0ZsYXRlRGVjb2RlL0ZpcnN0IDQvTGVuZ3RoIDE1MC9OIDEvVHlwZS9PYmpTdG0+PnN0cmVhbQ0KaN5sjLEKwjAUAH/lbU0QkrzQoJZSKHYVCoouXWoTMBAaeX3F37eDOLndcHdHMFDXul35mUlIfaIwcsxzN3IQXWUNHtDaEl254c7sC2OKr7X5/WWi+GKnfEpwC7RsJThllZX6nP2fCbrfpKfs1ymQaCfKj5GhiwvHlAIBGoUKYRD3OPv8XgYp9TVyCkI2zUeAAQDmXjPVDQplbmRzdHJlYW0NZW5kb2JqDTUgMCBvYmoNPDwvRGVjb2RlUGFybXM8PC9Db2x1bW5zIDQvUHJlZGljdG9yIDEyPj4vRmlsdGVyL0ZsYXRlRGVjb2RlL0lEWzwyMDhCMzIwNjNBREJDMjg4QzQ1RTcxNzEwQjA5Mzk4RD48MUE5MkE1MDQ5ODU1M0Q0QTlFMjRGMjZCMEUwQjg2MDk+XS9JbmZvIDkgMCBSL0xlbmd0aCA0OC9Sb290IDExIDAgUi9TaXplIDEwL1R5cGUvWFJlZi9XWzEgMiAxXT4+c3RyZWFtDQpo3mJiAAImxrSlDEwMjG1Agi8fxOoFEZ+BEp3PgSwGBkYgwfQfSDAyAAQYAJT/BhsNCmVuZHN0cmVhbQ1lbmRvYmoNc3RhcnR4cmVmDQoxMTYNCiUlRU9GDQo=",
    //                    ContentType= "application/pdf",
    //                    FileName = "test.pdf"
    //                }
    //            };
    //        }

    //        if (!string.IsNullOrEmpty(emsTrackingNumber))
    //        {
    //            model.EMSTrackingNumber = emsTrackingNumber;
    //        }

    //        if (!string.IsNullOrEmpty(userCanGetAppDate))
    //        {
    //            model.UserCanGetAppDate = userCanGetAppDate;
    //        }

    //        var request = new HttpRequestMessage();
    //        request.Headers.Add("Consumer-Key", "59a92baa-4418-4b69-8ea9-67eecc042aa2");
    //        ctrl.Request = request;

    //        HttpContext.Current = GetMockedHttpContext();

    //        var response = await ctrl.Requests(model);

    //        return response.Data.Adapt<ApplicationRequestEntity>();
    //    }

    //    private async Task clean()
    //    {
    //        var userManager = new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>(db));
    //        var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(db));
    //    }

    //    private HttpContext GetMockedHttpContext()
    //    {
    //        var httpRequest = new HttpRequest("", "http://localhost:45598/", "");
    //        var stringWriter = new StringWriter();
    //        var httpResponse = new HttpResponse(stringWriter);
    //        var httpContext = new HttpContext(httpRequest, httpResponse);

    //        var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
    //                                                new HttpStaticObjectsCollection(), 10, true,
    //                                                HttpCookieMode.AutoDetect,
    //                                                SessionStateMode.InProc, false);

    //        httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
    //                                                BindingFlags.NonPublic | BindingFlags.Instance,
    //                                                null, CallingConventions.Standard,
    //                                                new[] { typeof(HttpSessionStateContainer) },
    //                                                null)
    //                                          .Invoke(new object[] { sessionContainer });

    //        return httpContext;
    //    }

    //    #endregion

    //    #region[Step 1 ตรวจสอบเอกสาร]

    //    /// <summary>
    //    /// ตรวจสอบเอกสาร กำลังรอเจ้าหน้าที่ทำงาน
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CheckWithAgentWorking()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.CHECK;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.CHECK && appRequest.ActionReply == "EMPTY");
    //    }

    //    /// <summary>
    //    /// ตรวจสอบเอกสาร มีการแก้ไขรอ ผปก. ทำการแก้ไข
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CheckWithUserWorking()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.CHECK;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.CHECK && appRequest.ActionReply == "ADJUST");
    //    }

    //    /// <summary>
    //    /// ตรวจสอบเอกสาร ต้องการเอกสารเพิ่มรอ ผปก. ยื่นเอกสารเพิ่มเติม
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CheckWithRequestedFiles()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.CHECK;
    //        var statusOther = "";
    //        var requestedFiles = new FileMetadata[] { new FileMetadata { FileName = "test.pdf" } };

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.CHECK && appRequest.ActionReply == "ADJUST");
    //    }

    //    /// <summary>
    //    /// ตรวจสอบเอกสาร ไม่ขอเอกสารเพิ่ม
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CheckWithOutRequestedFiles()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.CHECK;
    //        var statusOther = "";
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.CHECK && appRequest.ActionReply == "EMPTY");
    //    }

    //    /// <summary>
    //    /// ตรวจสอบเอกสาร สถานะย่อยเป็นข้อความ
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CheckWithOtherStatus()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.CHECK;
    //        var statusOther = "กำลังทดสอบ";
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.CHECK && appRequest.ActionReply == "EMPTY");
    //    }

    //    /// <summary>
    //    /// ตรวจสอบเอกสาร ไม่มีสถานะย่อย
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CheckWithOutOtherStatus()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.CHECK;
    //        var statusOther = "กำลังทดสอบ";
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.CHECK && appRequest.ActionReply == "EMPTY");
    //    }

    //    #endregion

    //    #region[Step 2 รอเจ้าหน้าที่ดำเนินการ]

    //    /// <summary>
    //    /// กำลังดำเนินการ รอเจ้าหน้าที่ดำเนินการ
    //    /// </summary>
    //    [TestMethod]
    //    public async Task PendingWithAgentWorking()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.PENDING;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        // แปลงได้เป็น CHECK : APPROVE จากนั้นผ่านกระบวนการของ frontis ได้มาเป็น PENDING, WAITING_AGENT_WORKING, APPROVE
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PENDING && appRequest.ActionReply == "APPROVE");
    //    }

    //    /// <summary>
    //    /// ดำเนินการ รอ ผปก.ดำเนินการ
    //    /// </summary>
    //    [TestMethod]
    //    public async Task PendingWithUserWorking()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.PENDING;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PENDING && appRequest.ActionReply == "ADJUST");
    //    }

    //    /// <summary>
    //    /// ดำเนินการ รอ ผปก.ยื่นเอกสารเพิ่ม
    //    /// </summary>
    //    [TestMethod]
    //    public async Task PendingWithRequestedFile()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.PENDING;
    //        var statusOther = "";
    //        var requestedFiles = new FileMetadata[] { new FileMetadata { FileName = "test.pdf" } };

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PENDING && appRequest.ActionReply == "ADJUST");
    //    }

    //    /// <summary>
    //    /// ดำเนินการ ไม่ขอเอกสารเพิ่ม
    //    /// </summary>
    //    [TestMethod]
    //    public async Task PendingWithoutRequestedFile()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.PENDING;
    //        var statusOther = "";
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PENDING && appRequest.ActionReply == "EMPTY");
    //    }

    //    /// <summary>
    //    /// ดำเนินการ สถานะย่อยเป็นข้อความ
    //    /// </summary>
    //    [TestMethod]
    //    public async Task PendingWithOtherStatus()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.PENDING;
    //        var statusOther = "กำลังทดสอบ";
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PENDING && appRequest.ActionReply == "EMPTY");
    //    }

    //    /// <summary>
    //    /// ดำเนินการ ไม่มีสถานะย่อย
    //    /// </summary>
    //    [TestMethod]
    //    public async Task PendingWithOutOtherStatus()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.PENDING;
    //        var statusOther = "";
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PENDING && appRequest.ActionReply == "EMPTY");
    //    }

    //    #endregion

    //    #region[Step 3 ทำเรื่องจ่ายเงิน]

    //    /// <summary>
    //    /// ทำเรื่องจ่ายเงิน รอเจ้าหน้าที่ดำเนินการ
    //    /// </summary>
    //    [TestMethod]
    //    public async Task PayingWithAgentWorking()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        var requestedFiles = null as FileMetadata[];
    //        var fee = 500;

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && appRequest.ActionReply == "APPROVE");
    //    }

    //    /// <summary>
    //    /// ทำเรื่องจ่ายเงิน รอ ผปก.ดำเนินการ
    //    /// </summary>
    //    [TestMethod]
    //    public async Task PayingWithUserWorking()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        var requestedFiles = null as FileMetadata[];
    //        var fee = 500;

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && appRequest.ActionReply == "APPROVE");
    //    }

    //    /// <summary>
    //    /// ทำเรื่องจ่ายเงิน สถานะย่อยเป็นข้อความ
    //    /// </summary>
    //    [TestMethod]
    //    public async Task PayingWithOtherStatus()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        var statusOther = "กำลังทดสอบ";
    //        var requestedFiles = null as FileMetadata[];
    //        var fee = 500;

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && appRequest.ActionReply == "APPROVE");
    //    }

    //    /// <summary>
    //    /// ทำเรื่องจ่ายเงิน ไม่มีสถานะย่อย
    //    /// </summary>
    //    [TestMethod]
    //    public async Task PayingWithOutOtherStatus()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        var statusOther = "";
    //        var requestedFiles = null as FileMetadata[];
    //        var fee = 500;

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && appRequest.ActionReply == "APPROVE");
    //    }

    //    /// <summary>
    //    /// ทำเรื่องจ่ายเงิน แต่ไม่มีค่าบริการ
    //    /// </summary>
    //    [TestMethod]
    //    public async Task PayingNoFee()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        var statusOther = "";
    //        var requestedFiles = null as FileMetadata[];
    //        var fee = 0;

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && appRequest.ActionReply == "APPROVE");
    //    }

    //    /// <summary>
    //    /// ทำเรื่องจ่ายเงิน แต่ไม่มีค่าบริการ และมีใบอนุญาต
    //    /// </summary>
    //    [TestMethod]
    //    public async Task PayingNoFeeWithLicense()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        var statusOther = "";
    //        var requestedFiles = null as FileMetadata[];
    //        var fee = 0;

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && appRequest.ActionReply == "APPROVE");
    //    }

    //    /// <summary>
    //    /// ทำเรื่องจ่ายเงิน มีค่าบริการ แต่ไม่มีใบอนุญาต
    //    /// </summary>
    //    [TestMethod]
    //    public async Task PayingWithOutLicense()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType, false);
    //        var status = ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE;
    //        var statusOther = "";
    //        var requestedFiles = null as FileMetadata[];
    //        var fee = 500;

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && appRequest.ActionReply == "APPROVE");
    //    }

    //    #endregion

    //    #region[Step 4 ทำเรื่องออกใบอนุญาต]

    //    /// <summary>
    //    /// ออกใบอนุญาต รอเจ้าหน้าที่ดำเนินการ
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CreateLicenseWithAgentWorking()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && appRequest.ActionReply == "PAID_FEE");
    //    }


    //    /// <summary>
    //    /// ออกใบอนุญาต สถานะย่อยเป็นข้อความ
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CreateLicenseWithOtherStatus()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        var statusOther = "กำลังทดสอบ";
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && appRequest.ActionReply == "PAID_FEE");
    //    }

    //    /// <summary>
    //    /// ออกใบอนุญาต สถานะย่อยเป็นข้อความ
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CreateLicenseWithOutOtherStatus()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        var statusOther = "";
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && appRequest.ActionReply == "PAID_FEE");
    //    }

    //    #endregion

    //    #region[Step 5 รอผู้ประกอบการมารับใบอนุญาต]

    //    /// <summary>
    //    /// ออกใบอนุญาต ผปก.รับทาง email
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CreateLicenseWithUserWorkingByEmail()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, PermitDeliveryTypeValueConst.BY_MAIL);
    //        var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        var requestedFiles = null as FileMetadata[];
    //        var fee = 500;
    //        var trackingNumber = Guid.NewGuid().ToString();

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee, trackingNumber);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.COMPLETED && appRequest.ActionReply == ApplicationActionReplyRequestEnum.SENT_APPLICATION_BY_EMAIL.ToString() && appRequest.EMSTrackingNumber == trackingNumber);
    //    }


    //    /// <summary>
    //    /// ออกใบอนุญาต ผปก.รับทาง email แต่ไม่มี tracking number
    //    /// </summary>
    //    [TestMethod]
    //    [ExpectedException(typeof(Exception), "EMSTrackingNumber not found")]
    //    public async Task CreateLicenseWithUserWorkingByEmailNoTrackingNumber()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, PermitDeliveryTypeValueConst.BY_MAIL);
    //        var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        var requestedFiles = null as FileMetadata[];
    //        var fee = 500;
    //        var trackingNumber = "";

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee, trackingNumber);
    //    }

    //    /// <summary>
    //    /// ออกใบอนุญาต
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CompletedWithEMSTrackingNumber()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, null);
    //        var status = ApplicationStatusV2Enum.COMPLETED;
    //        var statusOther = "";
    //        var requestedFiles = null as FileMetadata[];
    //        var emsTrackingNumber = "กำลังทดสอบ";

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, 0, emsTrackingNumber);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.COMPLETED && appRequest.ActionReply == "EMPTY" && appRequest.EMSTrackingNumber == "กำลังทดสอบ");
    //    }

    //    /// <summary>
    //    /// ออกใบอนุญาต ผปก.รับทาง ORG
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CreateLicenseWithUserWorkingByOrg()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, PermitDeliveryTypeValueConst.AT_OWNER_ORG);
    //        var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        var requestedFiles = null as FileMetadata[];
    //        var fee = 500;
    //        var userCanGetAppDate = DateTime.Now.ToString("dd/MM/yyyy");

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee, "", userCanGetAppDate);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && appRequest.ActionReply == "TELL_USER_TO_GET_APPLICATION");
    //    }

    //    /// <summary>
    //    /// ออกใบอนุญาต ผปก.รับทาง ORG แต่ไม่มีวันที่นัดรับ
    //    /// </summary>
    //    [TestMethod]
    //    [ExpectedException(typeof(Exception), "UserCanGetAppDate not found")]
    //    public async Task CreateLicenseWithUserWorkingByOrgNoGetDate()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, PermitDeliveryTypeValueConst.AT_OWNER_ORG);
    //        var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        var requestedFiles = null as FileMetadata[];
    //        var fee = 500;
    //        var userCanGetAppDate = null as string;

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee, "", userCanGetAppDate);
    //    }

    //    /// <summary>
    //    /// ออกใบอนุญาต ผปก.รับทาง OSS
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CreateLicenseWithUserWorkingByOSS()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, PermitDeliveryTypeValueConst.AT_OSS);
    //        var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        var requestedFiles = null as FileMetadata[];
    //        var fee = 500;
    //        var userCanGetAppDate = DateTime.Now.ToString("dd/MM/yyyy");

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee, "", userCanGetAppDate);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE && appRequest.ActionReply == "TELL_USER_TO_GET_APPLICATION");
    //    }

    //    /// <summary>
    //    /// ออกใบอนุญาต ผปก.รับทาง ORG แต่ไม่มีวันที่นัดรับ
    //    /// </summary>
    //    [TestMethod]
    //    [ExpectedException(typeof(Exception), "UserCanGetAppDate not found")]
    //    public async Task CreateLicenseWithUserWorkingByOssNoGetDate()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, PermitDeliveryTypeValueConst.AT_OSS);
    //        var status = ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        var requestedFiles = null as FileMetadata[];
    //        var fee = 500;
    //        var userCanGetAppDate = null as string;

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles, fee, "", userCanGetAppDate);
    //    }

    //    #endregion

    //    #region[Step 6 ผปก. รับใบอนุญาตแล้วหรือไม่ต้องรอการรับ]

    //    /// <summary>
    //    /// ผปก.รับใบอนุญาตแล้ว หรือไม่ต้องรอการรับ หรือไม่มีใบอนุญาต
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CompletedWithAgentWorking()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, null);
    //        var status = ApplicationStatusV2Enum.COMPLETED;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.COMPLETED && appRequest.ActionReply == "EMPTY");
    //    }

    //    /// <summary>
    //    /// ผปก.รับใบอนุญาตแล้ว หรือไม่ต้องรอการรับ หรือไม่มีใบอนุญาต
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CompletedWithUserWorking()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, null);
    //        var status = ApplicationStatusV2Enum.COMPLETED;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.COMPLETED && appRequest.ActionReply == "EMPTY");
    //    }

    //    /// <summary>
    //    /// ผปก.รับใบอนุญาตแล้ว หรือไม่ต้องรอการรับ หรือไม่มีใบอนุญาต
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CompletedWithOtherStatus()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, null);
    //        var status = ApplicationStatusV2Enum.COMPLETED;
    //        var statusOther = "กำลังทดสอบ";
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.COMPLETED && appRequest.ActionReply == "EMPTY");
    //    }

    //    /// <summary>
    //    /// ผปก.รับใบอนุญาตแล้ว หรือไม่ต้องรอการรับ หรือไม่มีใบอนุญาต
    //    /// </summary>
    //    [TestMethod]
    //    public async Task CompletedWithOutOtherStatus()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType, true, ApplicationStatusV2Enum.CHECK, ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, null);
    //        var status = ApplicationStatusV2Enum.COMPLETED;
    //        var statusOther = "";
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.COMPLETED && appRequest.ActionReply == "EMPTY");
    //    }


    //    #endregion

    //    #region[Step 7 ปฏิเสธการออกใบอนุญาต]

    //    /// <summary>
    //    /// ปฏิเสธการออกใบอนุญาต
    //    /// </summary>
    //    [TestMethod]
    //    public async Task RejectedWithAgentWorking()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.REJECTED;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING;
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.REJECTED && appRequest.ActionReply == "EMPTY");
    //    }

    //    /// <summary>
    //    /// ปฏิเสธการออกใบอนุญาต
    //    /// </summary>
    //    [TestMethod]
    //    public async Task RejectedWithUserWorking()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.REJECTED;
    //        var statusOther = ApplicationStatusOtherValueConst.WAITING_USER_WORKING;
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.REJECTED && appRequest.ActionReply == "EMPTY");
    //    }

    //    /// <summary>
    //    /// ปฏิเสธการออกใบอนุญาต
    //    /// </summary>
    //    [TestMethod]
    //    public async Task RejectedWithOtherStatus()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.REJECTED;
    //        var statusOther = "กำลังทดสอบ";
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.REJECTED && appRequest.ActionReply == "EMPTY");
    //    }

    //    /// <summary>
    //    /// ปฏิเสธการออกใบอนุญาต
    //    /// </summary>
    //    [TestMethod]
    //    public async Task RejectedWithOutOtherStatus()
    //    {
    //        // arrange 
    //        var userType = UserTypeEnum.Citizen;
    //        var applicationRequestId = await init(userType);
    //        var status = ApplicationStatusV2Enum.REJECTED;
    //        var statusOther = "";
    //        var requestedFiles = null as FileMetadata[];

    //        // act
    //        var appRequest = await createRequest(userType, applicationId, applicationRequestId, status, statusOther, requestedFiles);

    //        // assert
    //        Assert.IsTrue(appRequest.Status == ApplicationStatusV2Enum.REJECTED && appRequest.ActionReply == "EMPTY");
    //    }

    //    #endregion
    //}
}

