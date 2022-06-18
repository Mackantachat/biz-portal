using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Apps;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using EGA.WS;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using System.Web;
using static BizPortal.ViewModels.Apps.DOAStandard.DOAPlants;
namespace BizPortal.AppsHook
{
    public class DOAOrganicPlantCancelAppHook : SingleFormRendererAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 0;
        }

        public override bool IsEnabledChat() => false;

        public override bool InvokeSingleForm(Guid trid, string currentSectionGroup, ref SingleFormRequestViewModel model)
        {
            var result = false;
            var darftData = SingleFormRequestEntity.Get(model.IdentityID);

            if (currentSectionGroup == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH")
            {
                // Clear section data
                var singleformReq = new SingleFormRequestEntity();
                singleformReq.DeleteSections(model.IdentityID, "APP_ORGANIC_PLANT_PRODUCTION_CANCEL", new string[] { "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION", "" });


            }
            result = true;

            return result;
        }

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = false;
            try
            {
                switch (stage)
                {
                    case AppsStage.UserUpdate:
                    case AppsStage.UserCreate:
                        //var post = string.Empty; // Model data
                        string requestval = string.Empty;
                        string requestvalTitle = string.Empty;
                        switch (model.IdentityType)
                        {
                            case UserTypeEnum.Citizen:

                                requestval = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                                requestvalTitle = requestval == "REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER" ? "ขออนุญาตเองโดยเจ้าของกิจการ" : "มอบอำนาจให้ผู้อื่นดำเนินการแทน";

                                break;
                            case UserTypeEnum.Juristic:

                                requestval = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                                requestvalTitle = requestval == "REQUESTOR_INFORMATION__REQUEST_TYPE_BOARD" ? "ขออนุญาตเองโดยเจ้าของกิจการ" : "มอบอำนาจให้ผู้อื่นดำเนินการแทน";

                                break;
                            default:
                                break;
                        }
                        //string requestval = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                        BizPortalInfo bizinfo = new BizPortalInfo();
                        bizinfo.ApplicationID = model.ApplicationID;
                        bizinfo.ApplicationRequestID = model.ApplicationRequestID.ToString();
                        bizinfo.IdentityID = model.IdentityID;
                        bizinfo.IdentityType = model.IdentityType.ToString();
                        bizinfo.IdentityName = model.IdentityName;
                        bizinfo.RequsetDateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'", new CultureInfo("en"));
                        bizinfo.RequsetTypeValue = requestval;
                        bizinfo.RequsetTypeName = requestvalTitle;//requestval == "REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER" ? "ขออนุญาตเองโดยเจ้าของกิจการ" : "มอบอำนาจให้ผู้อื่นดำเนินการแทน";
                        bizinfo.RequestAt = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("INFORMATION_HEADER__REQUEST_AT");
                        bizinfo.ApplicationRequestNumber = model.ApplicationRequestNumber;
                        string Consumer_Key = ConfigurationManager.AppSettings["DOA_CONSUMERKEY"];
                        string certificateID = string.Empty;
                        certificateID = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION").ThenGetStringData("AJAX_DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION_LICENSE_RENEW");
                        string Remark = string.Empty;
                        Remark = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_INFO_SECTION_1").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_INFO_SECTION_1_REASON_CANCEL");

                        List<OrganicSectionCancel> ListOrganicSectionCancel = new List<OrganicSectionCancel>();
                        int countitem = Convert.ToInt16(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_TOTAL"));
                        for (int i = 0; i < countitem; i++)
                        {

                            OrganicSectionCancel GardenItems = new OrganicSectionCancel();
                            GardenItems.plantAndProduct = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_ORGANIC_TYPE_" + i);
                            GardenItems.framCode = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_AREA_ID_" + i);
                            GardenItems.licenceCode = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_LICENCE_ID_" + i);
                            GardenItems.framAddressNo = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i);
                            GardenItems.framMooNo = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_MOO_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i);
                            GardenItems.framSoi = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_SOI_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i);
                            GardenItems.framRoad = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_ROAD_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i);
                            GardenItems.framBuilding = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_BUILDING_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i);
                            GardenItems.framRoomNo = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_ROOMNO_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i);
                            GardenItems.framFloorNo = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_FLOOR_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i);
                            GardenItems.framProvinceCode = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i);
                            GardenItems.framProvinceName = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_TEXT_" + i);
                            GardenItems.framAmphurCode = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i);
                            GardenItems.framAmphurName = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_TEXT_" + i);
                            GardenItems.framTambonCode = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i);
                            GardenItems.framTambonName = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_TEXT_" + i);
                            GardenItems.framPostCode = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_POSTCODE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i);
                            GardenItems.framTel = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i);
                            GardenItems.framTelExt = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i);
                            GardenItems.framFax = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").ThenGetStringData("ADDRESS_FAX_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i);
                            ListOrganicSectionCancel.Add(GardenItems);
                        }

                        List<DOAFileMetaData> listdocuments = new List<DOAFileMetaData>();
                        var up = model.UploadedFiles.ToArray();

                        if (stage == AppsStage.UserUpdate)
                        {
                            up = model.UploadedFiles.Where(x => x.Description == "REQUESTED_FILE").ToArray().OrderByDescending(x => x.CreatedDate).Take(1).ToArray();
                        }
                        foreach (var upload in up)
                        {
                            foreach (var f in upload.Files)
                            {
                                var fileMetaModel = new FileMetadata
                                {
                                    FileID = f.FileID,
                                    FileName = f.FileName,
                                    ContentType = f.ContentType,
                                    Extras = new Dictionary<string, string>()
                                };

                                if (f.Extras != null)
                                {
                                    foreach (var extra in f.Extras)
                                    {
                                        fileMetaModel.Extras.Add(extra.Key, extra.Value != null ? extra.Value.ToString() : string.Empty);
                                    }
                                }

                                string Description = "";

                                if (f.Extras.ContainsKey("DISPLAYNAME"))
                                {
                                    Description = f.Extras["DISPLAYNAME"].ToString();
                                }

                                var fileTypeCode = (upload.Description == "REQUESTED_FILE" && f.Extras.ContainsKey("FILETYPENAME")) ? f.Extras["FILETYPENAME"].DefaultString() : f.FileTypeCode;

                                var file = new DOAFileMetaData
                                {
                                    Name = fileTypeCode,
                                    Content = Convert.ToBase64String(fileMetaModel.GetBytes()),
                                    FileName = f.FileName,
                                    ContentType = f.ContentType,
                                    Description = Description == "" ? fileTypeCode : Description,
                                };
                                listdocuments.Add(file);
                            }
                        }
                        var requestdocumnt = GetOrgPdfFormContent(request, HttpContext.Current.Server.MapPath);
                        var requestdocuments = new List<DOAFileMetaData>
                        {
                            new DOAFileMetaData
                            {
                                Name = "REQUEST_DOCUMENT",
                                FileName = "ใบคำร้อง.pdf",
                                Content = Convert.ToBase64String(requestdocumnt),
                                ContentType = "application/pdf",
                                Description = "ใบคำร้อง"
                            }
                        };
                        var postReNew = new OrganicDetailCancel
                        {
                            ConsumerKey = Consumer_Key,
                            certificateID = certificateID,
                            idcard = model.IdentityID.ToString(),
                            garden_document = listdocuments,
                            garden_document_request = requestdocuments,
                            Remark = Remark,
                            cancelDetail = ListOrganicSectionCancel,
                            bizportal_info = bizinfo
                        };
                        string regisUrl = ConfigurationManager.AppSettings["DOA_REQUEST_FARMERCANCEL_WS_URL"];
                        var jsonPost = JsonConvert.SerializeObject(postReNew,
                        Newtonsoft.Json.Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
                        string DGA_WS_URL = ConfigurationManager.AppSettings["DGA_WS_URL"];
                        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(DGA_WS_URL + "/ws" + regisUrl);
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";
                        httpWebRequest.Headers.Add("Consumer-Key", Api.ConsumerKey);
                        httpWebRequest.Headers.Add("Token", Api.AccessToken);
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            streamWriter.Write(jsonPost);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }

                        using (var response = httpWebRequest.GetResponse() as HttpWebResponse)
                        {
                            if (httpWebRequest.HaveResponse && response != null)
                            {
                                using (var reader = new StreamReader(response.GetResponseStream()))
                                {
                                    var res = reader.ReadToEnd();
                                    if (response.StatusCode == HttpStatusCode.Created)
                                    {
                                        result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, "", response.StatusCode.ToString(), jsonPost);
                                        result.Success = true;
                                    }
                                    else
                                    {
                                        result.Message = response.StatusDescription;
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, result.Message, response.StatusCode.ToString(), jsonPost);
                                        result.Success = false;
                                    }
                                }
                            }
                            else
                            {
                                result.Message = response.StatusDescription;
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, result.Message, response.StatusCode.ToString(), jsonPost);
                                result.Success = false;
                            }
                        }
                        /*Api.OnCheckingApplicationError += (ex) =>
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

                        var response = Api.Call(regisUrl, HttpVerb.POST, null, jsonPost, ContentType.ApplicationJson);

                        if (response.HasValues)
                        {
                            result.Success = true;
                        }
                        else
                        {
                            result.Success = false;
                        }*/

                        break;
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
                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, result.Message, ex.ToString());
                result.Success = false;
            }
            return result;
        }

        public override bool HasOrgPdfForm
        {
            get
            {
                return true;
            }
        }

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            string src = serverMapPathFunc("~/Uploads/apps/ag/AG_CancelForm.pdf");
            List<PDFFieldValue> model = new List<PDFFieldValue>();

            model.Add(new PDFFieldValue() { FieldName = "txtDay", Value = req.CreatedDate.ToString("dd", CultureInfo.CreateSpecificCulture("th-TH")) });
            model.Add(new PDFFieldValue() { FieldName = "txtMonth", Value = req.CreatedDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("th-TH")) });
            model.Add(new PDFFieldValue() { FieldName = "txtYear", Value = req.CreatedDate.ToString("yyyy", CultureInfo.CreateSpecificCulture("th-TH")) });

            var generalInfo = req.Data["GENERAL_INFORMATION"].Data;
            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                string personID = string.Empty;
                int i = 1;
                model.Add(new PDFFieldValue() { FieldName = "txtJusristict", Value = "------------------------" });
                model.Add(new PDFFieldValue() { FieldName = "txtFullName", Value = string.Format("{0} {1} {2}", generalInfo.TryGetString("DROPDOWN_CITIZEN_TITLE_TEXT", ""), generalInfo.TryGetString("CITIZEN_NAME", ""), generalInfo.TryGetString("CITIZEN_LASTNAME", "")) });
                personID = generalInfo["IDENTITY_ID"].ToString();
                foreach (char c in personID)
                {
                    model.Add(new PDFFieldValue() { FieldName = "txtID1_" + i, Value = c.ToString() });
                    i++;
                }

                var addInfo = req.Data["CITIZEN_ADDRESS_INFORMATION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "txtVillNo", Value = addInfo.TryGetString("ADDRESS_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtVillageName", Value = "-" });
                model.Add(new PDFFieldValue() { FieldName = "txtLane", Value = addInfo.TryGetString("ADDRESS_SOI_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtRoad", Value = addInfo.TryGetString("ADDRESS_ROAD_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtMoo", Value = addInfo.TryGetString("ADDRESS_MOO_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtDistrict", Value = addInfo.TryGetString("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtAmphur", Value = addInfo.TryGetString("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtProvince", Value = addInfo.TryGetString("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtPostCode", Value = addInfo.TryGetString("ADDRESS_POSTCODE_CITIZEN_ADDRESS", "") });
                if (!string.IsNullOrEmpty(addInfo.TryGetString("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS", "")))
                {
                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtTel",
                        Value = addInfo.TryGetString("ADDRESS_TELEPHONE_CITIZEN_ADDRESS", "").ToString()
                        + " ต่อ. " + addInfo.TryGetString("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS", "").ToString()
                    });
                }
                else
                {
                    model.Add(new PDFFieldValue() { FieldName = "txtTel", Value = addInfo.TryGetString("ADDRESS_TELEPHONE_CITIZEN_ADDRESS", "") });
                }
                model.Add(new PDFFieldValue() { FieldName = "txtFax", Value = addInfo.TryGetString("ADDRESS_FAX_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtMobile", Value = "-" });
                model.Add(new PDFFieldValue() { FieldName = "txtEmail", Value = addInfo.TryGetString("CITIZEN_EMAIL", "") });
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                model.Add(new PDFFieldValue() { FieldName = "txtFullName", Value = generalInfo.TryGetString("COMPANY_NAME_TH", "") });
                int i = 1;
                foreach (char c in req.IdentityID)
                {
                    model.Add(new PDFFieldValue() { FieldName = "txtID2_" + i, Value = c.ToString() });
                    i++;
                }

                var addrInfo = req.Data["JURISTIC_ADDRESS_INFORMATION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "txtVillNo", Value = addrInfo.TryGetString("ADDRESS_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtVillageName", Value = "-" });
                model.Add(new PDFFieldValue() { FieldName = "txtLane", Value = addrInfo.TryGetString("ADDRESS_SOI_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtRoad", Value = addrInfo.TryGetString("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtMoo", Value = addrInfo.TryGetString("ADDRESS_MOO_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtDistrict", Value = addrInfo.TryGetString("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtAmphur", Value = addrInfo.TryGetString("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtProvince", Value = addrInfo.TryGetString("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtPostCode", Value = addrInfo.TryGetString("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS", "") });
                if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS", "")))
                {
                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtTel",
                        Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS", "").ToString()
                        + " ต่อ. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS", "").ToString()
                    });
                }
                else
                {
                    model.Add(new PDFFieldValue() { FieldName = "txtTel", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS", "") });
                }
                model.Add(new PDFFieldValue() { FieldName = "txtFax", Value = addrInfo.TryGetString("ADDRESS_FAX_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtMobile", Value = addrInfo.TryGetString("ADDRESS_MOBILE_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtEmail", Value = addrInfo.TryGetString("ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS", "") });
            }

            if (req.Data.ContainsKey("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION"))
            {
                var data = req.Data["APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION"].Data;
                int total = int.Parse(data.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_TOTAL", "0"));

                for (int i = 0; i < total; i++)
                {
                    model.Add(new PDFFieldValue() { FieldName = "txtPlantNameArr_" + (i + 1), Value = data.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_ORGANIC_TYPE_" + i, "") });
                    model.Add(new PDFFieldValue() { FieldName = "txtAreaCodeArr_" + (i + 1), Value = data.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_AREA_ID_" + i, "") });
                    model.Add(new PDFFieldValue() { FieldName = "txtLicenseArr_" + (i + 1), Value = data.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_LICENCE_ID_" + i, "") });

                    string addr = data.TryGetString("ADDRESS_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i, "");
                    if (!string.IsNullOrEmpty(data.TryGetString("ADDRESS_MOO_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i))) addr += " หมู่ " + data.TryGetString("ADDRESS_MOO_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i, "");
                    if (!string.IsNullOrEmpty(data.TryGetString("ADDRESS_ROAD_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i))) addr += " ถนน" + data.TryGetString("ADDRESS_ROAD_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i, "");
                    if (!string.IsNullOrEmpty(data.TryGetString("ADDRESS_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_TEXT_" + i))) addr += ", " + data.TryGetString("ADDRESS_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_TEXT_" + i, "");
                    if (!string.IsNullOrEmpty(data.TryGetString("ADDRESS_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_TEXT_" + i))) addr += ", " + data.TryGetString("ADDRESS_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_TEXT_" + i, "");
                    if (!string.IsNullOrEmpty(data.TryGetString("ADDRESS_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_TEXT_" + i))) addr += ", " + data.TryGetString("ADDRESS_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_TEXT_" + i, "");
                    if (!string.IsNullOrEmpty(data.TryGetString("ADDRESS_POSTCODE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i))) addr += ", " + data.TryGetString("ADDRESS_POSTCODE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION_" + i, "");

                    model.Add(new PDFFieldValue() { FieldName = "txtLocationArr_" + (i + 1), Value = addr, FontSize = 9 });
                }
            }

            if (req.Data.ContainsKey("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_INFO_SECTION_1"))
            {
                model.Add(new PDFFieldValue() { FieldName = "txtReason", Value = req.Data["APP_ORGANIC_PLANT_PRODUCTION_CANCEL_INFO_SECTION_1"].Data.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_INFO_SECTION_1_REASON_CANCEL", "") });
            }

            PDFConfig cfg = new PDFConfig() { FontName = "THSarabunNew.ttf", FontSize = 12 };
            var bytes = ApplyPDFFieldValues(src, model, cfg);
            return bytes;
        }


    }
}


