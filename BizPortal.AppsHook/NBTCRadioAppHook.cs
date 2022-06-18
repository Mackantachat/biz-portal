using System;
using System.Linq;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.V2;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using BizPortal.Utils.Helpers;
using System.Configuration;
using Newtonsoft.Json;
using EGA.WS;
using static BizPortal.ViewModels.Apps.NBTCStandard.NBTCRequest;
using System.Net;
using System.IO;
using BizPortal.Utils;
using System.Globalization;
using System.Security.Claims;
namespace BizPortal.AppsHook
{
    public class NBTCRadioAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            decimal fee = 0;
            var sec = sectionData.Where(x => x.SectionName == "APP_RADIO_SECTION").FirstOrDefault();
            if (sec != null)
            {
                if (sec.FormData.TryGetString("APP_RADIO_TYPE_OPTION", "") == "1")
                {
                    fee += 1070;
                }
                else if (sec.FormData.TryGetString("APP_RADIO_TYPE_OPTION", "") == "2")
                {
                    fee += 535;
                }
            }

            return fee;
        }

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            string src = serverMapPathFunc("~/Uploads/apps/nbct/28.1_nbct_radio.pdf");
            PDFFieldValue field;
            List<PDFFieldValue> model = new List<PDFFieldValue>();

            //string content = Newtonsoft.Json.JsonConvert.SerializeObject(req);

            #region Section 1

            if (!(req.Data.ContainsKey("APP_RADIO_SECTION") && req.Data["APP_RADIO_SECTION"].Data.TryGetString("APP_RADIO_TYPE_OPTION", "") == "1"))
                model.Add(new PDFFieldValue() { FieldName = "Opt1Sell", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(req.Data.ContainsKey("APP_RADIO_SECTION") && req.Data["APP_RADIO_SECTION"].Data.TryGetString("APP_RADIO_TYPE_OPTION", "") == "2"))
                model.Add(new PDFFieldValue() { FieldName = "Opt1Fix", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            model.Add(new PDFFieldValue() { FieldName = "Opt1Renew", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });


            var generalInfo = req.Data["GENERAL_INFORMATION"].Data;

            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                model.Add(new PDFFieldValue() { FieldName = "PersonFirstName", Value = generalInfo.TryGetString("CITIZEN_NAME", "") });
                model.Add(new PDFFieldValue() { FieldName = "PersonLastName", Value = generalInfo.TryGetString("CITIZEN_LASTNAME", "") });
                model.Add(new PDFFieldValue() { FieldName = "Nationality", Value = generalInfo.TryGetString("DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT", "") });

                model.Add(new PDFFieldValue() { FieldName = "PersonBirthDate", Value = generalInfo.TryGetString("BIRTH_DATE", "") });

                string identity = generalInfo["IDENTITY_ID"];
                FillIdentityDigits(model, identity, "Identity", 13);

                var storeInfo = req.Data["INFORMATION_STORE"].Data;
                if (storeInfo.TryGetString("INFORMATION_STORE_COMMERCE_REGISTRATION_CITIZEN_OPTION", "") == "INFORMATION_STORE_COMMERCE_REGISTRATION_CITIZEN_OPTION__YES")
                {
                    // ใช่ ฉันได้จดทะเบียนพาณิชย์
                    var addrInfo = req.Data["INFORMATION_STORE"].Data;
                    model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo.TryGetString("ADDRESS_INFORMATION_STORE__ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo.TryGetString("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo.TryGetString("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo.TryGetString("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo.TryGetString("ADDRESS_MOO_INFORMATION_STORE__ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo.TryGetString("ADDRESS_SOI_INFORMATION_STORE__ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo.TryGetString("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo.TryGetString("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo.TryGetString("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo.TryGetString("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo.TryGetString("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS", "") });

                    model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS", "") });

                    model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS", "") });
                    if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", "")))
                        model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", "");

                    model.Add(new PDFFieldValue() { FieldName = "Email", Value = addrInfo.TryGetString("INFORMATION_STORE_EMAIL", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Fax", Value = addrInfo.TryGetString("ADDRESS_FAX_INFORMATION_STORE__ADDRESS", "") });
                }
                else
                {
                    // ไม่ใช่ ฉันไม่ได้จดทะเบียนพาณิชย์
                    var addrInfo = req.Data["CITIZEN_ADDRESS_INFORMATION"].Data;
                    model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo.TryGetString("ADDRESS_CITIZEN_ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo.TryGetString("ADDRESS_BUILDING_CITIZEN_ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo.TryGetString("ADDRESS_ROOMNO_CITIZEN_ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo.TryGetString("ADDRESS_FLOOR_CITIZEN_ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo.TryGetString("ADDRESS_MOO_CITIZEN_ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo.TryGetString("ADDRESS_SOI_CITIZEN_ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo.TryGetString("ADDRESS_ROAD_CITIZEN_ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo.TryGetString("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo.TryGetString("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo.TryGetString("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo.TryGetString("ADDRESS_POSTCODE_CITIZEN_ADDRESS", "") });

                    model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_CITIZEN_ADDRESS", "") });
                    if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS", "")))
                        model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS", "");

                    model.Add(new PDFFieldValue() { FieldName = "Fax", Value = addrInfo.TryGetString("ADDRESS_FAX_CITIZEN_ADDRESS", "") });
                }
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                model.Add(new PDFFieldValue() { FieldName = "JuName", Value = generalInfo.TryGetString("COMPANY_NAME_TH", "") });
                model.Add(new PDFFieldValue() { FieldName = "JuID", Value = generalInfo.TryGetString("IDENTITY_ID", "") });
                model.Add(new PDFFieldValue() { FieldName = "JuRegisterDate", Value = generalInfo.TryGetString("REGISTER_DATE", "") });

                var addrInfo = req.Data["INFORMATION_STORE"].Data;
                model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo.TryGetString("ADDRESS_INFORMATION_STORE__ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo.TryGetString("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo.TryGetString("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo.TryGetString("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo.TryGetString("ADDRESS_MOO_INFORMATION_STORE__ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo.TryGetString("ADDRESS_SOI_INFORMATION_STORE__ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo.TryGetString("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo.TryGetString("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo.TryGetString("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo.TryGetString("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo.TryGetString("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS", "") });

                model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS", "") });
                if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", "")))
                    model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", "");

                model.Add(new PDFFieldValue() { FieldName = "Email", Value = addrInfo.TryGetString("INFORMATION_STORE_EMAIL", "") });
                model.Add(new PDFFieldValue() { FieldName = "Fax", Value = addrInfo.TryGetString("ADDRESS_FAX_INFORMATION_STORE__ADDRESS", "") });
            }

            #endregion

            #region Section 2

            if (req.Data.ContainsKey("INFORMATION_STORE"))
            {
                var storeInfo = req.Data["INFORMATION_STORE"].Data;
                model.Add(new PDFFieldValue() { FieldName = "StoreName", Value = storeInfo.TryGetString("INFORMATION_STORE_NAME_TH", "") });
            }

            // Document
            model.Add(new PDFFieldValue() { FieldName = "OptRenew", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            if (!(req.Data["INFORMATION_STORE"].Data.TryGetString("INFORMATION_STORE_COMMERCE_REGISTRATION_CITIZEN_OPTION", "") == "INFORMATION_STORE_COMMERCE_REGISTRATION_CITIZEN_OPTION__YES"))
                model.Add(new PDFFieldValue() { FieldName = "OptDocCommerce", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(req.IdentityType == UserTypeEnum.Juristic))
                model.Add(new PDFFieldValue() { FieldName = "OptDocJuristic", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });


            // Representative
            if (!(req.Data.ContainsKey("REQUESTOR_INFORMATION__HEADER")
                    && ((req.IdentityType == UserTypeEnum.Citizen && req.Data["REQUESTOR_INFORMATION__HEADER"].Data.TryGetString("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION", "") == "REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE"))
                        || (req.IdentityType == UserTypeEnum.Juristic && req.Data["REQUESTOR_INFORMATION__HEADER"].Data.TryGetString("REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION", "") == "REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE")))
            {
                model.Add(new PDFFieldValue() { FieldName = "OptDocRep", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
                model.Add(new PDFFieldValue() { FieldName = "OptDocOwnerID", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
                model.Add(new PDFFieldValue() { FieldName = "OptDocRepID", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            #endregion

            PDFConfig cfg = new PDFConfig() { FontName = "Angsima.ttf", FontSize = 12 };
            var bytes = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, model, cfg);

            return bytes;
        }

        public override bool HasOrgPdfForm
        {
            get
            {
                return true;
            }
        }

        public override bool IsEnabledChat()
        {
            return true;
        }

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            // หลังจากทำ Biz-api เสร็จแล้วจะต้องนำโค้ดชุดนี้ออก
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

            #region biz-api code
            var identity = System.Threading.Thread.CurrentPrincipal.Identity;
            identity.GetAddress();

            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = false;
            //var postinformations =  ;
            var jsonPost = "{}";
            string REF_CASE_ID = string.Empty;
            string regisUrl = string.Empty;
            string API_URL = string.Empty;
            string URL = string.Empty;
            string DGA_WS_URL = ConfigurationManager.AppSettings["DGA_WS_URL"];
            string NBTC_SECRET_KEY = ConfigurationManager.AppSettings["NBTC_SECRET_KEY"];
            var openIdProfile = System.Threading.Thread.CurrentPrincipal.Identity.GetOpenIdProfile();
         
            try
            {
                var contact = new Contact()
                {
                    AddressNo = "",
                    Building = "",
                    RoomNo = "",
                    Floor = "",
                    VilageName = "",
                    Moo = "",
                    Soi = "",
                    Road = "",
                    SubDistrict = "",
                    District = "",
                    Province = "",
                    Postcode = "",
                    Phone = "",
                    Fax = "",
                    MobilePhone = "",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                };

                var OPENID_AUTHEN = new OPENIDAUTHEN()
                {
                   // eAuthID = openIdProfile.UserID,
                   // eAuthUserName = openIdProfile.UserName,
                    //Title = "",
                    //FirstName = "",
                    //LastName = "",
                    //DateOfBirth = "",
                    //Gender = "",
                    NationalID = openIdProfile.IdentityID,
                    NationalVerification = true,
                    //Phone = "",
                    //Email = "",
                    Contact = contact
                };
                var INFORMATION_STORE = new STOREINFORMATION()
                {
                    USE_CITIZEN_ADDRESS = model.Data.TryGetData("INFORMATION_STORE").ThenGetBooleanData("INFORMATION_STORE__USE_CITIZEN_ADDRESS_INFORMATION_STORE__USE_CITIZEN_ADDRESS__TRUE"),
                    STORE_NAME_TH = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH"),
                    STORE_NAME_EN = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_EN"),
                    STORE_BUILDING_TYPE = NBTCUtility.GetAddressType().FirstOrDefault(x => x.Value == model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION")).Key,
                    STORE_BUILDING_RENTING_OWNER_TYPE = NBTCUtility.GetOwnerType().FirstOrDefault(x => x.Value == model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION")).Key,
                    ADDRESS_NUMBER = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS"),
                    ADDRESS_MOO = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS"),
                    ADDRESS_SOI = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS"),
                    ADDRESS_BUILDING = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS"),
                    ADDRESS_ROOMNO = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS"),
                    ADDRESS_FLOOR = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS"),
                    ADDRESS_ROAD = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"),
                    ADDRESS_PROVINCE = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"),
                    ADDRESS_AMPHUR = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"),
                    ADDRESS_TUMBOL = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"),
                    ADDRESS_POSTCODE = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"),
                    ADDRESS_TELEPHONE = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                    ADDRESS_TELEPHONE_EXT = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"),
                    ADDRESS_FAX = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS"),
                    ADDRESS_EMAIL = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS"),
                    ADDRESS_MOBILE = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOBILE_INFORMATION_STORE__ADDRESS"),
                    ADDRESS_LAT = model.Data.TryGetData("INFORMATION_STORE").ThenGetDoubleData("ADDRESS_LAT_INFORMATION_STORE__ADDRESS"),
                    ADDRESS_LNG = model.Data.TryGetData("INFORMATION_STORE").ThenGetDoubleData("ADDRESS_LNG_INFORMATION_STORE__ADDRESS"),
                };
                var chkAuthor = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");

                var attachList = new List<Attachment>();
                //int file_index = 1;
                foreach (FileGroup group in model.UploadedFiles)
                {
                    foreach (var item in group.Files)
                    {
                        var description = item.Extras.ContainsKey("FILETYPENAME") ? item.Extras["FILETYPENAME"].ToString() : string.Empty;

                        string fileType = NBTCUtility.GetFileTypeNBTCRef().FirstOrDefault(x => item.FileTypeCode.Contains(x.Key)).Value;
                        var attach = new Attachment()
                        {
                            Base64String = item.GetBased64String(),
                            ContentType = item.ContentType,
                            // description = description,
                            FileName = item.FileName,
                            // fileSize = item.FileSize.ToString(),
                            FileType = fileType,
                            FileID = item.FileID,
                            //seqNo = file_index++.ToString()
                        };
                        attachList.Add(attach);
                    }

                }
                switch (stage)
                {
                    case AppsStage.UserUpdate:
                        if (model.Status == ApplicationStatusV2Enum.CHECK || model.Status == ApplicationStatusV2Enum.PENDING)
                        {
                            #region [Attachs]
                            var attachListUpdate = new List<Attachment>();
                            // int file_index = 1;
                            var requestedFiles = model.UploadedFiles.Where(o => o.Description == "REQUESTED_FILE").OrderByDescending(o => o.CreatedDate).FirstOrDefault();
                            if (requestedFiles != null && requestedFiles.Files != null && requestedFiles.Files.Count > 0)
                            {
                                foreach (var item in requestedFiles.Files)
                                {

                                    // var seqNo = item.Extras.ContainsKey("SEQNO") ? item.Extras["SEQNO"].ToString() : string.Empty;
                                    var fileTypeDesc = item.Extras.ContainsKey("FILETYPEDESC") ? item.Extras["FILETYPEDESC"].ToString() : string.Empty;
                                    var fileType = item.Extras.ContainsKey("FILETYPE") ? item.Extras["FILETYPE"].ToString() : "16";
                                   // var fileTypeCode = DBDUtility.GetFileTypeRef().FirstOrDefault(x => x.Key.ToString() == fileType).Value.ToString();
                                   // var description = ResourceHelper.GetResourceWordWithDefault(fileTypeCode, "Apps_SingleForm_Filelist", fileTypeCode).Replace(": ({0} {1} {2})", "");
                                   // var fileIdOld = item.Extras.ContainsKey("FILEID") ? item.Extras["FILEID"].ToString() : string.Empty;
                                    //string fileType = DBDUtility.GetFileTypeRef().FirstOrDefault(x => item.FileTypeCode.Contains(x.Value)).Key.ToString();
                                    var attach = new Attachment()
                                    {
                                        Base64String = item.GetBased64String(),
                                        ContentType = item.ContentType,
                                        //description = fileTypeDesc == string.Empty ? description : fileTypeDesc,
                                        FileName = item.FileName,
                                        //fileSize = item.FileSize.ToString(),
                                        FileType = fileType.ToString(),//fileType,
                                        FileID = item.FileID,
                                        //seqNo = seqNo.ToString()
                                    };
                                    attachListUpdate.Add(attach);
                                }

                            }
                           


                            #endregion
                            var post = new NbtcRadio()
                            {
                                BizReqNo = model.ApplicationRequestID.ToString(),
                                BizReqDateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'", new CultureInfo("en")),//DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                                ReqNo = model.ApplicationRequestNumber,
                                ApplicationID = model.ApplicationID,
                                REF_CASE_ID = model.ApplicationRequestID.ToString(),
                                Attachments = attachListUpdate,
                                Remark = model.Remark.ToString()
                            };
                            jsonPost = JsonConvert.SerializeObject(post,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                            regisUrl = ConfigurationManager.AppSettings["NBTC_WS_URL_FILES_REQUEST"];
                            API_URL = ConfigurationManager.AppSettings["NBTC_URL_FILES_REQUEST"];
                          

                            if (ConfigurationManager.AppSettings["NBTC_USE_WS"].ToLower() == "false")
                            {
                                URL = API_URL;
                                ServicePointManager.Expect100Continue = true;
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            }
                            else
                            {
                                URL = DGA_WS_URL + "/ws" + regisUrl;
                            }
                            //string xx = DGA_WS_URL + "/ws" + regisUrl;
                            //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://164.115.9.184:90/BizPortalServicesUAT/lic/LicFrm0120");
                            //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(DGA_WS_URL + "/ws" + regisUrl);
                            HttpWebRequest httpWebRequestFile = (HttpWebRequest)WebRequest.Create(URL);
                            httpWebRequestFile.ContentType = "application/json";
                            httpWebRequestFile.Method = "POST";
                            httpWebRequestFile.Headers.Add("Consumer-Key", Api.ConsumerKey);
                            httpWebRequestFile.Headers.Add("Token", Api.AccessToken);
                            httpWebRequestFile.Headers.Add("SecretKey", NBTC_SECRET_KEY);
                            using (var streamWriter = new StreamWriter(httpWebRequestFile.GetRequestStream()))
                            {
                                streamWriter.Write(jsonPost);
                                streamWriter.Flush();
                                streamWriter.Close();
                            }

                            using (var response_ = httpWebRequestFile.GetResponse() as HttpWebResponse)
                            {
                                if (httpWebRequestFile.HaveResponse && response_ != null)
                                {
                                    using (var reader = new StreamReader(response_.GetResponseStream()))
                                    {
                                        var res = reader.ReadToEnd();
                                        if (response_.StatusCode == HttpStatusCode.OK)
                                        {
                                            result.Success = true;
                                            //result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, "", response_.StatusCode.ToString(), jsonPost);
                                        }
                                        else
                                        {
                                            result.Success = false;
                                           // result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, response_.StatusCode.ToString(), null, jsonPost, true);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            result.Success = true;
                        }
                        break;
                    case AppsStage.UserCreate:
                        switch (model.IdentityType)
                        {
                            case UserTypeEnum.Citizen:
                                var CITIZEN_ADDRESS = new CITIZENADDRESS()
                                {
                                    ADDRESS_NUMBER = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_CITIZEN_ADDRESS"),
                                    ADDRESS_MOO = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_CITIZEN_ADDRESS"),
                                    ADDRESS_SOI = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS"),
                                    ADDRESS_BUILDING = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_CITIZEN_ADDRESS"),
                                    ADDRESS_ROOMNO = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROOMNO_CITIZEN_ADDRESS"),
                                    ADDRESS_FLOOR = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_CITIZEN_ADDRESS"),
                                    ADDRESS_ROAD = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS"),
                                    ADDRESS_PROVINCE = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT"),
                                    ADDRESS_AMPHUR = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT"),
                                    ADDRESS_TUMBOL = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT"),
                                    ADDRESS_POSTCODE = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_CITIZEN_ADDRESS"),
                                    ADDRESS_TELEPHONE = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_CITIZEN_ADDRESS"),
                                    ADDRESS_TELEPHONE_EXT = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS"),
                                    ADDRESS_FAX = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_CITIZEN_ADDRESS"),
                                    ADDRESS_EMAIL = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_EMAIL_CITIZEN_ADDRESS"),
                                    ADDRESS_MOBILE = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOBILE_CITIZEN_ADDRESS"),
                                    //ADDRESS_LAT = 0,
                                    //ADDRESS_LNG = 0
                                };
                                var CITIZEN_INFORMATION = new CITIZENINFORMATION()
                                {
                                    CITIZEN_TITLE = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_TITLE_TEXT"),
                                    CITIZEN_NAME = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_NAME"),
                                    CITIZEN_LASTNAME = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME"),
                                    CITIZEN_NATIONALITY = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT"),
                                    CITIZEN_IDENTITY_ID = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("IDENTITY_ID"),
                                    CITIZEN_BIRTH_DATE = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetDateStringData("BIRTH_DATE"),
                                    CITIZEN_GENERAL_EMAIL = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL"),
                                    CITIZEN_ADDRESS = CITIZEN_ADDRESS
                                };
                                OPENID_AUTHEN.Title = CITIZEN_INFORMATION.CITIZEN_TITLE;
                                OPENID_AUTHEN.FirstName = CITIZEN_INFORMATION.CITIZEN_NAME;
                                OPENID_AUTHEN.LastName = CITIZEN_INFORMATION.CITIZEN_LASTNAME;
                                //OPENID_AUTHEN.DateOfBirth = CITIZEN_INFORMATION.CITIZEN_BIRTH_DATE;
                                //OPENID_AUTHEN.NationalID = CITIZEN_INFORMATION.CITIZEN_IDENTITY_ID;
                                OPENID_AUTHEN.Email = CITIZEN_INFORMATION.CITIZEN_GENERAL_EMAIL;
                               

                                var postcitizen = new NbtcCitizen()
                                {
                                    BizReqNo = model.ApplicationRequestID.ToString(),
                                    BizReqDateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'", new CultureInfo("en")),//DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                                    ReqNo = model.ApplicationRequestNumber,
                                    ApplicationID = model.ApplicationID,
                                    REF_CASE_ID = model.ApplicationRequestID.ToString(),
                                    OPENID_AUTHEN = OPENID_AUTHEN,
                                    CITIZEN_INFORMATION = CITIZEN_INFORMATION,
                                    REQUESTOR_TYPE = chkAuthor != "REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER" ? 0 : 1,
                                    STORE_INFORMATION = INFORMATION_STORE,
                                    STORE_COMMERCE_REGISTRATION = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_COMMERCE_REGISTRATION_CITIZEN_OPTION") == "INFORMATION_STORE_COMMERCE_REGISTRATION_CITIZEN_OPTION__YES" ? true : false,
                                    REQUEST_TYPE = model.Data.TryGetData("APP_RADIO_SECTION").ThenGetIntData("APP_RADIO_TYPE_OPTION"),
                                    Attachments = attachList
                                };
                                jsonPost = JsonConvert.SerializeObject(postcitizen,
                                Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                });
                                regisUrl = ConfigurationManager.AppSettings["NBTC_WS_URL_CITIZEN_REQUEST"];
                                API_URL = ConfigurationManager.AppSettings["NBTC_URL_CITIZEN_REQUEST"];
                                break;
                            case UserTypeEnum.Juristic:
                                var JURISTIC_ADDRESS = new JURISTICADDRESS()
                                {
                                    ADDRESS_NUMBER = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS"),
                                    ADDRESS_MOO = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"),
                                    ADDRESS_SOI = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS"),
                                    ADDRESS_BUILDING = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS"),
                                    ADDRESS_ROOMNO = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS"),
                                    ADDRESS_FLOOR = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS"),
                                    ADDRESS_ROAD = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"),
                                    ADDRESS_PROVINCE = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT"),
                                    ADDRESS_AMPHUR = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT"),
                                    ADDRESS_TUMBOL = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT"),
                                    ADDRESS_POSTCODE = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"),
                                    ADDRESS_TELEPHONE = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS"),
                                    ADDRESS_TELEPHONE_EXT = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS"),
                                    ADDRESS_FAX = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_JURISTIC_HQ_ADDRESS"),
                                    ADDRESS_EMAIL = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS"),
                                    ADDRESS_MOBILE = "",
                                    //ADDRESS_LAT = 0,
                                    //ADDRESS_LNG = 0
                                };

                                List<BOARDOFDIRECTOR> listBOARDOFDIRECTOR = new List<BOARDOFDIRECTOR>();
                                var boardTotal = int.Parse(model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("COMMITTEE_INFORMATION_TOTAL"));
                                if (boardTotal > 0)
                                {
                                    for (int i = 0; i < boardTotal; i++)
                                    {
                                        var boardAuth = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION_" + i);
                                        if (boardAuth == "yes")
                                        {
                                            var board = new BOARDOFDIRECTOR();

                                            board.CITIZEN_NAME = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_NAME_" + i);
                                            board.CITIZEN_LASTNAME = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_LASTNAME_" + i);
                                            var boardNation = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION_" + i);
                                            if (boardNation == "yes")
                                            {
                                                board.CITIZEN_IDENTITY_ID = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_CITIZEN_ID_" + i).Replace("-", "");
                                                board.CITIZEN_NATIONALITY = "ไทย";
                                            }
                                            else
                                            {
                                                board.CITIZEN_IDENTITY_ID = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("COMMITTEE_INFORMATION_PASSPORT_NUMBER_" + i).Replace("-", "");
                                                board.CITIZEN_NATIONALITY = "ต่างชาติ";
                                            }


                                            listBOARDOFDIRECTOR.Add(board);
                                        }

                                    }
                                }
                                //listBOARDOFDIRECTOR.Add(new BOARDOFDIRECTOR()
                                //{
                                //    CITIZEN_IDENTITY_ID = "1",
                                //    CITIZEN_NAME = "",
                                //    CITIZEN_LASTNAME = "",
                                //    CITIZEN_NATIONALITY = ""
                                //});
                                //listBOARDOFDIRECTOR.Add(new BOARDOFDIRECTOR()
                                //{
                                //    CITIZEN_IDENTITY_ID = "2",
                                //    CITIZEN_NAME = "",
                                //    CITIZEN_LASTNAME = "",
                                //    CITIZEN_NATIONALITY = ""
                                //});

                                var JURISTIC_GENERAL_INFORMATION = new JURISTICGENERALINFORMATION()
                                {
                                    REGISTERED_NO = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("IDENTITY_ID"),
                                    REGISTERED_DATE = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetDateStringData("REGISTER_DATE"),
                                    JURISTIC_NAME_TH = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_TH"),
                                    JURISTIC_NAME_EN = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_EN"),
                                    JURISTIC_ADDRESS = JURISTIC_ADDRESS,
                                    JURISTICN_GENERAL_EMAIL = "",
                                    BOARD_OF_DIRECTORS = listBOARDOFDIRECTOR
                                };
                              
                                //OPENID_AUTHEN.FirstName = JURISTIC_GENERAL_INFORMATION.JURISTIC_NAME_TH;                             
                                //OPENID_AUTHEN.DateOfBirth = JURISTIC_GENERAL_INFORMATION.REGISTERED_DATE;
                                //OPENID_AUTHEN.NationalID = JURISTIC_GENERAL_INFORMATION.REGISTERED_NO;
                                //OPENID_AUTHEN.Email = JURISTIC_GENERAL_INFORMATION.JURISTICN_GENERAL_EMAIL;

                                var postjuristic = new NbtcJuristic()
                                {
                                    BizReqNo = model.ApplicationRequestID.ToString(),
                                    BizReqDateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'", new CultureInfo("en")),//DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                                    ReqNo = model.ApplicationRequestNumber,
                                    ApplicationID = model.ApplicationID,
                                    REF_CASE_ID = model.ApplicationRequestID.ToString(),
                                    OPENID_AUTHEN = OPENID_AUTHEN,
                                    JURISTIC_GENERAL_INFORMATION = JURISTIC_GENERAL_INFORMATION,
                                    REQUESTOR_TYPE = chkAuthor != "REQUESTOR_INFORMATION__REQUEST_TYPE_BOARD" ? 0 : 1,
                                    STORE_INFORMATION = INFORMATION_STORE,
                                    REQUEST_TYPE = model.Data.TryGetData("APP_RADIO_SECTION").ThenGetIntData("APP_RADIO_TYPE_OPTION"),
                                    Attachments = attachList
                                };
                                jsonPost = JsonConvert.SerializeObject(postjuristic,
                                Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                });

                                regisUrl = ConfigurationManager.AppSettings["NBTC_WS_URL_JURISTIC_REQUEST"];
                                API_URL = ConfigurationManager.AppSettings["NBTC_URL_JURISTIC_REQUEST"];
                                break;
                            default:

                                break;
                        }




                       // string DGA_WS_URL = ConfigurationManager.AppSettings["DGA_WS_URL"];
                       // string NBTC_SECRET_KEY = ConfigurationManager.AppSettings["NBTC_SECRET_KEY"];
                        
                        if (ConfigurationManager.AppSettings["NBTC_USE_WS"].ToLower() == "false")
                        {
                            URL = API_URL;
                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        }
                        else
                        {
                            URL = DGA_WS_URL + "/ws" + regisUrl;
                        }
                        //string xx = DGA_WS_URL + "/ws" + regisUrl;
                        //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://164.115.9.184:90/BizPortalServicesUAT/lic/LicFrm0120");
                        //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(DGA_WS_URL + "/ws" + regisUrl);
                        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";
                        httpWebRequest.Headers.Add("Consumer-Key", Api.ConsumerKey);
                        httpWebRequest.Headers.Add("Token", Api.AccessToken);
                        httpWebRequest.Headers.Add("SecretKey", NBTC_SECRET_KEY);
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            streamWriter.Write(jsonPost);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }

                        using (var response_ = httpWebRequest.GetResponse() as HttpWebResponse)
                        {
                            if (httpWebRequest.HaveResponse && response_ != null)
                            {
                                using (var reader = new StreamReader(response_.GetResponseStream()))
                                {
                                    var res = reader.ReadToEnd();
                                    if (response_.StatusCode == HttpStatusCode.OK)
                                    {
                                        result.Success = true;
                                        result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, "", response_.StatusCode.ToString(), jsonPost);
                                    }
                                    else
                                    {
                                        result.Success = false;
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, response_.StatusCode.ToString(), null, jsonPost, true);
                                    }
                                }
                            }
                        }





                        /* Api.OnCheckingApplicationError += (ex) =>
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



                         if (response.HasValues && response["ResponseCode"].ToString() == "OK")
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
                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, result.Message, null, jsonPost, true);
                result.Success = false;
            }

            //result.Success = true;
            return result;

            #endregion
        }
    }
}
