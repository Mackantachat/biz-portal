using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels.Apps;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace BizPortal.AppsHook
{
    public class MEAUtilityHook : SingleFormRendererAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return null;
        }

        public override bool AllowFreeDocument { get; } = false;

        public override Dictionary<string, string> TranslateKeyValue(ApplicationRequestViewModel model)
        {
            var mailMerges = base.TranslateKeyValue(model);

            // Contact Data
            mailMerges.Add("CONTACT_NAME", "คุณ" + model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_FIRSTNAME") + " " + model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_LASTNAME"));
            mailMerges.Add("CONTACT_PHONE", model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL"));
            mailMerges.Add("CONTACT_EMAIL", model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL"));

            // Document Data
            mailMerges.Add("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY", ResourceHelper.GetResourceWord("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY", "Apps_Utility", "th"));
            mailMerges.Add("IDENTITY_COPY", ResourceHelper.GetResourceWord("IDENTITY_COPY", "FileType", "th"));
            mailMerges.Add("THAI_IDENTITY_COPY", ResourceHelper.GetResourceWord("THAI_IDENTITY_COPY", "Apps_Utility", "th"));
            mailMerges.Add("FOREIGNER_PASSPORT_COPY", ResourceHelper.GetResourceWord("FOREIGNER_PASSPORT_COPY", "Apps_Utility", "th"));
            mailMerges.Add("FOREIGNER_IDENTITY_COPY", ResourceHelper.GetResourceWord("FOREIGNER_IDENTITY_COPY", "Apps_Utility", "th"));
            mailMerges.Add("HOUSEHOLD_REGISTRATION_COPY", ResourceHelper.GetResourceWord("HOUSEHOLD_REGISTRATION_COPY", "Apps_Utility", "th"));
            mailMerges.Add("RENTAL_AGREEMENT_DOCUMENT", ResourceHelper.GetResourceWord("RENTAL_AGREEMENT_DOCUMENT", "Apps_Utility", "th"));
            mailMerges.Add("LANDWITHBUILDING_RENTAL_AGREEMENT", ResourceHelper.GetResourceWord("LANDWITHBUILDING_RENTAL_AGREEMENT", "Apps_Utility", "th"));
            mailMerges.Add("HOUSE_OWNER_REG_COPY", ResourceHelper.GetResourceWord("HOUSE_OWNER_REG_COPY", "Apps_Utility", "th"));
            mailMerges.Add("HOUSE_ID_DOC_COPY", ResourceHelper.GetResourceWord("HOUSE_ID_DOC_COPY", "Apps_Utility", "th"));
            mailMerges.Add("LANDWITHBUILDING_CONTRACT_AGREEMENT", ResourceHelper.GetResourceWord("LANDWITHBUILDING_CONTRACT_AGREEMENT", "Apps_Utility", "th"));
            mailMerges.Add("CONTRACT_AGREEMENT", ResourceHelper.GetResourceWord("CONTRACT_AGREEMENT", "Apps_Utility", "th"));
            mailMerges.Add("HOUSEHOLD_REGISTRATION_OF_APPLICANT_COPY", ResourceHelper.GetResourceWord("HOUSEHOLD_REGISTRATION_OF_APPLICANT_COPY", "FileType", "th"));
            mailMerges.Add("VAT_REGISTRATION", ResourceHelper.GetResourceWord("VAT_REGISTRATION", "Apps_Utility", "th"));
            mailMerges.Add("CERTIFICATION_OF_SPECIFIED_BUSINESS", ResourceHelper.GetResourceWord("CERTIFICATION_OF_SPECIFIED_BUSINESS", "Apps_Utility", "th"));
            mailMerges.Add("COMMERCIAL_REGISTRATION", ResourceHelper.GetResourceWord("COMMERCIAL_REGISTRATION", "Apps_Utility", "th"));
            mailMerges.Add("INDIVIDUAL_TAXPAYER_COPY", ResourceHelper.GetResourceWord("INDIVIDUAL_TAXPAYER_COPY", "Apps_Utility", "th"));

            mailMerges.Add("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY_EN", ResourceHelper.GetResourceWord("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY", "Apps_Utility", "en"));
            mailMerges.Add("IDENTITY_COPY_EN", ResourceHelper.GetResourceWord("IDENTITY_COPY", "FileType", "en"));
            mailMerges.Add("THAI_IDENTITY_COPY_EN", ResourceHelper.GetResourceWord("THAI_IDENTITY_COPY", "Apps_Utility", "en"));
            mailMerges.Add("FOREIGNER_PASSPORT_COPY_EN", ResourceHelper.GetResourceWord("FOREIGNER_PASSPORT_COPY", "Apps_Utility", "en"));
            mailMerges.Add("FOREIGNER_IDENTITY_COPY_EN", ResourceHelper.GetResourceWord("FOREIGNER_IDENTITY_COPY", "Apps_Utility", "en"));
            mailMerges.Add("HOUSEHOLD_REGISTRATION_COPY_EN", ResourceHelper.GetResourceWord("HOUSEHOLD_REGISTRATION_COPY", "Apps_Utility", "en"));
            mailMerges.Add("RENTAL_AGREEMENT_DOCUMENT_EN", ResourceHelper.GetResourceWord("RENTAL_AGREEMENT_DOCUMENT", "Apps_Utility", "en"));
            mailMerges.Add("LANDWITHBUILDING_RENTAL_AGREEMENT_EN", ResourceHelper.GetResourceWord("LANDWITHBUILDING_RENTAL_AGREEMENT", "Apps_Utility", "en"));
            mailMerges.Add("HOUSE_OWNER_REG_COPY_EN", ResourceHelper.GetResourceWord("HOUSE_OWNER_REG_COPY", "Apps_Utility", "en"));
            mailMerges.Add("HOUSE_ID_DOC_COPY_EN", ResourceHelper.GetResourceWord("HOUSE_ID_DOC_COPY", "Apps_Utility", "en"));
            mailMerges.Add("LANDWITHBUILDING_CONTRACT_AGREEMENT_EN", ResourceHelper.GetResourceWord("LANDWITHBUILDING_CONTRACT_AGREEMENT", "Apps_Utility", "en"));
            mailMerges.Add("CONTRACT_AGREEMENT_EN", ResourceHelper.GetResourceWord("CONTRACT_AGREEMENT", "Apps_Utility", "en"));
            mailMerges.Add("HOUSEHOLD_REGISTRATION_OF_APPLICANT_COPY_EN", ResourceHelper.GetResourceWord("HOUSEHOLD_REGISTRATION_OF_APPLICANT_COPY", "FileType", "en"));
            mailMerges.Add("VAT_REGISTRATION_EN", ResourceHelper.GetResourceWord("VAT_REGISTRATION", "Apps_Utility", "en"));
            mailMerges.Add("CERTIFICATION_OF_SPECIFIED_BUSINESS_EN", ResourceHelper.GetResourceWord("CERTIFICATION_OF_SPECIFIED_BUSINESS", "Apps_Utility", "en"));
            mailMerges.Add("COMMERCIAL_REGISTRATION_EN", ResourceHelper.GetResourceWord("COMMERCIAL_REGISTRATION", "Apps_Utility", "en"));
            mailMerges.Add("INDIVIDUAL_TAXPAYER_COPY_EN", ResourceHelper.GetResourceWord("INDIVIDUAL_TAXPAYER_COPY", "Apps_Utility", "en"));

            return mailMerges;
        }

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = false;

#if DEBUG
            result.CcToEmails = new List<string>();
            result.CcToEmails.Add("somjet.tripattanaporn@ega.or.th");
            result.CcToEmails.Add("viriya.foopuntuwut@ega.or.th");
            result.CcToEmails.Add("tunchanok.kl@mea.or.th");
#endif

            try
            {
                if (stage == AppsStage.UserCreate)
                {
                    request.NoDocument = true;
                    // เพิ่มจังหวัด อำเภอ ตำบล ลงในใบคำร้อง
                    request.ProvinceID = int.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS"));
                    request.AmphurID = int.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS"));
                    request.TumbolID = int.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS"));

                    request.Province = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS_TEXT");
                    request.Amphur = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS_TEXT");
                    request.Tumbol = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS_TEXT");

                    // Request ครั้งแรก
                    var tsic = string.Empty;
                    if (model.IdentityType == UserTypeEnum.Citizen)
                    {
                        tsic = model.Data.TryGetData("MEA_INFORMATION").ThenGetStringData("DROPDOWN_MEA_LOCATION_TYPE_TEXT");
                    }
                    else if (model.IdentityType == UserTypeEnum.Juristic)
                    {
                        tsic = "คลังสินค้า";
                    }
                    var coVillage = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_UTILITY_ADDRESS") + " " +
                            model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_UTILITY_ADDRESS");

                    if (!string.IsNullOrEmpty(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_UTILITY_ADDRESS")))
                    {
                        coVillage = "ม." + model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_UTILITY_ADDRESS") + " " + coVillage;
                    }

                    MEAUtilityRequest post = new MEAUtilityRequest()
                    {
                        ApplicationRequestID = model.ApplicationRequestID,
                        ApplicationID = 05,
                        NOTI_TYPE = model.Data.TryGetData("MEA_INFORMATION").ThenGetStringData("DROPDOWN_MEA_SERVICE_TYPE_TEXT"),
                        TSIC_TYPE = tsic,
                        IWERK = model.Data.TryGetData("MEA_INFORMATION").ThenGetIntData("MEA_BRANCH_CODE"), // เลขสาขา
                        X = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetDoubleData("ADDRESS_LAT_UTILITY_ADDRESS"),
                        Y = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetDoubleData("ADDRESS_LNG_UTILITY_ADDRESS"),
                        TITLE = model.Data.TryGetData("DBD").ThenGetStringData("TYPE"),
                        NAME = model.Data.TryGetData("DBD").ThenGetStringData("JURISTIC_NAME_TH"),
                        BRANCH = model.Data.TryGetData("DBD").ThenGetStringData("BIZ_BRANCH", "00000"),
                        ZCRN = model.Data.TryGetData("DBD").ThenGetStringData("JURISTIC_ID"),
                        ZTAX20 = model.IdentityID,
                        ZTHID = model.IdentityID,
                        CONTACT_IDNO = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_CITIZEN_ID").Replace("-", ""),
                        CONTACT_FIRSTNAME = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_FIRSTNAME"),
                        CONTACT_LASTNAME = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_LASTNAME"),
                        CONTACT_TEL = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL"),
                        CONTACT_EMAIL = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL"),
                        // ข้อมูลสถานที่ติดตั้งไฟฟ้า
                        CO_HOUSEID = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ID_UTILITY_ADDRESS"),
                        CO_HOUSE_NUM1 = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_UTILITY_ADDRESS"),
                        CO_FLOOR = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_UTILITY_ADDRESS"),
                        CO_SOI = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_UTILITY_ADDRESS"),
                        CO_VILLAGE = coVillage,
                        CO_STREET = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_UTILITY_ADDRESS"),
                        CO_SUBDISTRICT = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS_TEXT"),
                        CO_DISTRICT = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS_TEXT"),
                        CO_PROVINCE = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS_TEXT"),
                        CO_POSTCODE = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_UTILITY_ADDRESS"),
                        INSTALL_SIZE = model.Data.TryGetData("MEA_INFORMATION").ThenGetStringData("AJAX_DROPDOWN_MEA_MEASUREMENT_SIZE_TEXT"),
                        SERVICE_TYPE = "BIZ"
                    };

                    // ข้อมูลผู้ขอใช้ไฟฟ้า
                    if (model.IdentityType == UserTypeEnum.Citizen)
                    {
                        post.TITLE = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_PREFIX_TH_TEXT");
                        post.NAME = model.Data.TryGetData("OPENID").ThenGetStringData("FIRSTNAME_TH");
                        post.LASTNAME = model.Data.TryGetData("OPENID").ThenGetStringData("LASTNAME_TH");
                        string birthDateStr = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("BIRTH_DATE");
                        DateTime birthDate = DateTime.Today;
                        if (DateTime.TryParseExact(birthDateStr, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th-TH"), DateTimeStyles.None, out birthDate))
                        {
                            post.BIRTHDATE = birthDate.ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US"));
                        }

                        post.SERVICE_TYPE = "OSS";

                        post.BP_HOUSE_NUM1 = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_UTILITY_ADDRESS");
                        post.BP_FLOOR = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_UTILITY_ADDRESS");
                        post.BP_VILLAGE = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_UTILITY_ADDRESS")
                                     + " "
                                     + model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_UTILITY_ADDRESS");
                        post.BP_SOI = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_UTILITY_ADDRESS");
                        post.BP_STREET = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_UTILITY_ADDRESS");
                        post.BP_SUBDISTRICT = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS_TEXT");
                        post.BP_DISTRICT = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS_TEXT");
                        post.BP_PROVINCE = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS_TEXT");
                        post.BP_POSTCODE = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_UTILITY_ADDRESS");
                        post.TELNO = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL");
                    }
                    else if (model.IdentityType == UserTypeEnum.Juristic)
                    {
                        post.BP_HOUSE_NUM1 = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS");
                        post.BP_FLOOR = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS");
                        post.BP_VILLAGE = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS")
                                     + " "
                                     + model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS");
                        post.BP_SOI = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS");
                        post.BP_STREET = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS");
                        post.BP_SUBDISTRICT = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT");
                        post.BP_DISTRICT = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT");
                        post.BP_PROVINCE = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT");
                        post.BP_POSTCODE = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS");
                        post.TELNO = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL");
                    }

                    if (model.IdentityType == UserTypeEnum.Juristic)
                    {
                        var committees = model.Data.TryGetData("DBD");
                        if (committees != null)
                        {
                            int count = (int)committees.ThenGetDoubleData("COMMITTEE_TOTAL");
                            for (int i = 0; i < count; i++)
                            {
                                post.COMMITTEE_INFORMATION.Add(new CommitteeInfo()
                                {
                                    ID = committees.ThenGetStringData("COMITTEE_" + i + "_CITIZEN_ID"),
                                    NAME = committees.ThenGetStringData("COMITTEE_" + i + "_FIRST_NAME")
                                           + " "
                                           + committees.ThenGetStringData("COMITTEE_" + " " +"_LAST_NAME")
                                });
                            }
                        }

                        // Attach DBD's Certificate
                        try
                        {
                            var certificate = Api.Get("/dbd/v2/juristic/certificate/signed?JuristicID=" + model.IdentityID);
                            if (certificate.HasValues)
                            {
                                post.ATTACH_FILE.Add(new MEAUtilityFile()
                                {
                                    FileName = certificate["FileName"].ToString(),
                                    FileDescription = "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                                    Base64String = certificate["Result"].ToString(),
                                    ContentType = certificate["mimeType"].ToString()
                                });
                            }
                        }
                        catch { }
                    }

#if Release
                    var jsonPost = JsonConvert.SerializeObject(post);
                    string appUrl = ConfigurationManager.AppSettings["MEA_WS_URL"];
                    var client = new RestClient(appUrl);
                    var restRequest = new RestRequest();
                    restRequest.AddHeader("Accept", "application/json");
                    restRequest.AddHeader("Content-Type", "application/json");
                    restRequest.Method = Method.POST;
                    restRequest.AddParameter("application/json", jsonPost, ParameterType.RequestBody);
                    IRestResponse postResp = client.Execute(restRequest);
                    if (postResp.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, "Created", postResp.Content, jsonPost);
                        result.Success = true;
                    }
                    else
                    {
                        string error = string.Format("Unable to request to {0}. Status code is {1}: {2}.", appUrl, postResp.StatusCode, postResp.StatusCode.ToString());
                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, postResp.Content, jsonPost, true);
                        throw new Exception(error);
                    }
#else
                    post.CONTACT_FIRSTNAME = post.CONTACT_LASTNAME = "ทดสอบ";
                    var jsonPost = JsonConvert.SerializeObject(post);

                    string appUrl = ConfigurationManager.AppSettings["MEA_WS_URL"];
                    var client = new RestClient(appUrl);
                    var restRequest = new RestRequest();
                    restRequest.AddHeader("Accept", "application/json");
                    restRequest.AddHeader("Content-Type", "application/json");
                    restRequest.Method = Method.POST;
                    restRequest.AddParameter("application/json", jsonPost, ParameterType.RequestBody);
                    
                    IRestResponse postResp = client.Execute(restRequest);
                    if (postResp.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, "Created", postResp.Content, jsonPost);
                        result.Success = true;
                    }
                    else
                    {
                        string error = string.Format("Unable to request to {0}. Status code is {1}: {2}.", appUrl, postResp.StatusCode, postResp.StatusCode.ToString());
                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, postResp.Content, jsonPost, true);
                        throw new Exception(error);
                    }

                    result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, "Created", "test", jsonPost);
                    result.Success = true;
#endif
                }
                else
                {
                    result.Success = true;
                }
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
