using System;
using System.Collections.Generic;
using BizPortal.ViewModels.V2;
using BizPortal.ViewModels.Apps;
using BizPortal.Utils.Extensions;
using System.Configuration;
using Newtonsoft.Json;
using BizPortal.Utils.Exceptions;
using System.Globalization;
using BizPortal.DAL.MongoDB;
using EGA.WS;
using Newtonsoft.Json.Linq;
using BizPortal.Utils.Helpers;
using System.Text;

namespace BizPortal.AppsHook
{
    public class TOTPhoneRequestHook : SingleFormRendererAppHook
    {
        public override decimal? CalculateFee(List<ViewModels.SingleForm.ISectionData> sectionData)
        {
            return null;
        }

        public override bool AllowFreeDocument { get; } = false;

        public override Dictionary<string, string> TranslateKeyValue(ApplicationRequestViewModel model)
        {
            var translates = base.TranslateKeyValue(model);

            translates.Add("VAT_REGISTRATION", ResourceHelper.GetResourceWord("VAT_REGISTRATION", "Apps_Utility"));
            translates.Add("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY", ResourceHelper.GetResourceWord("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY", "FileType"));
            translates.Add("IDENTITY_COPY", ResourceHelper.GetResourceWord("APPS_ATTACH_COPY_OF_CITIZEN_CARD", "Apps_Utility"));

            return translates;
        }

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = false;

#if DEBUG
            result.CcToEmails = new List<string>() { "viriya.foopuntuwut@ega.or.th" };
#endif

            try
            {
                switch (stage)
                {
                    case AppsStage.UserCreate:
                        request.NoDocument = true;
                        // เพิ่มจังหวัด อำเภอ ตำบล ลงในใบคำร้อง
                        request.ProvinceID = int.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS"));
                        request.AmphurID = int.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS"));
                        request.TumbolID = int.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS"));

                        request.Province = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS_TEXT");
                        request.Amphur = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS_TEXT");
                        request.Tumbol = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS_TEXT");

                        if (model.IdentityType == UserTypeEnum.Juristic)
                        {
                            TOTJuristicRequest post = new TOTJuristicRequest()
                            {
                                ApplicationReqID = model.ApplicationRequestID.DefaultString(),
                                RequestID = model.ApplicationRequestNumber,
                                IDCard = model.IdentityID,
                                TraderRegisterNo = model.IdentityID,
                                CompanyTitleName = model.Data.TryGetData("DBD").ThenGetStringData("TYPE"),
                                CompanyName = model.Data.TryGetData("DBD").ThenGetStringData("JURISTIC_NAME_TH"),
                                BranchCode = model.Data.TryGetData("TOT_INFORMATION").ThenGetStringData("TOT_1ST_BRANCH_ID"),
                                CompanyType = model.Data.TryGetData("DBD").ThenGetStringData("TYPE"),
                                // ข้อมูลผู้ขอใช้ไฟฟ้า
                                HouseNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS"),
                                VillageNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"),
                                FloorNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS"),
                                BuildName = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS"),
                                Lane = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS"),
                                Road = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"),
                                SubDistrict = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS"),
                                District = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS"),
                                Province = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS"),
                                ZipCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"),
                                TelNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS"),
                                ContactIDCard = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_CITIZEN_ID").Replace("-", ""),
                                ContactTitleName = "",
                                ContactFirstname = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_FIRSTNAME"),
                                ContactLastname = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_LASTNAME"),
                                ContactTelNo = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL"),
                                ContactEmail = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL"),
                                // ข้อมูลสถานที่ติดตั้งไฟฟ้า
                                HomeBasedID = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ID_UTILITY_ADDRESS"),
                                ServiceHouseNo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_UTILITY_ADDRESS"),
                                ServiceVillageNo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_UTILITY_ADDRESS"),
                                ServiceFloorNo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_UTILITY_ADDRESS"),
                                ServiceBuildName = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_UTILITY_ADDRESS"),
                                ServiceLane = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_UTILITY_ADDRESS"),
                                ServiceRoad = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_UTILITY_ADDRESS"),
                                ServiceSubDistrict = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS"),
                                ServiceDistrict = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS"),
                                ServiceProvince = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS"),
                                ServiceZipCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_UTILITY_ADDRESS"),
                                Latitude = double.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_LAT_UTILITY_ADDRESS", "0")),
                                Longitude = double.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_LNG_UTILITY_ADDRESS", "0")),
                                // ข้อมูลแบบฟอร์ม
                                ServiceLocationCode = model.Data.TryGetData("TOT_INFORMATION").ThenGetStringData("TOT_1ST_BRANCH_ID"),
                                TOTServiceCode = model.Data.TryGetData("TOT_INFORMATION").ThenGetStringData("AJAX_DROPDOWN_TOT_SERVICE_TYPE"),
                                RequestDtm = model.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
                            };

                            // Attach DBD's Certificate
                            try
                            {
                                var certificate = Api.Get("/dbd/v2/juristic/certificate/signed?JuristicID=" + model.IdentityID);
                                if (certificate.HasValues)
                                {
                                    post.Attachments.Add(new TOTBase64File()
                                    {
                                        FileName = certificate["FileName"].ToString(),
                                        Base64String = certificate["Result"].ToString(),
                                        ContentType = certificate["mimeType"].ToString()
                                    });
                                }
                            }
                            catch { }

                            var committees = model.Data.TryGetData("DBD");
                            if (committees != null)
                            {
                                int count = (int)committees.ThenGetDoubleData("COMMITTEE_TOTAL");
                                for (int i = 0; i < count; i++)
                                {
                                    post.OperatedBy1 = committees.ThenGetStringData("COMITTEE_" + i + "_FIRST_NAME");
                                    break;
                                }
                            }
                            post.AdjustStringLength(); // Adjust String Length depend on StringLengthAttribute

                            string regisUrl = ConfigurationManager.AppSettings["TOT_JURISTIC_REGIS_WS_URL"];
                            var jsonPost = JsonConvert.SerializeObject(post);

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

                            var totResult = Api.Call(regisUrl, EGA.WS.HttpVerb.POST, null, jsonPost, EGA.WS.ContentType.ApplicationJson);
                            if (totResult != null && totResult.HasValues && totResult["ERR_CODE"].ToString() == "00000")
                            {
                                result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, totResult["MESSAGE"].ToString(), totResult.ToString(), jsonPost);
                                result.Success = true;
                            }
                            else
                            {
                                string error = string.Format("{0}: {1}", totResult["ERR_CODE"].ToString(), totResult["MESSAGE"].ToString(), jsonPost);
                                throw new ExceptionWithData(error, totResult.ToString());
                            }
                        }
                        else if (model.IdentityType == UserTypeEnum.Citizen)
                        {
                            request.Status = ApplicationStatusV2Enum.INCOMPLETE;
                            
                            var conTel = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL");
                            var conEmail = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL");

                            var gender = string.Empty;
                            var prefixText = string.Empty;

                            var prefix = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_PREFIX_TH");
                            if (prefix == "01")
                            {
                                gender = "Male";
                                prefixText = "Mr.";
                            }
                            else if (prefix == "02")
                            {
                                gender = "Female";
                                prefixText = "Mrs.";
                            }
                            else
                            {
                                gender = "Female";
                                prefixText = "Miss";
                            }

                            TOTCitizenRequest post = new TOTCitizenRequest()
                            {
                                AuthContracts = Convert.ToBase64String(Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes("esSRO")))),
                                AuthCode = Convert.ToBase64String(Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes("viO3d5fo")))),
                                IpAddress = model.SourceIPAddress,
                                Language = "TH",
                                RequestNumber = model.ApplicationRequestNumber,
                                ApplicationRequestID = model.ApplicationRequestID != null ? model.ApplicationRequestID.ToString() : string.Empty,
                                NationalID = Convert.ToBase64String(Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(model.IdentityID)))),
                                PrefixName = prefixText,
                                FirstName = model.Data.TryGetData("OPENID").ThenGetStringData("FIRSTNAME_TH"),
                                LastName = model.Data.TryGetData("OPENID").ThenGetStringData("LASTNAME_TH"),
                                Gender = gender,
                                PhoneNumber = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_CITIZEN_ADDRESS"),
                                HomeNumber = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_CITIZEN_ADDRESS"),
                                VillageBuilding = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_CITIZEN_ADDRESS"),
                                Floor = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_CITIZEN_ADDRESS"),
                                Moo = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_CITIZEN_ADDRESS"),
                                Soi = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS"),
                                Road = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS"),
                                SubDistrict = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT").Replace("แขวง", ""),
                                District = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT").Replace("เขต", ""),
                                Province = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT"),
                                Zipcode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_CITIZEN_ADDRESS"),
                                ContactEmail = !string.IsNullOrEmpty(conEmail) ? conEmail : "-",
                                ContactNumber = !string.IsNullOrEmpty(conTel) ? conTel : "-",
                                InstallHomeBasedID = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ID_UTILITY_ADDRESS"),
                                InstallHomeNumber = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_UTILITY_ADDRESS"),
                                InstallVillageBuilding = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_UTILITY_ADDRESS") + " " +
                                    model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_UTILITY_ADDRESS"),
                                InstallFloor = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_UTILITY_ADDRESS"),
                                InstallMoo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_UTILITY_ADDRESS"),
                                InstallSoi = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_UTILITY_ADDRESS"),
                                InstallRoad = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_UTILITY_ADDRESS"),
                                InstallSubDistrict = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS_TEXT").Replace("แขวง", ""),
                                InstallDistrict = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS_TEXT").Replace("เขต", ""),
                                InstallProvince = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS_TEXT"),
                                InstallZipcode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_UTILITY_ADDRESS"),
                                InstallLatitude = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetDoubleData("ADDRESS_LAT_UTILITY_ADDRESS"),
                                InstallLongitude = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetDoubleData("ADDRESS_LNG_UTILITY_ADDRESS"),
                            };

                            string birthDate = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("BIRTH_DATE");

                            if (!string.IsNullOrEmpty(birthDate))
                            {
                                DateTime dateTime = DateTime.ParseExact(birthDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th"));
                                post.Birthdate = dateTime.ToString("dd-MM-yyyy", CultureInfo.CreateSpecificCulture("th"));
                            }

                            post.AdjustStringLength(); // Adjust String Length depend on StringLengthAttribute

                            var jsonPost = JsonConvert.SerializeObject(post);

                            Dictionary<string, string> respData = new Dictionary<string, string>();
                            string regisUrl = ConfigurationManager.AppSettings["TOT_CITIZEN_REGIS_WS_URL"];
                            var totResult = Api.Call(regisUrl, EGA.WS.HttpVerb.POST, null, jsonPost, EGA.WS.ContentType.ApplicationJson);
                            if (totResult != null && totResult.HasValues)
                            {
                                if (totResult["statuscode"].ToString() == "200")
                                {
                                    result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, totResult["status"].ToString(), totResult.ToString(), jsonPost);
                                    result.Success = true;

                                    respData.Add("RESPONSE_MESSAGE", totResult["status"].ToString());
                                    respData.Add("RESPONSE_CODE", totResult["statuscode"].ToString());

                                    var returnUrl = (JArray)totResult["rurl"];
                                    respData.Add("RESULT_URL", returnUrl.Count > 0 ? returnUrl[0].ToString() : string.Empty);

                                    if (request.Data.ContainsKey("TOT_RESPONSE_DATA"))
                                    {
                                        request.Data.Remove("TOT_RESPONSE_DATA");
                                    }

                                    request.Data.Add("TOT_RESPONSE_DATA", new ApplicationRequestDataGroupEntity()
                                    {
                                        GroupName = "TOT_RESPONSE_DATA",
                                        Data = respData
                                    });
                                }
                                else
                                {
                                    respData.Add("RESPONSE_MESSAGE", totResult["status"].ToString());
                                    respData.Add("RESPONSE_CODE", totResult["statuscode"].ToString());

                                    JArray errors = (JArray)totResult["errors"];
                                    for (int i = 0; i < errors.Count; i++)
                                    {
                                        var item = errors[i];
                                        var msg = item["msg"];
                                        respData.Add(string.Format("ERROR_{0}", i + 1), msg["_th"].ToString());
                                    }

                                    if (request.Data.ContainsKey("TOT_RESPONSE_DATA"))
                                    {
                                        request.Data.Remove("TOT_RESPONSE_DATA");
                                    }

                                    request.Data.Add("TOT_RESPONSE_DATA", new ApplicationRequestDataGroupEntity()
                                    {
                                        GroupName = "TOT_RESPONSE_DATA",
                                        Data = respData
                                    });

                                    string error = string.Format("{0}: {1}", totResult["statuscode"].ToString(), totResult["status"].ToString(), jsonPost);
                                    throw new ExceptionWithData(error, totResult.ToString());
                                }
                            }
                            else
                            {
                                throw new Exception("Error occured whild requesting TOT's web service");
                            }
                        }
                        break;
                    case AppsStage.UserUpdate:
                        if (model.IdentityType == UserTypeEnum.Juristic)
                        {
                            TOTJuristicRequest post = new TOTJuristicRequest()
                            {
                                ApplicationReqID = model.ApplicationRequestID.DefaultString(),
                                RequestID = model.ApplicationRequestNumber,
                                IDCard = model.IdentityID,
                                TraderRegisterNo = model.IdentityID,
                                CompanyTitleName = model.Data.TryGetData("DBD").ThenGetStringData("TYPE"),
                                CompanyName = model.Data.TryGetData("DBD").ThenGetStringData("JURISTIC_NAME_TH"),
                                BranchCode = model.Data.TryGetData("TOT_INFORMATION").ThenGetStringData("TOT_1ST_BRANCH_ID"),
                                CompanyType = model.Data.TryGetData("DBD").ThenGetStringData("TYPE"),
                                // BusinessType = "",
                                // ข้อมูลผู้ขอใช้ไฟฟ้า
                                HouseNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS"),
                                VillageNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"),
                                FloorNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS"),
                                BuildName = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS"),
                                Lane = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS"),
                                Road = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"),
                                SubDistrict = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS"),
                                District = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS"),
                                Province = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS"),
                                ZipCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"),
                                TelNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS"),
                                //OperatedBy1 = "",
                                ContactIDCard = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_CITIZEN_ID").Replace("-", ""),
                                ContactTitleName = "",
                                ContactFirstname = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_FIRSTNAME"),
                                ContactLastname = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_LASTNAME"),
                                ContactTelNo = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL"),
                                ContactEmail = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_EMAIL"),
                                // ข้อมูลสถานที่ติดตั้งไฟฟ้า
                                HomeBasedID = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION ").ThenGetStringData("ADDRESS_ID_UTILITY_ADDRESS"),
                                ServiceHouseNo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION ").ThenGetStringData("ADDRESS_UTILITY_ADDRESS"),
                                ServiceVillageNo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION ").ThenGetStringData("ADDRESS_MOO_UTILITY_ADDRESS"),
                                ServiceFloorNo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION ").ThenGetStringData("ADDRESS_FLOOR_UTILITY_ADDRESS"),
                                ServiceBuildName = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION ").ThenGetStringData("ADDRESS_BUILDING_UTILITY_ADDRESS"),
                                ServiceLane = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION ").ThenGetStringData("ADDRESS_SOI_UTILITY_ADDRESS"),
                                ServiceRoad = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION ").ThenGetStringData("ADDRESS_ROAD_UTILITY_ADDRESS"),
                                ServiceSubDistrict = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION ").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS"),
                                ServiceDistrict = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION ").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS"),
                                ServiceProvince = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION ").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS"),
                                ServiceZipCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION ").ThenGetStringData("ADDRESS_POSTCODE_UTILITY_ADDRESS"),
                                Latitude = double.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION ").ThenGetStringData("ADDRESS_LAT_UTILITY_ADDRESS", "0")),
                                Longitude = double.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION ").ThenGetStringData("ADDRESS_LNG_UTILITY_ADDRESS", "0")),
                                // ข้อมูลแบบฟอร์ม
                                ServiceLocationCode = model.Data.TryGetData("TOT_INFORMATION").ThenGetStringData("TOT_1ST_BRANCH_ID"),
                                TOTServiceCode = model.Data.TryGetData("TOT_INFORMATION").ThenGetStringData("AJAX_DROPDOWN_TOT_SERVICE_TYPE"),
                                RequestDtm = model.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
                            };

                            if (model.UploadedFiles != null)
                            {
                                foreach (var group in model.UploadedFiles)
                                {
                                    if (group.Files != null)
                                    {
                                        foreach (var file in group.Files)
                                        {
                                            if (!file.Extras.TryGetBool(AppsHookKey.FILE_SYNCED_STATUS.ToString()))
                                            {
                                                post.Attachments.Add(new TOTBase64File()
                                                {
                                                    FileName = file.FileName,
                                                    Base64String = file.GetBased64String(),
                                                    ContentType = file.ContentType
                                                });
                                                file.Extras[AppsHookKey.FILE_SYNCED_STATUS.ToString()] = true.ToString();
                                                file.Extras[AppsHookKey.FILE_SYNCED_DATE.ToString()] = DateTime.Now.ToString();
                                            }
                                        }
                                    }
                                }
                            }

                            post.AdjustStringLength(); // Adjust String Length depend on StringLengthAttribute

                            string regisUrl = ConfigurationManager.AppSettings["TOT_JURISTIC_REGIS_WS_URL"];
                            var jsonPost = JsonConvert.SerializeObject(post);
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

                            var totResult = Api.Call(regisUrl, EGA.WS.HttpVerb.POST, null, jsonPost, EGA.WS.ContentType.ApplicationJson);
                            if (totResult != null && totResult.HasValues && totResult["ERR_CODE"].ToString() == "00000")
                            {
                                result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, totResult["MESSAGE"].ToString(), totResult.ToString(), jsonPost);
                                result.Success = true;
                            }
                            else
                            {
                                string error = string.Format("{0}: {1}", totResult["ERR_CODE"].ToString(), totResult["MESSAGE"].ToString(), jsonPost);
                                throw new ExceptionWithData(error, totResult.ToString());
                            }
                        }
                        else
                        {
                            result.Success = true;
                        }
                        break;
                    case AppsStage.AgentUpdate:
                    case AppsStage.ApiUpdate:
                    case AppsStage.None:
                    default:
                        result.Success = true;
                        break;
                }
            }
            catch (ExceptionWithData ex)
            {
                result.Message = ex.Message;
                result.Exception = ex;
                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, ex.Message, ex.CustomData.ToString(), null, true);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Exception = ex;
            }

            return result;
        }
    }
}
