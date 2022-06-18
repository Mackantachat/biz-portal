using BizPortal.Areas.WebApiV2.Controllers;
using BizPortal.DAL;
using BizPortal.DAL.MongoDB;
using BizPortal.Models;
using BizPortal.Utils;
using BizPortal.ViewModels.Apps;
using BizPortal.ViewModels.V2;
using EGA.EGA_Development.Util.MailV2.Data;
using EGA.Owin.Security.EGAOpenID;
using Mapster;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

    public class MappingTest
    {
        #region[Initial]

        string citizenId = "1709900381574";
        string juristicId = "0503560002791";
        int applicationId = 59;
        string applicationRequestModel = "{\"ApplicationID\":59,\"Status\":3,\"StatusOther\":\"WAITING_AGENT_READ_REQUEST\",\"StatusOtherText\":null,\"StatusBeforeCancel\":null,\"ActionReply\":0,\"PermitDeliveryAddress\":null,\"PermitDeliveryType\":null,\"PermitDeliverOnPayment_OK\":false,\"PaymentMethod\":null,\"PaymentMethodEnabledChoice\":null,\"BillPaymentTypeForPaymentMethod\":null,\"PermitDeliveryTypeEnabledChoice\":null,\"EMSTrackingNumber\":null,\"DisableUpdateStatus\":null,\"ApplicationRequestNumber\":null,\"ApplicationRequestRunningNumber\":0,\"IsRequestNumberAgent\":false,\"ApplicationRequestNumberAgent\":null,\"IdentityID\":\"5633071108732\",\"IdentityName\":\"นาย test14 test\",\"IdentityType\":0,\"ApplicationRequestID\":null,\"RequestBatchID\":\"dcc9787a-4b0f-46ab-94fe-0eefb972d382\",\"Fee\":null,\"EMSFee\":50.0,\"DueDateForPayFee\":null,\"Duration\":null,\"PermitCanBeDeliveredOnPayment\":false,\"UserCanGetAppDate\":null,\"ExpectedFinishDate\":null,\"ProvinceID\":null,\"AmphurID\":null,\"TumbolID\":null,\"Province\":null,\"Amphur\":null,\"Tumbol\":null,\"SourceIPAddress\":\"::1\",\"ReplyFromOrg\":false,\"ReplyFromApiUpdate\":false,\"TransactionCode\":null,\"TransactionDate\":null,\"Data\":{\"SELL_FOOD_INFORMATION\":{\"GroupName\":\"SELL_FOOD_INFORMATION\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"SELL_FOOD_INFORMATION__APP_TYPE_OPTION\":\"หนังสือรับรองการแจ้ง\",\"SELL_FOOD_INFORMATION__LICENSE_TYPE_OPTION\":\"หนังสือรับรองการแจ้ง\",\"SELL_FOOD_INFORMATION__BUSINESS_TYPE_OPTION\":\"APP_SELL_FOOD_LOCATION_TYPE_OPTION__SELL\",\"SELL_FOOD_INFORMATION__PURPOSE_OPTION\":\"APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__SELL\",\"SELL_FOOD_INFORMATION__FOOD_TYPE\":\"test\"}},\"SELL_FOOD_FOOD_WORKER_INFO\":{\"GroupName\":\"SELL_FOOD_FOOD_WORKER_INFO\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"SELL_FOOD_FOOD_WORKER_INFO_TOTAL\":\"0\"}},\"SELL_FOOD_FOOD_MANAGER_INFO\":{\"GroupName\":\"SELL_FOOD_FOOD_MANAGER_INFO\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__TITLE\":\"01\",\"SELL_FOOD_FOOD_MANAGER_INFO__NAME\":\"test\",\"SELL_FOOD_FOOD_MANAGER_INFO__LASTNAME\":\"test\",\"SELL_FOOD_FOOD_MANAGER_INFO__AGE\":\"31\",\"DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__NATIONALITY\":\"70\",\"ADDRESS_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"123\",\"ADDRESS_MOO_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"ADDRESS_SOI_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"ADDRESS_ROAD_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"ADDRESS_PROVINCE_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"10\",\"ADDRESS_AMPHUR_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"05\",\"ADDRESS_TUMBOL_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"08\",\"ADDRESS_TELEPHONE_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"0123456789\",\"ADDRESS_TELEPHONE_EXT_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"ADDRESS_FAX_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS\":\"\",\"SELL_FOOD_CONFIRM_SELL_FOOD_CONFIRM__TRUE\":\"true\",\"DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__TITLE_TEXT\":\"นาย\",\"DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__NATIONALITY_TEXT\":\"ไทย\",\"ADDRESS_PROVINCE_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS_TEXT\":\"กรุงเทพมหานคร\",\"ADDRESS_AMPHUR_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS_TEXT\":\"เขตบางเขน\",\"ADDRESS_TUMBOL_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS_TEXT\":\"ท่าแร้ง\"}},\"CITIZEN_GENERAL_INFORMATION_HEADER\":{\"GroupName\":\"CITIZEN_GENERAL_INFORMATION_HEADER\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{}},\"GENERAL_INFORMATION\":{\"GroupName\":\"GENERAL_INFORMATION\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"INFORMATION_HEADER__REQUEST_DATE\":\"1/3/2562 16:21:43\",\"INFORMATION_HEADER__REQUEST_AT\":\"Biz Portal\",\"INFORMATION__REQUEST_AS_OPTION\":\"บุคคลธรรมดา\",\"DROPDOWN_CITIZEN_TITLE\":\"01\",\"CITIZEN_NAME\":\"test14\",\"CITIZEN_LASTNAME\":\"test\",\"GENERAL_INFORMATION__CITIZEN_AGE\":\"31\",\"DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY\":\"70\",\"IDENTITY_ID\":\"5633071108732\",\"DROPDOWN_CITIZEN_TITLE_TEXT\":\"นาย\",\"DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT\":\"ไทย\"}},\"CITIZEN_ADDRESS_INFORMATION\":{\"GroupName\":\"CITIZEN_ADDRESS_INFORMATION\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"ADDRESS_CITIZEN_ADDRESS\":\"123\",\"ADDRESS_MOO_CITIZEN_ADDRESS\":\"\",\"ADDRESS_SOI_CITIZEN_ADDRESS\":\"\",\"ADDRESS_BUILDING_CITIZEN_ADDRESS\":\"\",\"ADDRESS_ROOMNO_CITIZEN_ADDRESS\":\"\",\"ADDRESS_FLOOR_CITIZEN_ADDRESS\":\"\",\"ADDRESS_ROAD_CITIZEN_ADDRESS\":\"\",\"ADDRESS_PROVINCE_CITIZEN_ADDRESS\":\"10\",\"ADDRESS_AMPHUR_CITIZEN_ADDRESS\":\"15\",\"ADDRESS_TUMBOL_CITIZEN_ADDRESS\":\"04\",\"ADDRESS_POSTCODE_CITIZEN_ADDRESS\":\"10600\",\"ADDRESS_TELEPHONE_CITIZEN_ADDRESS\":\"0123456789\",\"ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS\":\"\",\"ADDRESS_FAX_CITIZEN_ADDRESS\":\"\",\"ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT\":\"กรุงเทพมหานคร\",\"ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT\":\"เขตธนบุรี\",\"ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT\":\"บุคคโล\"}},\"REQUESTOR_INFORMATION__HEADER\":{\"GroupName\":\"REQUESTOR_INFORMATION__HEADER\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION\":\"REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER\"}},\"INFORMATION_STORE\":{\"GroupName\":\"INFORMATION_STORE\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"INFORMATION_STORE__USE_CITIZEN_ADDRESS_INFORMATION_STORE__USE_CITIZEN_ADDRESS__TRUE\":\"true\",\"INFORMATION_STORE_NAME_TH\":\"test\",\"INFORMATION_STORE_NAME_EN\":\"\",\"ADDRESS_INFORMATION_STORE__ADDRESS\":\"123\",\"ADDRESS_MOO_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_SOI_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_ROAD_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS\":\"10\",\"ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS\":\"15\",\"ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS\":\"04\",\"ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS\":\"10600\",\"ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS\":\"0123456789\",\"ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_FAX_INFORMATION_STORE__ADDRESS\":\"\",\"ADDRESS_LAT_INFORMATION_STORE__ADDRESS\":\"13.7553323\",\"ADDRESS_LNG_INFORMATION_STORE__ADDRESS\":\"100.5226751\",\"SEARCH_TEXT_GOOGLE_MAP\":\"บุคคโล เขตธนบุรี กรุงเทพมหานคร\",\"INFORMATION_STORE_AREA\":\"50\",\"CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION\":\"INFORMATION_STORE_BUILDING_TYPE_OPTION__OWNED\",\"INFORMATION_STORE_HEALTH_OTHER\":\"\",\"ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT\":\"กรุงเทพมหานคร\",\"ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT\":\"เขตธนบุรี\",\"ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT\":\"บุคคโล\"}},\"OPENID\":{\"GroupName\":\"OPENID\",\"GroupDescription\":null,\"Visible\":true,\"Data\":{\"FULLNAME\":\"นาย test14 test\",\"PREFIX_TH\":\"นาย\",\"FIRSTNAME_TH\":\"test14\",\"LASTNAME_TH\":\"test\",\"GENDER\":\"ชาย\"}}},\"Remark\":null,\"OrgNameTH\":null,\"OrgAddress\":null,\"DisabledSendingSystemEmail\":false,\"EmailMessage\":null,\"SmsMessage\":null,\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"UploadedFiles\":[{\"FileGroupID\":\"a51f62b2-1cc6-48a0-9ac2-da3c923f8b4a\",\"Description\":\"CITIZEN_INFORMATION\",\"Files\":[{\"FileID\":\"5c78fd6a70b8b312889bf296\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"ID_CARD_COPY\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"ID_CARD_COPY\",\"DISPLAYNAME\":\"เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: บุคคลผู้ขออนุญาต *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7070b8b312889bf299\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"CITIZEN_RENAME_MARRIAGE_DOC\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"CITIZEN_RENAME_MARRIAGE_DOC\",\"DISPLAYNAME\":\"ใบสำคัญการเปลี่ยนชื่อ/ทะเบียนสมรส\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7370b8b312889bf29c\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"CITIZEN_BKK_FOOD_HEALTH_CERT\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"CITIZEN_BKK_FOOD_HEALTH_CERT\",\"DISPLAYNAME\":\"หนังสือรับรองผ่านการอบรมหลักสูตรสุขาภิบาลอาหารของกรุงเทพมหานคร: บุคคลผู้ขออนุญาต\",\"PREDOC\":\"false\"},\"TYPE\":null}],\"Extras\":null},{\"FileGroupID\":\"791addfd-7f9f-472c-bec6-202e236b67aa\",\"Description\":\"INFORMATION_STORE_FILE_SECTION\",\"Files\":[{\"FileID\":\"5c78fd7670b8b312889bf29f\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT\",\"DISPLAYNAME\":\"หนังสือแจ้งการใช้หรือเปลี่ยนแปลงการใช้ประโยชน์ที่ดินตามกฎหมายว่าด้วยการผังเมือง *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7870b8b312889bf2a2\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_BUILDING_OWNER_DOC\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_BUILDING_OWNER_DOC\",\"DISPLAYNAME\":\"เอกสารแสดงความเป็นเจ้าของอาคารสถานที่ที่ใช้เป็นร้าน/สถานประกอบการ เช่น ใบอนุญาตก่อสร้าง หรือสัญญาซื้อขาย *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7b70b8b312889bf2a5\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_MAP\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_MAP\",\"DISPLAYNAME\":\"แผนที่สังเขป แสดงสถานที่ตั้งของร้าน/สถานประกอบการ *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd7e70b8b312889bf2a8\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD\",\"DISPLAYNAME\":\"ทะเบียนบ้าน:อาคารที่ตั้งร้าน / สถานประกอบการ *\",\"PREDOC\":\"false\"},\"TYPE\":null}],\"Extras\":null},{\"FileGroupID\":\"a910792d-7a4e-41a1-af51-49668e13a859\",\"Description\":\"BUILDING_FILE_SECTION\",\"Files\":[{\"FileID\":\"5c78fd8170b8b312889bf2ab\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"INFORMATION_STORE_BUILDING_CONTROL_DOC\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"INFORMATION_STORE_BUILDING_CONTROL_DOC\",\"DISPLAYNAME\":\"หลักฐานเกี่ยวกับการใช้อาคารตามกฏหมายว่าด้วยการควบคุมอาคาร เช่น ใบอนุญาตก่อสร้าง (อ.1) หรือใบรับรองการเปิดใช้อาคาร *\",\"PREDOC\":\"false\"},\"TYPE\":null}],\"Extras\":null},{\"FileGroupID\":\"77c76377-97f9-4d48-9de5-e67e8209652d\",\"Description\":\"SELL_FOOD_FILE_SECTION\",\"Files\":[{\"FileID\":\"5c78fd8370b8b312889bf2ae\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"SELL_FOOD_POLUTION_CONTROL_CHART\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"SELL_FOOD_POLUTION_CONTROL_CHART\",\"DISPLAYNAME\":\"แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงการป้องกันมลพิษ *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd8770b8b312889bf2b1\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"SELL_FOOD_HEALTH_CONTROL_CHART\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"SELL_FOOD_HEALTH_CONTROL_CHART\",\"DISPLAYNAME\":\"แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงสุขลักษณะภายในสถานประกอบการ *\",\"PREDOC\":\"false\"},\"TYPE\":null},{\"FileID\":\"5c78fd8a70b8b312889bf2b4\",\"GovDocumentName\":null,\"GovDocumentDescription\":null,\"EPermitFileName\":null,\"EPermitFileDescription\":null,\"EPermitExpireDate\":\"1/1/0544 0:00:00\",\"FileReason\":null,\"FileName\":\"test.png\",\"FileTypeCode\":\"SELL_FOOD_SEFTY_CONTROL_CHART\",\"FileTypeName\":null,\"FileSize\":3564,\"IsPublic\":false,\"ContentType\":\"image/png\",\"UploadStatus\":0,\"Extras\":{\"FILETYPECODE\":\"SELL_FOOD_SEFTY_CONTROL_CHART\",\"DISPLAYNAME\":\"แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงระบบความปลอดภัยในการทำงาน *\",\"PREDOC\":\"false\"},\"TYPE\":null}],\"Extras\":null}],\"GovFiles\":null,\"RequestedFiles\":null,\"EPermitFiles\":null,\"EPermitFilesForDisplay\":null,\"BillPaymentFiles\":null,\"AttachBillPayment\":null,\"Attachments\":null,\"CreatedDate\":\"0001-01-01T00:00:00\",\"UpdatedDate\":\"0001-01-01T00:00:00\",\"UpdatedDateByRequestor\":\"2019-03-03T12:46:49.0155433+07:00\",\"UpdatedDateByAgent\":\"0001-01-01T00:00:00\",\"UpdatedByAgent\":null,\"LastRequestorUpdateEmail\":null,\"isPassStepWaiting\":null,\"Chats\":null,\"CgdPayments\":null}";

        ApplicationsController ctrl = new ApplicationsController();
        ApplicationDbContext db = new ApplicationDbContext();

        private async Task<ApplicationRequestEntity> init(UserTypeEnum userType, bool isLicensing = true, ApplicationStatusV2Enum status = ApplicationStatusV2Enum.CHECK, string statusOther = ApplicationStatusOtherValueConst.WAITING_AGENT_READ_REQUEST, string deliveryType = PermitDeliveryTypeValueConst.BY_MAIL)
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

            return appRequest;
        }

        #endregion

        [TestMethod]
        public async Task ConvertFromBizToOrg()
        {
            // arrange 
            var bizData = ApplicationRequestEntity.Get(Guid.Parse("9e6ca2a9-b3a8-468e-a2f4-ac7ec6b001fd"));

            // act
            var mapper = new Dictionary<string, string>
            {
                {"IdentityID","IdentityID"},
                {"IdentityName","IdentityName"},
                {"Data{APP_BUILDING_G1_INFORMATION}.APP_BUILDING_G1_TYPE","APP_BUILDING_G1_TYPE"},
                {"Data{APP_BUILDING_G1_INFORMATION}.APP_BUILDING_G1_DURATION","APP_BUILDING_G1_DURATION"},
            };

            var orgData = MappingHelper.Convert<ApplicationRequestEntity, object>(bizData, new object { }, mapper);
            var OrgDataJObject = JObject.FromObject(orgData);

            // assert
            Assert.IsTrue(OrgDataJObject["IdentityID"].ToString() == "0105561082522"
                && OrgDataJObject["IdentityName"].ToString() == "คัลเจอร์ ดี จำกัด"
                && OrgDataJObject["APP_BUILDING_G1_TYPE"].ToString() == "APP_BUILDING_BUILDING_SECTION_TYPE_BUILDING"
                && OrgDataJObject["APP_BUILDING_G1_DURATION"].ToString() == "150");

        }

        [TestMethod]
        public async Task ConvertFromOrgToBiz()
        {
            // arrange 
            var bizData = ApplicationRequestEntity.Get(Guid.Parse("9e6ca2a9-b3a8-468e-a2f4-ac7ec6b001fd"));

            // act
            var mapper = new Dictionary<string, string>
            {
                {"IdentityID","IdentityID"},
                {"IdentityName","IdentityName"},
                {"Data{APP_BUILDING_G1_INFORMATION}.APP_BUILDING_G1_TYPE","APP_BUILDING_G1_TYPE"},
                {"Data{APP_BUILDING_G1_INFORMATION}.APP_BUILDING_G1_DURATION","APP_BUILDING_G1_DURATION"},
            };

            var bizToOrgData = MappingHelper.Convert<ApplicationRequestEntity, object>(bizData, new object { }, mapper);
            var orgToBizData = MappingHelper.Convert<object, ApplicationRequestEntity>(bizToOrgData, bizData, mapper, MappingHelper.Mappingirection.RightToLeft);
            var orgToBizDataJObject = JObject.FromObject(orgToBizData);

            // assert
            Assert.IsTrue(orgToBizDataJObject["IdentityID"].ToString() == "0105561082522"
                && orgToBizDataJObject["IdentityName"].ToString() == "คัลเจอร์ ดี จำกัด"
                && orgToBizDataJObject["Data.APP_BUILDING_G1_INFORMATION.Data.APP_BUILDING_G1_TYPE"].ToString() == "APP_BUILDING_BUILDING_SECTION_TYPE_BUILDING"
                && orgToBizDataJObject["Data.APP_BUILDING_G1_INFORMATION.Data.APP_BUILDING_G1_DURATION"].ToString() == "150");

        }
    }
}
