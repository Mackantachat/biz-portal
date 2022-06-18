using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Apps.DOTStandard;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using EGA.WS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BizPortal.AppsHook
{
    public class DOTTourismNewApphook : SingleFormRendererAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 0;
        }

        public override bool IsEnabledChat() => false;

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            //หลังจากทำ Biz-api เสร็จแล้วจะต้องนำโค้ดชุดนี้ออก
            if (request.Data.ContainsKey("INFORMATION_STORE"))
            {
                var storeInfo = request.Data["INFORMATION_STORE"].Data;
                if (storeInfo.ContainsKey("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS"))
                {
                    request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS"]);
                    request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS"]);
                    request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS"]);

                    request.Province = (storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"]);
                    request.Amphur = (storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"]);
                    request.Tumbol = (storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"]);
                }
            }

            // TODO: Updated on 2020-05-08, do not submit data to third-party.
            return new InvokeResult() { Success = true };

            #region API-API
            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = true;

            Dictionary<string, string> citizenFileList = new Dictionary<string, string>
            {
                { "ID_CARD_COPY", "1" },
                { "CITIZEN_RENAME_DOC", "2" },
                { "CITIZEN_HOUSEHOLD_REGISTRATION_COPY", "3" },
                { "APP_TOURISM_BUSINESS_PICTURE_OFFICE_CITIZEN", "4" },
                { "APP_TOURISM_BUSINESS_PICTURE_INSIDE_OFFICE", "4" },
                { "APP_TOURISM_BUSINESS_LOCATION_OFFICE_CITIZEN", "5" },
                { "APP_TOURISM_BUSINESS_DOCUMENT_OWNERD_AREA", "6" },
                { "INFORMATION_STORE_BUILDING_OWNER_DOC", "6" },
                { "INFORMATION_STORE_BUILDING_OWNER_ID_CARD", "6" },
                { "INFORMATION_STORE_RENTAL_CONTRACT", "6" },
                { "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT", "6" },
                { "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION", "6" },
                { "INFORMATION_STORE_HOUSEHOLD_RENT", "6" },
                { "APP_TOURISM_BUSINESS_COPY_DOCUMENT_POLICY", "7" },
                { "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC", "8" },
                { "AUTHORIZATION_AUTHORIZEE_ID_CARD", "8" },
                { "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD", "8" },
                { "APP_TOURISM_BUSINESS_DOCUMENT_COLLATERAL_CITIZEN", "9" },
                { "FREE_DOC", "10" }
            };

            Dictionary<string, string> juristicFileList = new Dictionary<string, string>
            {
                { "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY", "11" },
                { "LIST_OF_SHAREHOLDERS_5_JURISTIC_OPTIONAL", "12" }, //บัญชีรายชื่อผู้ถือหุ้น (บอจ.5)
                { "MEMORANDUM_2_JURISTIC_OPTIONAL", "13" }, //หนังสือบริคณห์สนธิ (บอจ.2)
                { "APP_TOURISM_BUSINESS_COMPANY_RULES", "14" }, //ข้อบังคับของบริษัท
                { "APP_TOURISM_BUSINESS_MEETING_REPORT", "15" }, //รายงานการประชุม
                { "REGISTRATION_LIST_3_JURISTIC_OPTIONAL", "16" }, //รายการจดทะเบียนจัดตั้ง (บอจ.3)
                { "JURISTIC_REGISTRATION_TYPE_PARTNERSHIP", "17" }, //รายการจดทะเบียนจัดตั้งห้างหุ้นส่วนสามัญจดทะเบียนหรือห้างหุ้นส่วนจำกัด (หส.2)
                { "APP_TOURISM_BUSINESS_REPORT_AMENDMENT", "18" }, //รายงานการจดทะเบียนแก้ไขเพิ่มเติม
                { "JURISTIC_COMMITTEE_ID_CARD", "19" }, //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: กรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคล
                { "JURISTIC_COMMITTEE_CHANGE_NAME_DOCUMENT", "20" }, //ใบสำคัญการเปลี่ยนชื่อ
                { "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY", "21" }, //ทะเบียนบ้าน: กรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคล
                { "APP_TOURISM_BUSINESS_PICTURE_OFFICE_JURISTIC", "22" },
                { "APP_TOURISM_BUSINESS_PICTURE_INSIDE_OFFICE", "22" },
                { "APP_TOURISM_BUSINESS_SEAL_COMPANY", "22" },
                { "APP_TOURISM_BUSINESS_LOCATION_OFFICE_JURISTIC", "23" },
                { "APP_TOURISM_BUSINESS_DOCUMENT_OWNERD_AREA", "24" },
                { "INFORMATION_STORE_BUILDING_OWNER_DOC", "24" },
                { "INFORMATION_STORE_BUILDING_OWNER_ID_CARD", "24" },
                { "INFORMATION_STORE_RENTAL_CONTRACT", "24" },
                { "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT", "24" },
                { "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION", "24" },
                { "INFORMATION_STORE_HOUSEHOLD_RENT", "24" },
                { "APP_TOURISM_BUSINESS_COPY_DOCUMENT_POLICY", "25" },
                { "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC", "26" },
                { "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE", "26" },
                { "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD", "26" },
                { "APP_TOURISM_BUSINESS_DOCUMENT_COLLATERAL_JURISTIC", "27" },
                { "FREE_DOC", "28" }
            };

            try
            {
                string tokenUrl = ConfigurationManager.AppSettings["DOT_TOURISM_BUSINESS_TOKEN_WS_URL"];
                string regisUrl = ConfigurationManager.AppSettings["DOT_TOURISM_BUSINESS_REGIS_WS_URL"];
                string DGA_WS_URL = ConfigurationManager.AppSettings["DGA_WS_URL"].ToString();

                switch (stage)
                {

                    //case AppsStage.UserUpdate:
                    //    result.Success = true;
                    //    break;
                    case AppsStage.UserUpdate:                  
                    case AppsStage.UserCreate:
                        {
                            // add check status
                            if (model.Status == ApplicationStatusV2Enum.CHECK || model.Status == ApplicationStatusV2Enum.PENDING)
                            {
                                var token = string.Empty;
                                var jsonPost = string.Empty;
                                var jsonLog = string.Empty;
                                string flag_ = string.Empty;
                                flag_ = "A";
                                if (stage == AppsStage.UserUpdate) flag_ = "E"; else flag_ = "A";
                                // if (AppsStage.UserCreate.ToString() == "UserCreate") flag_ = "A"; else flag_ = "E";
                                //flag_ = "E";
                                Api.OnCheckingApplicationError += (ex) =>
                                {
                                    result.Exception = ex;
                                    var egaEx = ex as EGAWSAPIException;
                                    if (egaEx != null)
                                    {
                                        var message = string.Format("{0}: {1}", (int)egaEx.HttpStatusCode, egaEx.ResponseData["Message"].ToString());
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, message, egaEx.ResponseData.ToString(), jsonPost);
                                        result.Message = egaEx.ResponseData["Message"].ToString();
                                    }
                                    else
                                    {
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, ex.Message, ex.StackTrace, jsonPost);
                                        result.Message = ex.Message;
                                    }
                                };

                                ////var client = new RestClient("http://103.80.100.105:8080");
                                ////var restRequest = new RestRequest("/api/GetToken/opdc/opdc", Method.GET);
                                ////restRequest.AddHeader("Content-Type", "application/json");
                                ////IRestResponse restResp = client.Execute(restRequest);
                                ////if (restResp != null && restResp.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(restResp.Content))
                                ////{
                                ////    token = JObject.Parse(restResp.Content)["Token"].DefaultString();
                                ////}
                                ////else
                                ////{
                                ////    throw new Exception("ไม่สามารถขอ Token ได้");
                                ////}

                                string u = DGA_WS_URL + "/ws" + tokenUrl;
                                var client = new RestClient(u);
                                var restRequest = new RestRequest(Method.POST);
                                restRequest.AddHeader("Content-Type", "application/json");
                                restRequest.AddHeader("Token", Api.AccessToken);
                                restRequest.AddHeader("Consumer-Key", Api.ConsumerKey);
                                //request_.AddParameter("application/json", jsonPost, ParameterType.RequestBody);
                                restRequest.AddParameter("username", "opdc", ParameterType.GetOrPost);
                                restRequest.AddParameter("password", "opdc", ParameterType.GetOrPost);
                                IRestResponse restResp = client.Execute(restRequest);

                                if (restResp != null && restResp.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(restResp.Content))
                                {
                                    token = JObject.Parse(restResp.Content)["Token"].DefaultString();
                                }
                                else
                                {
                                    throw new Exception("ไม่สามารถขอ Token ได้");
                                }

                                //var tokenResponse = Api.Get(tokenUrl, ContentType.ApplicationJson);
                                //if (tokenResponse != null && tokenResponse.HasValues && !string.IsNullOrEmpty(tokenResponse["Token"].ToString()))
                                //{
                                //    token = tokenResponse["Token"].ToString();
                                //}
                                //else
                                //{
                                //    string error = "ไม่สามารถเชื่อมต่อกับระบบของกรมการท่องเที่ยวได้ (Token)";
                                //    result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                //    throw new Exception(error);
                                //}

                                DOTTourismBase post = new DOTTourismBase()
                                {
                                    Token = token,
                                    RequestFormNo = model.ApplicationRequestNumber.ToString(),
                                    ApplicationId = model.ApplicationID.ToString(),
                                    ApplicationRequestid = model.ApplicationRequestID.ToString(),
                                    Flag = flag_,

                                    EstablishmentNameTH = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH"),
                                    EstablishmentNameEN = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_EN"),
                                    EstablishCall = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_EN_THAI_SPELL"),
                                    EstablishmentDate = "",// model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData(""),
                                    EstablishTel = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                                    EstablishFax = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS"),
                                    EstablishEmail = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS"),
                                    EstablishMobile = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOBILE_INFORMATION_STORE__ADDRESS"),
                                    EstablishWebsite = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_WEBSITE"),
                                    AddressNameTH = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS"),
                                    BuildingNameTH = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS"),
                                    FloorNameTH = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS"),
                                    MooNameTH = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS"),
                                    SoiNameTH = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS"),
                                    VillageNameTH = string.Empty,
                                    RoadNameTH = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"),
                                    SubDistrictNameTH = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"),
                                    DistrictNameTH = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"),
                                    ProvinceNameTH = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"),
                                    ZipCode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"),
                                    AddressNameEN = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EN_INFORMATION_STORE__ADDRESS_EN"),
                                    BuildingNameEN = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EN_BUILDING_INFORMATION_STORE__ADDRESS_EN"),
                                    FloorNameEN = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EN_FLOOR_INFORMATION_STORE__ADDRESS_EN"),
                                    MooNameEN = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EN_MOO_INFORMATION_STORE__ADDRESS_EN"),
                                    SoiNameEN = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EN_SOI_INFORMATION_STORE__ADDRESS_EN"),
                                    VillageNameEN = string.Empty,
                                    RoadNameEN = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EN_ROAD_INFORMATION_STORE__ADDRESS_EN"),
                                    SubDistrictNameEN = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EN_TUMBOL_INFORMATION_STORE__ADDRESS_EN_TEXT"),
                                    DistrictNameEN = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EN_AMPHUR_INFORMATION_STORE__ADDRESS_EN_TEXT"),
                                    ProvinceNameEN = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EN_PROVINCE_INFORMATION_STORE__ADDRESS_EN_TEXT"),
                                    BranchQty = model.Data.TryGetData("APP_TOURISM_BUSINESS_INFO_SECTION").ThenGetStringData("APP_TOURISM_BUSINESS_INFO_SECTION_BRANCH"),
                                    GuaranteeTypeNameTH = model.Data.TryGetData("APP_TOURISM_BUSINESS_INFO_SECTION").ThenGetStringData("DROPDOWN_APP_TOURISM_BUSINESS_INFO_SECTION_GUARANTEE"),
                                    GaranteeAmount = model.Data.TryGetData("APP_TOURISM_BUSINESS_INFO_SECTION").ThenGetStringData("APP_TOURISM_BUSINESS_INFO_SECTION_AMOUNT"),
                                    BankNameTH = model.Data.TryGetData("APP_TOURISM_BUSINESS_INFO_SECTION").ThenGetStringData("DROPDOWN_APP_TOURISM_BUSINESS_INFO_SECTION_BANK"),
                                    BusinessType = new DOTBusinessType[]
                                    {
                                    new DOTBusinessType
                                    {
                                        BusinessType = model.Data.TryGetData("APP_TOURISM_BUSINESS_INFO_SECTION").ThenGetStringData("APP_TOURISM_BUSINESS_INFO_SECTION_TYPE_OPTION"),
                                        Qty = model.Data.TryGetData("APP_TOURISM_BUSINESS_INFO_SECTION").ThenGetStringData("APP_TOURISM_BUSINESS_INFO_SECTION_PLAN_TOURISM"),
                                        Remark = model.Data.TryGetData("APP_TOURISM_BUSINESS_INFO_SECTION").ThenGetStringData("APP_TOURISM_BUSINESS_INFO_SECTION_COUNTRY")
                                    }
                                    }
                                };
                                string CardExpireDate = string.Empty;
                                List<DOTDocument> documents = new List<DOTDocument>();
                                Dictionary<string, string> fileList = null;
                                var defaultFileId = string.Empty;

                                if (model.IdentityType == UserTypeEnum.Juristic)
                                {
                                    post.EstablishmentType = "J";
                                    post.JusRegisterNo = model.IdentityID;
                                    //post.JusRegisterDate = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("REGISTER_DATE");

                                    string tmp = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("REGISTER_DATE");
                                    String[] CardExpireDateArr = tmp.Split('/');
                                    if (CardExpireDateArr.Length == 3) CardExpireDate = CardExpireDateArr[2] + "-" + CardExpireDateArr[1] + "-" + CardExpireDateArr[0]; else CardExpireDate = "1990-01-01";
                                    post.JusRegisterDate = CardExpireDate;
                                    fileList = juristicFileList;
                                    defaultFileId = "28";
                                    // post.CardExpireDate = "";

                                    //String[] CardExpireDateArr = tmp.Split('/');
                                    //if (CardExpireDateArr.Length == 3) CardExpireDate = CardExpireDateArr[2] + "-" + CardExpireDateArr[1] + "-" + CardExpireDateArr[0]; else CardExpireDate = "1990-01-01";
                                    //post.CardExpireDate = DateTime.Now.ToString("yyyy-MM-dd");

                                    string tmp2 = string.Empty;
                                    var boardTotal = int.Parse(model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("COMMITTEE_INFORMATION_TOTAL"));
                                    if (boardTotal > 0)
                                    {
                                        for (int i = 0; i < boardTotal; i++)
                                        {
                                            var boardAuth = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION_" + i);
                                            if (boardAuth != "yes")
                                            {
                                                if (i == 0)
                                                {
                                                    post.TitleNameTH = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("DROPDOWN_JURISTIC_COMMITTEE_TITLE_TEXT_" + i);
                                                    post.TitleNameEN = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("DROPDOWN_JURISTIC_COMMITTEE_TITLE_EN_TEXT_" + i);
                                                    post.FirstNameTH = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_NAME_" + i);
                                                    post.LastNameTH = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_LASTNAME_" + i);
                                                    post.FirstNameEN = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_FIRSTNAME_EN_" + i);
                                                    post.LastNameEN = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_LASTNAME_EN_" + i);
                                                    //post.CardId = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_CITIZEN_ID_" + i).Replace("-", "");
                                                    post.CardId = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("COMMITTEE_INFORMATION_CITIZEN_ID_" + i).Replace("-", "");
                                                    post.CardExpireDate = DateTime.Now.ToString("yyyy-MM-dd");

                                                    tmp2 = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ID_CARD_EXPIRE_COMMITTEE_" + i);
                                                    string[] CardExpireDateArr2 = tmp2.Split('/');
                                                    if (CardExpireDateArr2.Length == 3) CardExpireDate = CardExpireDateArr2[2] + "-" + CardExpireDateArr2[1] + "-" + CardExpireDateArr2[0]; else CardExpireDate = "2000-01-01";// DateTime.Now.ToString("yyyy-MM-dd");
                                                    post.CardExpireDate = CardExpireDate;
                                                }
                                                else if (i == 1)
                                                {
                                                    post.TitleNameTH2 = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("DROPDOWN_JURISTIC_COMMITTEE_TITLE_TEXT_" + i);
                                                    post.TitleNameEN2 = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("DROPDOWN_JURISTIC_COMMITTEE_TITLE_EN_TEXT_" + i);
                                                    post.FirstNameTH2 = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_NAME_" + i);
                                                    post.LastNameTH2 = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_LASTNAME_" + i);
                                                    post.FirstNameEN2 = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_FIRSTNAME_EN_" + i);
                                                    post.LastNameEN2 = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_LASTNAME_EN_" + i);
                                                     //post.CardId2 = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_CITIZEN_ID_" + i).Replace("-", "");
                                                    post.CardId2 = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("COMMITTEE_INFORMATION_CITIZEN_ID_" + i).Replace("-", "");
                                                    tmp2 = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ID_CARD_EXPIRE_COMMITTEE_" + i);
                                                    string[] CardExpireDateArr2 = tmp2.Split('/');
                                                    if (CardExpireDateArr2.Length == 3) CardExpireDate = CardExpireDateArr2[2] + "-" + CardExpireDateArr2[1] + "-" + CardExpireDateArr2[0]; else CardExpireDate = "2000-01-01";//DateTime.Now.ToString("yyyy-MM-dd");
                                                    post.CardExpireDate2 = CardExpireDate;
                                                }

                                                /*board.ref_prefix_id = Convert.ToInt32(uPrefixList.responseModel.data.FirstOrDefault(x => x.name == model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_TITLE_" + 1)).id);
                                                board.first_name = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_NAME_" + i);
                                                board.last_name = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_LASTNAME_" + i);
                                                var boardNation = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION_" + i);
                                                if (boardNation == "yes")
                                                {
                                                    board.idcard = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_CITIZEN_ID_" + i).Replace("-", "");
                                                }
                                                else
                                                {
                                                    board.idcard = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("COMMITTEE_INFORMATION_PASSPORT_NUMBER_" + i).Replace("-", "");
                                                }

                                                board.is_proved = 0;
                                                boardList.Add(board);*/
                                            }

                                        }
                                    }
                                }
                                else if (model.IdentityType == UserTypeEnum.Citizen)
                                {
                                    post.EstablishmentType = "I";
                                    post.CardId = model.IdentityID;
                                    //post.CardExpireDate ="2020-02-24";// 
                                    string tmp = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("ID_CARD_EXPIRE");

                                    string[] CardExpireDateArr = tmp.Split('/');
                                    if (CardExpireDateArr.Length == 3) CardExpireDate = CardExpireDateArr[2] + "-" + CardExpireDateArr[1] + "-" + CardExpireDateArr[0]; else CardExpireDate = "1990-01-01";
                                    post.CardExpireDate = CardExpireDate;
                                    post.TitleNameTH = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_TITLE_TEXT");
                                    post.FirstNameTH = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_NAME");
                                    post.LastNameTH = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME");

                                    post.TitleNameEN = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_TITLE_EN_TEXT");
                                    post.FirstNameEN = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_FIRSTNAME_EN");
                                    post.LastNameEN = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME_EN");

                                    fileList = citizenFileList;
                                    defaultFileId = "10";
                                }

                                //var up = model.UploadedFiles.ToArray();
                                //if (stage == AppsStage.UserUpdate)
                                //{

                                //    up = model.UploadedFiles.Where(x => x.Description == "REQUESTED_FILE").ToArray().OrderByDescending(x => x.CreatedDate).Take(1).ToArray();
                                //    foreach (var fgs in up)
                                //    //foreach (var fg in model.UploadedFiles)
                                //    {
                                //        foreach (var filer in fgs.Files)
                                //        {

                                //            var fileTypeDesc = filer.Extras.ContainsKey("FILETYPEDESC") ? filer.Extras["FILETYPEDESC"].ToString() : string.Empty;
                                //            var fileType = filer.Extras.ContainsKey("FILETYPE") ? filer.Extras["FILETYPE"].ToString() : string.Empty;
                                //            var FILETYPEDESC = filer.Extras.ContainsKey("FILETYPEDESC") ? filer.Extras["FILETYPEDESC"].ToString() : string.Empty;                                  
                                //            documents.Add(new DOTDocument()
                                //            {
                                //                FileName = filer.FileName,
                                //                DocumentType = fileType,
                                //                Base64 = filer.GetBased64String(),
                                //                Remark = FILETYPEDESC
                                //            });
                                //        }
                                //    }

                                //}
                                //else
                                //{

                                //    foreach (var fg in model.UploadedFiles)
                                //    {
                                //        foreach (var file in fg.Files)
                                //        {
                                //            var selectedFile = fileList.Where(o => o.Key.Contains(file.FileTypeCode)).SingleOrDefault();
                                //            var fileId = !selectedFile.IsDefault() ? selectedFile.Value : defaultFileId;
                                //            documents.Add(new DOTDocument()
                                //            {
                                //                FileName = file.FileName,
                                //                DocumentType = fileId,
                                //                Base64 = file.GetBased64String(),
                                //                Remark = file.FileTypeName
                                //            });
                                //        }
                                //    }

                                //}


                                //foreach (var fg in model.UploadedFiles)
                                //{
                                //    foreach (var file in fg.Files)
                                //    {
                                //        var selectedFile = fileList.Where(o => o.Key.Contains(file.FileTypeCode) ).SingleOrDefault();
                                //        var fileId = !selectedFile.IsDefault() ? selectedFile.Value : defaultFileId;
                                //        documents.Add(new DOTDocument()
                                //        {
                                //            FileName = file.FileName,
                                //            DocumentType = fileId,
                                //            Base64 = file.GetBased64String(),
                                //            Remark = file.FileTypeName
                                //        });
                                //    }
                                //}
                                foreach (var fg in model.UploadedFiles)
                                {
                                    foreach (var file in fg.Files)
                                    {

                                        var fileTypeDesc = file.Extras.ContainsKey("FILETYPEDESC") ? file.Extras["FILETYPEDESC"].ToString() : string.Empty;
                                        var fileType = file.Extras.ContainsKey("FILETYPE") ? file.Extras["FILETYPE"].ToString() : string.Empty;
                                        var FILETYPEDESC = file.Extras.ContainsKey("FILETYPEDESC") ? file.Extras["FILETYPEDESC"].ToString() : string.Empty;
                                        if (string.IsNullOrEmpty(fileType))
                                        {
                                            var selectedFile = fileList.Where(o => o.Key.Contains(file.FileTypeCode)).SingleOrDefault();
                                            var fileId = !selectedFile.IsDefault() ? selectedFile.Value : defaultFileId;
                                            documents.Add(new DOTDocument()
                                            {
                                                FileName = file.FileName,
                                                DocumentType = fileId,
                                                Base64 = file.GetBased64String(),
                                                Remark = file.FileTypeName
                                            });
                                        }
                                        else
                                        {
                                            documents.Add(new DOTDocument()
                                            {
                                                FileName = file.FileName,
                                                DocumentType = fileType,
                                                Base64 = file.GetBased64String(),
                                                Remark = FILETYPEDESC
                                            });
                                        }


                                    }
                                }
                                jsonLog = JsonConvert.SerializeObject(post);
                                post.Documents = documents.ToArray();
                                jsonPost = JsonConvert.SerializeObject(post);

                                //var client2 = new RestClient("http://103.80.100.105:8080/api/SubmitRequestForm");
                                var client2 = new RestClient(DGA_WS_URL + "/ws" + regisUrl);
                                var request_ = new RestRequest(Method.POST);
                                request_.RequestFormat = DataFormat.Json;
                                request_.AddHeader("Token", Api.AccessToken);
                                request_.AddHeader("Consumer-Key", Api.ConsumerKey);
                                request_.AddHeader("Content-Type", "application/json");
                                request_.AddParameter("application/json", jsonPost, ParameterType.RequestBody);
                                IRestResponse resp = client2.Execute(request_);
                                if (resp.StatusCode == HttpStatusCode.OK)
                                {
                                    DotResponse DotRes = JsonConvert.DeserializeObject<DotResponse>(resp.Content);
                                    if (DotRes.IsSuccess == "true")
                                    {
                                        result.Success = true;
                                        result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, "", resp.StatusCode.ToString(), jsonLog);

                                        //                        using (System.IO.StreamWriter file =
                                        //new System.IO.StreamWriter(@"C:\xampp7\dotCreate", true))
                                        //                        {
                                        //                            file.WriteLine(jsonPost);
                                        //                        }
                                    }
                                    else
                                    {
                                        result.Success = false;
                                    }
                                }
                                else
                                {
                                    if (resp.StatusCode == HttpStatusCode.BadRequest)
                                    {
                                        DotErrorResponse DotErrRes = JsonConvert.DeserializeObject<DotErrorResponse>(resp.Content);
                                        throw new Exception(DotErrRes.Err);
                                    }
                                    result.Success = false;
                                    result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, result.Message, null, jsonLog, true);

                                }

                                /*var apiResponse = Api.Call(regisUrl, HttpVerb.POST, null, jsonPost, ContentType.ApplicationJson);
                                if (apiResponse != null && apiResponse.HasValues)
                                {
                                    if (!string.IsNullOrEmpty(apiResponse["Result"].ToString()))
                                    {
                                        result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, apiResponse.ToString(), apiResponse.ToString(), jsonPost);
                                        result.Success = true;
                                    }
                                    else
                                    {
                                        string msg = string.Format("[{0}]{1}", apiResponse.ToString(), apiResponse.ToString());
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, msg, apiResponse.ToString(), jsonPost, true);
                                        throw new Exception(msg);
                                    }
                                }
                                else
                                {
                                    string error = "Unable to request to " + regisUrl + ".";
                                    result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                    throw new Exception(error);
                                }*/
                            }
                        }
                        break;
                    //case AppsStage.UserUpdate:
                    case AppsStage.None:
                    case AppsStage.AgentUpdate:
                    case AppsStage.ApiUpdate:
                    default:
                        result.Success = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Exception = ex;
                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, result.Message, null, null, true);

                result.Success = false;
            }

            return result;
            #endregion
        }
    }

    public class DotResponse
    {

        public string IsSuccess { get; set; }
        public string ApplicationId { get; set; }
        public string RequestFormNo { get; set; }

    }
    public class DotErrorResponse
    {
        public string Err { get; set; }       

    }
}
